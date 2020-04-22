using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace ArgTrack.Features.TrackDrawing
{
    public static class PointCollectionExtensions
    {
        public static void AddRange(this PointCollection original, PointCollection collection)
        {
            foreach (var point in collection)
            {
                original.Add(point);
            }
        }

        public static void AddRange(this PointCollection original, IEnumerable<Point> collection)
        {
            foreach (var point in collection)
            {
                original.Add(point);
            }
        }
    }
}
