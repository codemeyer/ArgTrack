using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ArgTrack.Features.TrackDrawing
{
    // TODO: name... this converts PointCollection to "styled" data

    public static class LayoutToPointsConverter
    {
        public static ObservableCollection<PointData> Convert(PointCollection points, DrawingLayout drawingLayout)
        {
            return Convert(points, drawingLayout.StrokeBrush, drawingLayout.StrokeThickness, drawingLayout.FillBrush);
        }

        public static ObservableCollection<PointData> Convert(PointCollection points, Brush strokeBrush, double strokeThickness, Brush fillBrush)
        {
            var pointDataList = new ObservableCollection<PointData>();

            var data = new PointData(points)
            {
                StrokeThickness = strokeThickness,
                Stroke = strokeBrush,
                Fill = fillBrush
            };


            pointDataList.Add(data);

            return pointDataList;
        }
    }
}
