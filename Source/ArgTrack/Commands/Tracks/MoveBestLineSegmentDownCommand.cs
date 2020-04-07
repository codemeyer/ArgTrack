using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Tracks
{
    public class MoveBestLineSegmentDownCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as BestLineSegmentViewModel;

            if (vm == null)
                return;

            vm.ParentList.Move(vm.Index, vm.Index + 1);
        }

        public override bool CanExecute(object parameter)
        {
            var vm = parameter as BestLineSegmentViewModel;

            if (vm == null)
                return false;

            return vm.Index < vm.ParentList.Count - 1;
        }
    }
}
