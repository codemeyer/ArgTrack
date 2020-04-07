using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Tracks
{
    /// <summary>
    /// Moves a track section (or a pit lane section) down in the list.
    /// </summary>
    public class MoveTrackSectionDownCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackSectionViewModel;

            if (vm == null)
                return;

            vm.ParentList.Move(vm.Index, vm.Index + 1);
        }

        public override bool CanExecute(object parameter)
        {
            var vm = parameter as TrackSectionViewModel;

            if (vm == null)
                return false;

            return vm.Index < vm.ParentList.Count - 1;
        }
    }
}
