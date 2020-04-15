using System.IO;
using ArgData;
using ArgData.Entities;
using ArgTrack.Features.Main;
using ArgTrack.Features.Persistence;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace ArgTrack.Tests
{
    public class TrackMappingIntegrationFacts
    {
        [WpfFact]
        public void TrackCameras()
        {
            var tracks = GetTracks();

            for (var index = 0; index < tracks.OriginalTrack.TrackCameraCommands.Count; index++)
            {
                var originalCamera = tracks.OriginalTrack.TrackCameraCommands[index];
                var newCamera = tracks.NewTrack.TrackCameraCommands[index];

                if (originalCamera is DeleteTrackCameraCommand originalDeleteCamera)
                {
                    var newDeleteCamera = newCamera as DeleteTrackCameraCommand;

                    newDeleteCamera.CameraIndex.Should().Be(originalDeleteCamera.CameraIndex);
                }

                if (originalCamera is TrackCameraAdjustmentCommand originalAdjustCamera)
                {
                    var newAdjustCamera = newCamera as TrackCameraAdjustmentCommand;

                    newAdjustCamera.CameraIndex.Should().Be(originalAdjustCamera.CameraIndex);
                    newAdjustCamera.Adjustment.Should().Be(originalAdjustCamera.Adjustment);
                    newAdjustCamera.TrackSide.Should().Be(originalAdjustCamera.TrackSide);
                }

                if (originalCamera is TrackCameraRangeRightSideAdjustmentCommand originalRightCamera)
                {
                    var newAdjustCamera = newCamera as TrackCameraRangeRightSideAdjustmentCommand;

                    newAdjustCamera.CameraIndexFrom.Should().Be(originalRightCamera.CameraIndexFrom);
                    newAdjustCamera.CameraIndexTo.Should().Be(originalRightCamera.CameraIndexTo);
                }
            }
        }

        [WpfFact]
        public void ComputerCarBehavior()
        {
            var tracks = GetTracks();

            var origBehavior = tracks.OriginalTrack.ComputerCarBehavior;
            var newBehavior = tracks.NewTrack.ComputerCarBehavior;

            newBehavior.FormationLength.Should().Be(origBehavior.FormationLength);
            newBehavior.LateBrakingFactorNonRace.Should().Be(origBehavior.LateBrakingFactorNonRace);
            newBehavior.LateBrakingFactorRace.Should().Be(origBehavior.LateBrakingFactorRace);
            newBehavior.LateBrakingFactorWetRace.Should().Be(origBehavior.LateBrakingFactorWetRace);
            newBehavior.PowerFactor.Should().Be(origBehavior.PowerFactor);
            newBehavior.StrategyChance.Should().Be(origBehavior.StrategyChance);
            newBehavior.StrategyFirstPitStopLap.Should().Be(origBehavior.StrategyFirstPitStopLap);
            newBehavior.UnknownData.Should().BeEquivalentTo(origBehavior.UnknownData);
        }

        [WpfFact]
        public void CarSettings()
        {
            var tracks = GetTracks();

            var originalSettings = tracks.OriginalTrack.CarSettings;
            var newSettings = tracks.NewTrack.CarSettings;

            newSettings.Acceleration.Should().Be(originalSettings.Acceleration);
            newSettings.AirResistance.Should().Be(originalSettings.AirResistance);
            newSettings.FuelLoad.Should().Be(originalSettings.FuelLoad);
            newSettings.GripFactor.Should().Be(originalSettings.GripFactor);
            newSettings.TyreWearNonQualifying.Should().Be(originalSettings.TyreWearNonQualifying);
            newSettings.TyreWearQualifying.Should().Be(originalSettings.TyreWearQualifying);
        }

        [WpfFact]
        public void ObjectSettings()
        {
            var tracks = GetTracks();

            var originalSettings = tracks.OriginalTrack.ObjectSettings;
            var newSettings = tracks.NewTrack.ObjectSettings;

            for (var index = 0; index < originalSettings.Count; index++)
            {
                var originalSetting = originalSettings[index];
                var newSetting = newSettings[index];

                newSetting.AngleX.Should().Be(originalSetting.AngleX);
                newSetting.AngleY.Should().Be(originalSetting.AngleY);
                newSetting.DetailLevel.Should().Be(originalSetting.DetailLevel);
                newSetting.DistanceFromTrack.Should().Be(originalSetting.DistanceFromTrack);
                newSetting.Height.Should().Be(originalSetting.Height);
                newSetting.Id.Should().Be(originalSetting.Id);
                newSetting.Id2.Should().Be(originalSetting.Id2);
                newSetting.Offset.Should().Be(originalSetting.Offset);
                newSetting.Unknown.Should().Be(originalSetting.Unknown);
                newSetting.Unknown2.Should().Be(originalSetting.Unknown2);
            }
        }

        private TrackPair GetTracks()
        {
            var originalPath = ExampleDataHelper.GetExampleDataPath("F1CT01.DAT", TestDataFileType.Tracks);

            var originalTrack = new TrackReader().Read(originalPath);

            var originalViewModel = TrackMapper.MapEntireTrackToViewModel(originalTrack);

            var newTrack = TrackMapper.MapEntireTrackViewModelToTrack(originalViewModel);

            return new TrackPair
            {
                OriginalTrack = originalTrack,
                NewTrack = newTrack
            };
        }

        private class TrackPair
        {
            public Track OriginalTrack { get; set; }

            public Track NewTrack { get; set; }
        }

        /// <summary>
        /// This is a ugly, just a catch-all...
        /// </summary>
        [WpfFact]
        public void LoadSaveLoad()
        {
            var mainView = new MainViewModel();
            MainViewModelPasserArounder.MainViewModel = mainView;

            var originalPath = ExampleDataHelper.GetExampleDataPath("F1CT01.DAT", TestDataFileType.Tracks);
            var newPath = ExampleDataHelper.GetTempFileName(".DAT");

            var loadTrackCommand = new LoadTrackCommand(originalPath);
            loadTrackCommand.Execute(mainView);

            var sub = Substitute.For<IFileDialog>();
            sub.SelectDatFileToSave().Returns(newPath);

            var saveTrackCommand = new SaveTrackCommand(sub);
            saveTrackCommand.Execute(mainView);

            var originalBytes = File.ReadAllBytes(originalPath);
            var newBytes = File.ReadAllBytes(newPath);

            File.Delete(newPath);

            newBytes.Length.Should().Be(originalBytes.Length);
            newBytes.Should().BeEquivalentTo(originalBytes);
        }
    }
}
