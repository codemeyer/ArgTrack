using System.Collections.ObjectModel;

namespace ArgTrack.ViewModels.Track
{
    public class TrackCameraCommandViewModelCollection : ObservableCollection<TrackCameraCommandViewModel>
    {
        private readonly TrackViewModel _trackViewModel;

        public TrackCameraCommandViewModelCollection(TrackViewModel trackViewModel)
        {
            _trackViewModel = trackViewModel;
        }
    }
}
