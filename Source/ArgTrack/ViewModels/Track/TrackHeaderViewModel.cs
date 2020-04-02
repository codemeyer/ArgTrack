using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using ArgData.Entities;
using ArgTrack.AppStuff;
using ArgTrack.TrackStuff;

namespace ArgTrack.ViewModels.Track
{
    public class TrackHeaderViewModel : BaseViewModel
    {
        private ushort _firstSectionAngle;

        public ushort FirstSectionAngle
        {
            get { return _firstSectionAngle; }
            set { SetProperty(ref _firstSectionAngle, value, () => FirstSectionAngle); }
        }


        private short _firstSectionHeight;

        public short FirstSectionHeight
        {
            get { return _firstSectionHeight; }
            set { SetProperty(ref _firstSectionHeight, value, () => FirstSectionHeight); }
        }


        private short _trackCenterX;

        public short TrackCenterX
        {
            get { return _trackCenterX; }
            set { SetProperty(ref _trackCenterX, value, () => TrackCenterX); }
        }

        private short _trackCenterY;

        public short TrackCenterY
        {
            get { return _trackCenterY; }
            set { SetProperty(ref _trackCenterY, value, () => TrackCenterY); }
        }

        private short _trackCenterZ;

        public short TrackCenterZ
        {
            get { return _trackCenterZ; }
            set { SetProperty(ref _trackCenterZ, value, () => TrackCenterZ); }
        }


        private short _trackStartWidth;

        public short TrackStartWidth
        {
            get { return _trackStartWidth; }
            set
            {
                if (SetProperty(ref _trackStartWidth, value, () => TrackStartWidth))
                {
                    UpdateTrackWidthDescription();
                }
            }
        }


        private void UpdateTrackWidthDescription()
        {
            TrackWidthDescription = UnitConvert.TrackWidthToMetersDescription(TrackStartWidth);
        }


        private string _trackWidthDescription;

        public string TrackWidthDescription
        {
            get { return _trackWidthDescription; }
            set { SetProperty(ref _trackWidthDescription, value, () => TrackWidthDescription); }
        }



        private byte _leftVergeStartWidth;

        public byte LeftVergeStartWidth
        {
            get { return _leftVergeStartWidth; }
            set { SetProperty(ref _leftVergeStartWidth, value, () => LeftVergeStartWidth); }
        }

        private byte _rightVergeStartWidth;

        public byte RightVergeStartWidth
        {
            get { return _rightVergeStartWidth; }
            set { SetProperty(ref _rightVergeStartWidth, value, () => RightVergeStartWidth); }
        }


        private KerbType _kerbType;

        [System.Obsolete("Moved to TrackSettingsViewModel")]
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

        [System.Obsolete("Moved to TrackSettingsViewModel")]
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

        [System.Obsolete("Moved to TrackSettingsViewModel")]
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

        [System.Obsolete("Moved to TrackSettingsViewModel")]
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

        [System.Obsolete("Moved to TrackSettingsViewModel")]
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

        [System.Obsolete("Moved to TrackSettingsViewModel")]
        public Visibility ShowKerbColor2
        {
            get { return _showKerbColor2; }
            set { SetProperty(ref _showKerbColor2, value, () => ShowKerbColor2); }
        }


        private ObservableCollection<Rectangle> _upperKerbExampleColorRectangles;

        [System.Obsolete("Moved to TrackSettingsViewModel")]
        public ObservableCollection<Rectangle> UpperKerbExampleColorRectangles
        {
            get { return _upperKerbExampleColorRectangles; }
            set { SetProperty(ref _upperKerbExampleColorRectangles, value, () => UpperKerbExampleColorRectangles); }
        }

        private ObservableCollection<Rectangle> _lowerKerbExampleColorRectangles;

        [System.Obsolete("Moved to TrackSettingsViewModel")]
        public ObservableCollection<Rectangle> LowerKerbExampleColorRectangles
        {
            get { return _lowerKerbExampleColorRectangles; }
            set { SetProperty(ref _lowerKerbExampleColorRectangles, value, () => LowerKerbExampleColorRectangles); }
        }

        [System.Obsolete("Moved to TrackSettingsViewModel")]
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

        [System.Obsolete("Moved to TrackSettingsViewModel")]
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


        // also contains Offsets and RawData for now

        private int _unknownLong1;

        public int UnknownLong1
        {
            get { return _unknownLong1; }
            set { SetProperty(ref _unknownLong1, value, () => UnknownLong1); }
        }

        private int _unknownLong2;

        public int UnknownLong2
        {
            get { return _unknownLong2; }
            set { SetProperty(ref _unknownLong2, value, () => UnknownLong2); }
        }

        private short _checksumPosition;

        public short ChecksumPosition
        {
            get { return _checksumPosition; }
            set { SetProperty(ref _checksumPosition, value, () => ChecksumPosition); }
        }

        private short _objectData;

        public short ObjectData
        {
            get { return _objectData; }
            set { SetProperty(ref _objectData, value, () => ObjectData); }
        }

        private short _trackData;

        public short TrackData
        {
            get { return _trackData; }
            set { SetProperty(ref _trackData, value, () => TrackData); }
        }



        // TODO: these three items should be somewhere else, not really related to TrackHeader anymore

        //private byte[] _dataAfterSetup;

        //public byte[] DataAfterSetup
        //{
        //    get { return _dataAfterSetup; }
        //    set { SetProperty(ref _dataAfterSetup, value, () => DataAfterSetup); }
        //}

        //private byte[] _finalData1;

        //public byte[] FinalData1
        //{
        //    get { return _finalData1; }
        //    set { SetProperty(ref _finalData1, value, () => FinalData1); }
        //}


        //private byte[] _finalData2;

        //public byte[] FinalData2
        //{
        //    get { return _finalData2; }
        //    set { SetProperty(ref _finalData2, value, () => FinalData2); }
        //}


        //public ICommand EditGenericByteDataCommand { get; } = new EditGenericByteDataCommand();

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
