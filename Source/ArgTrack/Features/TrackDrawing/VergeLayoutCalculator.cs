using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using ArgTrack.Features.Main;

namespace ArgTrack.Features.TrackDrawing
{
    public static class VergeLayoutCalculator
    {
        public static ObservableCollection<PointData> Calculate(List<RoadLayoutSectionData> layoutSections, List<RoadLayoutSectionData> detailedRoadSections)
        {
            // TODO: verges for pitlane?

            var vergePoints = new ObservableCollection<PointData>();

            var leftVerges = ToSectionList(layoutSections, VergeSide.Left);
            var rightVerges = ToSectionList(layoutSections, VergeSide.Right);

            var surroundingArea = MainViewModelPasserArounder.MainViewModel.TrackViewModel.TrackSettings.SurroundingArea;

            foreach(var section in leftVerges)
            {
                var x = GetVergePointCollection(section, VergeSide.Left, detailedRoadSections);
                var y = LayoutToPointsConverter.Convert(x, DrawingLayout.ForVerges(surroundingArea));

                vergePoints.AddRange(y);
            }

            foreach(var section in rightVerges)
            {
                var x = GetVergePointCollection(section, VergeSide.Right, detailedRoadSections);
                var y = LayoutToPointsConverter.Convert(x, DrawingLayout.ForVerges(surroundingArea));

                vergePoints.AddRange(y);
            }

            return vergePoints;
        }

        private static List<VergeLayoutSectionData> ToSectionList(List<RoadLayoutSectionData> layoutSections, VergeSide vergeSide)
        {
            var vergeSections = new List<VergeLayoutSectionData>();
            var vergeSection = new VergeLayoutSectionData();
            bool shouldCreateNew = false;

            byte latestEndWidth = 0;

            for (int i = 0; i < layoutSections.Count; i++)
            {
                var layoutSection = layoutSections[i];

                if (shouldCreateNew)
                {
                    vergeSection = new VergeLayoutSectionData();
                    vergeSection.StartPosition = layoutSection.StartPosition;
                    vergeSection.StartAngle = layoutSection.StartAngle;
                    vergeSection.StartWidth = layoutSection.StartWidth;
                    vergeSection.SetVergeStartWidthRaw(latestEndWidth, vergeSide);
                    shouldCreateNew = false;
                }

                if (i == 0)
                {
                    // first always starts at 0
                    vergeSection.StartPosition = layoutSection.StartPosition;
                    vergeSection.StartAngle = layoutSection.StartAngle;
                    vergeSection.StartWidth = layoutSection.StartWidth;

                    byte initialStartWidth = vergeSide == VergeSide.Left
                        ? TrackViewModelPasserArounder.TrackViewModel.TrackHeader.LeftVergeStartWidth
                        : TrackViewModelPasserArounder.TrackViewModel.TrackHeader.RightVergeStartWidth;

                    vergeSection.SetVergeStartWidthRaw(initialStartWidth, vergeSide);
                }
                else
                {
                    if (vergeSide == VergeSide.Left && !layoutSection.RelatedSection.BridgedLeftFence ||
                        vergeSide == VergeSide.Right && !layoutSection.RelatedSection.BridgedRightFence)
                    {
                        // TODO: seems like ArgTrack currently bridging one section too far?

                        // ends here
                        vergeSection.EndPosition = layoutSection.EndPosition;
                        vergeSection.EndAngle = layoutSection.EndAngle;
                        vergeSection.EndWidth = layoutSection.EndWidth;

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

                        // this section is finished, add it
                        vergeSections.Add(vergeSection);
                        shouldCreateNew = true;
                    }
                    else
                    {
                        // does not end at this layoutSection, go to the next one
                    }
                }
            }

            return vergeSections;
        }

        private static PointCollection GetVergePointCollection(VergeLayoutSectionData vergeLayout, VergeSide vergeSide, List<RoadLayoutSectionData> detailedRoad)
        {
            // left or right from center point, based on vergeSide

            var points = new PointCollection();

            if (vergeLayout.IsStraight)
            {
                var straightPoints = GetStraight(vergeLayout, vergeSide);

                foreach (var point in straightPoints)
                {
                    points.Add(point);
                }
            }
            else
            {
                // TODO: reinstate GetCurve
                var curvedPoints = GetStraight(vergeLayout, vergeSide); //, detailedRoad);

                foreach (var point in curvedPoints)
                {
                    points.Add(point);
                }
            }



            int lastDetailedIndex = 0;

            // backtrack along track center here - should be done both for straights and corners (or only corners?)
            foreach (var roadSec in detailedRoad)
            {
                if (Math.Abs(roadSec.EndPosition.X - vergeLayout.EndPosition.X) < 0.00001 && Math.Abs(roadSec.EndPosition.Y - vergeLayout.EndPosition.Y) < 0.00001)
                {
                    // found it, this is the last detailed road section where the verge ends
                    lastDetailedIndex = detailedRoad.IndexOf(roadSec);
                    break;
                }
            }


            for (int i = lastDetailedIndex; i >= 0; i--)
            {
                var roadSec = detailedRoad[i];

                if (Math.Abs(roadSec.StartPosition.X - vergeLayout.StartPosition.X) < 0.00001 && Math.Abs(roadSec.StartPosition.Y - vergeLayout.StartPosition.Y) < 0.00001)
                {
                    // back at the start
                    break;
                }

                points.Add(new Point(roadSec.EndPosition.X, roadSec.EndPosition.Y));
            }


            // TODO: add first point again to close shape?
            //points.Add(new Point(vergeLayout.StartPosition.X, vergeLayout.StartPosition.Y));

            return points;
        }

        public enum VergeSide
        {
            Left = 0,
            Right = 1
        }

        private static PointCollection GetStraight(VergeLayoutSectionData vergeLayout, VergeSide vergeSide)
        {
            var points = new PointCollection();

            points.Add(new Point(vergeLayout.StartPosition.X, vergeLayout.StartPosition.Y));

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

            double diffStartX = Math.Cos(Math.PI - vergeLayout.StartAngle * LayoutConstants.AngleScaleRadians) * fullTrackStartWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double diffStartY = Math.Sin(Math.PI - vergeLayout.StartAngle * LayoutConstants.AngleScaleRadians) * fullTrackStartWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            double diffEndX = Math.Cos(Math.PI - vergeLayout.EndAngle * LayoutConstants.AngleScaleRadians) * fullTrackEndWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double diffEndY = Math.Sin(Math.PI - vergeLayout.EndAngle * LayoutConstants.AngleScaleRadians) * fullTrackEndWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            switch (vergeSide)
            {
                case VergeSide.Left:
                    points.Add(new Point(vergeLayout.StartPosition.X + diffStartX, vergeLayout.StartPosition.Y - diffStartY));
                    points.Add(new Point(vergeLayout.EndPosition.X + diffEndX, vergeLayout.EndPosition.Y - diffEndY));
                    break;
                case VergeSide.Right:
                    points.Add(new Point(vergeLayout.StartPosition.X - diffStartX, vergeLayout.StartPosition.Y + diffStartY));
                    points.Add(new Point(vergeLayout.EndPosition.X - diffEndX, vergeLayout.EndPosition.Y + diffEndY));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(vergeSide), vergeSide, null);
            }

            points.Add(new Point(vergeLayout.EndPosition.X, vergeLayout.EndPosition.Y));

            return points;
        }

        //private static PointCollection GetCurved(VergeLayoutSectionData vergeLayout, VergeSide vergeSide, List<RoadLayoutSectionData> detailedRoad)
        //{
        //    var points = new PointCollection();

        //    // TODO: is radius set properly? or just 0?
        //    // TODO: is circleCenterForCurve set properly?
        //    // TODO: is curvature set properly? (only used for right/left?)


        //    int fullTrackStartWidth;
        //    int fullTrackEndWidth;

        //    switch (vergeSide)
        //    {
        //        case VergeSide.Left:
        //            fullTrackStartWidth = vergeLayout.LeftVergeStartWidth;
        //            fullTrackEndWidth = vergeLayout.LeftVergeEndWidth;
        //            break;
        //        case VergeSide.Right:
        //            fullTrackStartWidth = vergeLayout.RightVergeStartWidth;
        //            fullTrackEndWidth = vergeLayout.RightVergeEndWidth;
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException(nameof(vergeSide), vergeSide, null);
        //    }

        //    // TODO: surely this depends on left/right?
        //    double dOuterRadiusStart = vergeLayout.Radius + fullTrackStartWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
        //    double dInnerRadiusStart = vergeLayout.Radius - fullTrackStartWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
        //    double dOuterRadiusEnd = vergeLayout.Radius + fullTrackEndWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
        //    double dInnerRadiusEnd = vergeLayout.Radius - fullTrackEndWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
        //    double dStartAngle = Math.PI - vergeLayout.StartAngle * LayoutConstants.AngleScaleRadians;
        //    double dEndAngle = Math.PI - vergeLayout.EndAngle * LayoutConstants.AngleScaleRadians;
        //    double dCenterX = vergeLayout.CircleCenterForCurve.X;
        //    double dCenterY = vergeLayout.CircleCenterForCurve.Y;

        //    // TODO: would be better to just draw an arc from two known points?

        //    if (vergeLayout.Curvature > 0)
        //    {
        //        // right
        //        var outerLine = BuildArc(dCenterX, dCenterY, dStartAngle, dEndAngle, dOuterRadiusStart, dOuterRadiusEnd);
        //        //var innerLine = BuildArc(dCenterX, dCenterY, dStartAngle, dEndAngle, dInnerRadiusStart, dInnerRadiusEnd);

        //        foreach (var p in outerLine)
        //        {
        //            points.Add(p);
        //        }
        //    }
        //    else if (vergeLayout.Curvature < 0)
        //    {
        //        // left
        //        var outerLine = BuildArc(dCenterX, dCenterY, dEndAngle, dStartAngle, dOuterRadiusStart, dOuterRadiusEnd);
        //        //var innerLine = BuildArc(dCenterX, dCenterY, dEndAngle, dStartAngle, dInnerRadiusStart, dInnerRadiusEnd);

        //        foreach (var p in outerLine)
        //        {
        //            points.Add(p);
        //        }
        //    }

        //    return points;
        //}
    }
}
