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
            var shapeList = new ObservableCollection<PointData>();

            foreach (var road in roadSections)
            {
                var sectionPoints = BuildSectionPointCollectionFromRoadLayout(road);

                road.RelatedSection.SectionPoints = sectionPoints;

                var sectionShapeData = LayoutToPointsConverter.Convert(sectionPoints, drawingLayout);

                shapeList.AddRange(sectionShapeData);
            }

            return shapeList;
        }

        private static PointCollection BuildSectionPointCollectionFromRoadLayout(RoadLayoutSectionData layoutSection)
        {
            PointCollection points;

            if (layoutSection.IsStraight)
            {
                points = GetStraight(layoutSection);
            }
            else
            {
                points = GetCurve(layoutSection);
            }

            return points;
        }

        private static PointCollection GetStraight(RoadLayoutSectionData t)
        {
            // straight
            //
            //   2      1
            //   x^^^---x
            //   |      |
            //   |      |
            //   |      |
            //   x***---x
            //   3     0/4
            //
            // *** = startwidth
            // ^^^ = endwidth

            var angleRadians = GeometryConversions.RadiansFromGameAngle(t.StartAngle);

            double startWidthTrackUnits = t.StartWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            var pointBottomRight = Geometry.GetLegacyPointPerpendicular(t.StartPosition, angleRadians, startWidthTrackUnits);
            var pointBottomLeft = Geometry.GetLegacyPointPerpendicular(t.StartPosition, angleRadians, -startWidthTrackUnits);

            double endWidthTrackUnits = t.EndWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            var pointTopRight = Geometry.GetLegacyPointPerpendicular(t.EndPosition, angleRadians, endWidthTrackUnits);
            var pointTopLeft = Geometry.GetLegacyPointPerpendicular(t.EndPosition, angleRadians, -endWidthTrackUnits);

            var points = new PointCollection
            {
                pointBottomRight,
                pointTopRight,
                pointTopLeft,
                pointBottomLeft,
                pointBottomRight
            };

            return points;
        }

        private static PointCollection GetCurve(RoadLayoutSectionData t)
        {
            var points = new PointCollection();

            if (t.Curvature > 0)
            {
                //
                //          ________ outer.last (before Reverse())
                //         /   ____|
                //        /   /      inner (last)
                //        |  |
                // outer  |__| inner[0]
                //   [0]

                var inner = Geometry.BuildTrackArcRightInner(t.CircleCenterForCurve, t.StartAngle, t.EndAngle, t.Radius, t.StartWidth, t.EndWidth);
                points.AddRange(inner);

                var outer = Geometry.BuildTrackArcRightOuter(t.CircleCenterForCurve, t.StartAngle, t.EndAngle, t.Radius, t.StartWidth, t.EndWidth);
                points.AddRange(outer.Reverse());    // reverse since we are building a polygon that must be closed

                // close the polygon
                points.Add(inner[0]);
            }
            else
            {
                //   outer.last
                //        ________
                //       |_____   \
                //  inner.last \   \
                //   (bef.Rev)  |  |
                //              |__| outer[0]
                //      inner[0] (before Reverse())

                var outer = Geometry.BuildTrackArcLeftOuter(t.CircleCenterForCurve, t.StartAngle, t.EndAngle, t.Radius, t.StartWidth, t.EndWidth);
                points.AddRange(outer);

                var inner = Geometry.BuildTrackArcLeftInner(t.CircleCenterForCurve, t.StartAngle, t.EndAngle, t.Radius, t.StartWidth, t.EndWidth);
                points.AddRange(inner.Reverse());   // reverse since we are building a polygon that must be closed

                // close the polygon
                points.Add(outer[0]);
            }

            return points;
        }
    }
}
