using System.Windows.Media;

namespace ArgTrack.Features.TrackDrawing
{
    // TODO: name! more than just point - ShapeData ?

    public class PointData
    {
        public PointData()
        {
            Points = new PointCollection();
        }

        public PointData(PointCollection points)
        {
            Points = points;
        }

        public PointCollection Points { get; private set; }

        public Brush Stroke { get; set; }

        public double StrokeThickness { get; set; }

        public Brush Fill { get; set; }
    }
}
