using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                if (section.IsStraight)
                {
                    continue;
                }

                var points = new PointCollection();

                var bolo1 = Geometry.GetPointPerpendicular(section.CircleCenterForCurve, 0, -1);
                var bolo2 = Geometry.GetPoint(section.CircleCenterForCurve, 0, 1);
                var bolo3 = Geometry.GetPointPerpendicular(section.CircleCenterForCurve, 0, 1);
                var bolo4 = Geometry.GetPoint(section.CircleCenterForCurve, 0, -1);

                points.Add(bolo1);
                points.Add(bolo2);
                points.Add(bolo3);
                points.Add(bolo4);

                var pointData = LayoutToPointsConverter.Convert(points, DrawingLayout.ForTechCircleCenter());
                list.AddRange(pointData);


                var pointsLine1 = new PointCollection();
                pointsLine1.Add(section.StartPosition);
                pointsLine1.Add(section.CircleCenterForCurve);

                var poda = LayoutToPointsConverter.Convert(pointsLine1, DrawingLayout.ForTechCircleCenter());
                list.AddRange(poda);


                var po2 = new PointCollection();
                po2.Add(section.EndPosition);
                po2.Add(section.CircleCenterForCurve);

                var poda2 = LayoutToPointsConverter.Convert(po2, DrawingLayout.ForTechCircleCenter());
                list.AddRange(poda2);
            }

            return list;
        }
    }
}
