using ArgData.Entities;
using ArgTrack.Infrastructure.Mapping;

namespace ArgTrack.Features.TrackEditing.RacingLine
{
    public static class RacingLineMapper
    {
        public static void MapTrackBestLineViewModel(BestLineSegmentViewModel source, BestLineSegmentViewModel target)
        {
            PropertyCopier.Copy(source, target);
        }

        public static void MapBestLineSegmentToViewModel(TrackComputerCarLineSegment section, BestLineSegmentViewModel viewModel)
        {
            viewModel.Length = (byte)section.Length;
            viewModel.Radius = section.Radius;
            viewModel.HighRadius = section.HighRadius;
            viewModel.LowRadius = section.LowRadius;
            viewModel.Correction = section.Correction;
            viewModel.SegmentType = section.SegmentType;
            viewModel.Type = section.SegmentType.ToString();
        }
    }
}
