using ArgTrack.ViewModels.Track;

namespace ArgTrack.Mappers
{
    public static class ViewModelCopyMapper
    {
        public static void CopyTrackHeaderViewModel(TrackHeaderViewModel source, TrackHeaderViewModel target)
        {
            PropertyCopier.Copy(source, target);
        }

        public static void CopyTrackObjectSettingsViewModel(TrackObjectSettingsViewModel source, TrackObjectSettingsViewModel target)
        {
            PropertyCopier.Copy(source, target);
        }
    }
}
