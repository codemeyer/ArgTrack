using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ArgTrack.Features.TrackDrawing
{
    /// <summary>
    /// Calculates a line drawn along the center of the track.
    /// </summary>
    public static class CenterLineDrawingCalculator
    {
        /// <summary>
        /// Calculates the shape for a line drawn along the center of the track, but drawn between each section start/end.
        /// </summary>
        /// <param name="layoutSections"></param>
        /// <returns></returns>
        public static ObservableCollection<TrackShapeData> CalculateDrawablePointsStraight(List<LayoutSectionData> layoutSections)
        {
            var list = new ObservableCollection<TrackShapeData>();

            foreach (var section in layoutSections)
            {
                var points = DrawLineStraight(section.StartPosition, section.EndPosition);

                var pointData = PointsToShapesConverter.Convert(points, DrawingLayout.ForTechCenterPointLine());

                list.AddRange(pointData);
            }

            return list;
        }

        /// <summary>
        /// Calculates the shape for a line drawn along the center of the track.
        /// </summary>
        /// <param name="layoutSections"></param>
        /// <returns></returns>
        public static ObservableCollection<TrackShapeData> CalculateDrawablePointsWithArcs(List<LayoutSectionData> layoutSections)
        {
            var retur = new ObservableCollection<TrackShapeData>();

            foreach (var section in layoutSections)
            {
                var pc = BuildSectionPointCollectionCenterLine(section);

                var something = PointsToShapesConverter.Convert(pc, DrawingLayout.ForTechCenterArcLine());

                retur.AddRange(something);
            }

            return retur;
        }

        private static PointCollection BuildSectionPointCollectionCenterLine(LayoutSectionData ls)
        {
            var points = new PointCollection();

            if (ls.IsStraight)
            {
                points = DrawLineStraight(ls.StartPosition, ls.EndPosition);
            }
            else
            {
                if (ls.Curvature > 0)
                {
                    // right
                    var outer = Geometry.BuildTrackArcRightOuter(ls.CircleCenterForCurve, ls.StartAngle, ls.EndAngle, ls.Radius, 1, 1);
                    points.AddRange(outer);

                    var inner = Geometry.BuildTrackArcRightInner(ls.CircleCenterForCurve, ls.StartAngle, ls.EndAngle, ls.Radius, 1, 1);
                    points.AddRange(inner.Reverse());

                    points.Add(outer[0]);
                }
                else
                {
                    // left
                    var inner = Geometry.BuildTrackArcLeftInner(ls.CircleCenterForCurve, ls.StartAngle, ls.EndAngle, ls.Radius, 1, 1);
                    points.AddRange(inner);

                    var outer = Geometry.BuildTrackArcLeftOuter(ls.CircleCenterForCurve, ls.StartAngle, ls.EndAngle, ls.Radius, 1, 1);
                    points.AddRange(outer.Reverse());

                    points.Add(inner[0]);
                }
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
            var points = new PointCollection
            {
                new Point
                {
                    X = startPosition.X,
                    Y = startPosition.Y
                },
                new Point
                {
                    X = endPosition.X,
                    Y = endPosition.Y
                }
            };

            return points;
        }
    }
}
