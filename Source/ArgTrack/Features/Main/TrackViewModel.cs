using System.Collections.ObjectModel;
using System.Windows.Input;
using ArgTrack.Features.TrackEditing.Camera;
using ArgTrack.Features.TrackEditing.CarSettings;
using ArgTrack.Features.TrackEditing.CarSetup;
using ArgTrack.Features.TrackEditing.ComputerCarBehavior;
using ArgTrack.Features.TrackEditing.ObjectSettings;
using ArgTrack.Features.TrackEditing.ObjectShapes;
using ArgTrack.Features.TrackEditing.RacingLine;
using ArgTrack.Features.TrackEditing.TrackSection;
using ArgTrack.Features.TrackEditing.TrackSettings;
using ArgTrack.Features.TrackEditing.WorldLocation;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.Main
{
    public class TrackViewModel : BaseViewModel
    {
        public TrackViewModel()
        {
            _trackSections = new TrackSectionViewModelCollection(this);
            _pitLaneSections = new TrackSectionViewModelCollection(this);
            _bestLineSegments = new BestLineSegmentViewModelCollection(this);
            _trackObjectSettings = new ObservableCollection<TrackObjectSettingsViewModel>();
            _trackObjectShapes = new ObservableCollection<TrackObjectShapeViewModel>();

            TrackSettings = new TrackSettingsViewModel();

            ComputerCarBehavior = new ComputerCarBehaviorViewModel();

            CarSettings = new CarSettingsViewModel();

            TrackCameraCommands = new TrackCameraCommandViewModelCollection(this);

            TrackHeader = new TrackHeaderViewModel();
            ComputerCarSetup = new SetupViewModel("TrackSetup");
        }

        private TrackSectionViewModelCollection _trackSections;

        public TrackSectionViewModelCollection TrackSections
        {
            get { return _trackSections; }
            set { SetProperty(ref _trackSections, value, () => TrackSections); }
        }


        public void RecalculateLayout()
        {
            // TODO: move this to MainViewModel or directly on TrackLayoutViewModel?

            var mainViewModel = MainViewModelPasserArounder.MainViewModel;
            var layoutViewModel = MainViewModelPasserArounder.MainViewModel.TrackLayoutViewModel;

            // save for restoring
            var currentZoom = layoutViewModel.ZoomFactor;
            var currentCenter = layoutViewModel.CenterPoint;
            var currentOffsetX = layoutViewModel.OffsetX;
            var currentOffsetY = layoutViewModel.OffsetY;
            // TODO: need the index or some other thing to identify the currently selected data,
            // because if it changes shape during the redraw this will still have the old shape
            //var selectedPointData = mainViewModel.TrackLayoutViewModel.SelectedTrackSection;

            // hack kludge until we just update what we want...
            layoutViewModel.PerformFullCalculation();

            layoutViewModel.SetDisplayTrackShapeData(mainViewModel.DrawTrack);
            layoutViewModel.SetDisplayPitLaneShapeData(mainViewModel.DrawPitLane);
            layoutViewModel.SetDisplayVergeData(mainViewModel.DrawVerges);
            layoutViewModel.SetDisplayTrackSideObjectData(mainViewModel.DrawTrackSideObjects);
            // TODO: debug stuff?

            // restore
            layoutViewModel.CenterPoint = currentCenter;
            layoutViewModel.ZoomFactor = currentZoom;
            layoutViewModel.OffsetX = currentOffsetX;
            layoutViewModel.OffsetY = currentOffsetY;
            // TODO: update SelectedTrackSection somehow
            //layoutViewModel.SelectedTrackSection = selectedPointData;
        }


        private TrackSectionViewModelCollection _pitLaneSections;

        public TrackSectionViewModelCollection PitLaneSections
        {
            get { return _pitLaneSections; }
            set { SetProperty(ref _pitLaneSections, value, () => PitLaneSections); }
        }


        private BestLineSegmentViewModelCollection _bestLineSegments;

        public BestLineSegmentViewModelCollection BestLineSegments
        {
            get { return _bestLineSegments; }
            set { SetProperty(ref _bestLineSegments, value, () => BestLineSegments); }
        }


        private ObservableCollection<TrackObjectSettingsViewModel> _trackObjectSettings;

        public ObservableCollection<TrackObjectSettingsViewModel> TrackObjectSettings
        {
            get { return _trackObjectSettings; }
            set { SetProperty(ref _trackObjectSettings, value, () => TrackObjectSettings); }
        }


        private ObservableCollection<TrackObjectShapeViewModel> _trackObjectShapes;

        public ObservableCollection<TrackObjectShapeViewModel> TrackObjectShapes
        {
            get { return _trackObjectShapes; }
            set { SetProperty(ref _trackObjectShapes, value, () => TrackObjectShapes); }
        }


        private TrackCameraCommandViewModelCollection _trackCameraCommands;

        public TrackCameraCommandViewModelCollection TrackCameraCommands
        {
            get { return _trackCameraCommands; }
            set { SetProperty(ref _trackCameraCommands, value, () => TrackCameraCommands); }
        }


        public TrackSettingsViewModel TrackSettings { get; }


        public TrackHeaderViewModel TrackHeader { get; }


        public SetupViewModel ComputerCarSetup { get; set; }


        public ComputerCarBehaviorViewModel ComputerCarBehavior { get; set; }


        public CarSettingsViewModel CarSettings { get; set; }


        private short _computerCarLineDisplacement;

        // place elsewhere in the future?
        public short ComputerCarLineDisplacement
        {
            get { return _computerCarLineDisplacement; }
            set { SetProperty(ref _computerCarLineDisplacement, value, () => ComputerCarLineDisplacement); }
        }


        private byte[] _horizonBytes;

        public byte[] HorizonBytes
        {
            get { return _horizonBytes; }
            set
            {
                SetProperty(ref _horizonBytes, value, () => HorizonBytes);
            }
        }


        public ICommand EditTrackSectionCommand { get; } = new EditTrackSectionCommand();
        public ICommand EditBestLineCommand { get; } = new EditBestLineCommand();
        public ICommand EditTrackObjectSettingsCommand { get; } = new EditTrackObjectSettingsCommand();
        public ICommand EditTrackObjectShapeCommand { get; } = new EditTrackObjectShapeCommand();
    }
}
