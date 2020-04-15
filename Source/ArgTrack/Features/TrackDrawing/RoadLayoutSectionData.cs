using System.Windows;

namespace ArgTrack.Features.TrackDrawing
{
    public class RoadLayoutSectionData : LayoutSectionData
    {
        public RoadLayoutSectionData()
        {
        }

        public RoadLayoutSectionData(LayoutSectionData sectionData)
        {
            StartPosition = sectionData.StartPosition;
            EndPosition = sectionData.EndPosition;
            StartAngle = sectionData.StartAngle;
            EndAngle = sectionData.EndAngle;
            Length = sectionData.Length;
            Curvature = sectionData.Curvature;
            RelatedSection = sectionData.RelatedSection;
            Radius = sectionData.Radius;
            CircleCenterForCurve = sectionData.CircleCenterForCurve;
        }

        // coordinates for point where track width change ends
        /// <summary>
        /// Gets the coordinates at the center of the track where a track width change ends.
        /// </summary>
        public Point WidthChangeEndPosition { get; set; }

        /// <summary>
        /// when track width changes across segment border
        /// </summary>
        public int WidthChangeLength { get; set; }

        /// <summary>
        /// when track width changes across segment border
        /// </summary>
        public int WidthChangeEnd { get; set; }

        /// <summary>
        /// Gets the track width at the start of the section.
        /// </summary>
        public int StartWidth { get; set; }

        /// <summary>
        /// Gets the track width at the end of the section.
        /// </summary>
        public int EndWidth { get; set; }
    }
}
