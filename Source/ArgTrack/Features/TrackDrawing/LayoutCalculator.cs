using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ArgData.Entities;
using ArgTrack.Features.Main;

namespace ArgTrack.Features.TrackDrawing
{


    // how about object with...


    //  vel-------tel---tec---ter-------ver
    //  |          |     |     |          |
    //  |          |     |     |          |
    //  |          |     |     |          |
    //  |          |     |     |          |
    //  vsl-------tsl---tsc---tsr-------vsr

    // i.e.
    // vel = VergeEndLeft
    // tel = TrackEndLeft
    // tec = TrackEndCenter
    // ter = TrackEndRight
    // ver = VergeEndRight
    // vsl = VergeStartLeft
    // tsl = TrackStartLeft
    // tsc = TrackStartCenter
    // tsr = TrackStartRight
    // vsr = VergeStartRight

    // all as Point(X,Y)

    // guess it will also have
    // curvature ?
    // radius ?
    // centercircle (X,Y)


    // plus
    // TrackWidth (always same left/right)
    //



    // TODO: split into...
    // public class VergeLayoutCalculator
    // public class RoadLayoutCalculator
    // public class ComputerCarLineLayoutCalculator
    // public class CameraLayoutCalculator
    // public class TrackSideObjectsLayoutCalculator

    // and general arc functions remain here? or in
    // public class



    // this is being made obsolete...
    [Obsolete("Remove this?")]
    public class LayoutCalculator
    {
        [Obsolete("Remove this")]
        public ObservableCollection<PointData> CalculateTrackLayout(TrackViewModel track)
        {
            int nWidthLength = 0;
            int nWidthEnd = 0;
            double dPosX = 0; // startPositionX;
            double dPosY = 0; // startPositionY;
            int startWidth = track.TrackHeader.TrackStartWidth;
            int startAngle = track.TrackHeader.FirstSectionAngle;

            int startLeftVerge = track.TrackHeader.LeftVergeStartWidth;
            int startRightVerge = track.TrackHeader.RightVergeStartWidth;

            var layoutSections = new List<TrackSectionLayoutData>();

            foreach (var section in TrackViewModelPasserArounder.TrackViewModel.TrackSections)
            {
                var ws = new TrackSectionLayoutData(section);

                ws.CalculateLayout(dPosX, dPosY, startWidth, startAngle, nWidthLength, nWidthEnd);

                dPosX = ws.EndPosition.X;
                dPosY = ws.EndPosition.Y;
                startWidth = ws.EndWidth;
                startAngle = ws.EndAngle;
                nWidthLength = ws.WidthChangeLength;
                nWidthEnd = ws.WidthChangeEnd;

                ws.LeftVergeStartWidthRaw = startLeftVerge;
                ws.RightVergeStartWidthRaw = startRightVerge;

                ws.LeftVergeEndWidthRaw = section.LeftVergeWidth;
                ws.RightVergeEndWidthRaw = section.RightVergeWidth;

                startLeftVerge = section.LeftVergeWidth;
                startRightVerge = section.RightVergeWidth;

                layoutSections.Add(ws);
            }

            // TODO: set flag where pitlane starts in code above?

            int pitLaneStartTrackSectionIndex = 0;

            foreach (var section in track.TrackSections)
            {
                foreach (var cmd in section.Commands)
                {
                    if (cmd.Command == 0x86)
                    {
                        // HELO!
                        pitLaneStartTrackSectionIndex = track.TrackSections.IndexOf(section);
                    }
                }
            }

            var pitlaneSections = new List<TrackSectionLayoutData>();

            if (pitLaneStartTrackSectionIndex > 0)
            {
                // TODO: draw pitlane
                var startSection = layoutSections[pitLaneStartTrackSectionIndex];

                startWidth = LayoutConstants.PitLaneWidth;
                startAngle = startSection.StartAngle;


                // calculate pitlane "shift" along section where pitlane starts
                double pitlaneShift = startSection.StartWidth - LayoutConstants.PitLaneWidth;

                double pitLaneShiftX = Math.Cos(Math.PI - startSection.StartAngle * LayoutConstants.AngleScaleRadians) * pitlaneShift / LayoutConstants.TrackWidthUnitsPerLengthUnit;
                double pitLaneShiftY = Math.Sin(Math.PI - startSection.StartAngle * LayoutConstants.AngleScaleRadians) * pitlaneShift / LayoutConstants.TrackWidthUnitsPerLengthUnit;

                if (track.TrackSettings.PitsSide == TrackSide.Right)
                {
                    dPosX = startSection.StartPosition.X - pitLaneShiftX;
                    dPosY = startSection.StartPosition.Y + pitLaneShiftY;
                }
                else
                {
                    dPosX = startSection.StartPosition.X - pitLaneShiftX * -1;
                    dPosY = startSection.StartPosition.Y + pitLaneShiftY * -1;
                }


                nWidthLength = 0;
                nWidthEnd = 0;

                foreach (var section in TrackViewModelPasserArounder.TrackViewModel.PitLaneSections)
                {
                    var ws = new TrackSectionLayoutData(section);

                    ws.CalculateLayout(dPosX, dPosY, startWidth, startAngle, nWidthLength, nWidthEnd);

                    dPosX = ws.EndPosition.X;
                    dPosY = ws.EndPosition.Y;
                    startWidth = ws.EndWidth;
                    startAngle = ws.EndAngle;
                    nWidthLength = ws.WidthChangeLength;
                    nWidthEnd = ws.WidthChangeEnd;

                    pitlaneSections.Add(ws);
                }
            }


            // calculate according to wanted zindex order

            // Verges
            // TrackSections
            // PitLaneSections
            // CCLine
            // TracksideObjects



            var ccLineLayoutList = new List<ComputerCarLineLayoutData>();

            foreach (var ccLine in track.BestLineSegments)
            {
                var ccLineLayout = new ComputerCarLineLayoutData(ccLine);
                ccLineLayout.CalculateRadius();

                ccLineLayoutList.Add(ccLineLayout);
            }

            RacingLineStuff.CalculateCCLine(ccLineLayoutList, layoutSections);



            // separate methods?



            // TODO: merge iteration above with iteration below?

            var min = new Point(0, 0);
            var max = new Point(0, 0);

            var pointDataList = new ObservableCollection<PointData>();


            // LEFT VERGES
            foreach (var section in layoutSections) //.Where(s => s.Curvature == 0))
            {
                //if (section.TrackSectionBuddy.BridgedLeftFence)
                //{
                //    continue;
                //}

                // width here == from center of track to edge of left verge

                PointCollection poci;
                // left verge

                //if (section.Curvature == 0)
                //{
                poci = GetStraightVergePointCollection(section.StartPosition, section.StartAngle,
                    section.LeftVergeStartWidth, section.EndPosition, section.EndAngle, section.LeftVergeEndWidth,
                    VergeSide.Left);
                //}
                //else
                //{
                //    poci = GetCurvio(section.Radius, section.LeftVergeStartWidth, section.StartAngle,
                //        section.LeftVergeEndWidth, section.EndAngle, section.CircleCenterForCurve, section.Curvature);
                //}

                var data = new PointData(poci);
                data.StrokeThickness = 0.025;
                data.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                data.Fill = new SolidColorBrush(Color.FromRgb(0x00, 0x89, 0x22));

                // IGNORE until arcs are correctly drawn both to the left and right
                //pointDataList.Add(data);
            }

            // RIGHT VERGES
            foreach (var section in layoutSections)
            {
                PointCollection poci;

                //if (section.TrackSectionBuddy.BridgedRightFence)
                //{
                //    continue;
                //}


                if (section.Curvature == 0)
                {
                    poci = GetStraightVergePointCollection(section.StartPosition, section.StartAngle, section.RightVergeStartWidth, section.EndPosition, section.EndAngle, section.RightVergeEndWidth, VergeSide.Right);
                }
                else
                {
                    poci = GetCurvio(section.Radius, section.RightVergeStartWidth, section.StartAngle,
                        section.RightVergeEndWidth, section.EndAngle, section.CircleCenterForCurve, section.Curvature);
                }

                var data = new PointData(poci);
                data.StrokeThickness = 0.025;
                data.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                data.Fill = new SolidColorBrush(Color.FromRgb(0x00, 0x89, 0x22));

                // IGNORE until arcs are correctly drawn both to the left and right
                //pointDataList.Add(data);
            }

            // TRACK
            foreach (var section in layoutSections)
            {
                var data = new PointData(section.SectionPoints);

                data.StrokeThickness = 0.1;
                data.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                data.Fill = new SolidColorBrush(Color.FromRgb(0x89, 0x89, 0x85));

                pointDataList.Add(data);
            }

            foreach (var section in pitlaneSections)
            {
                var data = new PointData(section.SectionPoints);

                data.StrokeThickness = 0.1;
                data.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                data.Fill = new SolidColorBrush(Color.FromRgb(0x89, 0x89, 0x85));

                pointDataList.Add(data);
            }


            foreach (var ccl in ccLineLayoutList)
            {
                var pc = new PointCollection();
                pc.Add(new Point(ccl.getPosXStart(), ccl.getPosYStart()));
                pc.Add(new Point(ccl.getPosXEnd(), ccl.getPosYEnd()));
                var data = new PointData(pc);
                data.StrokeThickness = 1;
                data.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 255));
                data.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 255));

                //pointDataList.Add(data);

                if (ccl.getShiftSegment() != null)
                {
                    var pc2 = new PointCollection();
                    pc2.Add(new Point(ccl.getPosXStart(), ccl.getPosYStart()));
                    pc2.Add(new Point(ccl.getPosXEnd(), ccl.getPosYEnd()));

                    var data2 = new PointData(pc2);
                    data2.StrokeThickness = 1;
                    data2.Stroke = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    data2.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0));

                    //pointDataList.Add(data2);
                }
            }

            return pointDataList;

        }


        public static PointCollection GetStraightTrackPointCollection(int startAngle, Point startPosition,
            int startTrackWidth, int endAngle, Point endPosition, int endTrackWidth)
        {
            // straight
            //
            //   1      2
            //   x^^^---x
            //   |      |
            //   |      |
            //   |      |
            //   x***---x
            //  0/4     3
            //
            // *** = startwidth
            // ^^^ = endwidth

            var points = new PointCollection();

            double dXDiff = Math.Cos(Math.PI - startAngle * LayoutConstants.AngleScaleRadians) * startTrackWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double dYDiff = Math.Sin(Math.PI - startAngle * LayoutConstants.AngleScaleRadians) * startTrackWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            // TODO: check this order...
            // 0?
            points.Add(new Point
            {
                X = startPosition.X - dXDiff,
                Y = startPosition.Y + dYDiff
            });
            // 1?
            points.Add(new Point
            {
                X = startPosition.X + dXDiff,
                Y = startPosition.Y - dYDiff
            });

            double dXDiff2 = Math.Cos(Math.PI - endAngle * LayoutConstants.AngleScaleRadians) * endTrackWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double dYDiff2 = Math.Sin(Math.PI - endAngle * LayoutConstants.AngleScaleRadians) * endTrackWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            //2?
            points.Add(new Point
            {
                X = endPosition.X + dXDiff2,
                Y = endPosition.Y - dYDiff2
            });
            //3?
            points.Add(new Point
            {
                X = endPosition.X - dXDiff2,
                Y = endPosition.Y + dYDiff2
            });

            // add start point again to finish the shape off!
            //4
            points.Add(new Point
            {
                X = startPosition.X - dXDiff,
                Y = startPosition.Y + dYDiff
            });

            return points;
        }

        // TODO: merge these two methods? only difference is how start/end is handled,
        // TODO:   i.e. whether startpoint is center, or startpoint is left or right corner

        /// <summary>
        ///
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="startAngle"></param>
        /// <param name="startTrackWidth">Start track width in TrackWidthUnits</param>
        /// <param name="endPosition"></param>
        /// <param name="endAngle"></param>
        /// <param name="endTrackWidth">Width of end of section in TrackWidthUnits</param>
        /// <param name="vergeSide"></param>
        /// <returns></returns>
        public static PointCollection GetStraightVergePointCollection(Point startPosition, int startAngle, int startTrackWidth, Point endPosition, int endAngle, int endTrackWidth, VergeSide vergeSide)
        {
            // left or right from center point, based on vergeSide

            var points = new PointCollection();
            points.Add(new Point(startPosition.X, startPosition.Y));

            double diffStartX = Math.Cos(Math.PI - startAngle * LayoutConstants.AngleScaleRadians) * startTrackWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double diffStartY = Math.Sin(Math.PI - startAngle * LayoutConstants.AngleScaleRadians) * startTrackWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            double diffEndX = Math.Cos(Math.PI - endAngle * LayoutConstants.AngleScaleRadians) * endTrackWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double diffEndY = Math.Sin(Math.PI - endAngle * LayoutConstants.AngleScaleRadians) * endTrackWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            switch (vergeSide)
            {
                case VergeSide.Left:
                    points.Add(new Point(startPosition.X + diffStartX, startPosition.Y - diffStartY));
                    points.Add(new Point(endPosition.X + diffEndX, endPosition.Y - diffEndY));
                    break;
                case VergeSide.Right:
                    points.Add(new Point(startPosition.X - diffStartX, startPosition.Y + diffStartY));
                    points.Add(new Point(endPosition.X - diffEndX, endPosition.Y + diffEndY));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(vergeSide), vergeSide, null);
            }

            points.Add(new Point(endPosition.X, endPosition.Y));

            // add first point again to close rectangle
            points.Add(new Point(startPosition.X, startPosition.Y));

            return points;
        }

        public enum VergeSide
        {
            Left = 0,
            Right = 1
        }



        // TODO: copied from TrackSectionLayoutData
        public static PointCollection GetCurvio(double Radius, int StartWidth, int StartAngle, int EndWidth, int EndAngle, Point CircleCenterForCurve, int Curvature)
        {
            var points = new PointCollection();

            // TODO: outer vs inner must surely depend on whether it's left or right...?
            // just call it leftside/rightside?
            double dOuterRadiusStart = Radius + StartWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double dInnerRadiusStart = Radius - StartWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double dOuterRadiusEnd = Radius + EndWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double dInnerRadiusEnd = Radius - EndWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double dStartAngle = Math.PI - StartAngle * LayoutConstants.AngleScaleRadians;
            double dEndAngle = Math.PI - EndAngle * LayoutConstants.AngleScaleRadians;
            double dCenterX = CircleCenterForCurve.X;
            double dCenterY = CircleCenterForCurve.Y;

            if (Curvature > 0)
            {
                // right
                var outerLine = BuildArc(dCenterX, dCenterY, dStartAngle, dEndAngle, dOuterRadiusStart, dOuterRadiusEnd);

                foreach (var p in outerLine)
                {
                    points.Add(p);
                }

                var innerLine = BuildArc(dCenterX, dCenterY, dStartAngle, dEndAngle, dInnerRadiusStart, dInnerRadiusEnd);

                foreach (var p in innerLine.Reverse())
                {
                    points.Add(p);
                }
            }
            else
            {
                // left
                var outerLine = BuildArc(dCenterX, dCenterY, dEndAngle, dStartAngle, dOuterRadiusStart, dOuterRadiusEnd);
                // TODO: below fixes the track width but makes all left-hand turns jagged
                //var outerLine = BuildArc(dCenterX, dCenterY, dStartAngle, dEndAngle, dOuterRadiusStart, dOuterRadiusEnd);

                foreach (var p in outerLine)
                {
                    points.Add(p);
                }



                var innerLine = BuildArc(dCenterX, dCenterY, dEndAngle, dStartAngle, dInnerRadiusStart, dInnerRadiusEnd);
                // TODO: below fixes the track width but makes all left-hand turns jagged
                //var innerLine = BuildArc(dCenterX, dCenterY, dStartAngle, dEndAngle, dInnerRadiusStart, dInnerRadiusEnd);

                foreach (var p in innerLine.Reverse())
                {
                    points.Add(p);
                }
            }

            return points;
        }

        // TODO: only handles right-hand corners properly, left hand corners need to pass in "reverse" values by
        // flipping the start and end values to not draw the corner in a single line, however this means that width changes are reversed
        public static PointCollection BuildArc(double x, double y, double startAngle, double endAngle, double startRadius, double endRadius)
        {
            //if (startAngle > dEndAngle)
            //{
            //    // going left?
            //    return BuildArcLeft(x, y, startAngle, dEndAngle, dStartRadius, dEndRadius);
            //}

            double workEndAngle = endAngle;

            while (workEndAngle - startAngle > Math.PI * 2)
            {
                workEndAngle -= Math.PI * 2;
            }

            // radius change per angle unit (radians)
            double a = (endRadius - startRadius) / (workEndAngle - startAngle);

            double dAngleIncrement;
            if (Math.Abs(startRadius) > 50)
            {
                dAngleIncrement = Math.Abs(2 * Math.Asin(5.0 / startRadius));
            }
            else
            {
                //dAngleIncrement = 2 * Math.PI / 32;   // original
                dAngleIncrement = 2 * Math.PI / 64;
            }

            double nPosXStart = x + (startRadius * Math.Cos(startAngle));
            double nPosYStart = y - (startRadius * Math.Sin(startAngle));


            var points = new PointCollection
            {
                new Point(nPosXStart, nPosYStart)
            };

            // draw a number of short lines
            double dAngle = startAngle + dAngleIncrement;
            while (dAngle < workEndAngle)
            {
                // calculate radius for current angle
                double radius = startRadius + (dAngle - startAngle) * a;

                // calculate point on circle for this angle
                double nPosX = x + radius * Math.Cos(dAngle);
                double nPosY = y - radius * Math.Sin(dAngle);

                points.Add(new Point(nPosX, nPosY));

                dAngle = dAngle + dAngleIncrement;
            }

            // add final point
            double nx = x + (endRadius * Math.Cos(workEndAngle));
            double ny = y - (endRadius * Math.Sin(workEndAngle));
            points.Add(new Point(nx, ny));

            return points;
        }
    }
}
