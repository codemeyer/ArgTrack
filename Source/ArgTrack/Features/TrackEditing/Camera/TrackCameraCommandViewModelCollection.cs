using System.Collections.ObjectModel;
using ArgTrack.Features.Main;

namespace ArgTrack.Features.TrackEditing.Camera
{
    public class TrackCameraCommandViewModelCollection : ObservableCollection<CameraCommandEditingViewModel>
    {
        private readonly TrackViewModel _trackViewModel;

        public TrackCameraCommandViewModelCollection(TrackViewModel trackViewModel)
        {
            _trackViewModel = trackViewModel;
        }
    }
}
