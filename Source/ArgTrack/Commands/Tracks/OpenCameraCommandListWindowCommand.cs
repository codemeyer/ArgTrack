using System.Windows;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class OpenCameraCommandListWindowCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var viewModel = new CameraCommandListViewModel();
            viewModel.CameraCommands = TrackViewModelPasserArounder.TrackViewModel.TrackCameraCommands;

            var window = new CameraCommandListWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = viewModel
            };

            window.ShowDialog();
        }
    }
}
