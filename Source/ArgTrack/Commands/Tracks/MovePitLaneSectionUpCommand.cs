using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Tracks
{
    public class MovePitLaneSectionUpCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackViewModel;

            if (vm == null)
                return;

            vm.PitLaneSections.Move(vm.SelectedPitLaneSectionIndex, vm.SelectedPitLaneSectionIndex - 1);
            vm.RecalculatePitLaneSectionIndexz();
        }

        public override bool CanExecute(object parameter)
        {
            var vm = parameter as TrackViewModel;

            if (vm == null)
                return false;

            return vm.SelectedPitLaneSectionIndex > 0;
        }
    }
}
