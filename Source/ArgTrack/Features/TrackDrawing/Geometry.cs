using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ArgTrack.Features.TrackDrawing
{
    /// <summary>
    /// Lots of geometry functions.
    /// </summary>
    public static class Geometry
    {
        /// <summary>
        /// Given a start position, an angle and a distance, will return the new position going in a
        /// </summary>
        /// <param name="startPosition">Starting position.</param>
        /// <param name="angle">Angle in radians where 0=N, -1.57 is E</param>
        /// <param name="distance">Distance in any unit.</param>
        /// <returns></returns>
        public static Point GetLegacyPointPerpendicular(Point startPosition, double angle, double distance)
        {
            double diffX = Math.Cos(angle) * distance;
            double diffY = Math.Sin(angle) * distance;

            return new Point
            {
                X = startPosition.X + diffX,
                Y = startPosition.Y + diffY
            };
        }

        /// <summary>
        /// Given a start position, an angle and a distance, will return the new position going in a
        /// </summary>
        /// <param name="startPosition">Starting position.</param>
        /// <param name="angle">Angle in radians where 0=N</param>
        /// <param name="distance">Distance in any unit.</param>
        /// <returns></returns>
        public static Point GetPointPerpendicular(Point startPosition, double angle, double distance)
        {
            double diffX = Math.Cos(angle) * distance;
            double diffY = Math.Sin(angle) * distance;

            return new Point
            {
                X = startPosition.X + diffX,
                Y = startPosition.Y - diffY
            };
        }

        // TODO: redo this so that the angle value matches the existing calculation...

        /// <summary>
        /// Given a starting position, an angle, and a distance, will return the new position going forward (or backward).
        /// </summary>
        /// <param name="startPosition">Starting position.</param>
        /// <param name="angle">Angle in radians, where 0 is N, -1.57 (!!!) is E.</param>
        /// <param name="distance">Distance in any unit.</param>
        /// <returns></returns>
        public static Point GetLegacyPoint(Point startPosition, double angle, double distance)
        {
            double diffX = Math.Cos(Math.PI / 2 - angle) * distance;
            double diffY = Math.Sin(Math.PI / 2 - angle) * distance;

            return new Point
            {
                X = startPosition.X - diffX,
                Y = startPosition.Y + diffY
            };
        }


        /// <summary>
        /// Given a starting position, an angle, and a distance, will return the new position going forward (or backward).
        /// </summary>
        /// <param name="startPosition">Starting position.</param>
        /// <param name="angle">Angle in radians, where 0 is N, 1.57 is E.</param>
        /// <param name="distance">Distance in any unit.</param>
        /// <returns></returns>
        public static Point GetPoint(Point startPosition, double angle, double distance)
        {
            double diffX = Math.Cos(Math.PI / 2 - angle) * distance;
            double diffY = Math.Sin(Math.PI / 2 - angle) * distance;

            return new Point
            {
                X = startPosition.X + diffX,
                Y = startPosition.Y + diffY
            };
        }



        // name: GameCurvature?
        public static double GetRadiusForCurvature(int curvature)
        {
            return 1 / (curvature * LayoutConstants.AngleScaleRadians);
        }



        public static Point GetLegacyCircleCenter(Point position, double radius, double angle)
        {
            double circleCenterX = position.X + radius * Math.Cos(angle);
            double circleCenterY = position.Y + radius * Math.Sin(angle);

            return new Point(circleCenterX, circleCenterY);
        }


        public static Point GetLegacyCurveEndpoint(Point circleCenter, double radius, double endAngle)
        {
            double endX = circleCenter.X - radius * Math.Cos(endAngle);
            double endY = circleCenter.Y - radius * Math.Sin(endAngle);

            return new Point(endX, endY);
        }



        // curvature =
        // Sector Arc 	short 	Angle value as a signed integer to be divided by 65536/360 = 182.04444 (PER LENGTH UNIT)

        // first corner PHO

        // 3 x
        // len 2
        // curv 2731

        // using above formula, (2731 / 182) = 15, and corner is 6 units long, i.e. 15 * 6 == 90 (!)
        // TODO: calculate and display this in the user interface somewhere


        // angle = layout.StartAngle * LayoutConstants.AngleScaleRadians  (=2 * Math.PI / 65536)


        // endangle 0 =  -5462
        // angle 0

        // endangle 1 = -10942
        // angle -0.52366269146446076

        // endangle 2 = -16386  (90 grader???)  (antar att 16384 _egentligen_ är 90 grader)
        // angle -1.0473253829289215

        // negative == right ???



        public static PointCollection BuildArcForLeftVerge(Point circleCenter, double startAngle, double endAngle, double radius, int startWidth, int endWidth)
        {
            if (startAngle > endAngle)
            {
                // turns right
                return BuildTrackArcRightOuter(circleCenter, startAngle, endAngle, radius, startWidth, endWidth);
            }
            else
            {
                // turns left
                return BuildTrackArcLeftInner(circleCenter, startAngle, endAngle, radius, startWidth, endWidth);
            }
        }

        public static PointCollection BuildArcForRightVerge(Point circleCenter, double startAngle, double endAngle, double radius, int startWidth, int endWidth)
        {
            if (startAngle > endAngle)
            {
                // turns right
                return BuildTrackArcRightInner(circleCenter, startAngle, endAngle, radius, startWidth, endWidth);
            }
            else
            {
                // turns left
                return BuildTrackArcLeftOuter(circleCenter, startAngle, endAngle, radius, startWidth, endWidth);
            }
        }


        // while we still have the "legacy" calculation, left-hand turns will INCREASE the angle
        // startWidth endWidth handling here is based on how TRACK sections are drawn, not how VERGES are drawn

        public static PointCollection BuildTrackArcRightInner(Point circleCenter, double startAngle, double endAngle, double radius, int startWidth, int endWidth)
        {
            double dStartAngle = GetCalculationAngle(startAngle);
            double dEndAngle = GetCalculationAngle(endAngle);

            double radiusStart = radius - startWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double radiusEnd = radius - endWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            return BuildArc(circleCenter, dStartAngle, dEndAngle, radiusStart, radiusEnd);
        }

        public static PointCollection BuildTrackArcRightOuter(Point circleCenter, double startAngle, double endAngle, double radius, int startWidth, int endWidth)
        {
            double dStartAngle = GetCalculationAngle(startAngle);
            double dEndAngle = GetCalculationAngle(endAngle);

            double radiusStart = radius + startWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double radiusEnd = radius + endWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            return BuildArc(circleCenter, dStartAngle, dEndAngle, radiusStart, radiusEnd);
        }

        public static PointCollection BuildTrackArcLeftOuter(Point circleCenter, double startAngle, double endAngle, double radius, int startWidth, int endWidth)
        {
            double dStartAngle = GetCalculationAngle(startAngle);
            double dEndAngle = GetCalculationAngle(endAngle);

            double radiusStart = radius - startWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double radiusEnd = radius - endWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            // parameters must be "flipped" for left-hand corners
            var arcPoints = BuildArc(circleCenter, dEndAngle, dStartAngle, radiusEnd, radiusStart);

            // reverse so that the list is ordered from near to far
            return new PointCollection(arcPoints.Reverse());
        }

        public static PointCollection BuildTrackArcLeftInner(Point circleCenter, double startAngle, double endAngle, double radius, int startWidth, int endWidth)
        {
            double dStartAngle = GetCalculationAngle(startAngle);
            double dEndAngle = GetCalculationAngle(endAngle);

            double radiusStart = radius + startWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;
            double radiusEnd = radius + endWidth / LayoutConstants.TrackWidthUnitsPerLengthUnit;

            // parameters must be "flipped" for left-hand corners
            var arcPoints = BuildArc(circleCenter, dEndAngle, dStartAngle, radiusEnd, radiusStart);

            // reverse so that the list is ordered from near to far
            return new PointCollection(arcPoints.Reverse());
        }

        private static double GetCalculationAngle(double angle)
        {
            return Math.PI - angle * LayoutConstants.AngleScaleRadians;
        }

        private static PointCollection BuildArc(Point circleCenter, double startAngle, double endAngle, double startRadius, double endRadius)
        {
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

            double nPosXStart = circleCenter.X + startRadius * Math.Cos(startAngle);
            double nPosYStart = circleCenter.Y - startRadius * Math.Sin(startAngle);

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
                double nPosX = circleCenter.X + radius * Math.Cos(dAngle);
                double nPosY = circleCenter.Y - radius * Math.Sin(dAngle);

                points.Add(new Point(nPosX, nPosY));

                dAngle = dAngle + dAngleIncrement;
            }

            // add final point
            double nx = circleCenter.X + endRadius * Math.Cos(workEndAngle);
            double ny = circleCenter.Y - endRadius * Math.Sin(workEndAngle);

            points.Add(new Point(nx, ny));

            return points;
        }
    }
}
