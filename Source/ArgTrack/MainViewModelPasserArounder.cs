using ArgTrack.ViewModels;
using ArgTrack.ViewModels.Track;

namespace ArgTrack
{
    // TODO: try to use these as little as possible...

    public static class MainViewModelPasserArounder
    {
        public static MainViewModel MainViewModel { get; set; }
    }

    public static class TrackViewModelPasserArounder
    {
        public static TrackViewModel TrackViewModel { get; set; }
    }
}
