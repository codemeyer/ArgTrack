using System;
using System.Collections.Generic;
using System.Windows;
using ArgTrack.Features.TrackEditing.TrackSection;

namespace ArgTrack.Features.TrackDrawing
{
    public class VergeLayoutSectionData : RoadLayoutSectionData
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



        // TODO: calculate Point properties for OuterStart, OuterEnd ?

        public Point OuterStartPosition { get; set; }
        public Point OuterEndPosition { get; set; }


        public bool DrawStraight { get; set; }
        public bool DrawArc { get; set; }



        // TODO: more to do here re: verges... the Raw values below should come from the section
        // but remember to support bridged/joined sections/verges

        // raw = verge width value from section(?)

        public byte LeftVergeStartWidthRaw { get; set; }
        public byte LeftVergeEndWidthRaw { get; set; }
        public byte RightVergeStartWidthRaw { get; set; }
        public byte RightVergeEndWidthRaw { get; set; }



        public void SetVergeStartWidthRaw(byte value, VergeSide side)
        {
            if (side == VergeSide.Left)
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

        /// <summary>
        /// Gets the total width of the verge AND track from the center line.
        /// </summary>
        /// <param name="vergeRaw">Verge width value as specified in the section</param>
        /// <param name="trackWidth">Track width of section</param>
        /// <returns></returns>
        private int GetVergeWidth(int vergeRaw, int trackWidth)
        {
            // original "guesstimate" formula
            //double pct = vergeRaw * 0.04;
            //var vergeWidth = Convert.ToInt32(trackWidth * pct) + trackWidth;

            //return vergeWidth;

            var widthMeters = GetFenceWidthMeters(Convert.ToDouble(vergeRaw), trackWidth);
            var reverseInWidthUnits = widthMeters / LayoutConstants.MetersPerTrackWidthUnit;
            var totalBasedOnMeters = trackWidth + reverseInWidthUnits;

            return Convert.ToInt32(totalBasedOnMeters);
        }

        private double GetFenceWidthMeters(double fenceWidth, int trackWidth)
        {
            double divisor = 32768.0 / 30.0;  //track, pitlane is "/ 20"

            double fenceWidthMeters = fenceWidth * (1024.0 / 30.0) * trackWidth / 1000.0 / divisor * 4.87;

            return fenceWidthMeters;
        }

        // from http://www.grandprix2.de/Anleitung/tutus/Command_Lib/command_lib%20gp3%20gp2/cmdlib3a.htm
        // Formulas of Robin de Paus for track- and verge width

        // "I have done some research about how to convert GP2-track width and fence width to meters. I have come to the following formulas:
        // divisor is a variable:
        // for pitlane values, divisor = 32768 / 20 (=1638.4)
        // for track values, divisor = 32768 / 30 (=1092.266666)

        // TrackWidth(Meters)= (TrackWidthGP2 / divisor) * 4.87;

        // FenceWidth(Meters)=((FenceWidthGP2*(1024/30))*TrackWidthGP2/1000)/divisor*4.87
        //  FenceWidth(Meters) = (FenceWidth  *  34.1333  *  TrackWidth / 1000) / divisor * 4.87

        //I have tested these formulas and they are highly accurate."
    }
}
