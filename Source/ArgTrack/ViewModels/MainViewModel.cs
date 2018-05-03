using System.ComponentModel;
using System.Windows.Input;
using ArgTrack.Commands;
using ArgTrack.Commands.Tracks;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.ViewModels
{
    public class MainViewModel : BaseViewModel, IDataErrorInfo
    {
        public MainViewModel()
        {
            AboutTabViewModel = new AboutTabViewModel();
            TrackTabViewModel = new TrackTabViewModel();
        }

        public ICommand LoadTrackCommand { get; } = new LoadTrackCommand(new FileDialog());
        public ICommand SaveTrackCommand { get; } = new SaveTrackCommand(new FileDialog());
        public ICommand ExitApplicationCommand { get; } = new ExitApplicationCommand();

        public ICommand HelpDocsCommand { get; } = new HelpDocsCommand();
        
        public ICommand ShowWarningMessageCommand { get; } = new ShowWarningMessageCommand();
        public ICommand ShowErrorMessageCommand { get; } = new ShowErrorMessageCommand();
        public ICommand ShowMessageCommand { get; } = new ShowMessageCommand();

        //public ICommand UndoBrokerCommand { get; } = new UndoBrokerCommand();
        //public ICommand RedoBrokerCommand { get; } = new RedoBrokerCommand();


        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                return "";
            }
        }

        public AboutTabViewModel AboutTabViewModel { get; set; }

        public TrackTabViewModel TrackTabViewModel { get; set; }


        private int _selectedMainTabIndex;

        public int SelectedMainTabIndex
        {
            get { return _selectedMainTabIndex; }
            set { SetProperty(ref _selectedMainTabIndex, value, () => SelectedMainTabIndex); }
        }
    }
}
