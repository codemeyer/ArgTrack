using System.Windows.Input;
using ArgTrack.Features.Application.Shared;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.WorldLocation
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


        public ICommand HelpDocsCommand { get; } = new HelpDocsCommand("screens/world-location-settings-window/");
    }
}
