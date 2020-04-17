using System;

namespace ArgTrack.Features.TrackDrawing
{
    public static class GeometryConversions
    {
        public static double DegreesToRadians(double angle)
        {
            return Math.PI / 180 * angle;
        }

        public static double RadiansToDegrees(double angle)
        {
            return angle * 180 / Math.PI;
        }

        public static double RadiansFromGameAngle(int gameAngle)
        {
            return gameAngle * LayoutConstants.AngleScaleRadians;
        }

        public static double DegreesFromGameAngle(int gameAngle)
        {
            return RadiansToDegrees(gameAngle * LayoutConstants.AngleScaleRadians);
        }
    }
}
