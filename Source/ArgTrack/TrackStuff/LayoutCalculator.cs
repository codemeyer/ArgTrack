using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ArgData.Entities;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.TrackStuff
{
    public class LayoutCalculator
    {
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

                section.Layout = ws;

                layoutSections.Add(ws);
            }

            // TODO: set flag where pitlane starts in code above?

            int index = 0;
            foreach (var section in track.TrackSections)
            {
                foreach (var cmd in section.Commands)
                {
                    if (cmd.Command == 0x86)
                    {
                        // HELO!
                        index = track.TrackSections.IndexOf(section);
                    }
                }
            }

            var pitlaneSections = new List<TrackSectionLayoutData>();

            if (index > 0)
            {
                // TODO: draw pitlane
                var startSection = layoutSections[index];

                startWidth = LayoutConstants.PitLaneWidth;
                startAngle = startSection.StartAngle;


                // calculate pitlane "shift" along section where pitlane starts
                double pitlaneShift = startSection.StartWidth - LayoutConstants.PitLaneWidth;

                double pitLaneShiftX = Math.Cos(Math.PI - startSection.StartAngle * LayoutConstants.AngleScaleRadians) * pitlaneShift / LayoutConstants.TrackWidthUnitsPerLengthUnit;
                double pitLaneShiftY = Math.Sin(Math.PI - startSection.StartAngle * LayoutConstants.AngleScaleRadians) * pitlaneShift / LayoutConstants.TrackWidthUnitsPerLengthUnit;

                if (track.TrackHeader.PitsSide == TrackSide.Right)
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

                    section.Layout = ws;
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

            calculateCCLine(ccLineLayoutList, layoutSections);



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

        // TODO: copied from TrackSectionLayoutData

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

        // doesnt quite owkr....

        public static PointCollection BuildArcLeft(double x, double y, double startAngle, double dEndAngle, double dStartRadius, double dEndRadius)
        {
            // original
            //while (dEndAngle - startAngle > Math.PI * 2)
            //{
            //    dEndAngle -= Math.PI * 2;
            //}

            // IN HERE dEndAngle is ALWAYS lower than startAngle!

            var workEndAngle = dEndAngle;
            double diffLollipop = Math.Abs(workEndAngle - startAngle);

            while (diffLollipop > Math.PI * 2)
            {
                diffLollipop -= Math.PI * 2;

                //diffLollipop = Math.Abs(workEndAngle - startAngle);
            }
            //while (workEndAngle - startAngle > Math.PI * 2)
            //{
            //    workEndAngle -= Math.PI * 2;
            //}

            diffLollipop = diffLollipop * -1;
            workEndAngle = diffLollipop;

            // fluip it?

            // hack
            //             dEndAngle = -2.035;

            // radius change per angle unit (radiens)
            double a = (dEndRadius - dStartRadius) / (workEndAngle - startAngle);

            double dAngleIncrement;
            if (Math.Abs(dStartRadius) > 50)
            {
                dAngleIncrement = Math.Abs(2 * Math.Asin(5.0 / dStartRadius));
            }
            else
            {
                //dAngleIncrement = 2 * Math.PI / 32;   // original
                dAngleIncrement = 2 * Math.PI / 64;
            }

            double nPosXStart = x + (dStartRadius * Math.Cos(startAngle));
            double nPosYStart = y - (dStartRadius * Math.Sin(startAngle));


            var points = new PointCollection
            {
                new Point(nPosXStart, nPosYStart)
            };

            // draw a number of short lines
            //double dAngle = startAngle + dAngleIncrement;
            double dAngle = startAngle - dAngleIncrement;
            while (dAngle > workEndAngle)
            {
                // calculate radius for current angle
                double radius = dStartRadius + (dAngle - startAngle) * a;

                // calculate point on circle for this angle
                double nPosX = x + radius * Math.Cos(dAngle);
                double nPosY = y - radius * Math.Sin(dAngle);

                points.Add(new Point(nPosX, nPosY));

                dAngle = dAngle - dAngleIncrement;
            }

            // add final point
            double nx = x + (dEndRadius * Math.Cos(workEndAngle));
            double ny = y - (dEndRadius * Math.Sin(workEndAngle));
            points.Add(new Point(nx, ny));

            return points;
        }





        // TODO: value types for TrackWidthUnit, TrackLengthUnit, etc



        double s_dANGLE_SCALE = (2 * Math.PI) / 65536;



        public void calculateCCLine(List<ComputerCarLineLayoutData> ccLine, List<TrackSectionLayoutData> trackSectionLayoutData)
        {
            var m_TrackSegments = trackSectionLayoutData;
            //ObservableCollection<TrackSectionViewModel> m_TrackSegments = TrackViewModelPasserArounder.TrackViewModel.TrackSections;
            //ObservableCollection<BestLineSegmentViewModel> m_CCLine = TrackViewModelPasserArounder.TrackViewModel.BestLineSegments;
            var m_CCLine = ccLine;

            if (m_CCLine.Count == 0)
                return;

            // To calculate the CCline, both CCline data itself and
            // track segment data is needed.
            //BestLineSegmentViewModel ccLineSegment;
            ComputerCarLineLayoutData ccLineSegment;
            //TrackSectionViewModel trackSegment;
            TrackSectionLayoutData trackSegment;


            int nCumulatedTrackTlu = 0, nCumulatedCCLineTlu = 0;
            //int nTrackSegmentIndex = 1, nCCLineSegmentIndex = 1;
            int nTrackSegmentIndex = 0, nCCLineSegmentIndex = 0;
            double dXPos, dYPos, dOffset, dAngleStart, dAngleEnd, dRadius;

            // Get the first segments
            ccLineSegment = m_CCLine[nCCLineSegmentIndex];
            nCCLineSegmentIndex++;
            nCumulatedCCLineTlu = ccLineSegment.Length;
            trackSegment = m_TrackSegments[nTrackSegmentIndex];
            nTrackSegmentIndex++;
            //trackSegment = m_TrackSegments[nTrackSegmentIndex++];
            nCumulatedTrackTlu = trackSegment.Length;

            // First ccLine segment is related to the first track segment:
            // starting point is on start/finish line, with an offset to the
            // middle of the road.
            //dXPos = trackSegment.getPosXStart();
            //dYPos = trackSegment.getPosYStart();
            dXPos = trackSegment.StartPosition.X;
            dYPos = trackSegment.StartPosition.Y;
            // calculate offset from middle of the track
            //dOffset = ccLineSegment.getParam(0) / 1024.0; // given in width scales, i.e. 1/1024 tlu
            //                                              // calculate starting angle in radiens
            dOffset = ccLineSegment.Displacement / 1024.0; // given in width scales, i.e. 1/1024 tlu
                                                           // calculate starting angle in radiens
            dAngleStart = trackSegment.StartAngle * s_dANGLE_SCALE;
            ccLineSegment.setAngleStart(dAngleStart);
            ccLineSegment.setPosXStart(dXPos + Math.Cos(dAngleStart) * dOffset);
            ccLineSegment.setPosYStart(dYPos + Math.Sin(dAngleStart) * dOffset);

            do
            {
                // retrieve starting values
                dXPos = ccLineSegment.getPosXStart();
                dYPos = ccLineSegment.getPosYStart();
                dAngleStart = ccLineSegment.getAngleStart();

                // calculate radius
                dRadius = ccLineSegment.CalculateRadius();

                // calculate end point and end angle.
                // TLU value from CCLine segment is used for TrackSegments lookup, not
                // direct length calculations. Intersect CCLine segment with end
                // of corresponding track segment to find end point. For curved segments,
                // this calculation also gives the end angle.

                // Find the track segment where the CCLine segment ends.
                while (nCumulatedCCLineTlu > nCumulatedTrackTlu)
                {
                    trackSegment = m_TrackSegments[nTrackSegmentIndex++];
                    if (trackSegment == null)
                    {
                        // end of track reached
                        nCumulatedTrackTlu = nCumulatedCCLineTlu;
                        // previous TrackSegmentIndex pointed already behind the list and
                        // was incremented once. Last track segment is a dummy segment, so
                        // subtract 3 to get the last real segment.
                        nTrackSegmentIndex = nTrackSegmentIndex - 3;
                        if (nTrackSegmentIndex < 1)
                            nTrackSegmentIndex = 1;
                        trackSegment = m_TrackSegments[nTrackSegmentIndex];
                    }
                    else
                    {
                        nCumulatedTrackTlu += trackSegment.Length;
                    }
                }

                // Calculate angle of track segment border at the end of the ccLine segment.
                double dTrackAngle = trackSegment.EndAngle
                                     - (nCumulatedCCLineTlu - nCumulatedTrackTlu) * trackSegment.Curvature;
                dTrackAngle = dTrackAngle * s_dANGLE_SCALE;

                // Calculate point on the track segment border at the end of the ccLine segment.
                double dTrackPosX;
                double dTrackPosY;
                if (trackSegment.Curvature == 0)
                {
                    // straight
                    dTrackPosX = trackSegment.StartPosition.X
                                 - (nCumulatedCCLineTlu - nCumulatedTrackTlu + trackSegment.Length) * Math.Sin(dTrackAngle);
                    dTrackPosY = trackSegment.StartPosition.Y
                                 + (nCumulatedCCLineTlu - nCumulatedTrackTlu + trackSegment.Length) * Math.Cos(dTrackAngle);
                }
                else
                {
                    // curved: use center and radius for calculations
                    dTrackPosX = trackSegment.CircleCenterForCurve.X
                                 - trackSegment.Radius * Math.Cos(dTrackAngle);
                    dTrackPosY = trackSegment.CircleCenterForCurve.Y
                                 - trackSegment.Radius * Math.Sin(dTrackAngle);
                }

                // Intersect CCLine with end of track segment. Sets values
                // for end point and end angle in ccline segment.
                intersect(ccLineSegment, dTrackPosX, dTrackPosY, dTrackAngle);

                // get next ccline segment
                if (nCCLineSegmentIndex < m_CCLine.Count)
                {
                    // get end values of last segment
                    dXPos = ccLineSegment.getPosXEnd();
                    dYPos = ccLineSegment.getPosYEnd();
                    dAngleEnd = ccLineSegment.getAngleEnd();
                    // get next segment
                    ccLineSegment = m_CCLine[nCCLineSegmentIndex];
                    nCCLineSegmentIndex++;
                    // add tlu's to cumulated value
                    nCumulatedCCLineTlu += ccLineSegment.getTlu();
                    // transfer end values of previous segment to new one.
                    ccLineSegment.setPosXStart(dXPos);
                    ccLineSegment.setPosYStart(dYPos);
                    ccLineSegment.setAngleStart(dAngleEnd);
                }
                else
                    ccLineSegment = null;
            } while (ccLineSegment != null);
        }

        /**
    Intersect ccLine segment with end of track segment.
    Parameters: CCline segment
                point and angle of track at end of CCLine segment.
    Calculate end point and angle for ccLineSegment and store into segment.
*/
        //public void intersect(CCLineSegment ccLineSegment, double dTrackPosX, double dTrackPosY, double dTrackAngle)
        public void intersect(ComputerCarLineLayoutData ccLineSegment, double dTrackPosX, double dTrackPosY, double dTrackAngle)
        {
            double dRadius = ccLineSegment.getRadius();
            double dCClineAngle = ccLineSegment.getAngleStart();

            // Calculations based on code from Kristof Kaly-Kulai (DOS Track editor)
            if (ccLineSegment.isStraight())
            {
                ccLineSegment.setPosXCenter(0.0);
                ccLineSegment.setPosYCenter(0.0);
                // angle of segment can be modified by "shift" value
                ccLineSegment.setAngleStart(ccLineSegment.getAngleStart()
                                             - s_dANGLE_SCALE * ccLineSegment.getShift());
                // straight segment can not be shifted in driving direction
                ccLineSegment.setShiftSegment(null);
                double dAux;
                dAux = Math.Sin(ccLineSegment.getAngleStart()) * Math.Sin(dTrackAngle)
                       + Math.Cos(ccLineSegment.getAngleStart()) * Math.Cos(dTrackAngle);

                if (Math.Abs(dAux) < 1e-10)
                {
                    ccLineSegment.setS(100.0); // sense???
                    ccLineSegment.setLength(ccLineSegment.getTlu());
                }
                else
                {
                    ccLineSegment.setLength((Math.Cos(dTrackAngle) * (dTrackPosY - ccLineSegment.getPosYStart()))
                                             - (Math.Sin(dTrackAngle) * (dTrackPosX - ccLineSegment.getPosXStart())));
                    ccLineSegment.setS((-Math.Sin(ccLineSegment.getAngleStart()) * (dTrackPosY - ccLineSegment.getPosYStart())
                                         - Math.Cos(ccLineSegment.getAngleStart()) * (dTrackPosX - ccLineSegment.getPosXStart()))
                                        / dAux);
                }
                ccLineSegment.setAngleEnd(ccLineSegment.getAngleStart());
                ccLineSegment.setPosXEnd(dTrackPosX + ccLineSegment.getS() * Math.Cos(dTrackAngle));
                ccLineSegment.setPosYEnd(dTrackPosY + ccLineSegment.getS() * Math.Sin(dTrackAngle));
            }
            else
            {
                int i;
                double dS = 0.0;

                // ccLineSegment is curved
                // Check if shift value is present
                if (ccLineSegment.getShift() > 0)
                {
                    // Shift segment along driving direction. Insert small auxiliary
                    // segment to fill the gap.
                    var ccLineAux = new ComputerCarLineLayoutData(null); // always special type 1 (aux)
                                                                         // Shift segment is always straight
                    ccLineAux.setRadius(0.0);
                    ccLineAux.setPosXCenter(0.0);
                    ccLineAux.setPosYCenter(0.0);
                    // starts at starting position of current ccline segment
                    ccLineAux.setPosXStart(ccLineSegment.getPosXStart());
                    ccLineAux.setPosYStart(ccLineSegment.getPosYStart());
                    ccLineAux.setAngleStart(ccLineSegment.getAngleStart());
                    ccLineAux.setAngleEnd(ccLineSegment.getAngleStart());
                    // length is calculated from shift value
                    double dLength = ccLineSegment.getShift() / 256.0;
                    ccLineSegment.setLength(dLength);
                    ccLineSegment.setS(0.0);
                    // calculate end point
                    ccLineAux.setPosXEnd(ccLineAux.getPosXStart() - dLength * Math.Sin(ccLineAux.getAngleStart()));
                    ccLineAux.setPosYEnd(ccLineAux.getPosYStart() + dLength * Math.Cos(ccLineAux.getAngleStart()));
                    // store into current segment
                    ccLineSegment.setShiftSegment(ccLineAux);
                    // move current segment to end of shift segment
                    ccLineSegment.setPosXStart(ccLineAux.getPosXEnd());
                    ccLineSegment.setPosYStart(ccLineAux.getPosYEnd());
                }
                else
                {
                    // no shift segment
                    ccLineSegment.setShiftSegment(null);
                }

                // Calculate center of circle
                ccLineSegment.setPosXCenter(ccLineSegment.getPosXStart()
                                             + ccLineSegment.getRadius() * Math.Cos(ccLineSegment.getAngleStart()));
                ccLineSegment.setPosYCenter(ccLineSegment.getPosYStart()
                                             + ccLineSegment.getRadius() * Math.Sin(ccLineSegment.getAngleStart()));

                // calculate end points and angles
                double[] adAlfa = new double[2];  // variable names are terrible, but I cant figure out what they mean, so
                                                  // I kept the names of the DOS version.
                double[] adS = new double[2];
                double dCotTrackAngle, dA, dC, dF, dF2;
                if (Math.Abs(Math.Cos(dTrackAngle)) < Math.Abs(Math.Sin(dTrackAngle)))
                {
                    dCotTrackAngle = Math.Cos(dTrackAngle) / Math.Sin(dTrackAngle);
                    dA = 1.0 / Math.Sqrt(1 + dCotTrackAngle * dCotTrackAngle);
                    dC = dA * (dTrackPosX - ccLineSegment.getPosXCenter()
                                + dCotTrackAngle * (ccLineSegment.getPosYCenter() - dTrackPosY))
                            / ccLineSegment.getRadius();
                    dC = -dC; // negate sign
                    dF = Math.Atan2(-dA, dCotTrackAngle * dA);
                    if (Math.Abs(dC) > 1.0)
                    {
                        ccLineSegment.setS(100.0); // @@@ sense of value S??
                        ccLineSegment.setAngleEnd(ccLineSegment.getAngleStart());
                        ccLineSegment.setLength(1.0); // @@@ sense of length??
                        ccLineSegment.setPosXEnd(ccLineSegment.getPosXStart() - Math.Sin(ccLineSegment.getAngleStart()));
                        ccLineSegment.setPosYEnd(ccLineSegment.getPosYStart() + Math.Cos(ccLineSegment.getAngleStart()));
                        // Segment and all following segments get "invalid" marker (?)
                        ccLineSegment.setValid(false);
                        return;
                    }
                    else
                    {
                        dF2 = Math.Asin(dC);
                    }
                    adAlfa[0] = -dF2 - dF;
                    adS[0] = (ccLineSegment.getPosYCenter() - dTrackPosY
                               - ccLineSegment.getRadius() * Math.Sin(adAlfa[0]))
                              / Math.Sin(dTrackAngle);
                    adAlfa[1] = Math.PI + dF2 - dF;
                    adS[1] = (ccLineSegment.getPosYCenter() - dTrackPosY
                               - ccLineSegment.getRadius() * Math.Sin(adAlfa[1]))
                              / Math.Sin(dTrackAngle);
                }
                else
                {
                    double dTanTrackAngle;
                    dTanTrackAngle = Math.Tan(dTrackAngle);
                    dA = 1.0 / Math.Sqrt(1 + dTanTrackAngle * dTanTrackAngle);
                    dC = dA * (dTrackPosY - ccLineSegment.getPosYCenter()
                                + dTanTrackAngle * (ccLineSegment.getPosXCenter() - dTrackPosX))
                            / ccLineSegment.getRadius();
                    dC = -dC; // negate sign
                    dF = Math.Atan2(dTanTrackAngle * dA, -dA);
                    if (Math.Abs(dC) > 1.0)
                    {
                        // ccLine left track???
                        ccLineSegment.setS(100.0);
                        ccLineSegment.setLength(1.0);
                        ccLineSegment.setAngleEnd(ccLineSegment.getAngleStart());
                        ccLineSegment.setPosXEnd(ccLineSegment.getPosXStart() - Math.Sin(ccLineSegment.getAngleStart()));
                        ccLineSegment.setPosYEnd(ccLineSegment.getPosYStart() + Math.Cos(ccLineSegment.getAngleStart()));
                        // Mark segment and all followers as invalid (see above)
                        ccLineSegment.setValid(false);
                        return;
                    }
                    dF2 = Math.Asin(dC);
                    adAlfa[0] = -dF2 - dF;
                    adS[0] = (ccLineSegment.getPosXCenter() - dTrackPosX
                                 - ccLineSegment.getRadius() * Math.Cos(adAlfa[0]))
                               / Math.Cos(dTrackAngle);
                    adAlfa[1] = Math.PI + dF2 - dF;
                    adS[1] = (ccLineSegment.getPosXCenter() - dTrackPosX
                                 - ccLineSegment.getRadius() * Math.Cos(adAlfa[1]))
                               / Math.Cos(dTrackAngle);
                }

                // Possibly modify angles
                for (i = 0; i < 2; i++)
                {
                    while ((adAlfa[i] - ccLineSegment.getAngleStart()) > (2.0 * Math.PI))
                    {
                        adAlfa[i] -= 2.0 * Math.PI;
                    }
                    while ((adAlfa[i] - ccLineSegment.getAngleStart()) <= (-2.0 * Math.PI))
                    {
                        adAlfa[i] += 2.0 * Math.PI;
                    }
                    if (ccLineSegment.turnsRight())
                    {
                        while (-adAlfa[i] <= -ccLineSegment.getAngleStart())
                        {
                            adAlfa[i] -= 2.0 * Math.PI;
                        }
                    }
                    else
                    {
                        // turns left
                        while (adAlfa[i] <= ccLineSegment.getAngleStart())
                        {
                            adAlfa[i] += 2.0 * Math.PI;
                        }
                    }
                }

                if (((ccLineSegment.getPosYStart() - dTrackPosY) * Math.Cos(dTrackAngle)
                       - (ccLineSegment.getPosXStart() - dTrackPosX) * Math.Sin(dTrackAngle)) > 0.0)
                {
                    if ((ccLineSegment.turnsRight() && (-adAlfa[0] < -adAlfa[1]))
                         || (ccLineSegment.turnsLeft() && (adAlfa[0] < adAlfa[1])))
                    {
                        ccLineSegment.setAngleEnd(adAlfa[1]);
                        ccLineSegment.setS(adS[1]);
                    }
                    else
                    {
                        ccLineSegment.setAngleEnd(adAlfa[0]);
                        ccLineSegment.setS(adS[0]);
                    }
                }
                else
                {
                    if ((ccLineSegment.turnsRight() && (-adAlfa[0] < -adAlfa[1]))
                         || (ccLineSegment.turnsLeft() && (adAlfa[0] < adAlfa[1])))
                    {
                        ccLineSegment.setAngleEnd(adAlfa[0]);
                        ccLineSegment.setS(adS[0]);
                    }
                    else
                    {
                        ccLineSegment.setAngleEnd(adAlfa[1]);
                        ccLineSegment.setS(adS[1]);
                    }
                }
                // calculate length
                ccLineSegment.setLength(Math.Abs((ccLineSegment.getAngleEnd() - ccLineSegment.getAngleStart()) * ccLineSegment.getRadius()));

                // calculate end points once again from "S" value.
                // Places end point on end of track subsegment???
                ccLineSegment.setPosXEnd(dTrackPosX + ccLineSegment.getS() * Math.Cos(dTrackAngle));
                ccLineSegment.setPosYEnd(dTrackPosY + ccLineSegment.getS() * Math.Sin(dTrackAngle));
            }
        }

    }
}
