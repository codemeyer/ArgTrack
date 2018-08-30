using ArgData.Entities;
using ArgTrack.ViewModels.Track;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ArgTrack.Mappers
{
    public static class TrackMapper
    {
        public static TrackViewModel MapEntireTrackToViewModel(Track track)
        {
            var viewModel = new TrackViewModel();

            viewModel.LapCount = track.LapCount;

            viewModel.HorizonBytes = track.Horizon.GetBytes();

            MapHeaderToViewModel(track.TrackDataHeader, viewModel.TrackHeader);
            MapOffsetsToHeaderViewModel(track.Offsets, viewModel.TrackHeader);
            MapRawDataToHeaderViewModel(track.RawData, viewModel.TrackHeader);

            foreach (var section in track.TrackSections)
            {
                var sectionViewModel = new TrackSectionViewModel(viewModel.TrackSections);
                MapSectionToViewModel(section, sectionViewModel);

                // may need to change when rearranging becomes possible?
                sectionViewModel.Index = viewModel.TrackSections.Count;

                viewModel.TrackSections.Add(sectionViewModel);
            }

            foreach (var section in track.PitLaneSections)
            {
                var sectionViewModel = new TrackSectionViewModel(viewModel.PitLaneSections);
                MapSectionToViewModel(section, sectionViewModel);

                // may need to change when rearranging becomes possible?
                sectionViewModel.Index = viewModel.PitLaneSections.Count;

                viewModel.PitLaneSections.Add(sectionViewModel);
            }

            foreach (var section in track.BestLineSegments)
            {
                var sectionViewModel = new BestLineSegmentViewModel(viewModel.BestLineSegments);
                MapBestLineSegmentToViewModel(section, sectionViewModel);

                viewModel.BestLineSegments.Add(sectionViewModel);
            }

            foreach (var obj in track.ObjectSettings)
            {
                var objectViewModel = new TrackObjectSettingsViewModel();
                MapTrackObjectToViewModel(obj, objectViewModel);

                viewModel.TrackObjectSettings.Add(objectViewModel);
            }

            int id = 17;

            foreach (var obj in track.ObjectShapes)
            {
                var objectViewModel = new TrackObjectShapeViewModel();
                MapTrackObjectShapeToViewModel(obj, objectViewModel);
                objectViewModel.Id = id;
                id++;

                viewModel.TrackObjectShapes.Add(objectViewModel);
            }

            Mapper.MapCarSetupToViewModel(track.ComputerCarSetup, viewModel.ComputerCarSetup);


            return viewModel;
        }

        public static void MapSectionToViewModel(TrackSection section, TrackSectionViewModel viewModel)
        {
            viewModel.Length = section.Length;
            viewModel.Curvature = section.Curvature;
            viewModel.Height = section.Height;
            viewModel.LeftVergeWidth = section.LeftVergeWidth;
            viewModel.RightVergeWidth = section.RightVergeWidth;

            viewModel.KerbHeight = section.KerbHeight;
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
                var commandViewModel = new TrackCommandViewModel();
                MapCommandToViewModel(command, commandViewModel);
                viewModel.Commands.Add(commandViewModel);
            }
        }

        public static void MapCommandToViewModel(TrackCommand command, TrackCommandViewModel viewModel)
        {
            viewModel.Command = command.Command;
            viewModel.Arguments.Clear();

            for (int i = 0; i < command.Arguments.Length; i++)
            {
                var arg = GetIt(command.Command, i);
                arg.Value = command.Arguments[i];
                arg.Description = CommandDescriptions.GetCommandArgumentDescription(command.Command, i);

                viewModel.Arguments.Add(arg);
            }
        }

        private static TrackCommandArgumentViewModel GetIt(byte command, int argumentIndex)
        {
            if (command == 0x80 && argumentIndex == 1)
                return new TrackCommand80Argument1ViewModel();
            else
                return new TrackCommandArgumentViewModel();
        }

        private static void MapBestLineSegmentToViewModel(TrackBestLineSegment section, BestLineSegmentViewModel viewModel)
        {
            viewModel.Length = (byte)section.Length;
            viewModel.Radius = section.Radius;
            viewModel.HighRadius = section.HighRadius;
            viewModel.LowRadius = section.LowRadius;
            viewModel.Displacement = section.Displacement;
            viewModel.Correction = section.Correction;
            viewModel.SegmentType = section.SegmentType;
            viewModel.Type = section.SegmentType.ToString();
        }

        private static void MapTrackObjectToViewModel(TrackObjectSettings objectData, TrackObjectSettingsViewModel viewModel)
        {
            viewModel.Id = objectData.Id;
            viewModel.DetailLevel = objectData.DetailLevel;
            viewModel.Unknown = objectData.Unknown;
            viewModel.AngleX = objectData.AngleX;
            viewModel.AngleY = objectData.AngleY;
            viewModel.DistanceFromTrack = objectData.DistanceFromTrack;
            viewModel.Id = objectData.Id;
            viewModel.Unknown2 = objectData.Unknown2;
            viewModel.Height = objectData.Height;

            viewModel.Offset = objectData.Offset;
        }

        private static void MapHeaderToViewModel(TrackSectionHeader headerData, TrackHeaderViewModel viewModel)
        {
            viewModel.FirstSectionAngle = headerData.FirstSectionAngle;
            viewModel.FirstSectionHeight = headerData.FirstSectionHeight;
            viewModel.TrackCenterX = headerData.TrackCenterX;
            viewModel.TrackCenterY = headerData.TrackCenterY;
            viewModel.TrackCenterZ = headerData.TrackCenterZ;
            viewModel.KerbType = headerData.KerbType;
            viewModel.KerbUpperColor = headerData.KerbUpperColor;
            viewModel.KerbLowerColor = headerData.KerbLowerColor;
            viewModel.KerbUpperColor2 = headerData.KerbUpperColor2;
            viewModel.KerbLowerColor2 = headerData.KerbLowerColor2;
            viewModel.PoleSide = headerData.PoleSide;
            viewModel.PitsSide = headerData.PitsSide;
            viewModel.SurroundingArea = headerData.SurroundingArea;
            viewModel.TrackStartWidth = headerData.StartWidth;
            viewModel.LeftVergeStartWidth = headerData.LeftVergeStartWidth;
            viewModel.RightVergeStartWidth = headerData.RightVergeStartWidth;
        }

        private static void MapOffsetsToHeaderViewModel(TrackOffsets offsets, TrackHeaderViewModel viewModel)
        {
            viewModel.UnknownLong1 = offsets.UnknownLong1;
            viewModel.UnknownLong2 = offsets.UnknownLong2;
            viewModel.ChecksumPosition = offsets.ChecksumPosition;
            viewModel.ObjectData = offsets.ObjectData;
            viewModel.TrackData = offsets.TrackData;
        }

        private static void MapRawDataToHeaderViewModel(TrackRawData rawData, TrackHeaderViewModel viewModel)
        {
            viewModel.DataAfterSetup = rawData.DataAfterSetup;
            viewModel.FinalData1 = rawData.FinalData1;
            viewModel.FinalData2 = rawData.FinalData2;
        }

        private static void MapTrackObjectShapeToViewModel(TrackObjectShape objectData,
            TrackObjectShapeViewModel viewModel)
        {
            // TODO: use CopyProperties?

            viewModel.HeaderIndex = objectData.HeaderIndex;
            viewModel.DataIndex = objectData.DataIndex;

            viewModel.Offset1 = objectData.Offset1;
            viewModel.Offset2 = objectData.Offset2;
            viewModel.Offset3 = objectData.Offset3;
            viewModel.Offset4 = objectData.Offset4;
            viewModel.Offset5 = objectData.Offset5;
            viewModel.OffsetData1 = objectData.OffsetData1;
            viewModel.OffsetData2 = objectData.OffsetData2;
            viewModel.OffsetData3 = objectData.OffsetData3;
            viewModel.OffsetData4 = objectData.OffsetData4;
            viewModel.OffsetData5 = objectData.OffsetData5;
            viewModel.HeaderValue1 = objectData.HeaderValue1;
            viewModel.HeaderValue2 = objectData.HeaderValue2;
            viewModel.HeaderValue3 = objectData.HeaderValue3;
            viewModel.HeaderValue4 = objectData.HeaderValue4;
            viewModel.HeaderValue5 = objectData.HeaderValue5;
            viewModel.HeaderValue6 = objectData.HeaderValue6;
        }

        // map source to target
        public static void MapTrackSectionViewModel(TrackSectionViewModel source, TrackSectionViewModel target)
        {
            CopyProperties(source, target);

            var newCommands = new ObservableCollection<TrackCommandViewModel>();

            foreach (var sourceCommand in source.Commands)
            {
                var targetCommand = new TrackCommandViewModel();
                MapTrackCommandViewModel(sourceCommand, targetCommand);
                newCommands.Add(targetCommand);
            }

            target.Commands = newCommands;
        }

        public static void MapTrackCommandViewModel(TrackCommandViewModel source, TrackCommandViewModel target)
        {
            CopyProperties(source, target);

            var newArgs = new ObservableCollection<TrackCommandArgumentViewModel>();

            foreach(var sourceArg in source.Arguments)
            {
                var targetArg = GetIt(source.Command, source.Arguments.IndexOf(sourceArg));
                MapTrackCommandArgumentViewModel(sourceArg, targetArg);
                newArgs.Add(targetArg);
            }

            target.Arguments = newArgs;
        }

        public static void MapTrackHeaderViewModel(TrackHeaderViewModel source, TrackHeaderViewModel target)
        {
            CopyProperties(source, target);
        }

        private static void MapTrackCommandArgumentViewModel(TrackCommandArgumentViewModel source, TrackCommandArgumentViewModel target)
        {
            CopyProperties(source, target);
        }

        public static void MapTrackBestLineViewModel(BestLineSegmentViewModel source, BestLineSegmentViewModel target)
        {
            CopyProperties(source, target);
        }

        public static void MapTrackObjectSettingsViewModel(TrackObjectSettingsViewModel source, TrackObjectSettingsViewModel target)
        {
            CopyProperties(source, target);
        }

        public static void MapTrackObjectShapeViewModel(TrackObjectShapeViewModel source, TrackObjectShapeViewModel target)
        {
            CopyProperties(source, target);
        }

        private static void CopyProperties(object from, object to) //, string[] excludedProperties)
        {
            var targetType = to.GetType();
            var sourceType = from.GetType();

            PropertyInfo[] sourceProps = sourceType.GetProperties();

            foreach (var propInfo in sourceProps)
            {
                PropertyInfo toProp = targetType == sourceType
                    ? propInfo
                    : targetType.GetProperty(propInfo.Name);

                if (toProp != null && toProp.CanWrite)
                {
                    var value = propInfo.GetValue(from, null);
                    toProp.SetValue(to, value, null);
                }
            }
        }

        public static Track MapEntireTrackViewModelToTrack(TrackViewModel viewModel)
        {
            var track = new Track();
            track.LapCount = viewModel.LapCount;

            for (int i = 0; i <= 4095; i++)
            {
                track.Horizon.Update(i, viewModel.HorizonBytes[i]);
            }

            CopyProperties(viewModel.TrackHeader, track.TrackDataHeader);
            track.TrackDataHeader.StartWidth = viewModel.TrackHeader.TrackStartWidth;
            CopyProperties(viewModel.TrackHeader, track.Offsets);
            CopyProperties(viewModel.TrackHeader, track.RawData);

            foreach (var section in viewModel.TrackSections)
            {
                var trackSection = new TrackSection();
                MapSectionViewModelToData(section, trackSection);
                track.TrackSections.Add(trackSection);
            }

            foreach (var section in viewModel.PitLaneSections)
            {
                var trackSection = new TrackSection();
                MapSectionViewModelToData(section, trackSection);
                track.PitLaneSections.Add(trackSection);
            }

            foreach (var bestLine in viewModel.BestLineSegments)
            {
                var target = new TrackBestLineSegment();
                CopyProperties(bestLine, target);
                track.BestLineSegments.Add(target);
            }

            foreach (var obj in viewModel.TrackObjectSettings)
            {
                var target = new TrackObjectSettings();
                CopyProperties(obj, target);
                track.ObjectSettings.Add(target);
            }

            foreach (var obj in viewModel.TrackObjectShapes)
            {
                var target = new TrackObjectShape(obj.HeaderIndex, obj.DataIndex);
                CopyProperties(obj, target);
                track.ObjectShapes.Add(target);
            }

            CopyProperties(viewModel.ComputerCarSetup, track.ComputerCarSetup);

            return track;
        }

        private static void MapSectionViewModelToData(TrackSectionViewModel viewModel, TrackSection section)
        {
            section.Length = viewModel.Length;
            section.Curvature = viewModel.Curvature;
            section.Height = viewModel.Height;
            section.LeftVergeWidth = viewModel.LeftVergeWidth;
            section.RightVergeWidth = viewModel.RightVergeWidth;

            section.KerbHeight = viewModel.KerbHeight;
            section.PitLaneEntrance = viewModel.PitLaneEntrance;
            section.PitLaneExit = viewModel.PitLaneExit;
            section.BridgedLeftFence = viewModel.BridgedLeftFence;
            section.BridgedRightFence = viewModel.BridgedRightFence;
            section.HasLeftKerb = viewModel.HasLeftKerb;
            section.HasRightKerb = viewModel.HasRightKerb;
            section.RemoveLeftWall = viewModel.RemoveLeftWall;
            section.RemoveRightWall = viewModel.RemoveRightWall;
            section.RoadSignArrow = viewModel.RoadSignArrow;
            section.RoadSignArrow100 = viewModel.RoadSignArrow100;
            section.RoadSigns = viewModel.RoadSigns;
            section.Unknown1 = viewModel.Unknown1;
            section.Unknown2 = viewModel.Unknown2;
            section.Unknown3 = viewModel.Unknown3;
            section.Unknown4 = viewModel.Unknown4;

            foreach (var commandViewModel in viewModel.Commands)
            {
                var command = TrackCommand.Create(commandViewModel.Command);

                //short[] args = new short[command.Arguments.Count];

                for (int i = 0; i < commandViewModel.Arguments.Count; i++)
                {
                    command.Arguments[i] = commandViewModel.Arguments[i].Value;
                    //args[i] = command.Arguments[i].Value;
                }

                //var commandViewModel = new TrackCommand(command.Command, args);

                section.Commands.Add(command);
            }
        }
    }
}
