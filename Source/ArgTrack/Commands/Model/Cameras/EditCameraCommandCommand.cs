using System.Windows;
using ArgTrack.Mappers;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows;

namespace ArgTrack.Commands.Model.Cameras
{
    public class EditCameraCommandCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var origViewModel = parameter as CameraCommandEditingViewModel;
            var clone = new CameraCommandEditingViewModel();
            TrackMapper.MapCameraCommandViewModel(origViewModel, clone);

            var window = new CameraCommandDetailsWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                TrackMapper.MapCameraCommandViewModel(clone, origViewModel);
            }
        }
    }
}
