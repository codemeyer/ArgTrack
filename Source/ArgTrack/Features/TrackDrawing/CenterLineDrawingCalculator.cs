using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ArgTrack.Features.TrackDrawing
{
    public static class CircleCenterIndicatorDrawing
    {
        public static ObservableCollection<PointData> DrawCircleCenterIndicators(List<LayoutSectionData> layoutSections)
        {
            var list = new ObservableCollection<PointData>();

            foreach (var section in layoutSections)
            {
                //var points = DrawLineStraight(section.StartPosition, section.EndPosition);

                if (section.IsCurved)
                {
                    var points = new PointCollection();

                    var bolo1 = Geometry.GetPointPerpendicular(section.CircleCenterForCurve, 0, -1);
                    var bolo2 = Geometry.GetPoint(section.CircleCenterForCurve, 0, 1);
                    var bolo3 = Geometry.GetPointPerpendicular(section.CircleCenterForCurve, 0, 1);
                    var bolo4 = Geometry.GetPoint(section.CircleCenterForCurve, 0, -1);

                    points.Add(bolo1);
                    points.Add(bolo2);
                    points.Add(bolo3);
                    points.Add(bolo4);

                    var pointData = LayoutToPointsConverter.Convert(points, new SolidColorBrush(Color.FromRgb(0, 200, 255)), 0.2, new SolidColorBrush(Color.FromRgb(0, 200, 255)));
                    list.AddRange(pointData);


                    var pointsLine1 = new PointCollection();
                    pointsLine1.Add(section.StartPosition);
                    pointsLine1.Add(section.CircleCenterForCurve);

                    var poda = LayoutToPointsConverter.Convert(pointsLine1, new SolidColorBrush(Color.FromRgb(0, 200, 255)), 0.2, new SolidColorBrush(Color.FromRgb(0, 200, 255)));
                    list.AddRange(poda);


                    var po2 = new PointCollection();
                    po2.Add(section.EndPosition);
                    po2.Add(section.CircleCenterForCurve);

                    var poda2 = LayoutToPointsConverter.Convert(po2, new SolidColorBrush(Color.FromRgb(0, 200, 255)), 0.2, new SolidColorBrush(Color.FromRgb(0, 200, 255)));
                    list.AddRange(poda2);

                }
            }

            return list;
        }
    }

    public static class CenterLineDrawingCalculator
    {
        public static ObservableCollection<PointData> CalculateDrawablePointsStraightUp(List<LayoutSectionData> layoutSections)
        {
            var list = new ObservableCollection<PointData>();

            foreach (var section in layoutSections)
            {
                var points = DrawLineStraight(section.StartPosition, section.EndPosition);

                var pointData = LayoutToPointsConverter.Convert(points, new SolidColorBrush(Color.FromRgb(0, 200, 255)), 0.2, new SolidColorBrush(Color.FromRgb(0, 200, 255)));

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

                var something = LayoutToPointsConverter.Convert(pc, new SolidColorBrush(Color.FromRgb(200, 0, 255)), 0.2, new SolidColorBrush(Color.FromRgb(200, 0, 255)));

                retur.AddRange(something);
            }

            return retur;
        }

        private static PointCollection BuildSectionPointCollectionCenterLine(LayoutSectionData layDoo)
        {
            var points = new PointCollection();

            if (layDoo.Curvature == 0)
            {
                // straight
                points = DrawLineStraight(layDoo.StartPosition, layDoo.EndPosition);
            }
            else
            {
                // curve!
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
                    var outerLine = LayoutCalculator.BuildArc(dCenterX, dCenterY, dStartAngle, dEndAngle, dOuterRadiusStart, dOuterRadiusEnd);

                    foreach (var p in outerLine)
                    {
                        points.Add(p);
                    }

                    var innerLine = LayoutCalculator.BuildArc(dCenterX, dCenterY, dStartAngle, dEndAngle, dInnerRadiusStart, dInnerRadiusEnd);

                    foreach (var p in innerLine.Reverse())
                    {
                        points.Add(p);
                    }
                }
                else
                {
                    // left
                    var outerLine = LayoutCalculator.BuildArc(dCenterX, dCenterY, dEndAngle, dStartAngle, dOuterRadiusStart, dOuterRadiusEnd);

                    foreach (var p in outerLine)
                    {
                        points.Add(p);
                    }

                    var innerLine = LayoutCalculator.BuildArc(dCenterX, dCenterY, dEndAngle, dStartAngle, dInnerRadiusStart, dInnerRadiusEnd);

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
