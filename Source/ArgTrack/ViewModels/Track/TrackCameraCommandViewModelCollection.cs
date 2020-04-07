using System.Collections.ObjectModel;

namespace ArgTrack.ViewModels.Track
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
