using ArgData.Entities;

namespace ArgTrack.ViewModels.Track
{
    public abstract class TrackCameraCommandViewModel : BaseViewModel
    {
    }

    public class DeleteTrackCameraCommandViewModel : TrackCameraCommandViewModel
    {
        public DeleteTrackCameraCommandViewModel(int cameraIndex)
        {
            CameraIndex = cameraIndex;
        }

        private int _cameraIndex;

        public int CameraIndex
        {
            get { return _cameraIndex; }
            set { SetProperty(ref _cameraIndex, value, () => CameraIndex); }
        }
    }

    public class TrackCameraAdjustmentCommandViewModel : TrackCameraCommandViewModel
    {
        public TrackCameraAdjustmentCommandViewModel(byte cameraIndex, byte adjustment, TrackSide trackSide)
        {
            CameraIndex = cameraIndex;
            Adjustment = adjustment;
            TrackSide = trackSide;
        }

        private byte _cameraIndex;

        public byte CameraIndex
        {
            get { return _cameraIndex; }
            set { SetProperty(ref _cameraIndex, value, () => CameraIndex); }
        }

        private byte _adjustment;

        public byte Adjustment
        {
            get { return _adjustment; }
            set { SetProperty(ref _adjustment, value, () => Adjustment); }
        }

        private TrackSide _trackSide;

        public TrackSide TrackSide
        {
            get { return _trackSide; }
            set { SetProperty(ref _trackSide, value, () => TrackSide); }
        }
    }

    public class TrackCameraRangeRightSideAdjustmentCommandViewModel : TrackCameraCommandViewModel
    {
        public TrackCameraRangeRightSideAdjustmentCommandViewModel(byte cameraIndexFrom, byte cameraIndexTo)
        {
            CameraIndexFrom = cameraIndexFrom;
            CameraIndexTo = cameraIndexTo;
        }

        private byte _cameraIndexFrom;

        public byte CameraIndexFrom
        {
            get { return _cameraIndexFrom; }
            set { SetProperty(ref _cameraIndexFrom, value, () => CameraIndexFrom); }
        }

        private byte _cameraIndexTo;

        public byte CameraIndexTo
        {
            get { return _cameraIndexTo; }
            set { SetProperty(ref _cameraIndexTo, value, () => CameraIndexTo); }
        }
    }
}
