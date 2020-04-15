using System;
using System.Collections.Generic;
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="vergeRaw">Verge width value as specified in the section</param>
        /// <param name="trackWidth">Track width of section</param>
        /// <returns></returns>
        private int GetVergeWidth(int vergeRaw, int trackWidth)
        {
            // TODO: is this correct, or should we use the formula below?

            // noo, 50=same width as FULL track, i.e. twice trackWidth?

            // huh?
            //float pct = vergeRaw * 2 / 100;
            //double pct = vergeRaw * 0.02;
            double pct = vergeRaw * 0.04; //?
            var vergeWidth = Convert.ToInt32(trackWidth * pct) + trackWidth;
            return vergeWidth;
        }

        // from http://www.grandprix2.de/Anleitung/tutus/Command_Lib/command_lib%20gp3%20gp2/cmdlib3a.htm
        // Formulas of Robin de Paus for track- and verge width
        // "I have done some research about how to convert GP2-track width and fence width to meters. I have come to the following formulas:
        // Deler is a variable:
        // for pitlane values, deler = 32768 / 20
        // for track values, deler = 32768 / 30

        // TrackWidth(Meters)= (TrackWidthGP2 / deler) * 4.87;

        // FenceWidth(Meters)=((FenceWidthGP2*(1024/30))*TrackWidthGP2/1000)/deler*4.87

        //I have tested these formulas and they are highly accurate."
    }
}
