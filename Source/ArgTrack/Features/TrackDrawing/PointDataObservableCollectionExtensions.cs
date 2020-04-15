using System.Collections.ObjectModel;

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
}
