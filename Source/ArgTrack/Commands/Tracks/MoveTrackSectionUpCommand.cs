using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Tracks
{
    public class MoveTrackSectionUpCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackViewModel;

            if (vm == null)
                return;

            vm.TrackSections.Move(vm.SelectedTrackSectionIndex, vm.SelectedTrackSectionIndex - 1);
            vm.RecalculateTrackSectionIndexz();
        }

        public override bool CanExecute(object parameter)
        {
            var vm = parameter as TrackViewModel;

            if (vm == null)
                return false;

            return vm.SelectedTrackSectionIndex > 0;
        }
    }
}
