using System;

namespace ArgTrack.Features.TrackDrawing
{
    public static class LayoutConstants
    {
        //public const double AngleScaleDegrees = 360.0 / 65536;
        public const double AngleScaleRadians = 2 * Math.PI / 65536;


        /// <summary>
        /// Width unit is 1/1024 of track length
        /// </summary>
        public const double TrackWidthUnitsPerLengthUnit = 1024.0;
        // and one length unit is 16 feet...
        // 16 feet = 487.68cm = 4.8768m (ie = width 1024)
        // m/f = 0.3048
        // feet per lengthunit = 16

        public const double MetersPerTrackWidthUnit = 0.0047625; //ie 1m=210

        // NOTE: track width is the width of each HALF the track

        // F1 car 1991 approx 2.2m wide = "[1993] --- overall car width reduced to 200 cm from 220 cm." https://en.wikipedia.org/wiki/History_of_Formula_One_regulations
        // half = 1.1 which is 231 twu, which JUST fits the car in F1GP :)

        // in trackwidth units
        //startWidth = 1344; // according to CF
        //public const int PitLaneWidth = 800;   // my rough guess
        public const int PitLaneWidth = 640;    // GP2 track editor tuts

        // more units?
        // TrackLengthUnit
        // TrackWidthUnit
        // VergeWidthUnit (1/30 twu?)
        // more?


        // track width is _between_ white lines?
        // between huh? yeah looks like it

        // NOTE: Verge width is dependent on, and RELATIVE TO, the track width!
        // yes, verge~48-50 makes the verge roughly as wide as the track
    }
}
