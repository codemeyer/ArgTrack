using ArgData.Entities;
using ArgTrack.Infrastructure.Mapping;

namespace ArgTrack.Features.TrackEditing.ObjectSettings
{
    public static class ObjectSettingsMapper
    {
        public static void CopyTrackObjectSettingsViewModel(TrackObjectSettingsViewModel source, TrackObjectSettingsViewModel target)
        {
            PropertyCopier.Copy(source, target);
        }

        public static void MapTrackObjectToViewModel(TrackObjectSettings objectData, TrackObjectSettingsViewModel viewModel)
        {
            viewModel.Id = objectData.Id;
            viewModel.DetailLevel = objectData.DetailLevel;
            viewModel.Unknown = objectData.Unknown;
            viewModel.AngleX = objectData.AngleX;
            viewModel.AngleY = objectData.AngleY;
            viewModel.DistanceFromTrack = objectData.DistanceFromTrack;
            viewModel.Id2 = objectData.Id2;
            viewModel.Unknown2 = objectData.Unknown2;
            viewModel.Height = objectData.Height;

            viewModel.Offset = objectData.Offset;
        }
    }
}
