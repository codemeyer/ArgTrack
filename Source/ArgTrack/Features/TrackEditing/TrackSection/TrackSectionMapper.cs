using System.Collections.ObjectModel;
using ArgData.Entities;
using ArgTrack.Infrastructure.Mapping;

namespace ArgTrack.Features.TrackEditing.TrackSection
{
    public static class TrackSectionMapper
    {
        public static void MapTrackSectionViewModel(TrackSectionViewModel source, TrackSectionViewModel target)
        {
            PropertyCopier.Copy(source, target);

            var newCommands = new ObservableCollection<TrackSectionCommandViewModel>();

            foreach (var sourceCommand in source.Commands)
            {
                var targetCommand = new TrackSectionCommandViewModel();
                MapTrackCommandViewModel(sourceCommand, targetCommand);
                newCommands.Add(targetCommand);
            }

            target.Commands = newCommands;
        }

        public static void MapSectionToViewModel(ArgData.Entities.TrackSection section, TrackSectionViewModel viewModel)
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
                var commandViewModel = new TrackSectionCommandViewModel();
                MapCommandToViewModel(command, commandViewModel);
                viewModel.Commands.Add(commandViewModel);
            }
        }

        public static void MapCommandToViewModel(TrackSectionCommand command, TrackSectionCommandViewModel viewModel)
        {
            viewModel.Command = command.Command;
            viewModel.Arguments.Clear();

            for (int i = 0; i < command.Arguments.Length; i++)
            {
                var arg = GetIt(command.Command, i);
                arg.Value = command.Arguments[i];
                arg.Description = TrackSectionCommandDescriptions.GetCommandArgumentDescription(command.Command, i);

                viewModel.Arguments.Add(arg);
            }

            viewModel.UpdateCommandDetails();
        }

        public static void MapTrackCommandViewModel(TrackSectionCommandViewModel source, TrackSectionCommandViewModel target)
        {
            PropertyCopier.Copy(source, target);

            var newArgs = new ObservableCollection<TrackSectionCommandArgumentViewModel>();

            foreach(var sourceArg in source.Arguments)
            {
                var targetArg = GetIt(source.Command, source.Arguments.IndexOf(sourceArg));
                MapTrackCommandArgumentViewModel(sourceArg, targetArg);
                newArgs.Add(targetArg);
            }

            target.Arguments = newArgs;
        }

        private static void MapTrackCommandArgumentViewModel(TrackSectionCommandArgumentViewModel source, TrackSectionCommandArgumentViewModel target)
        {
            PropertyCopier.Copy(source, target);
        }

        private static TrackSectionCommandArgumentViewModel GetIt(byte command, int argumentIndex)
        {
            if (command == 0x80 && argumentIndex == 1)
                return new TrackSectionCommand80Argument1ViewModel();
            else
                return new TrackSectionCommandArgumentViewModel();
        }

        public static void MapSectionViewModelToData(TrackSectionViewModel viewModel, ArgData.Entities.TrackSection section)
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
                var command = TrackSectionCommand.Create(commandViewModel.Command);

                for (int i = 0; i < commandViewModel.Arguments.Count; i++)
                {
                    command.Arguments[i] = commandViewModel.Arguments[i].Value;
                }

                section.Commands.Add(command);
            }
        }
    }
}
