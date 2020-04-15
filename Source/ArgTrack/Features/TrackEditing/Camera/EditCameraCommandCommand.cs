using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.Camera
{
    public class EditCameraCommandCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var origViewModel = parameter as CameraCommandEditingViewModel;
            var clone = new CameraCommandEditingViewModel();

            CameraCommandMapper.MapCameraCommandViewModel(origViewModel, clone);

            var window = new CameraCommandDetailsWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                CameraCommandMapper.MapCameraCommandViewModel(clone, origViewModel);
            }
        }
    }
}
