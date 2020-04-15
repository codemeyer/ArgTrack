using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ArgTrack.Features.TrackDrawing
{
    // TODO: split into to two classes,
    // one that turns List<LayoutSectionData> into List<RoadLayoutSectionData>
    // one that turns List<RoadLayoutSectionData> into ObservableCollection<PointData> ?

    public static class RoadLayoutCalculator
    {
        /// <summary>
        /// Calculates all points for either track or pit lane.
        /// </summary>
        public static ObservableCollection<PointData> Calculate(List<LayoutSectionData> layoutSections, short trackStartWidth, DrawingLayout drawingLayout)
        {
            var roadLayoutSections = CalculateRoadSections(layoutSections, trackStartWidth);

            // adds "styling" to the points...
            var pointDataList = ConvertRoadLayoutToPointDataList(roadLayoutSections, drawingLayout);

            return pointDataList;
        }

        public static ObservableCollection<PointData> Calculate(List<RoadLayoutSectionData> roadLayoutSections, DrawingLayout drawingLayout)
        {
            // adds "styling" to the points...
            var pointDataList = ConvertRoadLayoutToPointDataList(roadLayoutSections, drawingLayout);

            return pointDataList;
        }


        public static List<RoadLayoutSectionData> CalculateRoadSections(List<LayoutSectionData> layoutSections, short trackStartWidth)
        {
            int nWidthLength = 0;
            int nWidthEnd = 0;
            int startWidth = trackStartWidth;

            var roadSections = new List<RoadLayoutSectionData>();

            foreach (var section in layoutSections)
            {
                var layout = CalculateLayout(section, startWidth, nWidthLength, nWidthEnd);

                // start stuff for NEXT section
                //dPosX = layout.EndPosition.X;
                //dPosY = layout.EndPosition.Y;
                startWidth = layout.EndWidth;
                //startAngle = layout.EndAngle;
                nWidthLength = layout.WidthChangeLength;
                nWidthEnd = layout.WidthChangeEnd;

                roadSections.Add(layout);
            }

            return roadSections;
        }




        // TODO: place all these together in another location?

        public static DrawingLayout TrackDrawingLayout
        {
            get
            {
                return new DrawingLayout
                {
                    StrokeThickness = 0.1,
                    StrokeBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FillBrush = new SolidColorBrush(Color.FromRgb(0x89, 0x89, 0x85))
                };
            }
        }

        public static DrawingLayout PitLaneDrawingLayout
        {
            get
            {
                return new DrawingLayout
                {
                    StrokeThickness = 0.1,
                    StrokeBrush = new SolidColorBrush(Color.FromRgb(255, 255, 0)),
                    FillBrush = new SolidColorBrush(Color.FromRgb(0x89, 0x89, 0x85))
                };
            }
        }


        /// <summary>
        /// Calculates the full track (incl. width) based on the center point LayoutSectionData provided.
        /// </summary>
        /// <param name="layoutData"></param>
        /// <param name="initialWidth"></param>
        /// <param name="widthChangeLength"></param>
        /// <param name="widthChangeEnd"></param>
        /// <returns></returns>
        private static RoadLayoutSectionData CalculateLayout(LayoutSectionData layoutData, int initialWidth, int widthChangeLength, int widthChangeEnd)
        {
            var t2 = new RoadLayoutSectionData(layoutData)
            {
                StartWidth = initialWidth
            };

            // TODO: from outside?
            var cmd = layoutData.RelatedSection.Commands.SingleOrDefault(c => c.Command == 0x85); // TrackCommand for width change
            if (cmd != null)
            {
                widthChangeLength = cmd.Arguments[1].Value;
                widthChangeEnd = cmd.Arguments[2].Value;  // width on each side of middle, in width scales = 0.0047625 meters
            }

            // track width change detected
            if (widthChangeLength > 0)
            {
                if (widthChangeLength > t2.Length)
                {
                    // width change to be continued on next segment
                    // calculate width at end of segment
                    t2.EndWidth = t2.StartWidth + t2.Length * (widthChangeEnd - t2.StartWidth) / widthChangeLength;
                    // store values needed by next segment
                    t2.WidthChangeEnd = widthChangeEnd;
                    t2.WidthChangeLength = widthChangeLength - t2.Length;
                    // need flag to indicate width change ends in middle of segment? @@@
                }
                else
                {
                    // whole change takes place on this segment
                    if (widthChangeLength < t2.Length)
                    {
                        // change complete in the middle of the segment.
                        // calculate point where the change ends.
                        if (t2.Curvature != 0)
                        {
                            // curved segment
                            double dAngleMid = t2.StartAngle + widthChangeLength * t2.Curvature;
                            double widthChangeX = t2.CircleCenterForCurve.X - t2.Radius * Math.Cos(dAngleMid * LayoutConstants.AngleScaleRadians);
                            double widthChangeY = t2.CircleCenterForCurve.Y + t2.Radius * Math.Sin(dAngleMid * LayoutConstants.AngleScaleRadians);
                            t2.WidthChangeEndPosition = new Point(widthChangeX, widthChangeY);
                        }
                        else
                        {
                            // straight
                            double widthChangeX = t2.StartPosition.X - widthChangeLength * Math.Sin(t2.StartAngle * LayoutConstants.AngleScaleRadians);
                            double widthChangeY = t2.StartPosition.Y + widthChangeLength * Math.Cos(t2.StartAngle * LayoutConstants.AngleScaleRadians);
                            t2.WidthChangeEndPosition = new Point(widthChangeX, widthChangeY);
                        }
                    }

                    t2.EndWidth = widthChangeEnd;
                    // nothing to do on next segment
                    t2.WidthChangeLength = 0;
                    t2.WidthChangeEnd = 0;
                }
            }
            else
            {
                // No width change
                t2.EndWidth = t2.StartWidth;
                // nothing to do on next segment
                t2.WidthChangeLength = 0;
                t2.WidthChangeEnd = 0;
            }

            return t2;
        }


        private static ObservableCollection<PointData> ConvertRoadLayoutToPointDataList(List<RoadLayoutSectionData> roadSections, DrawingLayout drawingLayout)
        {
            var z = new ObservableCollection<PointData>();

            foreach (var road in roadSections)
            {
                var x = BuildSectionPointCollectionFromRoadLayout(road);

                road.RelatedSection.SectionPoints = x;

                var y = LayoutToPointsConverter.Convert(x, drawingLayout);

                z.AddRange(y);
            }

            return z;
        }

        private static PointCollection BuildSectionPointCollectionFromRoadLayout(RoadLayoutSectionData layoutSection)
        {
            PointCollection points;

            if (layoutSection.Curvature == 0)
            {
                // width here == half track

                // straight
                points = GetStraightTrackPointCollection(layoutSection.StartAngle, layoutSection.StartPosition, layoutSection.StartWidth, layoutSection.EndAngle, layoutSection.EndPosition, layoutSection.EndWidth);
            }
            else
            {
                // curve!
                points = GetCurve(layoutSection);
            }

            return points;
        }

        private static PointCollection GetStraightTrackPointCollection(int startAngle, Point startPosition,
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

        private static PointCollection GetCurve(RoadLayoutSectionData t)
        {
            var points = new PointCollection();
            // TODO: this is pretty much identical to LayoutCalculator.GetCurvio() !!!

            // what about widthmid?
            double dOuterRadiusStart = t.Radius + t.StartWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double dInnerRadiusStart = t.Radius - t.StartWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double dOuterRadiusEnd = t.Radius + t.EndWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double dInnerRadiusEnd = t.Radius - t.EndWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double dStartAngle = Math.PI - t.StartAngle * LayoutConstants.AngleScaleRadians;
            double dEndAngle = Math.PI - t.EndAngle * LayoutConstants.AngleScaleRadians;
            double dCenterX = t.CircleCenterForCurve.X;
            double dCenterY = t.CircleCenterForCurve.Y;

            if (t.Curvature > 0)
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
                // TODO: below fixes the track width but makes all left-hand turns jagged
                //var outerLine = LayoutCalculator.BuildArc(dCenterX, dCenterY, dStartAngle, dEndAngle, dOuterRadiusStart, dOuterRadiusEnd);

                foreach (var p in outerLine)
                {
                    points.Add(p);
                }



                var innerLine = LayoutCalculator.BuildArc(dCenterX, dCenterY, dEndAngle, dStartAngle, dInnerRadiusStart, dInnerRadiusEnd);
                // TODO: below fixes the track width but makes all left-hand turns jagged
                //var innerLine = LayoutCalculator.BuildArc(dCenterX, dCenterY, dStartAngle, dEndAngle, dInnerRadiusStart, dInnerRadiusEnd);

                foreach (var p in innerLine.Reverse())
                {
                    points.Add(p);
                }
            }

            // TODO: for curvature <> 0, final point is never added!

            return points;
        }

    }
}
