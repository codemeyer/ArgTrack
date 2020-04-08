using System;
using System.Windows;
using ArgData;
using ArgTrack.AppStuff;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Tracks
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
                return;

            try
            {
                var track = new TrackReader().Read(path);

                var viewModel = Mappers.TrackMapper.MapEntireTrackToViewModel(track);

                // blergh
                TrackViewModelPasserArounder.TrackViewModel = viewModel;
                MainViewModelPasserArounder.MainViewModel.HasLoadedTrack = true;
                MainViewModelPasserArounder.MainViewModel.TrackViewModel = viewModel;

                // TEMP.AF
                var layoutVM = TrackLayoutViewModel.FromTrackViewModel(viewModel);

                MainViewModelPasserArounder.MainViewModel.TrackLayoutViewModel = layoutVM;

            //var twin = new MainTreeWindow();
            //twin.DataContext = MainViewModelPasserArounder.MainViewModel;
            //twin.Show();

                //var win = new HackWin();
                //win.DataContext = layoutVM;
                //win.ShowDialog();
                //
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "An error occurred when loading the track. Make sure that the specified file is an F1GP track file and try again.",
                    "ArgTrack", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
