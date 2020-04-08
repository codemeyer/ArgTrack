using System.Windows.Input;
using ArgTrack.Commands;

namespace ArgTrack.ViewModels
{
    public class TrackConversionViewModel : BaseViewModel
    {
        private string _sourceTrackPath;

        public string SourceTrackPath
        {
            get { return _sourceTrackPath; }
            set { SetProperty(ref _sourceTrackPath, value, () => SourceTrackPath); }
        }

        private string _templateTrackPath;

        public string TemplateTrackPath
        {
            get { return _templateTrackPath; }
            set { SetProperty(ref _templateTrackPath, value, () => TemplateTrackPath); }
        }

        private string _targetTrackPath;

        public string TargetTrackPath
        {
            get { return _targetTrackPath; }
            set { SetProperty(ref _targetTrackPath, value, () => TargetTrackPath); }
        }


        public ICommand HelpDocsCommand { get; } = new HelpDocsCommand("screens/gp2-track-conversion-window/");
    }
}
