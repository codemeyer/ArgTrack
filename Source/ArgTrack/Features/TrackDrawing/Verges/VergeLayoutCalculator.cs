using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using ArgTrack.Features.Main;
using ArgTrack.Features.TrackEditing.TrackSection;

namespace ArgTrack.Features.TrackDrawing
{
    public static class VergeLayoutCalculator
    {
        public static ObservableCollection<TrackShapeData> Calculate(List<RoadLayoutSectionData> layoutSections, List<RoadLayoutSectionData> detailedRoadSections)
        {
            var vergePoints = new ObservableCollection<TrackShapeData>();

            var leftVerges = CalculateLeftVerges(layoutSections, MainViewModelPasserArounder.MainViewModel.TrackViewModel.TrackHeader.LeftVergeStartWidth);
            var rightVerges = CalculateRightVerges(layoutSections, MainViewModelPasserArounder.MainViewModel.TrackViewModel.TrackHeader.RightVergeStartWidth);

            var surroundingArea = MainViewModelPasserArounder.MainViewModel.TrackViewModel.TrackSettings.SurroundingArea;

            foreach (var section in leftVerges)
            {
                var x = GetVergePointCollection(section, VergeSide.Left, detailedRoadSections);
                var y = PointsToShapesConverter.Convert(x, DrawingLayout.ForVerges(surroundingArea));

                vergePoints.AddRange(y);
            }

            foreach (var section in rightVerges)
            {
                var x = GetVergePointCollection(section, VergeSide.Right, detailedRoadSections);
                var y = PointsToShapesConverter.Convert(x, DrawingLayout.ForVerges(surroundingArea));

                vergePoints.AddRange(y);
            }

            return vergePoints;
        }

        public static List<VergeLayoutSectionData> CalculateLeftVerges(List<RoadLayoutSectionData> layoutSections, byte initialVergeWidth)
        {
            var rawOnes = ToSectionListOneToOne(layoutSections, VergeSide.Left, initialVergeWidth);
            var merged = VergeLayoutMerger.MergeBridgedSections(rawOnes, VergeSide.Left);

            return merged;
        }

        public static List<VergeLayoutSectionData> CalculateRightVerges(List<RoadLayoutSectionData> layoutSections, byte initialVergeWidth)
        {
            var rawOnes = ToSectionListOneToOne(layoutSections, VergeSide.Right, initialVergeWidth);
            var merged = VergeLayoutMerger.MergeBridgedSections(rawOnes, VergeSide.Right);

            return merged;
        }

        public static List<VergeLayoutSectionData> ToSectionListOneToOne(List<RoadLayoutSectionData> layoutSections, VergeSide vergeSide, byte initialVergeWidth)
        {
            var vergeSections = new List<VergeLayoutSectionData>();

            byte latestEndWidth = initialVergeWidth;

            Point latestEndPosition = new Point(0, 0);

            for (int i = 0; i < layoutSections.Count; i++)
            {
                var layoutSection = layoutSections[i];

                if (layoutSection.RelatedSection.Length == 0)
                {
                    continue;
                }

                var vergeSection = new VergeLayoutSectionData();

                vergeSection.StartPosition = layoutSection.StartPosition;
                vergeSection.StartAngle = layoutSection.StartAngle;
                vergeSection.StartWidth = layoutSection.StartWidth;
                vergeSection.SetVergeStartWidthRaw(latestEndWidth, vergeSide);

                vergeSection.Curvature = layoutSection.Curvature;
                vergeSection.CircleCenterForCurve = layoutSection.CircleCenterForCurve;
                vergeSection.Radius = layoutSection.Radius;

                // first section
                if (i == 0)
                {
                    if (vergeSide == VergeSide.Right)
                    {
                        latestEndPosition = CalculateOuterPoint(vergeSection.StartPosition, vergeSide, vergeSection.StartAngle, vergeSection.RightVergeStartWidth);
                    }
                    else
                    {
                        latestEndPosition = CalculateOuterPoint(vergeSection.StartPosition, vergeSide, vergeSection.StartAngle, vergeSection.LeftVergeStartWidth);
                    }
                }

                if (vergeSide == VergeSide.Left)
                {
                    vergeSection.LeftVergeEndWidthRaw = layoutSection.RelatedSection.LeftVergeWidth;
                    latestEndWidth = vergeSection.LeftVergeEndWidthRaw;
                }
                else
                {
                    vergeSection.RightVergeEndWidthRaw = layoutSection.RelatedSection.RightVergeWidth;
                    latestEndWidth = vergeSection.RightVergeEndWidthRaw;
                }

                vergeSection.RelatedSections = new List<TrackSectionViewModel>() { layoutSection.RelatedSection };
                vergeSection.RelatedSection = layoutSection.RelatedSection;

                vergeSection.EndPosition = layoutSection.EndPosition;
                vergeSection.EndAngle = layoutSection.EndAngle;
                vergeSection.EndWidth = layoutSection.EndWidth;


                // the end position of the previous one
                vergeSection.OuterStartPosition = latestEndPosition;

                if (vergeSide == VergeSide.Right)
                {
                    vergeSection.OuterEndPosition = CalculateOuterPoint(vergeSection.EndPosition, vergeSide, vergeSection.EndAngle, vergeSection.RightVergeEndWidth);
                }
                else
                {
                    vergeSection.OuterEndPosition = CalculateOuterPoint(vergeSection.EndPosition, vergeSide, vergeSection.EndAngle, vergeSection.LeftVergeEndWidth);
                }

                latestEndPosition = vergeSection.OuterEndPosition;

                // this section is finished, add it
                vergeSections.Add(vergeSection);
            }

            return vergeSections;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="position">Center position for section</param>
        /// <param name="vergeSide">Left or Right</param>
        /// <param name="angle">Start angle</param>
        /// <param name="fullTrackWidth">Full track width (incl. road and verge)</param>
        /// <returns></returns>
        private static Point CalculateOuterPoint(Point position, VergeSide vergeSide, double angle, int fullTrackWidth)
        {
            double diffStartX = Math.Cos(Math.PI - angle * LayoutConstants.AngleScaleRadians) * fullTrackWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double diffStartY = Math.Sin(Math.PI - angle * LayoutConstants.AngleScaleRadians) * fullTrackWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            switch (vergeSide)
            {
                case VergeSide.Left:
                    return new Point(position.X + diffStartX, position.Y - diffStartY);
                case VergeSide.Right:
                    return new Point(position.X - diffStartX, position.Y + diffStartY);
                default:
                    throw new ArgumentOutOfRangeException(nameof(vergeSide), vergeSide, null);
            }
        }



        // below turns VergeLayoutSectionData into PointCollections, place in another class?

        private static PointCollection GetVergePointCollection(VergeLayoutSectionData vergeLayout, VergeSide vergeSide, List<RoadLayoutSectionData> detailedRoad)
        {
            if (vergeLayout.IsStraight)
            {
                return GetVergePointCollectionStraight(vergeLayout, detailedRoad);
            }
            else
            {
                if (vergeLayout.DrawStraight)
                {
                    return GetVergePointCollectionStraight(vergeLayout, detailedRoad);
                }
                else
                {
                    return GetVergePointCollectionCurve(vergeLayout, vergeSide, detailedRoad);
                }
            }
        }

        private static PointCollection GetVergePointCollectionStraight(VergeLayoutSectionData vergeLayout, List<RoadLayoutSectionData> detailedRoad)
        {
            var points = GetStraight(vergeLayout);

            var backtracking = BacktrackingPoints(vergeLayout, detailedRoad);

            points.AddRange(backtracking);

            return points;
        }

        private static PointCollection GetStraight(VergeLayoutSectionData vergeLayout)
        {
            var points = new PointCollection();

            points.Add(new Point(vergeLayout.StartPosition.X, vergeLayout.StartPosition.Y));

            points.Add(new Point(vergeLayout.OuterStartPosition.X, vergeLayout.OuterStartPosition.Y));
            points.Add(new Point(vergeLayout.OuterEndPosition.X, vergeLayout.OuterEndPosition.Y));
            points.Add(new Point(vergeLayout.EndPosition.X, vergeLayout.EndPosition.Y));

            // will be closed by backtracking along circuit

            return points;
        }

        private static PointCollection GetVergePointCollectionCurve(VergeLayoutSectionData vergeLayout, VergeSide vergeSide, List<RoadLayoutSectionData> detailedRoad)
        {
            var points = GetCurve(vergeLayout, vergeSide);

            if (points.Count == 0)
            {
                // do not backtrack
                return points;
            }

            var backtracking = BacktrackingPoints(vergeLayout, detailedRoad);

            points.AddRange(backtracking);

            return points;
        }

        private static PointCollection GetCurve(VergeLayoutSectionData vergeLayout, VergeSide vergeSide)
        {
            var points = new PointCollection();
            int fullTrackStartWidth;
            int fullTrackEndWidth;

            switch (vergeSide)
            {
                case VergeSide.Left:
                    fullTrackStartWidth = vergeLayout.LeftVergeStartWidth;
                    fullTrackEndWidth = vergeLayout.LeftVergeEndWidth;
                    break;
                case VergeSide.Right:
                    fullTrackStartWidth = vergeLayout.RightVergeStartWidth;
                    fullTrackEndWidth = vergeLayout.RightVergeEndWidth;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(vergeSide), vergeSide, null);
            }

            if (vergeSide == VergeSide.Left)
            {
                var line = Geometry.BuildArcForLeftVerge(vergeLayout.CircleCenterForCurve, vergeLayout.StartAngle, vergeLayout.EndAngle, vergeLayout.Radius, fullTrackStartWidth, fullTrackEndWidth);
                points.AddRange(line);
            }
            else
            {
                var line = Geometry.BuildArcForRightVerge(vergeLayout.CircleCenterForCurve, vergeLayout.StartAngle, vergeLayout.EndAngle, vergeLayout.Radius, fullTrackStartWidth, fullTrackEndWidth);
                points.AddRange(line);
            }

            return points;
        }

        private static PointCollection BacktrackingPoints(VergeLayoutSectionData vergeLayout, List<RoadLayoutSectionData> detailedRoad)
        {
            var points = new PointCollection();
            int lastDetailedIndex = 0;

            foreach (var roadSec in detailedRoad)
            {
                if (Math.Abs(roadSec.EndPosition.X - vergeLayout.EndPosition.X) < 0.000001 && Math.Abs(roadSec.EndPosition.Y - vergeLayout.EndPosition.Y) < 0.000001)
                {
                    // found it, this is the last detailed road section where the verge ends
                    lastDetailedIndex = detailedRoad.IndexOf(roadSec);
                    break;
                }
            }

            for (int i = lastDetailedIndex; i >= 0; i--)
            {
                var roadSec = detailedRoad[i];

                points.Add(new Point(roadSec.EndPosition.X, roadSec.EndPosition.Y));

                if (Math.Abs(roadSec.StartPosition.X - vergeLayout.StartPosition.X) < 0.000001 && Math.Abs(roadSec.StartPosition.Y - vergeLayout.StartPosition.Y) < 0.000001)
                {
                    break;
                }
            }

            // add first point again to close shape
            points.Add(new Point(vergeLayout.StartPosition.X, vergeLayout.StartPosition.Y));

            return points;
        }
    }

    public enum VergeSide
    {
        Left = 0,
        Right = 1
    }
}
