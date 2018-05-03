using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Tracks
{
    public class DeleteBestLineSegmentCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackViewModel;

            if (vm == null)
                return;

            vm.BestLineSegments.RemoveAt(vm.SelectedBestLineSectionIndex);
        }

        public override bool CanExecute(object parameter)
        {
            var track = parameter as TrackViewModel;

            if (track == null)
                return false;

            return track.SelectedBestLineSectionIndex != 0;
        }
    }
}
