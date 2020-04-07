using System.Collections.Generic;
using System.Windows;
using ArgData.Entities;

namespace ArgTrack.ViewModels.Track
{
    public class CameraCommandEditingViewModel : BaseViewModel
    {
        public CameraCommandEditingViewModel()
        {
            CameraCommandType = CameraCommandType.Adjustment;
            CanEditType = true;
        }

        public CameraCommandEditingViewModel(TrackCameraAdjustmentCommand cameraCommand)
        {
            CanEditType = false;
            CameraIndex = cameraCommand.CameraIndex;
            Adjustment = cameraCommand.Adjustment;
            TrackSide = cameraCommand.TrackSide;
            CameraCommandType = CameraCommandType.Adjustment;
        }

        public CameraCommandEditingViewModel(TrackCameraRangeRightSideAdjustmentCommand cameraCommand)
        {
            CanEditType = false;
            CameraIndexFrom = cameraCommand.CameraIndexFrom;
            CameraIndexTo = cameraCommand.CameraIndexTo;
            CameraCommandType = CameraCommandType.RightRange;
        }

        public CameraCommandEditingViewModel(DeleteTrackCameraCommand cameraCommand)
        {
            CanEditType = false;
            CameraIndex = (byte)cameraCommand.CameraIndex;
            CameraCommandType = CameraCommandType.Delete;
        }


        private string _commandDescription;

        public string CommandDescription
        {
            get { return _commandDescription; }
            set { SetProperty(ref _commandDescription, value, () => CommandDescription); }
        }

        private string _commandDataDescription;

        public string CommandDataDescription
        {
            get { return _commandDataDescription; }
            set { SetProperty(ref _commandDataDescription, value, () => CommandDataDescription); }
        }


        private bool _canEditType;

        public bool CanEditType
        {
            get { return _canEditType; }
            set { SetProperty(ref _canEditType, value, () => CanEditType); }
        }


        public List<CameraCommandTypeBoundValue> AvailableCameraCommandTypes
        {
            get
            {
                var list = new List<CameraCommandTypeBoundValue>();

                list.Add(new CameraCommandTypeBoundValue(CameraCommandType.Adjustment, "Adjustment"));
                list.Add(new CameraCommandTypeBoundValue(CameraCommandType.RightRange, "Move Range Right"));
                list.Add(new CameraCommandTypeBoundValue(CameraCommandType.Delete, "Delete"));

                return list;
            }
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


        private CameraCommandType _cameraCommandType;

        public CameraCommandType CameraCommandType
        {
            get { return _cameraCommandType; }
            set
            {
                SetProperty(ref _cameraCommandType, value, () => CameraCommandType);
                UpdateVisibility(value);
                UpdateDescription();
            }
        }

        private void UpdateVisibility(CameraCommandType value)
        {
            if (value == CameraCommandType.Adjustment)
            {
                ShowAdjustment = Visibility.Visible;
                ShowCameraIndex = Visibility.Visible;
                ShowTrackSide = Visibility.Visible;
                ShowCameraIndexFrom = Visibility.Collapsed;
                ShowCameraIndexTo = Visibility.Collapsed;
            }

            if (value == CameraCommandType.RightRange)
            {
                ShowAdjustment = Visibility.Collapsed;
                ShowCameraIndex = Visibility.Collapsed;
                ShowTrackSide = Visibility.Collapsed;
                ShowCameraIndexFrom = Visibility.Visible;
                ShowCameraIndexTo = Visibility.Visible;
            }

            if (value == CameraCommandType.Delete)
            {
                ShowAdjustment = Visibility.Collapsed;
                ShowCameraIndex = Visibility.Visible;
                ShowTrackSide = Visibility.Collapsed;
                ShowCameraIndexFrom = Visibility.Collapsed;
                ShowCameraIndexTo = Visibility.Collapsed;
            }
        }


        private byte _cameraIndex;

        public byte CameraIndex
        {
            get { return _cameraIndex; }
            set
            {
                SetProperty(ref _cameraIndex, value, () => CameraIndex);
                UpdateDescription();
            }
        }

        private byte _adjustment;

        public byte Adjustment
        {
            get { return _adjustment; }
            set
            {
                SetProperty(ref _adjustment, value, () => Adjustment);
                UpdateDescription();
            }
        }

        private TrackSide _trackSide;

        public TrackSide TrackSide
        {
            get { return _trackSide; }
            set
            {
                SetProperty(ref _trackSide, value, () => TrackSide);
                UpdateDescription();
            }
        }

        private byte _cameraIndexFrom;

        public byte CameraIndexFrom
        {
            get { return _cameraIndexFrom; }
            set
            {
                SetProperty(ref _cameraIndexFrom, value, () => CameraIndexFrom);
                UpdateDescription();
            }
        }

        private byte _cameraIndexTo;

        public byte CameraIndexTo
        {
            get { return _cameraIndexTo; }
            set
            {
                SetProperty(ref _cameraIndexTo, value, () => CameraIndexTo);
                UpdateDescription();
            }
        }


        private void UpdateDescription()
        {
            switch (CameraCommandType)
            {
                case CameraCommandType.Delete:
                    CommandDescription = "Delete";
                    CommandDataDescription = $"Index: {CameraIndex}";
                    break;
                case CameraCommandType.Adjustment:
                    CommandDescription = "Adjustment";
                    CommandDataDescription = $"Index: {CameraIndex}, Adjustment: {Adjustment}, Track Side: {TrackSide}";
                    break;
                case CameraCommandType.RightRange:
                    CommandDescription = "Move Range Right";
                    CommandDataDescription = $"From Index: {CameraIndexFrom}, To Index: {CameraIndexTo}";
                    break;
                default:
                    CommandDataDescription = string.Empty;
                    break;
            }
        }


        private Visibility _showCameraIndex;

        public Visibility ShowCameraIndex
        {
            get { return _showCameraIndex; }
            set { SetProperty(ref _showCameraIndex, value, () => ShowCameraIndex); }
        }

        private Visibility _showCameraIndexFrom;

        public Visibility ShowCameraIndexFrom
        {
            get { return _showCameraIndexFrom; }
            set { SetProperty(ref _showCameraIndexFrom, value, () => ShowCameraIndexFrom); }
        }

        private Visibility _showCameraIndexTo;

        public Visibility ShowCameraIndexTo
        {
            get { return _showCameraIndexTo; }
            set { SetProperty(ref _showCameraIndexTo, value, () => ShowCameraIndexTo); }
        }


        private Visibility _showAdjustment;

        public Visibility ShowAdjustment
        {
            get { return _showAdjustment; }
            set { SetProperty(ref _showAdjustment, value, () => ShowAdjustment); }
        }

        private Visibility _showTrackSide;


        public Visibility ShowTrackSide
        {
            get { return _showTrackSide; }
            set { SetProperty(ref _showTrackSide, value, () => ShowTrackSide); }
        }
    }


    public enum CameraCommandType
    {
        Delete,
        Adjustment,
        RightRange
    }


    public class CameraCommandTypeBoundValue
    {
        public CameraCommandTypeBoundValue(CameraCommandType value, string displayValue)
        {
            Value = value;
            DisplayValue = displayValue;
        }

        public CameraCommandType Value { get; set; }
        public string DisplayValue { get; set; }
    }
}
