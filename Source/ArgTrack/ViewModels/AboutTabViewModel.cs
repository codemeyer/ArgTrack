using System.Reflection;
using System.Windows;
using System.Windows.Input;
using ArgTrack.Commands;

namespace ArgTrack.ViewModels
{
    public class AboutTabViewModel : BaseViewModel
    {
        public AboutTabViewModel()
        {
            CurrentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            LatestVersion = string.Empty;
            VersionLatestLabel = Visibility.Collapsed;
            VersionUnableToRetrieveLabel = Visibility.Collapsed;
            VersionNewAvailable = Visibility.Collapsed;
        }

        private string _currentVersion;
        private string _latestVersion;
        private Visibility _versionLatestLabel;
        private Visibility _versionUnableToRetrieveLabel;
        private Visibility _versionNewAvailable;

        public string CurrentVersion
        {
            get { return _currentVersion; }
            set { SetProperty(ref _currentVersion, value, () => CurrentVersion); }
        }

        public string LatestVersion
        {
            get { return _latestVersion; }
            set { SetProperty(ref _latestVersion, value, () => LatestVersion); }
        }

        public Visibility VersionLatestLabel
        {
            get { return _versionLatestLabel; }
            set { SetProperty(ref _versionLatestLabel, value, () => VersionLatestLabel); }
        }

        public Visibility VersionUnableToRetrieveLabel
        {
            get { return _versionUnableToRetrieveLabel; }
            set { SetProperty(ref _versionUnableToRetrieveLabel, value, () => VersionUnableToRetrieveLabel); }
        }

        public Visibility VersionNewAvailable
        {
            get { return _versionNewAvailable; }
            set { SetProperty(ref _versionNewAvailable, value, () => VersionNewAvailable); }
        }

        public ICommand CheckForUpdateCommand { get; } = new CheckForUpdateCommand();
        public ICommand OpenDownloadPageCommand { get; } = new OpenDownloadPageCommand();
        public ICommand HelpDocsCommand { get; } = new HelpDocsCommand();
        public ICommand OpenArgToolsSiteCommand { get; } = new OpenOtherSiteCommand("http://www.argtools.com");
        public ICommand OpenFanSiteCommand { get; } = new OpenOtherSiteCommand("https://sites.google.com/view/f1gpwc");
        public ICommand OpenGoogleGroupSiteCommand { get; } = new OpenOtherSiteCommand("https://groups.google.com/forum/#!forum/f1gpwc");
    }
}
