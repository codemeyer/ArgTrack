using System;

namespace ArgTrack.Features.TrackDrawing
{
    public static class GeometryConversions
    {
        /// <summary>
        /// Converts degrees into radians.
        /// </summary>
        /// <param name="angle">Angle in degrees.</param>
        /// <returns>Angle in radians.</returns>
        public static double DegreesToRadians(double angle)
        {
            return Math.PI / 180 * angle;
        }

        /// <summary>
        /// Converts radians into degrees.
        /// </summary>
        /// <param name="angle">Angle in radians.</param>
        /// <returns>Angle in degrees.</returns>
        public static double RadiansToDegrees(double angle)
        {
            return angle * 180 / Math.PI;
        }

        /// <summary>
        /// Converts a game angle (int) into radians.
        /// </summary>
        /// <param name="gameAngle"></param>
        /// <returns></returns>
        public static double RadiansFromGameAngle(int gameAngle)
        {
            return gameAngle * LayoutConstants.AngleScaleRadians;
        }

        /// <summary>
        /// Converts a game angle (int) into degrees.
        /// </summary>
        /// <param name="gameAngle"></param>
        /// <returns></returns>
        public static double DegreesFromGameAngle(int gameAngle)
        {
            return RadiansToDegrees(gameAngle * LayoutConstants.AngleScaleRadians);
        }
    }
}
