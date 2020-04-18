namespace ArgTrack.Features.TrackDrawing
{
    public class TrackDrawingElementsSettings
    {
        public TrackDrawingElementsSettings()
        {
            DrawTrack = true;
            DrawPitLane = true;
            DrawVerges = false;
            DrawTrackSideObjects = false;

            DrawCircleCenterIndicators = false;
            DrawLineAlongTrackCenter = false;
            DrawLineBetweenSectionCenters = false;
        }

        public bool DrawTrack { get; set; }
        public bool DrawPitLane { get; set; }
        public bool DrawVerges { get; set; }
        public bool DrawTrackSideObjects { get; set; }
        public bool DrawCircleCenterIndicators { get; set; }
        public bool DrawLineBetweenSectionCenters { get; set; }
        public bool DrawLineAlongTrackCenter { get; set; }
    }
}
