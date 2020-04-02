using System.Windows;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class EditComputerCarBehaviorCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as ComputerCarBehaviorViewModel;

            if (vm == null)
                return;

            var clone = new ComputerCarBehaviorViewModel();

            // TODO: move to mapper
            clone.FormationLength = vm.FormationLength;
            clone.LateBrakingFactorNonRace = vm.LateBrakingFactorNonRace;
            clone.LateBrakingFactorRace = vm.LateBrakingFactorRace;
            clone.LateBrakingFactorWetRace = vm.LateBrakingFactorWetRace;
            clone.PowerFactor = vm.PowerFactor;
            clone.StrategyChance = vm.StrategyChance;
            clone.StrategyFirstPitStopLap = vm.StrategyFirstPitStopLap;

            var window = new ComputerCarBehaviorWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = clone
                // TODO: also pass in original, to allow reseting all properties
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                // map clone to actual value
                vm.FormationLength = clone.FormationLength;
                vm.LateBrakingFactorNonRace = clone.LateBrakingFactorNonRace;
                vm.LateBrakingFactorRace = clone.LateBrakingFactorRace;
                vm.LateBrakingFactorWetRace = clone.LateBrakingFactorWetRace;
                vm.PowerFactor = clone.PowerFactor;
                vm.StrategyChance = clone.StrategyChance;
                vm.StrategyFirstPitStopLap = clone.StrategyFirstPitStopLap;
            }
        }
    }
}
