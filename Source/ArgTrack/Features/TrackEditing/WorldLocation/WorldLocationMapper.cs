using ArgData.Entities;
using ArgTrack.Infrastructure.Mapping;

namespace ArgTrack.Features.TrackEditing.WorldLocation
{
    public static class WorldLocationMapper
    {
        public static void CopyTrackHeaderViewModel(TrackHeaderViewModel source, TrackHeaderViewModel target)
        {
            PropertyCopier.Copy(source, target);
        }

        public static void MapHeaderToViewModel(TrackSectionHeader headerData, TrackHeaderViewModel viewModel)
        {
            viewModel.FirstSectionAngle = headerData.FirstSectionAngle;
            viewModel.FirstSectionHeight = headerData.FirstSectionHeight;
            viewModel.TrackCenterX = headerData.TrackCenterX;
            viewModel.TrackCenterY = headerData.TrackCenterY;
            viewModel.TrackCenterZ = headerData.TrackCenterZ;
            viewModel.TrackStartWidth = headerData.StartWidth;
            viewModel.LeftVergeStartWidth = headerData.LeftVergeStartWidth;
            viewModel.RightVergeStartWidth = headerData.RightVergeStartWidth;
        }
    }
}
