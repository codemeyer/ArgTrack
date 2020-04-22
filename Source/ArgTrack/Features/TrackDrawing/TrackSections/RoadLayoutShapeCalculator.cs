using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace ArgTrack.Features.TrackDrawing
{
    /// <summary>
    /// Turns *LayoutSectionData into  TrackShapeData ready to be drawn on screen.
    /// </summary>
    public static class RoadLayoutShapeCalculator
    {
        /// <summary>
        /// Calculates all points for either track or pit lane.
        /// </summary>
        public static ObservableCollection<TrackShapeData> Calculate(List<LayoutSectionData> layoutSections, short trackStartWidth, DrawingLayout drawingLayout)
        {
            var roadLayoutSections = RoadLayoutGeometryCalculator.CalculateRoadSections(layoutSections, trackStartWidth);

            // adds "styling" to the points...
            var shapeDataList = ConvertRoadLayoutToShapeDataList(roadLayoutSections, drawingLayout);

            return shapeDataList;
        }

        public static ObservableCollection<TrackShapeData> Calculate(List<RoadLayoutSectionData> roadLayoutSections, DrawingLayout drawingLayout)
        {
            // adds "styling" to the points...
            var shapeDataList = ConvertRoadLayoutToShapeDataList(roadLayoutSections, drawingLayout);

            return shapeDataList;
        }

        private static ObservableCollection<TrackShapeData> ConvertRoadLayoutToShapeDataList(List<RoadLayoutSectionData> roadSections, DrawingLayout drawingLayout)
        {
            var shapeList = new ObservableCollection<TrackShapeData>();

            foreach (var road in roadSections)
            {
                var sectionPoints = BuildSectionPointCollectionFromRoadLayout(road);

                road.RelatedSection.SectionPoints = sectionPoints;

                var sectionShapeData = PointsToShapesConverter.Convert(sectionPoints, drawingLayout);

                shapeList.AddRange(sectionShapeData);
            }

            return shapeList;
        }

        // or should this (and below) be in RoadLayoutGeometryCalculator instead?
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
