using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ArgTrack.Features.Application.Shared;
using ArgTrack.Features.TrackDrawing;
using ArgTrack.Features.TrackEditing;
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

            TrackSections.CollectionChanged += TrackSections_CollectionChanged;
        }

        private void TrackSections_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            TotalLengthText = UnitConvert.TrackLengthToMetersDescription(TrackSections.Sum(t => t.Length));
        }

        private TrackSectionViewModelCollection _trackSections;

        public TrackSectionViewModelCollection TrackSections
        {
            get { return _trackSections; }
            set { SetProperty(ref _trackSections, value, () => TrackSections); }
        }


        public void RecalculateLayout()
        {
            // save for restoring
            var currentZoom = MainViewModelPasserArounder.MainViewModel.TrackLayoutViewModel.ZoomFactor;
            var currentCenter = MainViewModelPasserArounder.MainViewModel.TrackLayoutViewModel.CenterPoint;

            var settings = new TrackDrawingElementsSettings
            {
                DrawTrack = MainViewModelPasserArounder.MainViewModel.DrawTrack,
                DrawPitLane = MainViewModelPasserArounder.MainViewModel.DrawPitLane,
                DrawVerges = MainViewModelPasserArounder.MainViewModel.DrawVerges
            };

            var layoutViewModel = TrackLayoutViewModel.FromTrackViewModel(this, settings);

            // restore
            layoutViewModel.CenterPoint = currentCenter;
            layoutViewModel.ZoomFactor = currentZoom;

            MainViewModelPasserArounder.MainViewModel.TrackLayoutViewModel = layoutViewModel;
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


        private string _totalLengthText;

        public string TotalLengthText
        {
            get { return _totalLengthText; }
            set { SetProperty(ref _totalLengthText, value, () => TotalLengthText); }
        }


        public ICommand EditTrackSectionCommand { get; } = new EditTrackSectionCommand();
        public ICommand EditBestLineCommand { get; } = new EditBestLineCommand();
        public ICommand EditTrackObjectSettingsCommand { get; } = new EditTrackObjectSettingsCommand();
        public ICommand EditTrackObjectShapeCommand { get; } = new EditTrackObjectShapeCommand();

        public ICommand HelpDocsCommandObjectSettings { get; } = new HelpDocsCommand("screens/object-settings-list-window/");
        public ICommand HelpDocsCommandObjectShapes { get; } = new HelpDocsCommand("screens/object-shapes-list-window/");
    }
}
