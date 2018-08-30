using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.TrackStuff
{
    public class TrackSectionLayoutData // TODO: name?
    {
        private readonly TrackSectionViewModel _section;

        public TrackSectionLayoutData(TrackSectionViewModel section)
        {
            _section = section;

            StartPosition = new Point();

            // TODO: calculate right away? or no constructor but static method instead?

            //StartPoints = new PointCollection();
            //StartPoints.SetupWithPoints(2);
        }

        // helper properties for prettier code
        public int Length => _section.Length;
        public int Curvature => _section.Curvature;

        public TrackSectionViewModel TrackSectionBuddy => _section;

        /// <summary>
        /// Gets the coordinates for the start of the section (center of track?)
        /// </summary>
        /// <returns></returns>
        public Point StartPosition { get; private set; }
        // call it StartCenterPosition?

        /// <summary>
        /// Gets the coordinates for the end of the section (center of track?)
        /// </summary>
        /// <returns></returns>
        public Point EndPosition { get; private set; }


        /// <summary>
        /// Gets the coordinates for the center of the circle that defines a curved segment.
        /// </summary>
        public Point CircleCenterForCurve { get; private set; }


        // coordinates for point where track width change ends
        /// <summary>
        /// Gets the coordinates at the center of the track where a track width change ends.
        /// </summary>
        public Point WidthChangeEndPosition { get; private set; }

        /// <summary>
        /// when track width changes across segment border
        /// </summary>
        public int WidthChangeLength { get; private set; }

        /// <summary>
        /// when track width changes across segment border
        /// </summary>
        public int WidthChangeEnd { get; private set; }

        /// <summary>
        /// Gets the track width at the start of the section.
        /// </summary>
        public int StartWidth { get; private set; }

        // in track-width units
        public int LeftVergeStartWidth
        {
            get
            {
                return GetVergeWidth(LeftVergeStartWidthRaw, StartWidth);
            }
        }

        public int LeftVergeEndWidth
        {
            get
            {
                return GetVergeWidth(LeftVergeEndWidthRaw, EndWidth);
            }
        }

        public int RightVergeStartWidth
        {
            get
            {
                return GetVergeWidth(RightVergeStartWidthRaw, StartWidth);
            }
        }

        public int RightVergeEndWidth
        {
            get
            {
                return GetVergeWidth(RightVergeEndWidthRaw, EndWidth);
            }
        }

        private int GetVergeWidth(int vergeRaw, int trackWidth)
        {
            // noo, 50=same width as FULL track, i.e. twice trackWidth?

            // huh?
            //float pct = vergeRaw * 2 / 100;
            //double pct = vergeRaw * 0.02;
            double pct = vergeRaw * 0.04; //?
            var vergeWidth = Convert.ToInt32(trackWidth * pct) + trackWidth;
            return vergeWidth;
        }

        // TODO: all these names are a mess!!!

        public int LeftVergeStartWidthRaw { get; set; }  // ie PREVIOUS section Leftverge width 
        public int LeftVergeEndWidthRaw { get; set; }  // ie section LeftVerge width
        public int RightVergeStartWidthRaw { get; set; }  // ie PREVIOUS section rightverge width 
        public int RightVergeEndWidthRaw { get; set; }  // ie section rightVerge width



        /// <summary>
        /// Gets the track width at the end of the section.
        /// </summary>
        public int EndWidth { get; private set; }


        /// <summary>
        /// Gets the angle direction at the start of the section.
        /// </summary>
        public int StartAngle { get; private set; }

        /// <summary>
        /// Gets the angle direction at the end of the section.
        /// </summary>
        public int EndAngle { get; private set; }

        /// <summary>
        /// Gets the radius of circle (middle of track)
        /// </summary>
        /// <returns></returns>
        public double Radius { get; private set; }

        /// <summary>
        /// Calculate section coordinates and angles depending on start values.
        /// </summary>
        /// <param name="initialPositionX">X position of end of previous section</param>
        /// <param name="initialPositionY">Y position of end of previous section</param>
        /// <param name="initialWidth">End width of previous section</param>
        /// <param name="initialAngle">End angle of previous section</param>
        /// <param name="widthChangeLength"></param>
        /// <param name="widthChangeEnd"></param>
        public void CalculateLayout(double initialPositionX, double initialPositionY, int initialWidth, int initialAngle, int widthChangeLength, int widthChangeEnd)
        {
            StartPosition = new Point(initialPositionX, initialPositionY);
            StartAngle = initialAngle;
            StartWidth = initialWidth;

            // resulting direction
            EndAngle = StartAngle - Length * Curvature;

            // end coordinates
            double angle = StartAngle * LayoutConstants.AngleScaleRadians;

            if (Curvature == 0)
            {
                // Straight
                double endX = StartPosition.X - Length * Math.Sin(angle);
                double endY = StartPosition.Y + Length * Math.Cos(angle);
                EndPosition = new Point(endX, endY);
            }
            else
            {
                // Curve: first calculate center of circle
                Radius = 1 / (Curvature * LayoutConstants.AngleScaleRadians);   // TODO: remove "1/..." if flipping Curvature?
                
                double circleCenterX = StartPosition.X + Radius * Math.Cos(angle);
                double circleCenterY = StartPosition.Y + Radius * Math.Sin(angle);
                CircleCenterForCurve = new Point(circleCenterX, circleCenterY);

                // from center, calculate end point
                angle = EndAngle * LayoutConstants.AngleScaleRadians;
                double endX = circleCenterX - Radius * Math.Cos(angle);
                double endY = circleCenterY - Radius * Math.Sin(angle);
                EndPosition = new Point(endX, endY);
            }


            // TODO: from outside?
            var cmd = _section.Commands.SingleOrDefault(c => c.Command == 0x85); // TrackCommand
            if (cmd != null)
            {
                widthChangeLength = cmd.Arguments[1].Value;
                widthChangeEnd = cmd.Arguments[2].Value;  // width on each side of middle, in width scales = 0.0047625 meters
            }

            // track width change detected
            if (widthChangeLength > 0)
            {
                if (widthChangeLength > Length)
                {
                    // width change to be continued on next segment
                    // calculate width at end of segment
                    EndWidth = StartWidth + Length * (widthChangeEnd - StartWidth) / widthChangeLength;
                    // store values needed by next segment
                    WidthChangeEnd = widthChangeEnd;
                    WidthChangeLength = widthChangeLength - Length;
                    // need flag to indicate width change ends in middle of segment? @@@
                }
                else
                {
                    // whole change takes place on this segment
                    if (widthChangeLength < Length)
                    {
                        // change complete in the middle of the segment.
                        // calculate point where the change ends.
                        if (Curvature != 0)
                        {
                            // curved segment
                            double dAngleMid = StartAngle + widthChangeLength * Curvature;
                            double widthChangeX = CircleCenterForCurve.X - Radius * Math.Cos(dAngleMid * LayoutConstants.AngleScaleRadians);
                            double widthChangeY = CircleCenterForCurve.Y + Radius * Math.Sin(dAngleMid * LayoutConstants.AngleScaleRadians);
                            WidthChangeEndPosition = new Point(widthChangeX, widthChangeY);
                        }
                        else
                        {
                            // straight
                            double widthChangeX = StartPosition.X - widthChangeLength * Math.Sin(StartAngle * LayoutConstants.AngleScaleRadians);
                            double widthChangeY = StartPosition.Y + widthChangeLength * Math.Cos(StartAngle * LayoutConstants.AngleScaleRadians);
                            WidthChangeEndPosition = new Point(widthChangeX, widthChangeY);
                        }
                    }
                    EndWidth = widthChangeEnd;
                    // nothing to do on next segment
                    WidthChangeLength = 0;
                    WidthChangeEnd = 0;
                }
            }
            else
            {
                // No width change
                EndWidth = StartWidth;
                // nothing to do on next segment
                WidthChangeLength = 0;
                WidthChangeEnd = 0;
            }


            var wp = BuildSectionPointCollection();

            SectionPoints = wp;
        }

        // TODO: separate properties for:

        //public PointCollection StartPoints { get; private set; }

        // NearPoints (always two) StartPoints?
        // LeftSidePoints (two for straights, ? for curves)
        // RightSidePoints (two for straights, ? for curves)
        // or InnerPoints/OuterPoints? or both
        // FarPoints (always two) EndPoints?
        // SectionPoints (combo of all, starting from left/start -> left side -> far -> right-side (reversed)

        /// <summary>
        /// Gets a PointCollection containing all the points that define the track section, describing a "circle" clockwise from start/left.
        /// </summary>
        public PointCollection SectionPoints { get; private set; }

        private PointCollection BuildSectionPointCollection()
        {
            // TODO: calculate stuff completely outside of the object,
            // we will need to navigate the list of sections to draw properly!
            // or is that only needed when calculating the verges? yeah nah yeah!

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

            if (Curvature == 0)
            {

                // width here == half track

                // straight
                points = LayoutCalculator.GetStraightTrackPointCollection(StartAngle, StartPosition, StartWidth, EndAngle, EndPosition, EndWidth);
            }
            else
            {
                // curve!
                // TODO: this is pretty much identical to LayoutCalculator.GetCurvio() !!!

                // what about widthmid?
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

                // TODO: for curvature <> 0, final point is never added!
            }

            return points;
        }


        // TODO: only handles right-hand corners properly
        private PointCollection BuildArc(double x, double y, double startAngle, double dEndAngle, double dStartRadius, double dEndRadius)
        {
            return LayoutCalculator.BuildArc(x, y, startAngle, dEndAngle, dStartRadius, dEndRadius);

            //while (dEndAngle - startAngle > Math.PI * 2)
            //{
            //    dEndAngle -= Math.PI * 2;
            //}

            //// radius change per angle unit (radiens)
            //double a = (dEndRadius - dStartRadius) / (dEndAngle - startAngle);

            //double dAngleIncrement;
            //if (Math.Abs(dStartRadius) > 50)
            //{
            //    dAngleIncrement = Math.Abs(2 * Math.Asin(5.0 / dStartRadius));
            //}
            //else
            //{
            //    //dAngleIncrement = 2 * Math.PI / 32;   // original
            //    dAngleIncrement = 2 * Math.PI / 64;
            //}

            //double nPosXStart = x + (dStartRadius * Math.Cos(startAngle));
            //double nPosYStart = y - (dStartRadius * Math.Sin(startAngle));


            //var points = new PointCollection
            //{
            //    new Point(nPosXStart, nPosYStart)
            //};

            //// draw a number of short lines
            //double dAngle = startAngle + dAngleIncrement;
            //while (dAngle < dEndAngle)
            //{
            //    // calculate radius for current angle
            //    double radius = dStartRadius + (dAngle - startAngle) * a;

            //    // calculate point on circle for this angle
            //    double nPosX = x + radius * Math.Cos(dAngle);
            //    double nPosY = y - radius * Math.Sin(dAngle);

            //    points.Add(new Point(nPosX, nPosY));

            //    dAngle = dAngle + dAngleIncrement;
            //}

            //// add final point
            //double nx = x + (dEndRadius * Math.Cos(dEndAngle));
            //double ny = y - (dEndRadius * Math.Sin(dEndAngle));
            //points.Add(new Point(nx, ny));

            //return points;
        }
    }
}
