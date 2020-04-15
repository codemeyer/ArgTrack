namespace ArgTrack.Features.TrackDrawing
{
    public class TrackDrawingElementsSettings
    {
        public TrackDrawingElementsSettings()
        {
            DrawTrack = true;
            DrawPitLane = true;
            DrawVerges = false;
        }

        public bool DrawTrack { get; set; }
        public bool DrawPitLane { get; set; }
        public bool DrawVerges { get; set; }
    }
}
