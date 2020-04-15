using System.Windows;
using ArgTrack.Features.TrackEditing.TrackSection;

namespace ArgTrack.Features.TrackDrawing
{
    /// <summary>
    /// something that contains a list of "sections" with
    /// - StartPosX
    /// - StartPosY
    /// - EndPosX
    /// - EndPosY
    /// - StartAngle
    /// - EndAngle
    /// </summary>
    public class LayoutSectionData
    {
        /// <summary>
        /// Gets or sets the start position of the section.
        /// </summary>
        public Point StartPosition { get; set; }

        /// <summary>
        /// Gets or sets the end position of the section.
        ///
        /// In a sequence of sections, this will be the same as the StartPosition of the next section.
        /// </summary>
        public Point EndPosition { get; set; }

        /// <summary>
        /// Gets or sets the start angle of the section.
        /// </summary>
        public int StartAngle { get; set; }

        /// <summary>
        /// Gets or sets the end angle of the section.
        /// </summary>
        public int EndAngle { get; set; }

        public bool IsCurved => StartAngle != EndAngle;

        public bool IsStraight => StartAngle == EndAngle;

        /// <summary>
        /// Gets or sets the length of the section.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the curvature of the section (radius of circle that defines it).
        /// </summary>
        public int Curvature { get; set; }

        // to handle verges, maybe have several RelatedSections? but usually just one

        public TrackSectionViewModel RelatedSection { get; set; }

        /// <summary>
        /// Gets (or sets) the calculated radius value.
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Gets (or sets) the circle center of the curve.
        /// </summary>
        public Point CircleCenterForCurve { get; set; }
    }
}
