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
            var t2 = new RoadLayoutSectionData(layoutData)
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
                if (widthChangeLength > t2.Length)
                {
                    // width change to be continued on next segment
                    // calculate width at end of segment
                    t2.EndWidth = t2.StartWidth + t2.Length * (widthChangeEnd - t2.StartWidth) / widthChangeLength;
                    // store values needed by next segment
                    t2.WidthChangeEnd = widthChangeEnd;
                    t2.WidthChangeLength = widthChangeLength - t2.Length;
                    // need flag to indicate width change ends in middle of segment? @@@
                }
                else
                {
                    // whole change takes place on this segment
                    if (widthChangeLength < t2.Length)
                    {
                        // change complete in the middle of the segment.
                        // calculate point where the change ends.
                        if (t2.Curvature != 0)
                        {
                            // curved segment
                            double dAngleMid = t2.StartAngle + widthChangeLength * t2.Curvature;
                            double widthChangeX = t2.CircleCenterForCurve.X - t2.Radius * Math.Cos(dAngleMid * LayoutConstants.AngleScaleRadians);
                            double widthChangeY = t2.CircleCenterForCurve.Y + t2.Radius * Math.Sin(dAngleMid * LayoutConstants.AngleScaleRadians);
                            t2.WidthChangeEndPosition = new Point(widthChangeX, widthChangeY);
                        }
                        else
                        {
                            // straight
                            double widthChangeX = t2.StartPosition.X - widthChangeLength * Math.Sin(t2.StartAngle * LayoutConstants.AngleScaleRadians);
                            double widthChangeY = t2.StartPosition.Y + widthChangeLength * Math.Cos(t2.StartAngle * LayoutConstants.AngleScaleRadians);
                            t2.WidthChangeEndPosition = new Point(widthChangeX, widthChangeY);
                        }
                    }

                    t2.EndWidth = widthChangeEnd;
                    // nothing to do on next segment
                    t2.WidthChangeLength = 0;
                    t2.WidthChangeEnd = 0;
                }
            }
            else
            {
                // no width change
                t2.EndWidth = t2.StartWidth;
                // nothing to do on next segment
                t2.WidthChangeLength = 0;
                t2.WidthChangeEnd = 0;
            }

            return t2;
        }

    }
}
