using System.Windows.Media;
using ArgTrack.Features.TrackEditing.ObjectSettings;

namespace ArgTrack.Features.TrackDrawing
{
    public class TrackSideObjectDrawingLayout
    {
        public int SectionIndex { get; set; }

        public double DistanceFromCenter { get; set; }

        public TrackObjectSettingsViewModel ObjectSetting { get; set; }

        public PointCollection Points { get; set; }

        public DrawingLayout GetDrawingLayout()
        {
            if (ObjectSetting.Id == 5)
            {
                // bush/tree
                return DrawingLayout.ForTrackSideObjectsId5();
            }

            return DrawingLayout.ForTrackSideObjects();
        }
    }
}
