using System.Windows;

namespace ArgTrack.Features.TrackDrawing
{
    /// <summary>
    /// Class that represents a piece of road, either a track section or a pit lane section.
    /// </summary>
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

        /// <summary>
        /// Gets the track width at the start of the section.
        /// </summary>
        public int StartWidth { get; set; }

        /// <summary>
        /// Gets the track width at the end of the section.
        /// </summary>
        public int EndWidth { get; set; }

        /// <summary>
        /// Gets or sets the coordinates at the center of the track where a track width change ends.
        /// </summary>
        public Point WidthChangeEndPosition { get; set; }

        /// <summary>
        /// When track width changes across segment border
        /// </summary>
        public int WidthChangeLength { get; set; }

        /// <summary>
        /// When track width changes across segment border
        /// </summary>
        public int WidthChangeEnd { get; set; }
    }
}
