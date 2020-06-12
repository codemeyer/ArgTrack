using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ArgData.Entities;
using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackDrawing
{
    /// <summary>
    /// This is the view model that is responsible for what is drawn on the screen.
    /// </summary>
    public class TrackLayoutViewModel : BaseViewModel
    {
        private readonly TrackViewModel _trackViewModel;

        public TrackLayoutViewModel(TrackViewModel trackViewModel)
        {
            _trackViewModel = trackViewModel;

            PerformFullCalculation();
        }

        public void PerformFullCalculation()
        {
            // TODO: split into sub-methods

            var centerPoints = CenterPointsGeometryCalculator.CalculateCenterPointsPerSection(_trackViewModel);
            //centerPoints.TrackSections
            //centerPoints.PitLaneSections
            // these aren't exposed, just used for calculations
            TrackSections = centerPoints.TrackSections;
            PitLaneSections = centerPoints.PitLaneSections;

            RoadSections = RoadLayoutGeometryCalculator.CalculateRoadSections(centerPoints.TrackSections, _trackViewModel.TrackHeader.TrackStartWidth);

            // needed for both verges and track-side object
            var centerPointsOneSectionPerLength = CenterPointsGeometryCalculator.CalculateCenterPointsPerSectionOneLengthEach(_trackViewModel);

            RoadSectionsOnePerLength = RoadLayoutGeometryCalculator.CalculateRoadSections(centerPointsOneSectionPerLength.TrackSections, _trackViewModel.TrackHeader.TrackStartWidth);

            VergeData = VergeLayoutCalculator.Calculate(RoadSections, RoadSectionsOnePerLength);
            // TODO: calculate PitLane verges somewhere

            TrackData = RoadLayoutShapeCalculator.Calculate(RoadSections, DrawingLayout.ForTrack());

            PitLaneShapeData = RoadLayoutShapeCalculator.Calculate(centerPoints.PitLaneSections, LayoutConstants.PitLaneWidth, DrawingLayout.ForPitLane());

            TrackSideObjectsShapeData  = TracksideObjectsCalculator.Calculate(RoadSections, RoadSectionsOnePerLength, _trackViewModel.TrackObjectSettings);

            // TODO: add debug items

            DebugCircleCenterIndicators = CircleCenterIndicatorDrawing.DrawCircleCenterIndicators(centerPoints.TrackSections, DrawingLayout.ForTechCircleCenter());
            DebugLineBetweenSectionCenters = CenterLineDrawingCalculator.CalculateDrawablePointsStraight(centerPoints.TrackSections);
            DebugLineAlongTrackCenter = CenterLineDrawingCalculator.CalculateDrawablePointsWithArcs(centerPoints.TrackSections);


            // TODO: make surrounding area much "dimmer"
            if (_trackViewModel.TrackSettings.SurroundingArea == SurroundingArea.Green)
            {
                SurroundingAreaColor = "#5E8136";
            }
            else
            {
                SurroundingAreaColor = "#818274";
            }

            RecalculateViewAndScrollbars();
        }

        // TODO: not used now, but will be later
        private List<LayoutSectionData> TrackSections { get; set; }
        private List<LayoutSectionData> PitLaneSections { get; set; }

        private List<RoadLayoutSectionData> RoadSections { get; set; }
        private List<RoadLayoutSectionData> RoadSectionsOnePerLength { get; set; }


        // RECALCulate after section change
        // CenterPoints (including and after the edited one)
        // CenterPointsOneSectionPerLength (including and after the edited one)

        // VergeData (including and after the edited one) - IS THIS ENOUGH (e.g. bridged walls...) maybe do it all?
        //    if current has a bridge, go back to first non-bridged one and start from there?

        // RoadSections (incl. and after edited one)
        // RoadSectionOnePerLength (incl. and after edited one)
        // track side objects (including and after the edited one)

        // RECALCulate after object placement change
        // track side objects  (including and after the edited one)


        // TODO: throttle the recalculation? handle restore of zoom etc here?

        public void RecalculateViewAndScrollbars()
        {
            if (Math.Abs(ViewportHeight) < 0.001)
            {
                // hasn't been updated yet
                return;
            }

            var min = new Point(double.MaxValue, double.MaxValue);
            var max = new Point(double.MinValue, double.MinValue);

            foreach (var pointData in TrackData.Union(VergeData).Union(PitLaneShapeData).Union(TrackSideObjectsShapeData))
            {
                foreach (var point in pointData.Points)
                {
                    if (point.X < min.X)
                    {
                        min.X = point.X;
                    }

                    if (point.X > max.X)
                    {
                        max.X = point.X;
                    }

                    if (point.Y < min.Y)
                    {
                        min.Y = point.Y;
                    }

                    if (point.Y > max.Y)
                    {
                        max.Y = point.Y;
                    }
                }
            }

            var rectanglePoints = new PointCollection
            {
                new Point(min.X, min.Y),
                new Point(max.X, min.Y),
                new Point(max.X, max.Y),
                new Point(min.X, max.Y),
                new Point(min.X, min.Y)
            };

            BoundingRectangle = rectanglePoints;

            var center = new Point();
            center.X = min.X + (max.X - min.X) / 2;
            center.Y = min.Y + (max.Y - min.Y) / 2;

            CenterPoint = center;

            UpdateScrollWidthParameters(max.X, min.X);
            UpdateScrollHeightParameters(max.Y, min.Y);

            RecalculateMatrixTransform();
        }

        private void UpdateScrollWidthParameters(double maxX, double minX)
        {
            TrackRenderWidth = Math.Abs((Math.Abs(maxX) - Math.Abs(minX)) * ZoomFactor);

            var scrollableXRange = Math.Round(TrackRenderWidth - ViewportWidth);

            ScrollMinX = Convert.ToInt32(scrollableXRange / 2) * -1;
            ScrollMaxX = Convert.ToInt32(scrollableXRange / 2);
            ScrollWidth = Convert.ToInt32(scrollableXRange);
        }

        private void UpdateScrollHeightParameters(double maxY, double minY)
        {
            TrackRenderHeight = Math.Abs((Math.Abs(maxY) - Math.Abs(minY)) * ZoomFactor);

            var scrollableYRange = Math.Round(TrackRenderHeight - ViewportHeight);

            ScrollMinY = Convert.ToInt32(scrollableYRange / 2) * -1;
            ScrollMaxY = Convert.ToInt32(scrollableYRange / 2);
            ScrollHeight = Convert.ToInt32(scrollableYRange);
        }


        private long _lastRecalculation;

        private void RecalculateMatrixTransform()
        {
            var ticks = DateTime.Now.Ticks;

            if (ticks - _lastRecalculation < 100)
            {
                return;
            }

            //< !--M11,M12,M21,M22,OffsetX,OffsetY-- >
            //< !--1 0 0 - 1 0 0-- >

            Matrix = new Matrix(1, 0, 0, -1, (OffsetX + CenterPoint.X) * -1, (OffsetY - CenterPoint.Y) * -1);

            _lastRecalculation = ticks;
        }

        private PointCollection _boundingRectangle;

        public PointCollection BoundingRectangle
        {
            get { return _boundingRectangle; }
            set { SetProperty(ref _boundingRectangle, value, () => BoundingRectangle); }
        }

        private Point _centerPoint;

        public Point CenterPoint
        {
            get { return _centerPoint; }
            set { SetProperty(ref _centerPoint, value, () => CenterPoint); }
        }

        // TODO: group all these properties under some other obj?

        private Matrix _matrix;

        public Matrix Matrix
        {
            get { return _matrix; }
            set { SetProperty(ref _matrix, value, () => Matrix); }
        }


        // HACK: set by an event in the MainTreeWindow.xaml.cs...


        private double _viewportWidth;

        // width of viewable drawing area
        public double ViewportWidth
        {
            get { return _viewportWidth; }
            set
            {
                SetProperty(ref _viewportWidth, value, () => ViewportWidth);
                //RecalculateViewAndScrollbars();
            }
        }

        private double _viewportHeight;

        // height of viewable drawing area
        public double ViewportHeight
        {
            get { return _viewportHeight; }
            set
            {
                SetProperty(ref _viewportHeight, value, () => ViewportHeight);
                //RecalculateViewAndScrollbars();
            }
        }

        private double _trackRenderWidth;

        /// <summary>
        /// Width of track as rendered in window (i.e. basic width * zoom)
        /// </summary>
        public double TrackRenderWidth
        {
            get { return _trackRenderWidth; }
            set
            {
                SetProperty(ref _trackRenderWidth, value, () => TrackRenderWidth);
            }
        }


        private double _trackRenderHeight;

        /// <summary>
        /// Height of track as rendered in window (i.e. basic height * zoom)
        /// </summary>
        public double TrackRenderHeight
        {
            get { return _trackRenderHeight; }
            set
            {
                SetProperty(ref _trackRenderHeight, value, () => TrackRenderHeight);
            }
        }


        private int _scrollMinX;

        public int ScrollMinX
        {
            get { return _scrollMinX; }
            set { SetProperty(ref _scrollMinX, value, () => ScrollMinX); }
        }

        private int _scrollMaxX;

        public int ScrollMaxX
        {
            get { return _scrollMaxX; }
            set { SetProperty(ref _scrollMaxX, value, () => ScrollMaxX); }
        }

        private int _scrollWidth;

        public int ScrollWidth
        {
            get { return _scrollWidth; }
            set { SetProperty(ref _scrollWidth, value, () => ScrollWidth); }
        }


        private int _offsetX;

        public int OffsetX
        {
            get { return _offsetX; }
            set
            {
                SetProperty(ref _offsetX, value, () => OffsetX);
                RecalculateMatrixTransform();
            }
        }


        private int _scrollMinY;

        public int ScrollMinY
        {
            get { return _scrollMinY; }
            set { SetProperty(ref _scrollMinY, value, () => ScrollMinY); }
        }

        private int _scrollMaxY;

        public int ScrollMaxY
        {
            get { return _scrollMaxY; }
            set { SetProperty(ref _scrollMaxY, value, () => ScrollMaxY); }
        }


        private int _scrollHeight;

        public int ScrollHeight
        {
            get { return _scrollHeight; }
            set { SetProperty(ref _scrollHeight, value, () => ScrollHeight); }
        }


        private int _offsetY;

        public int OffsetY
        {
            get { return _offsetY; }
            set
            {
                SetProperty(ref _offsetY, value, () => OffsetY);
                RecalculateMatrixTransform();
            }
        }


        private string _surroundingAreaColor;

        /// <summary>
        /// Gets or sets the color that will be drawn for the entire track layout window background.
        /// </summary>
        public string SurroundingAreaColor
        {
            get { return _surroundingAreaColor; }
            set { SetProperty(ref _surroundingAreaColor, value, () => SurroundingAreaColor); }
        }

        private double _zoomFactor = 1.75;

        public double ZoomFactor
        {
            get { return _zoomFactor; }
            set
            {
                SetProperty(ref _zoomFactor, value, () => ZoomFactor);
                RecalculateViewAndScrollbars();
            }
        }



        public void SetDisplayVergeData(bool display)
        {
            DisplayVergeData = display ? Visibility.Visible : Visibility.Hidden;
        }

        private Visibility _displayVergeData = Visibility.Visible;

        public Visibility DisplayVergeData
        {
            get { return _displayVergeData; }
            set { SetProperty(ref _displayVergeData, value, () => DisplayVergeData); }
        }

        private ObservableCollection<TrackShapeData> _vergeData;

        /// <summary>
        /// Gets or sets the TrackShapeData values that represent the verge
        /// when data is rendered to the screen.
        /// </summary>
        public ObservableCollection<TrackShapeData> VergeData
        {
            get { return _vergeData; }
            set { SetProperty(ref _vergeData, value, () => VergeData); }
        }




        public void SetDisplayTrackShapeData(bool display)
        {
            DisplayTrackShapeData = display ? Visibility.Visible : Visibility.Hidden;
        }

        private Visibility _displayTrackShapeData = Visibility.Visible;

        public Visibility DisplayTrackShapeData
        {
            get { return _displayTrackShapeData; }
            set { SetProperty(ref _displayTrackShapeData, value, () => DisplayTrackShapeData); }
        }

        private ObservableCollection<TrackShapeData> _trackData;

        /// <summary>
        /// Gets or sets the TrackShapeData values that represent the road
        /// when data is rendered to the screen.
        /// </summary>
        public ObservableCollection<TrackShapeData> TrackData
        {
            get { return _trackData; }
            set { SetProperty(ref _trackData, value, () => TrackData); }
        }



        public void SetDisplayPitLaneShapeData(bool display)
        {
            DisplayPitLaneShapeData = display ? Visibility.Visible : Visibility.Hidden;
        }

        private Visibility _displayPitLaneShapeData = Visibility.Visible;

        public Visibility DisplayPitLaneShapeData
        {
            get { return _displayPitLaneShapeData; }
            set { SetProperty(ref _displayPitLaneShapeData, value, () => DisplayPitLaneShapeData); }
        }


        private ObservableCollection<TrackShapeData> _pitLaneShapeData;

        /// <summary>
        /// Gets or sets the TrackShapeData values that represent the pit lane
        /// when data is rendered to the screen.
        /// </summary>
        public ObservableCollection<TrackShapeData> PitLaneShapeData
        {
            get { return _pitLaneShapeData; }
            set { SetProperty(ref _pitLaneShapeData, value, () => PitLaneShapeData); }
        }




        public void SetDisplayTrackSideObjectData(bool display)
        {
            DisplayTrackSideObjectData = display ? Visibility.Visible : Visibility.Hidden;
        }

        private Visibility _displayTrackSideObjectData = Visibility.Visible;

        public Visibility DisplayTrackSideObjectData
        {
            get { return _displayTrackSideObjectData; }
            set { SetProperty(ref _displayTrackSideObjectData, value, () => DisplayTrackSideObjectData); }
        }

        private ObservableCollection<TrackShapeData> _trackSideObjectsShapeData;

        /// <summary>
        /// Gets or sets the TrackShapeData values that represent the track-side objects
        /// when data is rendered to the screen.
        /// </summary>
        public ObservableCollection<TrackShapeData> TrackSideObjectsShapeData
        {
            get { return _trackSideObjectsShapeData; }
            set { SetProperty(ref _trackSideObjectsShapeData, value, () => TrackSideObjectsShapeData); }
        }



        public void SetDisplayDebugCircleCenterIndicatorsData(bool display)
        {
            DisplayDebugCircleCenterIndicatorsData = display ? Visibility.Visible : Visibility.Hidden;
        }

        private Visibility _displayDebugCircleCenterIndicatorsData = Visibility.Hidden;

        public Visibility DisplayDebugCircleCenterIndicatorsData
        {
            get { return _displayDebugCircleCenterIndicatorsData; }
            set { SetProperty(ref _displayDebugCircleCenterIndicatorsData, value, () => DisplayDebugCircleCenterIndicatorsData); }
        }

        private ObservableCollection<TrackShapeData> _debugCircleCenterIndicatorsData;

        /// <summary>
        /// Gets or sets the TrackShapeData values that represent the circle center DEBUG
        /// when data is rendered to the screen.
        /// </summary>
        public ObservableCollection<TrackShapeData> DebugCircleCenterIndicators
        {
            get { return _debugCircleCenterIndicatorsData; }
            set { SetProperty(ref _debugCircleCenterIndicatorsData, value, () => DebugCircleCenterIndicators); }
        }



        public void SetDisplayDebugLineBetweenSectionCentersData(bool display)
        {
            DisplayDebugLineBetweenSectionCentersData = display ? Visibility.Visible : Visibility.Hidden;
        }

        private Visibility _displayDebugLineBetweenSectionCentersData = Visibility.Hidden;

        public Visibility DisplayDebugLineBetweenSectionCentersData
        {
            get { return _displayDebugLineBetweenSectionCentersData; }
            set { SetProperty(ref _displayDebugLineBetweenSectionCentersData, value, () => DisplayDebugLineBetweenSectionCentersData); }
        }

        private ObservableCollection<TrackShapeData> _debugLineBetweenSectionCentersData;

        /// <summary>
        /// Gets or sets the TrackShapeData values that represent the circle center DEBUG
        /// when data is rendered to the screen.
        /// </summary>
        public ObservableCollection<TrackShapeData> DebugLineBetweenSectionCenters
        {
            get { return _debugLineBetweenSectionCentersData; }
            set { SetProperty(ref _debugLineBetweenSectionCentersData, value, () => DebugLineBetweenSectionCenters); }
        }


        public void SetDisplayDebugLineAlongTrackCenterData(bool display)
        {
            DisplayDebugLineAlongTrackCenterData = display ? Visibility.Visible : Visibility.Hidden;
        }

        private Visibility _displayDebugLineAlongTrackCenterData = Visibility.Hidden;

        public Visibility DisplayDebugLineAlongTrackCenterData
        {
            get { return _displayDebugLineAlongTrackCenterData; }
            set { SetProperty(ref _displayDebugLineAlongTrackCenterData, value, () => DisplayDebugLineAlongTrackCenterData); }
        }

        private ObservableCollection<TrackShapeData> _debugLineAlongTrackCenterData;

        /// <summary>
        /// Gets or sets the TrackShapeData values that represent the circle center DEBUG
        /// when data is rendered to the screen.
        /// </summary>
        public ObservableCollection<TrackShapeData> DebugLineAlongTrackCenter
        {
            get { return _debugLineAlongTrackCenterData; }
            set { SetProperty(ref _debugLineAlongTrackCenterData, value, () => DebugLineAlongTrackCenter); }
        }




        // TODO: set to actual layout section, which then updates this data?
        private TrackShapeData _selectedTrackSection;

        public TrackShapeData SelectedTrackSection
        {
            get { return _selectedTrackSection; }
            set { SetProperty(ref _selectedTrackSection, value, () => SelectedTrackSection); }
        }


        public ICommand ZoomTrackViewInCommand { get; } = new ZoomTrackViewCommand(0.5);
        public ICommand ZoomTrackViewOutCommand { get; } = new ZoomTrackViewCommand(-0.5);
        public ICommand RedrawViewCommand { get; } = new RedrawViewCommand();
    }
}
