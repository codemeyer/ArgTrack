using ArgData;
using ArgTrack.Features.Main;
using ArgTrack.Features.TrackDrawing;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.Persistence
{
    public class LoadTrackCommand : CommandBase
    {
        private readonly string _path;
        private readonly IFileDialog _fileDialog;

        public LoadTrackCommand(string path)
        {
            _path = path;
        }

        public LoadTrackCommand(IFileDialog fileDialog)
        {
            _fileDialog = fileDialog;
        }

        public override void Execute(object parameter)
        {
            var path = _path;

            if (_fileDialog != null)
            {
                path = _fileDialog.SelectDatFileToOpen();
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

#if DEBUG
            LoadTrack(path);
#else
            try
            {
                LoadTrack(path);
            }
            catch (System.Exception)
            {
                System.Windows.MessageBox.Show(
                    "An error occurred when loading the track. Make sure that the specified file is an F1GP track file and try again.",
                    "ArgTrack", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
#endif
        }

        private void LoadTrack(string path)
        {
            var track = new TrackReader().Read(path);

            var trackViewModel = TrackMapper.MapEntireTrackToViewModel(track);
            TrackViewModelPasserArounder.TrackViewModel = trackViewModel;

            var mainViewModel = MainViewModelPasserArounder.MainViewModel;
            mainViewModel.HasLoadedTrack = true;

            // reset
            mainViewModel.ResetDrawingElements();

            mainViewModel.TrackViewModel = trackViewModel;

            // perhaps should be initialized elsewhere?
            //var layoutViewModel = TrackLayoutViewModel.FromTrackViewModel(trackViewModel, new TrackDrawingElementsSettings());
            var layoutViewModel = new TrackLayoutViewModel(trackViewModel);

            MainViewModelPasserArounder.MainViewModel.TrackLayoutViewModel = layoutViewModel;
        }
    }
}
