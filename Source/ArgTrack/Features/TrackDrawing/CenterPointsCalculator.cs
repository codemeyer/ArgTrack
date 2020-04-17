using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ArgData.Entities;
using ArgTrack.Features.Main;

namespace ArgTrack.Features.TrackDrawing
{
    public class CenterPointsCalculationResult
    {
        public CenterPointsCalculationResult()
        {
            TrackSections = new List<LayoutSectionData>();
            PitLaneSections = new List<LayoutSectionData>();
        }

        public List<LayoutSectionData> TrackSections { get; set; }

        public List<LayoutSectionData> PitLaneSections { get; set; }
    }


    // TODO: naming, since this Calculator is a bit different than the other Calculators...

    public static class CenterPointsCalculator
    {
        // return something that contains a list of "sections" with
        // - StartPosX
        // - StartPosY
        // - EndPosX
        // - EndPosY
        // - StartAngle
        // - EndAngle

        // this can then be used as the basis for all other calculations...

        public static CenterPointsCalculationResult CalculateCenterPointsPerSection(TrackViewModel track)
        {
            double dPosX = 0;
            double dPosY = 0;
            int startAngle = track.TrackHeader.FirstSectionAngle;

            var layoutSections = new List<LayoutSectionData>();

            foreach (var trackSection in track.TrackSections)
            {
                var layoutSection = CalculateLayoutCenterLine(dPosX, dPosY, startAngle, trackSection.Length, trackSection.Curvature);

                // start stuff for NEXT section
                dPosX = layoutSection.EndPosition.X;
                dPosY = layoutSection.EndPosition.Y;
                startAngle = layoutSection.EndAngle;

                layoutSection.RelatedSection = trackSection;

                layoutSections.Add(layoutSection);
            }


            // draw/calculate pitlane (move to separate method?)

            int pitLaneStartTrackSectionIndex = FindPitLaneConnectingSectionIndex(track);

            short sectionWidth = FindWidthOfPitLaneConnectingTrackSection(track, pitLaneStartTrackSectionIndex);

            var pitLaneSections = new List<LayoutSectionData>();

            if (pitLaneStartTrackSectionIndex >= 0)
            {
                var startSection = layoutSections[pitLaneStartTrackSectionIndex];

                startAngle = startSection.StartAngle;

                // calculate pitlane "shift" along section where pitlane starts
                double pitlaneShift = sectionWidth - LayoutConstants.PitLaneWidth;

                double pitLaneShiftX = Math.Cos(Math.PI - startSection.StartAngle * LayoutConstants.AngleScaleRadians) * pitlaneShift / LayoutConstants.TrackWidthUnitsPerLengthUnit;
                double pitLaneShiftY = Math.Sin(Math.PI - startSection.StartAngle * LayoutConstants.AngleScaleRadians) * pitlaneShift / LayoutConstants.TrackWidthUnitsPerLengthUnit;

                if (track.TrackSettings.PitsSide == TrackSide.Right)
                {
                    dPosX = startSection.StartPosition.X - pitLaneShiftX;
                    dPosY = startSection.StartPosition.Y + pitLaneShiftY;
                }
                else
                {
                    dPosX = startSection.StartPosition.X - pitLaneShiftX * -1;
                    dPosY = startSection.StartPosition.Y + pitLaneShiftY * -1;
                }

                foreach (var section in track.PitLaneSections)
                {
                    var pitLaneData = CalculateLayoutCenterLine(dPosX, dPosY, startAngle, section.Length, section.Curvature);

                    dPosX = pitLaneData.EndPosition.X;
                    dPosY = pitLaneData.EndPosition.Y;
                    startAngle = pitLaneData.EndAngle;

                    pitLaneData.RelatedSection = section;

                    pitLaneSections.Add(pitLaneData);
                }
            }


            return new CenterPointsCalculationResult
            {
                TrackSections = layoutSections,
                PitLaneSections = pitLaneSections
            };
        }

        private static int FindPitLaneConnectingSectionIndex(TrackViewModel track)
        {
            foreach (var section in track.TrackSections)
            {
                foreach (var cmd in section.Commands)
                {
                    if (cmd.Command == 0x86)  // TrackCommand for connecting pit lane
                    {
                        return track.TrackSections.IndexOf(section);
                    }
                }
            }

            return -1;
        }

        private static short FindWidthOfPitLaneConnectingTrackSection(TrackViewModel track, int pitLaneStartTrackSectionIndex)
        {
            short sectionWidth = 0;

            // find width of section containing pit lane
            for (int i = pitLaneStartTrackSectionIndex; i >= 0; i--)
            {
                var section = track.TrackSections[i];

                // has track width change?
                var cmd = section.Commands.SingleOrDefault(c => c.Command == 0x85); // TrackCommand for width change
                if (cmd != null)
                {
                    sectionWidth = cmd.Arguments[2].Value;  // width on each side of middle, in width scales = 0.0047625 meters
                }

                if (sectionWidth > 0)
                {
                    break;
                }
            }

            if (sectionWidth == 0)
            {
                // no change command found, use start width of track
                sectionWidth = track.TrackHeader.TrackStartWidth;
            }

            return sectionWidth;
        }

        public static CenterPointsCalculationResult CalculateCenterPointsPerSectionOneLengthEach(TrackViewModel track)
        {
            double dPosX = 0;
            double dPosY = 0;
            int startAngle = track.TrackHeader.FirstSectionAngle;

            var trackSections = new List<LayoutSectionData>();

            foreach (var section in track.TrackSections)
            {
                for (int i = 0; i <= section.Length - 1; i++)
                {
                    var layout = CalculateLayoutCenterLine(dPosX, dPosY, startAngle, 1, section.Curvature);

                    // start stuff for NEXT section
                    dPosX = layout.EndPosition.X;
                    dPosY = layout.EndPosition.Y;
                    startAngle = layout.EndAngle;

                    layout.RelatedSection = section;

                    trackSections.Add(layout);
                }
            }

            return new CenterPointsCalculationResult
            {
                TrackSections = trackSections
            };
        }

        /// <summary>
        /// Based on a starting position (X, Y), an angle, a length and a curvature, this method will return a
        /// LayoutSectionData object with the starting and ending values filled in.
        /// </summary>
        /// <param name="initialPositionX"></param>
        /// <param name="initialPositionY"></param>
        /// <param name="initialAngle"></param>
        /// <param name="length"></param>
        /// <param name="curvature"></param>
        /// <returns></returns>
        private static LayoutSectionData CalculateLayoutCenterLine(double initialPositionX, double initialPositionY, int initialAngle, int length, int curvature)
        {
            var layout = new LayoutSectionData();

            // set elsewhere?
            layout.Length = length;
            layout.Curvature = curvature;

            layout.StartPosition = new Point(initialPositionX, initialPositionY);
            layout.StartAngle = initialAngle;

            // resulting direction
            layout.EndAngle = layout.StartAngle - length * curvature;
            // TODO: this becomes NEGATIVE for East direction, which "feels" wrong...
            // TODO: that's why we use the GetLegacy* methods below...

            // end coordinates
            double angle = GeometryConversions.RadiansFromGameAngle(layout.StartAngle);

            if (curvature == 0)
            {
                // straight
                layout.EndPosition = Geometry.GetLegacyPoint(layout.StartPosition, angle, length);
            }
            else
            {
                // curve: first calculate center of circle
                layout.Radius = Geometry.GetRadiusForCurvature(curvature);

                layout.CircleCenterForCurve = Geometry.GetLegacyCircleCenter(layout.StartPosition, layout.Radius, angle);

                // from center, calculate end point
                double endAngle = GeometryConversions.RadiansFromGameAngle(layout.EndAngle);

                layout.EndPosition = Geometry.GetLegacyCurveEndpoint(layout.CircleCenterForCurve, layout.Radius, endAngle);
            }

            return layout;
        }
    }
}
