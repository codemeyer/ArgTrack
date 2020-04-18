using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ArgTrack.Features.TrackDrawing
{
    // TODO: split into to two classes,
    // one that turns List<LayoutSectionData> into List<RoadLayoutSectionData>
    // one that turns List<RoadLayoutSectionData> into ObservableCollection<PointData> ?

    public static class RoadLayoutCalculator
    {
        /// <summary>
        /// Calculates all points for either track or pit lane.
        /// </summary>
        public static ObservableCollection<PointData> Calculate(List<LayoutSectionData> layoutSections, short trackStartWidth, DrawingLayout drawingLayout)
        {
            var roadLayoutSections = CalculateRoadSections(layoutSections, trackStartWidth);

            // adds "styling" to the points...
            var pointDataList = ConvertRoadLayoutToPointDataList(roadLayoutSections, drawingLayout);

            return pointDataList;
        }

        public static ObservableCollection<PointData> Calculate(List<RoadLayoutSectionData> roadLayoutSections, DrawingLayout drawingLayout)
        {
            // adds "styling" to the points...
            var pointDataList = ConvertRoadLayoutToPointDataList(roadLayoutSections, drawingLayout);

            return pointDataList;
        }


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
            var cmd = layoutData.RelatedSection.Commands.SingleOrDefault(c => c.Command == 0x85); // TrackCommand for width change
            if (cmd != null)
            {
                widthChangeLength = cmd.Arguments[1].Value;
                widthChangeEnd = cmd.Arguments[2].Value;  // width on each side of middle, in width scales = 0.0047625 meters
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
                // No width change
                t2.EndWidth = t2.StartWidth;
                // nothing to do on next segment
                t2.WidthChangeLength = 0;
                t2.WidthChangeEnd = 0;
            }

            return t2;
        }


        private static ObservableCollection<PointData> ConvertRoadLayoutToPointDataList(List<RoadLayoutSectionData> roadSections, DrawingLayout drawingLayout)
        {
            var z = new ObservableCollection<PointData>();

            foreach (var road in roadSections)
            {
                var x = BuildSectionPointCollectionFromRoadLayout(road);

                road.RelatedSection.SectionPoints = x;

                var y = LayoutToPointsConverter.Convert(x, drawingLayout);

                z.AddRange(y);
            }

            return z;
        }

        private static PointCollection BuildSectionPointCollectionFromRoadLayout(RoadLayoutSectionData layoutSection)
        {
            PointCollection points;

            if (layoutSection.Curvature == 0)
            {
                // straight
                points = GetStraightTrackPointCollection(layoutSection.StartAngle, layoutSection.StartPosition, layoutSection.StartWidth, layoutSection.EndAngle, layoutSection.EndPosition, layoutSection.EndWidth);
            }
            else
            {
                // curve!
                points = GetCurve(layoutSection);
            }

            return points;
        }

        private static PointCollection GetStraightTrackPointCollection(int startAngle, Point startPosition,
            int startTrackWidth, int endAngle, Point endPosition, int endTrackWidth)
        {
            // TODO: one angle is enough since straights are always straight...?

            // straight
            //
            //   2      3
            //   x^^^---x
            //   |      |
            //   |      |
            //   |      |
            //   x***---x
            //   1     0/4
            //
            // *** = startwidth
            // ^^^ = endwidth

            var startAngleRadians = GeometryConversions.RadiansFromGameAngle(startAngle);

            double startWidthAci = startTrackWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            var botRightHuh = Geometry.GetLegacyPointPerpendicular(startPosition, startAngleRadians, startWidthAci);
            var botLeftHuh = Geometry.GetLegacyPointPerpendicular(startPosition, startAngleRadians, -startWidthAci);

            var endAngleRadians = GeometryConversions.RadiansFromGameAngle(endAngle);

            double endWidthAci = endTrackWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            var topRightHuh = Geometry.GetLegacyPointPerpendicular(endPosition, endAngleRadians, endWidthAci);
            var topLeftHuh = Geometry.GetLegacyPointPerpendicular(endPosition, endAngleRadians, -endWidthAci);

            var points = new PointCollection();
            points.Add(botRightHuh);
            points.Add(botLeftHuh);
            points.Add(topLeftHuh);
            points.Add(topRightHuh);
            points.Add(botRightHuh);

            return points;
        }

        private static PointCollection GetCurve(RoadLayoutSectionData t)
        {
            var points = new PointCollection();

            double dOuterRadiusStart = t.Radius + t.StartWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double dInnerRadiusStart = t.Radius - t.StartWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double dOuterRadiusEnd = t.Radius + t.EndWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double dInnerRadiusEnd = t.Radius - t.EndWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            // while we still have the "legacy" calculation, left-hand turns will INCREASE the angle
            double dStartAngle = Math.PI - t.StartAngle * LayoutConstants.AngleScaleRadians;
            double dEndAngle = Math.PI - t.EndAngle * LayoutConstants.AngleScaleRadians;

            if (t.Curvature > 0)
            {
                // right
                var outerLine = Geometry.BuildArc(t.CircleCenterForCurve, dStartAngle, dEndAngle, dOuterRadiusStart, dOuterRadiusEnd);

                foreach (var p in outerLine)
                {
                    points.Add(p);
                }

                var innerLine = Geometry.BuildArc(t.CircleCenterForCurve, dStartAngle, dEndAngle, dInnerRadiusStart, dInnerRadiusEnd);

                foreach (var p in innerLine.Reverse())
                {
                    points.Add(p);
                }
            }
            else
            {
                // left - flip the start and end values
                var outerLine = Geometry.BuildArc(t.CircleCenterForCurve, dEndAngle, dStartAngle, dOuterRadiusEnd, dOuterRadiusStart);

                foreach (var p in outerLine)
                {
                    points.Add(p);
                }

                var innerLine = Geometry.BuildArc(t.CircleCenterForCurve, dEndAngle, dStartAngle, dInnerRadiusEnd, dInnerRadiusStart);

                foreach (var p in innerLine.Reverse())
                {
                    points.Add(p);
                }
            }

            // TODO: for curvature <> 0, final point is never added!

            return points;
        }
    }
}
