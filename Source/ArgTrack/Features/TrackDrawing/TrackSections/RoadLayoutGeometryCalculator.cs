using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ArgTrack.Features.TrackDrawing
{
    /// <summary>
    /// Turns basic LayoutSectionData into track-specific RoadLayoutSectionData.
    /// </summary>
    public static class RoadLayoutGeometryCalculator
    {
        public static List<RoadLayoutSectionData> CalculateRoadSections(List<LayoutSectionData> layoutSections, short trackStartWidth)
        {
            int nWidthLength = 0;
            int nWidthEnd = 0;
            int startWidth = trackStartWidth;

            var roadSections = new List<RoadLayoutSectionData>();

            foreach (var section in layoutSections)
            {
                var layout = CalculateLayout(section, startWidth, nWidthLength, nWidthEnd);

                // start stuff for NEXT section
                startWidth = layout.EndWidth;
                nWidthLength = layout.WidthChangeLength;
                nWidthEnd = layout.WidthChangeEnd;

                roadSections.Add(layout);
            }

            return roadSections;
        }

        /// <summary>
        /// Calculates the full track (incl. width) based on the center point LayoutSectionData provided.
        /// </summary>
        /// <param name="layoutData"></param>
        /// <param name="initialWidth"></param>
        /// <param name="widthChangeLength"></param>
        /// <param name="widthChangeEnd"></param>
        /// <returns></returns>
        private static RoadLayoutSectionData CalculateLayout(LayoutSectionData layoutData, int initialWidth, int widthChangeLength, int widthChangeEnd)
        {
            var road = new RoadLayoutSectionData(layoutData)
            {
                StartWidth = initialWidth
            };

            // TODO: from outside?
            // handle multiple 0x85 commands (only use the first one)
            var cmd = layoutData.RelatedSection.Commands.FirstOrDefault(c => c.Command == 0x85); // TrackCommand for width change

            if (cmd != null)
            {
                widthChangeLength = cmd.Arguments[1].Value;
                widthChangeEnd = cmd.Arguments[2].Value;  // width on each side of middle
            }

            // track width change detected
            if (widthChangeLength > 0)
            {
                if (widthChangeLength > road.Length)
                {
                    // width change to be continued on next segment
                    // calculate width at end of segment
                    road.EndWidth = road.StartWidth + road.Length * (widthChangeEnd - road.StartWidth) / widthChangeLength;
                    // store values needed by next segment
                    road.WidthChangeEnd = widthChangeEnd;
                    road.WidthChangeLength = widthChangeLength - road.Length;
                    // need flag to indicate width change ends in middle of segment? @@@
                }
                else
                {
                    // whole change takes place on this segment
                    if (widthChangeLength < road.Length)
                    {
                        // change complete in the middle of the segment.
                        // calculate point where the change ends.
                        if (road.Curvature != 0)
                        {
                            // curved segment
                            double dAngleMid = road.StartAngle + widthChangeLength * road.Curvature;
                            double widthChangeX = road.CircleCenterForCurve.X - road.Radius * Math.Cos(dAngleMid * LayoutConstants.AngleScaleRadians);
                            double widthChangeY = road.CircleCenterForCurve.Y + road.Radius * Math.Sin(dAngleMid * LayoutConstants.AngleScaleRadians);
                            road.WidthChangeEndPosition = new Point(widthChangeX, widthChangeY);
                        }
                        else
                        {
                            // straight
                            double widthChangeX = road.StartPosition.X - widthChangeLength * Math.Sin(road.StartAngle * LayoutConstants.AngleScaleRadians);
                            double widthChangeY = road.StartPosition.Y + widthChangeLength * Math.Cos(road.StartAngle * LayoutConstants.AngleScaleRadians);
                            road.WidthChangeEndPosition = new Point(widthChangeX, widthChangeY);
                        }
                    }

                    road.EndWidth = widthChangeEnd;
                    // nothing to do on next segment
                    road.WidthChangeLength = 0;
                    road.WidthChangeEnd = 0;
                }
            }
            else
            {
                // no width change
                road.EndWidth = road.StartWidth;
                // nothing to do on next segment
                road.WidthChangeLength = 0;
                road.WidthChangeEnd = 0;
            }

            return road;
        }
    }
}
