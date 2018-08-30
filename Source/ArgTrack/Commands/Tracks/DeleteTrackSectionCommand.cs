using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Tracks
{
    [System.Obsolete("Replaced by RemoveTrackSectionCommand?")]
    public class DeleteTrackSectionCommand : CommandBase
    {
        private readonly bool _isTrack;

        // TODO: bool here = horrible design... split into DeletePitLaneSectionCommand?
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
            {
                vm.TrackSections.RemoveAt(vm.SelectedTrackSectionIndex);
                vm.RecalculateTrackSectionIndexz();
            }
            else
            {
                vm.PitLaneSections.RemoveAt(vm.SelectedPitLaneSectionIndex);
                vm.RecalculatePitLaneSectionIndexz();
            }
        }
    }
}
