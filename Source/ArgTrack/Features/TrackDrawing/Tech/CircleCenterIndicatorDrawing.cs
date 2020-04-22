using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ArgTrack.Features.TrackDrawing
{
    /// <summary>
    /// Calculates the shape data for the circle centers.
    /// </summary>
    public static class CircleCenterIndicatorDrawing
    {
        public static ObservableCollection<TrackShapeData> DrawCircleCenterIndicators(List<LayoutSectionData> layoutSections, DrawingLayout style)
        {
            var list = new ObservableCollection<TrackShapeData>();

            foreach (var section in layoutSections)
            {
                if (section.IsStraight)
                {
                    continue;
                }

                var points = new PointCollection();

                var left = Geometry.GetPointPerpendicular(section.CircleCenterForCurve, 0, -1);
                var top = Geometry.GetPoint(section.CircleCenterForCurve, 0, 1);
                var right = Geometry.GetPointPerpendicular(section.CircleCenterForCurve, 0, 1);
                var bottom = Geometry.GetPoint(section.CircleCenterForCurve, 0, -1);

                points.Add(left);
                points.Add(top);
                points.Add(right);
                points.Add(bottom);

                var pointData = PointsToShapesConverter.Convert(points, style);
                list.AddRange(pointData);


                var pointsLine1 = new PointCollection();
                pointsLine1.Add(section.StartPosition);
                pointsLine1.Add(section.CircleCenterForCurve);

                var poda = PointsToShapesConverter.Convert(pointsLine1, style);
                list.AddRange(poda);


                var po2 = new PointCollection();
                po2.Add(section.EndPosition);
                po2.Add(section.CircleCenterForCurve);

                var poda2 = PointsToShapesConverter.Convert(po2, style);
                list.AddRange(poda2);
            }

            return list;
        }
    }
}
