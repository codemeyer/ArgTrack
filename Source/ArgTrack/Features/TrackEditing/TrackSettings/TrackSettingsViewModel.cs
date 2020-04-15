using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ArgData.Entities;
using ArgTrack.Features.Application.Shared;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.TrackSettings
{
    public class TrackSettingsViewModel : BaseViewModel
    {
        private KerbType _kerbType;

        public KerbType KerbType
        {
            get { return _kerbType; }
            set
            {
                if (SetProperty(ref _kerbType, value, () => KerbType))
                {
                    if (value == KerbType.DualColor)
                        ShowKerbColor2 = Visibility.Collapsed;
                    else
                        ShowKerbColor2 = Visibility.Visible;

                    UpdateKerbExampleColors();
                }
            }
        }


        private byte _kerbUpperColor;

        public byte KerbUpperColor
        {
            get { return _kerbUpperColor; }
            set
            {
                SetProperty(ref _kerbUpperColor, value, () => KerbUpperColor);
                UpdateKerbExampleColors();
            }
        }

        private byte _kerbLowerColor;

        public byte KerbLowerColor
        {
            get { return _kerbLowerColor; }
            set
            {
                SetProperty(ref _kerbLowerColor, value, () => KerbLowerColor);
                UpdateKerbExampleColors();
            }
        }

        private byte _kerbUpperColor2;

        public byte KerbUpperColor2
        {
            get { return _kerbUpperColor2; }
            set
            {
                SetProperty(ref _kerbUpperColor2, value, () => KerbUpperColor2);
                UpdateKerbExampleColors();
            }
        }

        private byte _kerbLowerColor2;

        public byte KerbLowerColor2
        {
            get { return _kerbLowerColor2; }
            set
            {
                SetProperty(ref _kerbLowerColor2, value, () => KerbLowerColor2);
                UpdateKerbExampleColors();
            }
        }


        private Visibility _showKerbColor2 = Visibility.Collapsed;

        public Visibility ShowKerbColor2
        {
            get { return _showKerbColor2; }
            set { SetProperty(ref _showKerbColor2, value, () => ShowKerbColor2); }
        }


        private ObservableCollection<Rectangle> _upperKerbExampleColorRectangles;

        public ObservableCollection<Rectangle> UpperKerbExampleColorRectangles
        {
            get { return _upperKerbExampleColorRectangles; }
            set { SetProperty(ref _upperKerbExampleColorRectangles, value, () => UpperKerbExampleColorRectangles); }
        }

        private ObservableCollection<Rectangle> _lowerKerbExampleColorRectangles;

        public ObservableCollection<Rectangle> LowerKerbExampleColorRectangles
        {
            get { return _lowerKerbExampleColorRectangles; }
            set { SetProperty(ref _lowerKerbExampleColorRectangles, value, () => LowerKerbExampleColorRectangles); }
        }

        private void UpdateKerbExampleColors()
        {
            const int width = 20;
            const int upperHeight = 14;
            const int lowerHeight = 4;
            var upperColors = new ObservableCollection<Rectangle>();
            var lowerColors = new ObservableCollection<Rectangle>();

            var upperColorWhite = new SolidColorBrush(new Color { R = 0xFF, G = 0xFF, B = 0xEE, A = 0xFF });
            var lowerColorWhite = new SolidColorBrush(new Color { R = 0xDF, G = 0xDE, B = 0xD2, A = 0xFF });
            var upperColor1 = AvailableKerbColors[KerbUpperColor].DisplayValue;
            var lowerColor1 = AvailableKerbColors[KerbLowerColor].DisplayValue;

            if (KerbType == KerbType.DualColor)
            {
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColorWhite });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColor1 });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColorWhite });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColor1 });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColorWhite });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColor1 });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColorWhite });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColor1 });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColorWhite });

                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColorWhite });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColor1 });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColorWhite });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColor1 });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColorWhite });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColor1 });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColorWhite });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColor1 });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColorWhite });
            }
            else
            {
                var upperColor2 = AvailableKerbColors[KerbUpperColor2].DisplayValue;
                var lowerColor2 = AvailableKerbColors[KerbLowerColor2].DisplayValue;

                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColorWhite });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColor1 });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColorWhite });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColor2 });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColorWhite });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColor1 });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColorWhite });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColor2 });
                upperColors.Add(new Rectangle { Width = width, Height = upperHeight, Fill = upperColorWhite });

                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColorWhite });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColor1 });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColorWhite });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColor2 });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColorWhite });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColor1 });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColorWhite });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColor2 });
                lowerColors.Add(new Rectangle { Width = width, Height = lowerHeight, Fill = lowerColorWhite });
            }

            UpperKerbExampleColorRectangles = upperColors;
            LowerKerbExampleColorRectangles = lowerColors;
        }



        public List<KerbColorBoundValue> AvailableKerbColors
        {
            get
            {
                var list = new List<KerbColorBoundValue>();

                for (int i = 0; i <= 15; i++)
                {
                    var color = PaletteHelper.GetKerbColor(i);
                    var brush = new SolidColorBrush(color);
                    list.Add(new KerbColorBoundValue((byte)i, brush));
                }

                return list;
            }
        }


        private short _lapCount;

        public short LapCount
        {
            get { return _lapCount; }
            set { SetProperty(ref _lapCount, value, () => LapCount); }
        }


        private int _lapTimeIndication;

        public int LapTimeIndication
        {
            get { return _lapTimeIndication; }
            set { SetProperty(ref _lapTimeIndication, value, () => LapTimeIndication); }
        }



        private TrackSide _poleSide;

        public TrackSide PoleSide
        {
            get { return _poleSide; }
            set { SetProperty(ref _poleSide, value, () => PoleSide); }
        }


        private TrackSide _pitsSide;

        public TrackSide PitsSide
        {
            get { return _pitsSide; }
            set { SetProperty(ref _pitsSide, value, () => PitsSide); }
        }


        private SurroundingArea _surroundingArea;

        public SurroundingArea SurroundingArea
        {
            get { return _surroundingArea; }
            set { SetProperty(ref _surroundingArea, value, () => SurroundingArea); }
        }


        private short _timeFactorNonRace;

        public short TimeFactorNonRace
        {
            get { return _timeFactorNonRace; }
            set { SetProperty(ref _timeFactorNonRace, value, () => TimeFactorNonRace); }
        }


        private short _timeFactorRace;

        public short TimeFactorRace
        {
            get { return _timeFactorRace; }
            set { SetProperty(ref _timeFactorRace, value, () => TimeFactorRace); }
        }



        private short _unknownTrackDistance;

        public short UnknownTrackDistance
        {
            get { return _unknownTrackDistance; }
            set { SetProperty(ref _unknownTrackDistance, value, () => UnknownTrackDistance); }
        }

        private short _defaultPitLaneViewDistance;

        public short DefaultPitLaneViewDistance
        {
            get { return _defaultPitLaneViewDistance; }
            set { SetProperty(ref _defaultPitLaneViewDistance, value, () => DefaultPitLaneViewDistance); }
        }



        public List<TrackSideBoundValue> AvailableTrackSides
        {
            get
            {
                var list = new List<TrackSideBoundValue>
                {
                    new TrackSideBoundValue(TrackSide.Left, "Left"),
                    new TrackSideBoundValue(TrackSide.Right, "Right")
                };

                return list;
            }
        }

        public List<SurroundingAreaBoundValue> AvailableSurroundingAreas
        {
            get
            {
                var list = new List<SurroundingAreaBoundValue>
                {
                    new SurroundingAreaBoundValue(SurroundingArea.Green, "Green"),
                    new SurroundingAreaBoundValue(SurroundingArea.Gray1, "Gray1"),
                    new SurroundingAreaBoundValue(SurroundingArea.Gray2, "Gray2")
                };

                return list;
            }
        }

        public List<KerbTypeBoundValue> AvailableKerbTypes
        {
            get
            {
                var list = new List<KerbTypeBoundValue>
                {
                    new KerbTypeBoundValue(KerbType.DualColor, "Dual Color"),
                    new KerbTypeBoundValue(KerbType.TripleColor, "Triple Color")
                };

                return list;
            }
        }


        public ICommand HelpDocsCommand { get; } = new HelpDocsCommand("screens/misc-track-layout-settings-window/");
    }



    public class TrackSideBoundValue
    {
        public TrackSideBoundValue(TrackSide value, string displayValue)
        {
            Value = value;
            DisplayValue = displayValue;
        }

        public TrackSide Value { get; set; }
        public string DisplayValue { get; set; }
    }

    public class SurroundingAreaBoundValue
    {
        public SurroundingAreaBoundValue(SurroundingArea value, string displayValue)
        {
            Value = value;
            DisplayValue = displayValue;
        }

        public SurroundingArea Value { get; set; }
        public string DisplayValue { get; set; }
    }

    public class KerbTypeBoundValue
    {
        public KerbTypeBoundValue(KerbType value, string displayValue)
        {
            Value = value;
            DisplayValue = displayValue;
        }

        public KerbType Value { get; set; }
        public string DisplayValue { get; set; }
    }

    public class KerbColorBoundValue
    {
        public KerbColorBoundValue(byte value, SolidColorBrush displayValue)
        {
            Value = value;
            DisplayValue = displayValue;
        }

        public byte Value { get; set; }
        public SolidColorBrush DisplayValue { get; set; }
    }
}
