using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace ArgTrack.Features.TrackDrawing
{
    public static class PointDataObservableCollectionExtensions
    {
        public static void AddRange(this ObservableCollection<PointData> original, ObservableCollection<PointData> collection)
        {
            foreach (var point in collection)
            {
                original.Add(point);
            }
        }
    }

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
