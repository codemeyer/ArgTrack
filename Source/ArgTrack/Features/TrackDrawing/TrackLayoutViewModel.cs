using System;
using System.Collections.ObjectModel;
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
        public TrackLayoutViewModel(MainViewModel parent)
        {
            ParentMain = parent;
        }

        public MainViewModel ParentMain { get; set; }

        public static TrackLayoutViewModel FromTrackViewModel(TrackViewModel trackViewModel, TrackDrawingElementsSettings settings)
        {
            var centerPoints = CenterPointsGeometryCalculator.CalculateCenterPointsPerSection(trackViewModel);

            // the points that will be drawn on the screen
            var drawingPoints = new ObservableCollection<TrackShapeData>();

            var roadSections = RoadLayoutGeometryCalculator.CalculateRoadSections(centerPoints.TrackSections, trackViewModel.TrackHeader.TrackStartWidth);

            // needed for both verges and track-side object
            var centerPointsOneSectionPerLength = CenterPointsGeometryCalculator.CalculateCenterPointsPerSectionOneLengthEach(trackViewModel);
            var roadSectionsOnePerLength = RoadLayoutGeometryCalculator.CalculateRoadSections(centerPointsOneSectionPerLength.TrackSections, trackViewModel.TrackHeader.TrackStartWidth);

            if (settings.DrawVerges)
            {
                var vergePoints = VergeLayoutCalculator.Calculate(roadSections, roadSectionsOnePerLength);
                drawingPoints.AddRange(vergePoints);
            }

            if (settings.DrawTrack)
            {
                var roadPoints = RoadLayoutShapeCalculator.Calculate(roadSections, DrawingLayout.ForTrack());
                drawingPoints.AddRange(roadPoints);
            }

            if (settings.DrawPitLane)
            {
                if (settings.DrawVerges)
                {
                    // TODO: pit lane verges here
                }

                var pitPoints = RoadLayoutShapeCalculator.Calculate(centerPoints.PitLaneSections, LayoutConstants.PitLaneWidth, DrawingLayout.ForPitLane());
                drawingPoints.AddRange(pitPoints);
            }


            // these are toggled in the debug/hack menu

            if (settings.DrawTrackSideObjects)
            {
                var trackSideObjects = TracksideObjectsCalculator.Calculate(roadSections, roadSectionsOnePerLength, TrackViewModelPasserArounder.TrackViewModel.TrackObjectSettings);
                drawingPoints.AddRange(trackSideObjects);
            }

            if (settings.DrawCircleCenterIndicators)
            {
                var circleCenterIndicators = CircleCenterIndicatorDrawing.DrawCircleCenterIndicators(centerPoints.TrackSections, DrawingLayout.ForTechCircleCenter());
                drawingPoints.AddRange(circleCenterIndicators);
            }

            if (settings.DrawLineBetweenSectionCenters)
            {
                // center line with no arcs, only connects each section start point to the end point
                var centerLineStraight = CenterLineDrawingCalculator.CalculateDrawablePointsStraight(centerPoints.TrackSections);
                drawingPoints.AddRange(centerLineStraight);
            }

            if (settings.DrawLineAlongTrackCenter)
            {
                // arced center line
                var centerLineArced = CenterLineDrawingCalculator.CalculateDrawablePointsWithArcs(centerPoints.TrackSections);
                drawingPoints.AddRange(centerLineArced);
            }


            var layoutViewModel = new TrackLayoutViewModel(MainViewModelPasserArounder.MainViewModel)
            {
                TrackData = drawingPoints
            };

            if (trackViewModel.TrackSettings.SurroundingArea == SurroundingArea.Green)
            {
                layoutViewModel.SurroundingAreaColor = "#5E8136";
            }
            else
            {
                layoutViewModel.SurroundingAreaColor = "#818274";
            }


            layoutViewModel.RecalculateViewAndScrollbars();


            return layoutViewModel;
        }


        // TODO: throttle the recalculation? handle restore of zoom etc here?

        public void RecalculateViewAndScrollbars()
        {
            if (Math.Abs(ViewportHeight) < 0.001)
            {
                // hasn't been updated yet
                return;
            }

            var min = new Point(0, 0);
            var max = new Point(0, 0);

            foreach (var pointData in TrackData)
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


            // TODO: THERE IS SOMETHING WRONG, SOMEWHERE!
            // Zoom factor affects transform???
            // Centerpoint adjustment?


            TrackRenderWidth = (Math.Abs(min.X) + Math.Abs(max.X)) * ZoomFactor;

            // scroll-"mån"
            var scrollableX = TrackRenderWidth - ViewportWidth;
            var scrollableXMinAdjusted = -(scrollableX / 2); //- CenterPoint.Y;
            var scrollableXMaxAdjusted = (scrollableX / 2); //+ CenterPoint.Y;
            var scrollableXRange = scrollableXMaxAdjusted - scrollableXMinAdjusted;

            ScrollMinX = Convert.ToInt32(scrollableXRange / 2) * -1;
            ScrollMaxX = Convert.ToInt32(scrollableXRange / 2);
            ScrollWidth = Convert.ToInt32(scrollableXRange);


            TrackRenderHeight = (Math.Abs(min.Y) + Math.Abs(max.Y)) * ZoomFactor;

            var scrollableY = TrackRenderHeight - ViewportHeight;
            var scrollableYMinAdjusted = -(scrollableY / 2); //- CenterPoint.Y;
            var scrollableYMaxAdjusted = (scrollableY / 2); //+ CenterPoint.Y;
            var scrollableYRange = scrollableYMaxAdjusted - scrollableYMinAdjusted;

            ScrollMinY = Convert.ToInt32(scrollableYRange / 2) * -1;
            ScrollMaxY = Convert.ToInt32(scrollableYRange / 2);
            ScrollHeight = Convert.ToInt32(scrollableYRange);

            RecalculateMatrixTransform();
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

        // TODO: _maybe_ this doesn't need to be a WPF-style property
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

        // TODO: _maybe_ this doesn't need to be a WPF-style property
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

        // TODO: this doesn't need to be a WPF-style property
        // width of track as rendered in window (i.e. basic width * zoom)
        public double TrackRenderWidth
        {
            get { return _trackRenderWidth; }
            set
            {
                SetProperty(ref _trackRenderWidth, value, () => TrackRenderWidth);
            }
        }


        private double _trackRenderHeight;

        // TODO: this doesn't need to be a WPF-style property
        // height of track as rendered in window (i.e. basic height * zoom)
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

        // TODO: have another OffsetY which is the ActualOffsetY that does not include the CenterPoint.Y calculation and is used for the matrix calculation


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

        private ObservableCollection<TrackShapeData> _trackData;

        /// <summary>
        /// Gets or sets the TrackShapeData values that will be rendered on screen.
        /// </summary>
        public ObservableCollection<TrackShapeData> TrackData
        {
            get { return _trackData; }
            set { SetProperty(ref _trackData, value, () => TrackData); }
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
