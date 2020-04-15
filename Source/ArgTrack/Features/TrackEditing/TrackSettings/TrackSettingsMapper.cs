using ArgTrack.Infrastructure.Mapping;

namespace ArgTrack.Features.TrackEditing.TrackSettings
{
    public static class TrackSettingsMapper
    {
        public static void MapTrackSettingsToViewModel(ArgData.Entities.TrackSettings settings, TrackSettingsViewModel viewModel)
        {
            viewModel.UnknownTrackDistance = settings.UnknownTrackDistance;
            viewModel.DefaultPitLaneViewDistance = settings.DefaultPitLaneViewDistance;
            viewModel.KerbType = settings.KerbType;
            viewModel.KerbUpperColor = settings.KerbUpperColor;
            viewModel.KerbLowerColor = settings.KerbLowerColor;
            viewModel.KerbUpperColor2 = settings.KerbUpperColor2;
            viewModel.KerbLowerColor2 = settings.KerbLowerColor2;
            viewModel.PitsSide = settings.PitsSide;
            viewModel.PoleSide = settings.PoleSide;
            viewModel.SurroundingArea = settings.SurroundingArea;
            viewModel.LapCount = settings.LapCount;
            viewModel.LapTimeIndication = settings.LapTimeIndication;
            viewModel.TimeFactorNonRace = settings.TimeFactorNonRace;
            viewModel.TimeFactorRace = settings.TimeFactorRace;
        }

        public static void MapTrackSettingsViewModel(TrackSettingsViewModel source, TrackSettingsViewModel target)
        {
            PropertyCopier.Copy(source, target);
        }
    }
}
