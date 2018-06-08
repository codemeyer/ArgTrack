using System.Collections.Generic;
using System.Linq;
using EntitiesGP2 = Arg2Data.Entities;
using EntitiesF1GP = ArgData.Entities;
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

            templateSourceTrack.TrackSections.Clear();
            
            foreach (var trackSection in gp2Track.TrackSections)
            {
                var convertedTrackSection = MapTrackSection(trackSection);
                templateSourceTrack.TrackSections.Add(convertedTrackSection);
            }

            templateSourceTrack.PitLaneSections.Clear();

            foreach (var trackSection in gp2Track.PitLaneSections)
            {
                var convertedTrackSection = MapTrackSection(trackSection);
                templateSourceTrack.PitLaneSections.Add(convertedTrackSection);
            }

            // TODO: BestLine

            new TrackWriter().Write(templateSourceTrack, outputPath);
        }
        
        private static EntitiesF1GP.TrackSide ConvertGp2TrackSide(EntitiesGP2.TrackSide trackSideGp2)
        {
            if (trackSideGp2 == EntitiesGP2.TrackSide.Left)
                return EntitiesF1GP.TrackSide.Left;

            return EntitiesF1GP.TrackSide.Right;
        }

        private static void RemoveUnsupportedTrackCommands(IEnumerable<EntitiesGP2.TrackSection> originalSections)
        {
            foreach (var section in originalSections)
            {
                section.Commands.RemoveAll(c => c.Command > 0xac);
                section.Commands.RemoveAll(c => c.Command == 0x80);
            }
        }

        private static ArgData.Entities.TrackSection MapTrackSection(Arg2Data.Entities.TrackSection section)
        {
            var viewModel = new EntitiesF1GP.TrackSection();

            // TODO: use CopyProperties?

            viewModel.Length = section.Length;
            viewModel.Curvature = section.Curvature;
            viewModel.Height = section.Height;
            viewModel.LeftVergeWidth = section.LeftVergeWidth;
            viewModel.RightVergeWidth = section.RightVergeWidth;

            viewModel.KerbHeight = MapKerbHeight(section.KerbHeight);
            viewModel.PitLaneEntrance = section.PitLaneEntrance;
            viewModel.PitLaneExit = section.PitLaneExit;
            viewModel.BridgedLeftFence = section.BridgedLeftFence;
            viewModel.BridgedRightFence = section.BridgedRightFence;
            viewModel.HasLeftKerb = section.HasLeftKerb;
            viewModel.HasRightKerb = section.HasRightKerb;
            viewModel.RemoveLeftWall = section.RemoveLeftWall;
            viewModel.RemoveRightWall = section.RemoveRightWall;
            viewModel.RoadSignArrow = section.RoadSignArrow;
            viewModel.RoadSignArrow100 = section.RoadSignArrow100;
            viewModel.RoadSigns = section.RoadSigns;
            viewModel.Unknown1 = section.Unknown1;
            viewModel.Unknown2 = section.Unknown2;
            viewModel.Unknown3 = section.Unknown3;
            viewModel.Unknown4 = section.Unknown4;

            foreach (var command in section.Commands)
            {
                var newCommand = MapCommand(command);
                viewModel.Commands.Add(newCommand);
            }

            return viewModel;
        }

        private static EntitiesF1GP.KerbHeight MapKerbHeight(EntitiesGP2.KerbHeight source)
        {
            if (source == EntitiesGP2.KerbHeight.High)
                return EntitiesF1GP.KerbHeight.High;
            else
                return EntitiesF1GP.KerbHeight.Low;
        }

        private static EntitiesF1GP.TrackCommand MapCommand(EntitiesGP2.TrackCommand command)
        {
            var viewModel = EntitiesF1GP.TrackCommand.Create(command.Command);
            
            for (int i = 0; i < command.Arguments.Length; i++)
            {
                viewModel.Arguments[i] = command.Arguments[i];
            }

            return viewModel;
        }
    }
}
