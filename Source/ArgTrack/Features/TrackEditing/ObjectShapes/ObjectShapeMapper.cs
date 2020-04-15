using ArgData.Entities;
using ArgTrack.Infrastructure.Mapping;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public static class ObjectShapeMapper
    {
        public static void MapTrackObjectShapeToViewModel(TrackObjectShape objectData, TrackObjectShapeViewModel viewModel)
        {
            // use CopyProperties?
            viewModel.HeaderIndex = objectData.HeaderIndex;
            viewModel.DataIndex = objectData.DataIndex;

            viewModel.Offset1 = objectData.Offset1;
            viewModel.Offset2 = objectData.Offset2;
            viewModel.Offset3 = objectData.Offset3;
            viewModel.Offset4 = objectData.Offset4;
            viewModel.Offset5 = objectData.Offset5;
            viewModel.OffsetData1 = objectData.OffsetData1;
            viewModel.OffsetData2 = objectData.OffsetData2;
            viewModel.OffsetData3 = objectData.OffsetData3;
            viewModel.OffsetData4 = objectData.OffsetData4;
            viewModel.OffsetData5 = objectData.OffsetData5;
            viewModel.HeaderValue1 = objectData.HeaderValue1;
            viewModel.HeaderValue2 = objectData.HeaderValue2;
            viewModel.HeaderValue3 = objectData.HeaderValue3;
            viewModel.HeaderValue4 = objectData.HeaderValue4;
            viewModel.HeaderValue5 = objectData.HeaderValue5;
            viewModel.HeaderValue6 = objectData.HeaderValue6;
        }

        public static void MapTrackObjectShapeViewModel(TrackObjectShapeViewModel source, TrackObjectShapeViewModel target)
        {
            PropertyCopier.Copy(source, target);
        }
    }
}
