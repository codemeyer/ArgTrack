using System;
using System.Collections.Generic;
using System.Windows;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.TrackDrawing
{
    // TODO: extend this class track section drawing (startwidth, endwidth, etc)
    // reuse for verges? but then RelatedSection doesn't match up, since there can be several when the walls are bridged...

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


    // TODO:
    public class VergeLayoutSectionData : RoadLayoutSectionData // TODO: should inherit from RoadLayoutSectionData
    {
        public VergeLayoutSectionData()
        {
            RelatedSections = new List<TrackSectionViewModel>();
        }

        public VergeLayoutSectionData(RoadLayoutSectionData sectionData)
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
            WidthChangeEnd = sectionData.WidthChangeEnd;
            WidthChangeEndPosition = sectionData.WidthChangeEndPosition;
            WidthChangeLength = sectionData.WidthChangeLength;
            StartWidth = sectionData.StartWidth;
            EndWidth = sectionData.EndWidth;
            RelatedSections = new List<TrackSectionViewModel>();
        }

        public List<TrackSectionViewModel> RelatedSections { get; set; }



        // TODO: more to do here re: verges... the Raw values below should come from the section
        // but remember to support bridged/joined sections/verges

        // raw = verge width value from section(?)

        public byte LeftVergeStartWidthRaw { get; set; }
        public byte LeftVergeEndWidthRaw { get; set; }
        public byte RightVergeStartWidthRaw { get; set; }
        public byte RightVergeEndWidthRaw { get; set; }



        public void SetVergeStartWidthRaw(byte value, VergeLayoutCalculator.VergeSide side)
        {
            if (side == VergeLayoutCalculator.VergeSide.Left)
            {
                LeftVergeStartWidthRaw = value;
            }
            else
            {
                RightVergeStartWidthRaw = value;
            }
        }


        // in track-width units

        /// <summary>
        /// Gets the full width of the left verge (incl. track) at the start of the verge section.
        /// </summary>
        public int LeftVergeStartWidth
        {
            get
            {
                return GetVergeWidth(LeftVergeStartWidthRaw, StartWidth);
            }
        }

        // end = verge, start = end-of-previous

        /// <summary>
        /// Gets the full width of the left verge (incl. track) at the end of the verge section.
        /// </summary>
        public int LeftVergeEndWidth
        {
            get
            {
                return GetVergeWidth(LeftVergeEndWidthRaw, EndWidth);
            }
        }

        /// <summary>
        /// Gets the full width of the right verge (incl. track) at the start of the verge section.
        /// </summary>
        public int RightVergeStartWidth
        {
            get
            {
                return GetVergeWidth(RightVergeStartWidthRaw, StartWidth);
            }
        }

        /// <summary>
        /// Gets the full width of the right verge (incl. track) at the end of the verge section.
        /// </summary>
        public int RightVergeEndWidth
        {
            get
            {
                return GetVergeWidth(RightVergeEndWidthRaw, EndWidth);
            }
        }

        private int GetVergeWidth(int vergeRaw, int trackWidth)
        {
            // noo, 50=same width as FULL track, i.e. twice trackWidth?

            // huh?
            //float pct = vergeRaw * 2 / 100;
            //double pct = vergeRaw * 0.02;
            double pct = vergeRaw * 0.04; //?
            var vergeWidth = Convert.ToInt32(trackWidth * pct) + trackWidth;
            return vergeWidth;
        }

    }
}
