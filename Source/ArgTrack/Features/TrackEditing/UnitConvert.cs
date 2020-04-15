using System;
using System.Globalization;
using ArgTrack.Features.TrackDrawing;

namespace ArgTrack.Features.TrackEditing
{
    public static class UnitConvert
    {
        public const int FeetPerTrackLength = 16;
        //public const double FeetPerMeter = 0.3048;

        public static double TrackLengthToMeters(int trackLength)
        {
            int feet = trackLength * FeetPerTrackLength;
            double meters = Math.Round(feet * 0.3048, 1);

            return meters;
        }

        public static string TrackLengthToMetersDescription(int trackLength)
        {
            var lengthInMeters = TrackLengthToMeters(trackLength);
            var meters = lengthInMeters.ToString("F1", new CultureInfo("en-US"));

            return $"~{meters} meters";
        }

        public static double TrackWidthToMeters(int trackWidth)
        {
            return Math.Round(trackWidth * 2 * LayoutConstants.MetersPerTrackWidthUnit, 1);
        }

        public static string TrackWidthToMetersDescription(int trackWidth)
        {
            double lengthInMeters = TrackWidthToMeters(trackWidth);
            var meters = lengthInMeters.ToString("F1", new CultureInfo("en-US"));

            return $"~{meters} meters";
        }
    }
}
