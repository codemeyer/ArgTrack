using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ArgTrack.Features.TrackDrawing
{
    public static class PointsToShapesConverter
    {
        public static ObservableCollection<TrackShapeData> Convert(PointCollection points, DrawingLayout drawingLayout)
        {
            return Convert(points, drawingLayout.StrokeBrush, drawingLayout.StrokeThickness, drawingLayout.FillBrush);
        }

        public static ObservableCollection<TrackShapeData> Convert(PointCollection points, Brush strokeBrush, double strokeThickness, Brush fillBrush)
        {
            var shapeDataList = new ObservableCollection<TrackShapeData>();

            var data = new TrackShapeData(points)
            {
                StrokeThickness = strokeThickness,
                Stroke = strokeBrush,
                Fill = fillBrush
            };


            shapeDataList.Add(data);

            return shapeDataList;
        }
    }
}
