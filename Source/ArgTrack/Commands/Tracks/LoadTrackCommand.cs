using System;
using System.Windows;
using ArgData;
using ArgTrack.ViewModels;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Tracks
{
    public class LoadTrackCommand : CommandBase
    {
        private string _path;
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
            var vm = parameter as TrackTabViewModel;

            if (vm == null)
            {
                var main = parameter as MainViewModel;
                if (main == null)
                    return;

                vm = main.TrackTabViewModel;
            }

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

                vm.Track = viewModel;
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
