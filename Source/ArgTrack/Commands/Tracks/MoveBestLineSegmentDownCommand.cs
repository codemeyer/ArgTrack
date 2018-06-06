using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Tracks
{
    public class MoveBestLineSegmentDownCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackViewModel;

            if (vm == null)
                return;

            vm.BestLineSegments.Move(vm.SelectedBestLineSectionIndex, vm.SelectedBestLineSectionIndex + 1);
            vm.RecalculateBestLineSectionIndexz();
        }

        public override bool CanExecute(object parameter)
        {
            var vm = parameter as TrackViewModel;

            if (vm == null)
                return false;

            return vm.SelectedBestLineSectionIndex > 0 &&
                vm.SelectedBestLineSectionIndex < vm.BestLineSegments.Count - 1;
        }
    }
}
