using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Tracks
{
    public class DeleteTrackSectionCommand : CommandBase
    {
        private readonly bool _isTrack;

        // TODO: bool here = horrible design...
        public DeleteTrackSectionCommand(bool isTrack = true)
        {
            _isTrack = isTrack;
        }

        public override void Execute(object parameter)
        {
            var vm = parameter as TrackViewModel;

            if (vm == null)
                return;

            if (_isTrack)
                vm.TrackSections.RemoveAt(vm.SelectedTrackSectionIndex);
            else
                vm.PitLaneSections.RemoveAt(vm.SelectedPitLaneSectionIndex);
        }
    }
}
