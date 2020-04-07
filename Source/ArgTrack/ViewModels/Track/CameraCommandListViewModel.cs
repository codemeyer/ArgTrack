using System.Windows.Input;
using ArgTrack.Commands.Model.Cameras;

namespace ArgTrack.ViewModels.Track
{
    public class CameraCommandListViewModel : BaseViewModel
    {
        public TrackCameraCommandViewModelCollection CameraCommands { get; set; }


        private int _selectedCommandIndex;

        public int SelectedCommandIndex
        {
            get { return _selectedCommandIndex; }
            set { SetProperty(ref _selectedCommandIndex, value, () => SelectedCommandIndex); }
        }

        private CameraCommandEditingViewModel _selectedCameraCommand;

        public CameraCommandEditingViewModel SelectedCameraCommand
        {
            get { return _selectedCameraCommand; }
            set { SetProperty(ref _selectedCameraCommand, value, () => SelectedCameraCommand); }
        }


        public ICommand AddCameraCommandCommand { get; } = new AddCameraCommandCommand();
        public ICommand EditCameraCommandCommand { get; } = new EditCameraCommandCommand();
        public ICommand RemoveCameraCommandCommand { get; } = new RemoveCameraCommandCommand();
    }
}
