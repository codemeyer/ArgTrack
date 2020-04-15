using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.Camera
{
    public class OpenCameraCommandListWindowCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var viewModel = new CameraCommandListViewModel();
            viewModel.CameraCommands = TrackViewModelPasserArounder.TrackViewModel.TrackCameraCommands;

            var window = new CameraCommandListWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = viewModel
            };

            window.ShowDialog();
        }
    }
}
