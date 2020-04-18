using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ArgTrack.Features.TrackDrawing
{
    public static class CenterLineDrawingCalculator
    {
        public static ObservableCollection<PointData> CalculateDrawablePointsStraightUp(List<LayoutSectionData> layoutSections)
        {
            var list = new ObservableCollection<PointData>();

            foreach (var section in layoutSections)
            {
                var points = DrawLineStraight(section.StartPosition, section.EndPosition);

                var pointData = LayoutToPointsConverter.Convert(points, DrawingLayout.ForTechCenterPointLine());

                list.AddRange(pointData);
            }

            return list;
        }

        public static ObservableCollection<PointData> CalculateDrawablePointsWithArcs(List<LayoutSectionData> puntos)
        {
            var retur = new ObservableCollection<PointData>();

            foreach (var section in puntos)
            {
                var pc = BuildSectionPointCollectionCenterLine(section);

                var something = LayoutToPointsConverter.Convert(pc, DrawingLayout.ForTechCenterArcLine());

                retur.AddRange(something);
            }

            return retur;
        }

        private static PointCollection BuildSectionPointCollectionCenterLine(LayoutSectionData layDoo)
        {
            var points = new PointCollection();

            if (layDoo.IsStraight)
            {
                points = DrawLineStraight(layDoo.StartPosition, layDoo.EndPosition);
            }
            else
            {
                double dOuterRadiusStart = layDoo.Radius + 0 / LayoutConstants.TrackWidthUnitsPerLengthUnit;
                double dInnerRadiusStart = layDoo.Radius - 0 / LayoutConstants.TrackWidthUnitsPerLengthUnit;
                double dOuterRadiusEnd = layDoo.Radius + 0 / LayoutConstants.TrackWidthUnitsPerLengthUnit;
                double dInnerRadiusEnd = layDoo.Radius - 0 / LayoutConstants.TrackWidthUnitsPerLengthUnit;
                double dStartAngle = Math.PI - layDoo.StartAngle * LayoutConstants.AngleScaleRadians;
                double dEndAngle = Math.PI - layDoo.EndAngle * LayoutConstants.AngleScaleRadians;
                double dCenterX = layDoo.CircleCenterForCurve.X;
                double dCenterY = layDoo.CircleCenterForCurve.Y;

                // TODO: outerLine and innerLine are identical points, but both are needed because
                // TODO: of the way drawing works, always from one point to the next

                if (layDoo.Curvature > 0)
                {
                    // right
                    var outerLine = Geometry.BuildArc(dCenterX, dCenterY, dStartAngle, dEndAngle, dOuterRadiusStart, dOuterRadiusEnd);

                    foreach (var p in outerLine)
                    {
                        points.Add(p);
                    }

                    var innerLine = Geometry.BuildArc(dCenterX, dCenterY, dStartAngle, dEndAngle, dInnerRadiusStart, dInnerRadiusEnd);

                    foreach (var p in innerLine.Reverse())
                    {
                        points.Add(p);
                    }
                }
                else
                {
                    // left
                    var outerLine = Geometry.BuildArc(dCenterX, dCenterY, dEndAngle, dStartAngle, dOuterRadiusStart, dOuterRadiusEnd);

                    foreach (var p in outerLine)
                    {
                        points.Add(p);
                    }

                    var innerLine = Geometry.BuildArc(dCenterX, dCenterY, dEndAngle, dStartAngle, dInnerRadiusStart, dInnerRadiusEnd);

                    foreach (var p in innerLine.Reverse())
                    {
                        points.Add(p);
                    }
                }

                // TODO: for curvature <> 0, final point is never added! ???
            }

            return points;
        }

        /// <summary>
        /// Defines a straight line from the start position to the end position.
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="endPosition"></param>
        /// <returns></returns>
        private static PointCollection DrawLineStraight(Point startPosition, Point endPosition)
        {
            var points = new PointCollection();

            points.Add(new Point
            {
                X = startPosition.X,
                Y = startPosition.Y
            });

            points.Add(new Point
            {
                X = endPosition.X,
                Y = endPosition.Y
            }
            );

            return points;
        }
    }
}
