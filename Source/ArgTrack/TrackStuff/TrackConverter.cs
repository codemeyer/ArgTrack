using System.Collections.Generic;
using System.Linq;
using ArgData;

namespace ArgTrack.TrackStuff
{
    public static class TrackConverter
    {
        public static void ConvertGP2ToF1GP(Arg2Data.Entities.Track gp2Track, ArgData.Entities.Track templateSourceTrack, string outputPath)
        {
            int commandsBefore = gp2Track.TrackSections.Sum(t => t.Commands.Count);

            RemoveUnsupportedTrackCommands(gp2Track.TrackSections);
            RemoveUnsupportedTrackCommands(gp2Track.PitLaneSections);

            int commandsAfter = gp2Track.TrackSections.Sum(t => t.Commands.Count);

            templateSourceTrack.TrackDataHeader.FirstSectionAngle = gp2Track.TrackDataHeader.FirstSectionAngle;
            templateSourceTrack.TrackDataHeader.FirstSectionHeight = gp2Track.TrackDataHeader.FirstSectionHeight;
            templateSourceTrack.TrackDataHeader.TrackCenterX = gp2Track.TrackDataHeader.TrackCenterX;
            templateSourceTrack.TrackDataHeader.TrackCenterY = gp2Track.TrackDataHeader.TrackCenterY;
            templateSourceTrack.TrackDataHeader.TrackCenterZ = gp2Track.TrackDataHeader.TrackCenterZ;
            templateSourceTrack.TrackDataHeader.StartWidth = gp2Track.TrackDataHeader.StartWidth;
            templateSourceTrack.TrackDataHeader.PoleSide = ConvertGp2TrackSide(gp2Track.TrackDataHeader.PoleSide);
            templateSourceTrack.TrackDataHeader.PitsSide = ConvertGp2TrackSide(gp2Track.TrackDataHeader.PitsSide);
            templateSourceTrack.TrackDataHeader.LeftVergeStartWidth = gp2Track.TrackDataHeader.LeftVergeStartWidth;
            templateSourceTrack.TrackDataHeader.RightVergeStartWidth = gp2Track.TrackDataHeader.RightVergeStartWidth;
            //templateSourceTrack.TrackDataHeader.KerbType = gp2Track.TrackDataHeader.KerbType;
            //templateSourceTrack.TrackDataHeader.KerbLowerColor = gp2Track.TrackDataHeader.KerbLowerColor;
            //templateSourceTrack.TrackDataHeader.KerbLowerColor2 = gp2Track.TrackDataHeader.KerbLowerColor2;
            //templateSourceTrack.TrackDataHeader.KerbUpperColor = gp2Track.TrackDataHeader.KerbUpperColor;
            //templateSourceTrack.TrackDataHeader.KerbUpperColor2 = gp2Track.TrackDataHeader.KerbUpperColor2;
            // TODO: set fixed kerb colors?

            // TODO:
            //templateSourceTrack.TrackSections = gp2Track.TrackSections;
            //templateSourceTrack.PitLaneSections = gp2Track.PitLaneSections;
            // TODO: BestLine

            new TrackWriter().Write(templateSourceTrack, outputPath);
        }
        
        private static ArgData.Entities.TrackSide ConvertGp2TrackSide(Arg2Data.Entities.TrackSide trackSideGp2)
        {
            if (trackSideGp2 == Arg2Data.Entities.TrackSide.Left)
                return ArgData.Entities.TrackSide.Left;

            return ArgData.Entities.TrackSide.Right;
        }

        private static void RemoveUnsupportedTrackCommands(IEnumerable<Arg2Data.Entities.TrackSection> originalSections)
        {
            foreach (var section in originalSections)
            {
                section.Commands.RemoveAll(c => c.Command > 0xac);
                section.Commands.RemoveAll(c => c.Command == 0x80);
            }
        }
    }
}
