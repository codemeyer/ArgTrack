using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ArgData.Entities;
using ArgTrack.Features.Main;
using ArgTrack.Features.TrackEditing.TrackSection;

namespace ArgTrack.Features.TrackDrawing
{
    /// <summary>
    /// Calculates the center points where each track section starts/stops. Used as the basis for all other calculations.
    /// </summary>
    public static class CenterPointsGeometryCalculator
    {
        public static CenterPointsCalculationResult CalculateCenterPointsPerSection(TrackViewModel track)
        {
            var layoutSections = GetTrackLayoutSections(track);
            var pitLaneSections = GetPitLane(track, layoutSections, false);

            return new CenterPointsCalculationResult
            {
                TrackSections = layoutSections,
                PitLaneSections = pitLaneSections
            };
        }

        // TODO: overload that takes TrackViewModel, current list, starting point
        // and recalculates all sections including and after the starting point

        public static List<LayoutSectionData> GetTrackLayoutSections(TrackViewModel track)
        {
            double startX = CalculateStartX(track.TrackHeader.TrackCenterX);
            double startY = CalculateStartX(track.TrackHeader.TrackCenterY);

            return LayoutSectionDataFromTrackSectionList(track.TrackSections, track.TrackHeader.FirstSectionAngle, startX, startY, true);
        }

        /// <summary>
        /// Creates one LayoutSection per section length.
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        public static CenterPointsCalculationResult CalculateCenterPointsPerSectionOneLengthEach(TrackViewModel track)
        {
            var trackSections = new List<LayoutSectionData>();

            double nextStartX = CalculateStartX(track.TrackHeader.TrackCenterX);
            double nextStartY = CalculateStartY(track.TrackHeader.TrackCenterY);
            int startAngle = track.TrackHeader.FirstSectionAngle;

            foreach (var section in track.TrackSections)
            {
                for (int i = 0; i <= section.Length - 1; i++)
                {
                    var layout = CalculateLayoutCenterLine(nextStartX, nextStartY, startAngle, 1, section.Curvature);

                    // end data of THIS section will be the initial data for the NEXT section
                    nextStartX = layout.EndPosition.X;
                    nextStartY = layout.EndPosition.Y;
                    startAngle = layout.EndAngle;

                    layout.RelatedSection = section;

                    trackSections.Add(layout);
                }
            }

            // TODO: lots in GetPitLane is similar to the stuff above, can probably be "merged"
            var pits = GetPitLane(track, trackSections, true);

            return new CenterPointsCalculationResult
            {
                TrackSections = trackSections,
                PitLaneSections = pits
            };
        }

        private static double CalculateStartX(short originalStartX)
        {
            return originalStartX << 3;
        }

        private static double CalculateStartY(short originalStartY)
        {
            return originalStartY << 3;
        }


        private static List<LayoutSectionData> GetPitLane(TrackViewModel track, List<LayoutSectionData> layoutSections, bool oneSectionPerTlu)
        {
            int pitLaneStartTrackSectionIndex = FindPitLaneConnectingSectionIndex(layoutSections);

            if (pitLaneStartTrackSectionIndex == 0)
            {
                return new List<LayoutSectionData>();
            }

            short sectionWidth = FindWidthOfPitLaneConnectingTrackSection(layoutSections, track, pitLaneStartTrackSectionIndex);

            var startSection = layoutSections[pitLaneStartTrackSectionIndex];

            int startAngle = startSection.StartAngle;

            var pitLaneStartPosition = GetPitLaneStartCoords(sectionWidth, startSection, track.TrackSettings.PitsSide);

            return LayoutSectionDataFromTrackSectionList(track.PitLaneSections, startAngle, pitLaneStartPosition.X, pitLaneStartPosition.Y, !oneSectionPerTlu);
        }

        private static List<LayoutSectionData> LayoutSectionDataFromTrackSectionList(TrackSectionViewModelCollection sections, int startAngle, double startX, double startY, bool lengthFromSection)
        {
            var layoutSections = new List<LayoutSectionData>();

            double nextStartX = startX;
            double nextStartY = startY;

            foreach (var trackSection in sections)
            {
                int length = lengthFromSection ? trackSection.Length : 1;
                int toIterate = !lengthFromSection ? trackSection.Length : 1;

                for (int i = 0; i < toIterate; i++)
                {
                    var layoutSection = CalculateLayoutCenterLine(nextStartX, nextStartY, startAngle, length, trackSection.Curvature);

                    // end data of THIS section will be the initial data for the NEXT section
                    nextStartX = layoutSection.EndPosition.X;
                    nextStartY = layoutSection.EndPosition.Y;
                    startAngle = layoutSection.EndAngle;

                    layoutSection.RelatedSection = trackSection;

                    layoutSections.Add(layoutSection);
                }
            }

            return layoutSections;
        }

        private static short FindWidthOfPitLaneConnectingTrackSection(List<LayoutSectionData> layoutSections, TrackViewModel track, int pitLaneStartTrackSectionIndex)
        {
            short sectionWidth = 0;

            // find width of section containing pit lane
            for (int i = pitLaneStartTrackSectionIndex; i >= 0; i--)
            {
                var section = layoutSections[i].RelatedSection; //track.TrackSections[i];

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

        private static Point GetPitLaneStartCoords(int sectionWidth, LayoutSectionData startSection, TrackSide pitsSide)
        {
            double pitlaneShift = sectionWidth - LayoutConstants.PitLaneWidth;

            double pitLaneShiftX = Math.Cos(Math.PI - startSection.StartAngle * LayoutConstants.AngleScaleRadians) * pitlaneShift / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double pitLaneShiftY = Math.Sin(Math.PI - startSection.StartAngle * LayoutConstants.AngleScaleRadians) * pitlaneShift / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            double dPosX;
            double dPosY;

            if (pitsSide == TrackSide.Right)
            {
                dPosX = startSection.StartPosition.X - pitLaneShiftX;
                dPosY = startSection.StartPosition.Y + pitLaneShiftY;
            }
            else
            {
                dPosX = startSection.StartPosition.X - pitLaneShiftX * -1;
                dPosY = startSection.StartPosition.Y + pitLaneShiftY * -1;
            }

            return new Point(dPosX, dPosY);
        }

        private static int FindPitLaneConnectingSectionIndex(List<LayoutSectionData> layoutSections)
        {
            foreach (var section in layoutSections)
            {
                foreach (var cmd in section.RelatedSection.Commands)
                {
                    if (cmd.Command == 0x86)  // TrackCommand for connecting pit lane
                    {
                        return layoutSections.IndexOf(section);
                    }
                }
            }

            return -1;
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
