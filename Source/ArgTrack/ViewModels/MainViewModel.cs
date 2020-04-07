using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ArgTrack.AppStuff;
using ArgTrack.Commands;
using ArgTrack.Commands.Tracks;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.ViewModels
{
    public class MainViewModel : BaseViewModel, IDataErrorInfo
    {
        public ICommand LoadTrackCommand { get; } = new LoadTrackCommand(new FileDialog());
        public ICommand SaveTrackCommand { get; } = new SaveTrackCommand(new FileDialog());
        public ICommand ExitApplicationCommand { get; } = new ExitApplicationCommand();

        public ICommand HelpDocsCommand { get; } = new HelpDocsCommand();

        public ICommand ValidateTrackCommand { get; } = new ValidateTrackCommand();
        public ICommand ConvertTrackCommand { get; } = new ConvertTrackCommand();

        public ICommand OpenAboutWindowCommand { get; } = new OpenAboutWindowCommand();

        public ICommand ShowWarningMessageCommand { get; } = new ShowWarningMessageCommand();
        public ICommand ShowErrorMessageCommand { get; } = new ShowErrorMessageCommand();
        public ICommand ShowMessageCommand { get; } = new ShowMessageCommand();

        public ICommand OpenTrackSectionListWindowCommand { get; } = new OpenTrackSectionListWindowCommand();
        public ICommand OpenPitLaneSectionListWindowCommand { get; } = new OpenPitLaneSectionListWindowCommand();
        public ICommand OpenComputerCarRacingLineWindowCommand { get; } = new OpenComputerCarRacingLineWindowCommand();
        public ICommand OpenObjectSettingsListWindowCommand { get; } = new OpenObjectSettingsListWindowCommand();
        public ICommand OpenObjectShapesListWindowCommand { get; } = new OpenObjectShapesListWindowCommand();


        private bool _hasLoadedTrack;

        public bool HasLoadedTrack
        {
            get { return _hasLoadedTrack; }
            set
            {
                if (SetProperty(ref _hasLoadedTrack, value, () => HasLoadedTrack))
                {
                    UpdateTreeViewVisibility();
                }
            }
        }

        private void UpdateTreeViewVisibility()
        {
            VisibleAtStartup = HasLoadedTrack ? Visibility.Hidden : Visibility.Visible;
            VisibleWhenTrackHasBeenLoaded = HasLoadedTrack ? Visibility.Visible : Visibility.Hidden;
        }



        private Visibility _visibleAtStartup = Visibility.Visible;

        public Visibility VisibleAtStartup
        {
            get { return _visibleAtStartup; }
            set { SetProperty(ref _visibleAtStartup, value, () => VisibleAtStartup); }
        }



        private Visibility _visibleWhenTrackHasBeenLoaded = Visibility.Hidden;

        public Visibility VisibleWhenTrackHasBeenLoaded
        {
            get { return _visibleWhenTrackHasBeenLoaded; }
            set { SetProperty(ref _visibleWhenTrackHasBeenLoaded, value, () => VisibleWhenTrackHasBeenLoaded); }
        }


        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                return "";
            }
        }


        private TrackViewModel _trackViewModel;

        public TrackViewModel TrackViewModel
        {
            get { return _trackViewModel; }
            set { SetProperty(ref _trackViewModel, value, () => TrackViewModel); }
        }


        private TrackLayoutViewModel _trackLayoutViewModel;

        public TrackLayoutViewModel TrackLayoutViewModel
        {
            get { return _trackLayoutViewModel; }
            set { SetProperty(ref _trackLayoutViewModel, value, () => TrackLayoutViewModel); }
        }
    }
}
