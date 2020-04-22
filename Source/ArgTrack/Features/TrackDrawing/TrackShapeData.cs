using System.Windows.Media;

namespace ArgTrack.Features.TrackDrawing
{
    /// <summary>
    /// Represents a "shape" when it comes to track drawing, e.g. a single track section,
    /// a single verge section or a diamond square representing a track-side object.
    /// </summary>
    public class TrackShapeData
    {
        public TrackShapeData()
        {
            Points = new PointCollection();
        }

        public TrackShapeData(PointCollection points)
        {
            Points = points;
        }

        /// <summary>
        /// Gets or sets the points the define the polygon.
        /// </summary>
        public PointCollection Points { get; private set; }

        /// <summary>
        /// Gets or sets the brush that is used to paint the line around the polygon.
        /// </summary>
        public Brush Stroke { get; set; }

        /// <summary>
        /// Gets or sets the thickness of the line around the polygon.
        /// </summary>
        public double StrokeThickness { get; set; }

        /// <summary>
        /// Gets or sets the brush that is used to fill the polygon area.
        /// </summary>
        public Brush Fill { get; set; }
    }
}
