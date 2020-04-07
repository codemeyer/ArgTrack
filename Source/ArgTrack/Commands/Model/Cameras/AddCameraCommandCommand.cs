using System.Windows;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows;

namespace ArgTrack.Commands.Model.Cameras
{
    public class AddCameraCommandCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var commandList = parameter as CameraCommandListViewModel;

            var viewModel = new CameraCommandEditingViewModel();

            var window = new CameraCommandDetailsWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = viewModel
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                commandList.CameraCommands.Add(viewModel);
            }
        }
    }
}
