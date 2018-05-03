using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ArgData.Entities;
using ArgTrack.Commands.Tracks;

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


        private short _trackStartWidth;

        public short TrackStartWidth
        {
            get { return _trackStartWidth; }
            set { SetProperty(ref _trackStartWidth, value, () => TrackStartWidth); }
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
                }
            }
        }


        private byte _kerbUpperColor;

        public byte KerbUpperColor
        {
            get { return _kerbUpperColor; }
            set { SetProperty(ref _kerbUpperColor, value, () => KerbUpperColor); }
        }

        private byte _kerbLowerColor;

        public byte KerbLowerColor
        {
            get { return _kerbLowerColor; }
            set { SetProperty(ref _kerbLowerColor, value, () => KerbLowerColor); }
        }

        private byte _kerbUpperColor2;

        public byte KerbUpperColor2
        {
            get { return _kerbUpperColor2; }
            set { SetProperty(ref _kerbUpperColor2, value, () => KerbUpperColor2); }
        }

        private byte _kerbLowerColor2;

        public byte KerbLowerColor2
        {
            get { return _kerbLowerColor2; }
            set { SetProperty(ref _kerbLowerColor2, value, () => KerbLowerColor2); }
        }


        private Visibility _showKerbColor2 = Visibility.Collapsed;

        public Visibility ShowKerbColor2
        {
            get { return _showKerbColor2; }
            set { SetProperty(ref _showKerbColor2, value, () => ShowKerbColor2); }
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




        private byte[] _dataAfterSetup;

        public byte[] DataAfterSetup
        {
            get { return _dataAfterSetup; }
            set { SetProperty(ref _dataAfterSetup, value, () => DataAfterSetup); }
        }

        private byte[] _finalData1;

        public byte[] FinalData1
        {
            get { return _finalData1; }
            set { SetProperty(ref _finalData1, value, () => FinalData1); }
        }


        private byte[] _finalData2;

        public byte[] FinalData2
        {
            get { return _finalData2; }
            set { SetProperty(ref _finalData2, value, () => FinalData2); }
        }


        public ICommand EditGenericByteDataCommand { get; } = new EditGenericByteDataCommand();

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
