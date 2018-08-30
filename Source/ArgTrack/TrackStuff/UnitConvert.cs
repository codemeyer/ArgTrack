using System;
using System.Globalization;

namespace ArgTrack.TrackStuff
{
    public static class UnitConvert
    {
        public const int FeetPerTrackLength = 16;
        public const double FeetPerMeter = 0.3048;

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


        // from http://www.grandprix2.de/Anleitung/tutus/Command_Lib/command_lib%20gp3%20gp2/cmdlib3a.htm
        // Formulas of Robin de Paus for track- and verge width
        // "I have done some research about how to convert GP2-track width and fence width to meters. I have come to the following formulas:
        // Deler is a variable: 
        // for pitlane values, deler = 32768 / 20 
        // for track values, deler = 32768 / 30

        // TrackWidth(Meters)= (TrackWidthGP2 / deler) * 4.87;

        // FenceWidth(Meters)=((FenceWidthGP2*(1024/30))*TrackWidthGP2/1000)/deler*4.87

        //I have tested these formulas and they are highly accurate."
    }
}
