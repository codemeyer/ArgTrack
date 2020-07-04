using ArgData.Entities;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public static class ObjectShapeMapper
    {
        public static void MapTrackObjectShapeToViewModel(TrackObjectShape objectData, TrackObjectShapeViewModel viewModel)
        {
            // TODO: set id?

            viewModel.ShapeBuddy = objectData;

            viewModel.HeaderIndex = objectData.HeaderIndex;
            viewModel.DataIndex = objectData.DataIndex;

            viewModel.Offset1 = objectData.ScaleValueOffset;
            viewModel.Offset2 = objectData.Offset2;
            viewModel.Offset3 = objectData.PointDataOffset;
            viewModel.Offset4 = objectData.VectorDataOffset;
            viewModel.Offset5 = objectData.Offset5;
            //viewModel.OffsetData1 = objectData.OffsetData1;
            viewModel.OffsetData2 = objectData.OffsetData2;
            //viewModel.OffsetData3 = objectData.OffsetData3;
            //viewModel.OffsetData4 = objectData.OffsetData4;
            viewModel.OffsetData5 = objectData.OffsetData5;
            viewModel.HeaderValue1 = objectData.HeaderValue1;
            viewModel.HeaderValue2 = objectData.HeaderValue2;
            viewModel.HeaderValue3 = objectData.HeaderValue3;
            viewModel.HeaderValue4 = objectData.HeaderValue4;
            viewModel.HeaderValue5 = objectData.HeaderValue5;
            viewModel.HeaderValue6 = objectData.HeaderValue6;

            // foreach scalevalue
            foreach (var scaleValue in objectData.ScaleValues)
            {
                viewModel.ScaleValues.Add(scaleValue);
            }

            // foreach point
            foreach (var point in objectData.Points)
            {
                var pointVM = new TrackObjectShapePointViewModel(point, objectData);

                viewModel.Points.Add(pointVM);

                pointVM.Index = viewModel.Points.IndexOf(pointVM);
            }

            foreach (var vector in objectData.Vectors)
            {
                var vectorVM = new TrackObjectShapeVectorViewModel
                {
                    From = vector.From,
                    To = vector.To
                };

                viewModel.Vectors.Add(vectorVM);
            }

            viewModel.CalculateSignature();
        }

        public static void MapTrackObjectShapeViewModel(TrackObjectShapeViewModel source, TrackObjectShapeViewModel target)
        {
            // TODO: probably needs to be done "manually"

            target.Id = source.Id;
            target.HeaderIndex = source.HeaderIndex;
            target.DataIndex = source.DataIndex;

            target.ShapeBuddy = source.ShapeBuddy;

            target.Offset1 = source.Offset1;
            target.Offset2 = source.Offset2;
            target.Offset3 = source.Offset3;
            target.Offset4 = source.Offset4;
            target.Offset5 = source.Offset5;
            //target.OffsetData1 = source.OffsetData1;
            target.OffsetData2 = source.OffsetData2;
            //target.OffsetData3 = source.OffsetData3;
            //target.OffsetData4 = source.OffsetData4;
            target.OffsetData5 = source.OffsetData5;
            target.HeaderValue1 = source.HeaderValue1;
            target.HeaderValue2 = source.HeaderValue2;
            target.HeaderValue3 = source.HeaderValue3;
            target.HeaderValue4 = source.HeaderValue4;
            target.HeaderValue5 = source.HeaderValue5;
            target.HeaderValue6 = source.HeaderValue6;

            target.ScaleValues.Clear();

            foreach (var scaleValue in source.ScaleValues)
            {
                target.ScaleValues.Add(scaleValue);
            }

            target.Points.Clear();

            foreach (var sourcePoint in source.Points)
            {
                var targetPoint = new TrackObjectShapePointViewModel(sourcePoint.RelatedPoint, source.ShapeBuddy)
                {
                    Index = sourcePoint.Index,
                    PointType = sourcePoint.PointType,
                    PointIndex = sourcePoint.PointIndex,
                    //XPointIndex = sourcePoint.XPointIndex,
                    XScaleValueIndex = sourcePoint.XScaleValueIndex,
                    XIsNegative = sourcePoint.XIsNegative,
                    //YPointIndex = sourcePoint.YPointIndex,
                    YScaleValueIndex = sourcePoint.YScaleValueIndex,
                    YIsNegative = sourcePoint.YIsNegative,
                    Z = sourcePoint.Z
                };

                target.Points.Add(targetPoint);
            }

            target.Vectors.Clear();

            foreach (var sourceVector in source.Vectors)
            {
                var targetVector = new TrackObjectShapeVectorViewModel
                {
                    From = sourceVector.From,
                    To = sourceVector.To
                };

                target.Vectors.Add(targetVector);
            }

            target.Description = source.Description;
            target.Signature = source.Signature;
        }
    }
}
