using System.Windows;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Model.Cameras
{
    public class RemoveCameraCommandCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = GetParameterAs<CameraCommandEditingViewModel>(parameter);

            if (vm == null)
            {
                return;
            }

            var result = MessageBox.Show("Are you sure that you wish to remove the camera command?", "ArgTrack", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // TODO: don't use the global, use ParentList instead
                TrackViewModelPasserArounder.TrackViewModel.TrackCameraCommands.Remove(vm);
                //vm.ParentList.Remove(vm);
            }
        }
    }
}
