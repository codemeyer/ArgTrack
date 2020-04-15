using ArgData.Entities;
using ArgTrack.Features.TrackEditing.Camera;
using ArgTrack.Features.TrackEditing.CarSettings;
using ArgTrack.Features.TrackEditing.CarSetup;
using ArgTrack.Features.TrackEditing.ComputerCarBehavior;
using ArgTrack.Features.TrackEditing.ObjectSettings;
using ArgTrack.Features.TrackEditing.ObjectShapes;
using ArgTrack.Features.TrackEditing.RacingLine;
using ArgTrack.Features.TrackEditing.TrackSection;
using ArgTrack.Features.TrackEditing.TrackSettings;
using ArgTrack.Features.TrackEditing.WorldLocation;
using ArgTrack.Infrastructure.Mapping;

namespace ArgTrack.Features.Main
{
    public static class TrackMapper
    {
        public static TrackViewModel MapEntireTrackToViewModel(Track track)
        {
            var viewModel = new TrackViewModel();

            TrackSettingsMapper.MapTrackSettingsToViewModel(track.TrackSettings, viewModel.TrackSettings);

            viewModel.ComputerCarLineDisplacement = track.ComputerCarLineDisplacement;

            ComputerCarBehaviorMapper.MapComputerCarBehaviorToViewModel(track.ComputerCarBehavior, viewModel.ComputerCarBehavior);

            CarSettingsMapper.MapCarSettingsToViewModel(track.CarSettings, viewModel.CarSettings);

            viewModel.HorizonBytes = track.Horizon.GetBytes();

            WorldLocationMapper.MapHeaderToViewModel(track.TrackDataHeader, viewModel.TrackHeader);

            MapOffsetsToHeaderViewModel(track.Offsets, viewModel.TrackHeader);

            foreach (var section in track.TrackSections)
            {
                var sectionViewModel = new TrackSectionViewModel(viewModel.TrackSections);
                TrackSectionMapper.MapSectionToViewModel(section, sectionViewModel);

                // may need to change when rearranging becomes possible?
                sectionViewModel.Index = viewModel.TrackSections.Count;

                viewModel.TrackSections.Add(sectionViewModel);
            }

            foreach (var section in track.PitLaneSections)
            {
                var sectionViewModel = new TrackSectionViewModel(viewModel.PitLaneSections);
                TrackSectionMapper.MapSectionToViewModel(section, sectionViewModel);

                // may need to change when rearranging becomes possible?
                sectionViewModel.Index = viewModel.PitLaneSections.Count;

                viewModel.PitLaneSections.Add(sectionViewModel);
            }

            foreach (var section in track.ComputerCarLineSegments)
            {
                var sectionViewModel = new BestLineSegmentViewModel(viewModel.BestLineSegments);
                RacingLineMapper.MapBestLineSegmentToViewModel(section, sectionViewModel);

                viewModel.BestLineSegments.Add(sectionViewModel);
            }

            foreach (var obj in track.ObjectSettings)
            {
                var objectViewModel = new TrackObjectSettingsViewModel();
                ObjectSettingsMapper.MapTrackObjectToViewModel(obj, objectViewModel);

                viewModel.TrackObjectSettings.Add(objectViewModel);
            }

            int id = 17;

            foreach (var obj in track.ObjectShapes)
            {
                var objectViewModel = new TrackObjectShapeViewModel();
                ObjectShapeMapper.MapTrackObjectShapeToViewModel(obj, objectViewModel);
                objectViewModel.Id = id;
                id++;

                viewModel.TrackObjectShapes.Add(objectViewModel);
            }

            CarSetupMapper.MapCarSetupToViewModel(track.ComputerCarSetup, viewModel.ComputerCarSetup);


            foreach (TrackCameraCommand camera in track.TrackCameraCommands)
            {
                if (camera is DeleteTrackCameraCommand deleteCamera)
                {
                    var deleteVM = new CameraCommandEditingViewModel(deleteCamera);
                    viewModel.TrackCameraCommands.Add(deleteVM);
                }

                if (camera is TrackCameraAdjustmentCommand adjustCamera)
                {
                    var adjustVM = new CameraCommandEditingViewModel(adjustCamera);
                    viewModel.TrackCameraCommands.Add(adjustVM);
                }

                if (camera is TrackCameraRangeRightSideAdjustmentCommand rangeRightSide)
                {
                    var rangeVM = new CameraCommandEditingViewModel(rangeRightSide);
                    viewModel.TrackCameraCommands.Add(rangeVM);
                }
            }

            return viewModel;
        }

        private static void MapOffsetsToHeaderViewModel(TrackOffsets offsets, TrackHeaderViewModel viewModel)
        {
            viewModel.UnknownLong1 = offsets.UnknownLong1;
            viewModel.UnknownLong2 = offsets.UnknownLong2;
            viewModel.ChecksumPosition = offsets.ChecksumPosition;
            viewModel.ObjectData = offsets.ObjectData;
            viewModel.TrackData = offsets.TrackData;
        }

        public static Track MapEntireTrackViewModelToTrack(TrackViewModel viewModel)
        {
            var track = new Track();

            track.ComputerCarLineDisplacement = viewModel.ComputerCarLineDisplacement;

            PropertyCopier.Copy(viewModel.TrackSettings, track.TrackSettings);

            PropertyCopier.Copy(viewModel.CarSettings, track.CarSettings);
            PropertyCopier.Copy(viewModel.ComputerCarBehavior, track.ComputerCarBehavior);

            for (int i = 0; i <= 4095; i++)
            {
                track.Horizon.Update(i, viewModel.HorizonBytes[i]);
            }

            PropertyCopier.Copy(viewModel.TrackHeader, track.TrackDataHeader);
            track.TrackDataHeader.StartWidth = viewModel.TrackHeader.TrackStartWidth;
            PropertyCopier.Copy(viewModel.TrackHeader, track.Offsets);

            foreach (var section in viewModel.TrackSections)
            {
                var trackSection = new TrackSection();
                TrackSectionMapper.MapSectionViewModelToData(section, trackSection);
                track.TrackSections.Add(trackSection);
            }

            foreach (var section in viewModel.PitLaneSections)
            {
                var trackSection = new TrackSection();
                TrackSectionMapper.MapSectionViewModelToData(section, trackSection);
                track.PitLaneSections.Add(trackSection);
            }

            foreach (var bestLine in viewModel.BestLineSegments)
            {
                var target = new TrackComputerCarLineSegment();
                PropertyCopier.Copy(bestLine, target);
                track.ComputerCarLineSegments.Add(target);
            }

            foreach (var obj in viewModel.TrackObjectSettings)
            {
                var target = new TrackObjectSettings();
                PropertyCopier.Copy(obj, target);
                track.ObjectSettings.Add(target);
            }

            foreach (var obj in viewModel.TrackObjectShapes)
            {
                var target = new TrackObjectShape(obj.HeaderIndex, obj.DataIndex);
                PropertyCopier.Copy(obj, target);
                track.ObjectShapes.Add(target);
            }

            PropertyCopier.Copy(viewModel.ComputerCarSetup, track.ComputerCarSetup);

            foreach (var cameraViewModel in viewModel.TrackCameraCommands)
            {
                if (cameraViewModel.CameraCommandType == CameraCommandType.Delete)
                {
                    var camera = new DeleteTrackCameraCommand(cameraViewModel.CameraIndex);
                    track.TrackCameraCommands.Add(camera);
                }

                if (cameraViewModel.CameraCommandType == CameraCommandType.Adjustment)
                {
                    var camera = new TrackCameraAdjustmentCommand(cameraViewModel.CameraIndex, cameraViewModel.Adjustment, cameraViewModel.TrackSide);
                    track.TrackCameraCommands.Add(camera);
                }

                if (cameraViewModel.CameraCommandType == CameraCommandType.RightRange)
                {
                    var camera = new TrackCameraRangeRightSideAdjustmentCommand(cameraViewModel.CameraIndexFrom, cameraViewModel.CameraIndexTo);
                    track.TrackCameraCommands.Add(camera);
                }
            }

            return track;
        }
    }
}
