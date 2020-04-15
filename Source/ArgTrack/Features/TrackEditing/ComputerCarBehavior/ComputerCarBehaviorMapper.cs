using ArgData.Entities;

namespace ArgTrack.Features.TrackEditing.ComputerCarBehavior
{
    public static class ComputerCarBehaviorMapper
    {
        public static void MapComputerCarBehaviorToViewModel(TrackComputerCarBehavior behavior, ComputerCarBehaviorViewModel viewModel)
        {
            viewModel.FormationLength = behavior.FormationLength;
            viewModel.LateBrakingFactorNonRace = behavior.LateBrakingFactorNonRace;
            viewModel.LateBrakingFactorRace = behavior.LateBrakingFactorRace;
            viewModel.LateBrakingFactorWetRace = behavior.LateBrakingFactorWetRace;
            viewModel.PowerFactor = behavior.PowerFactor;
            viewModel.StrategyChance = behavior.StrategyChance;
            viewModel.StrategyFirstPitStopLap = behavior.StrategyFirstPitStopLap;
            viewModel.UnknownData = behavior.UnknownData;
        }
    }
}
