using System.Collections.ObjectModel;

namespace ArgTrack.Features.TrackDrawing
{
    public static class TrackShapeDataObservableCollectionExtensions
    {
        public static void AddRange(this ObservableCollection<TrackShapeData> original, ObservableCollection<TrackShapeData> collection)
        {
            foreach (var point in collection)
            {
                original.Add(point);
            }
        }
    }
}
