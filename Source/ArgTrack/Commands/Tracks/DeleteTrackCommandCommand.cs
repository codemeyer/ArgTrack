using System.Linq;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Tracks
{
    public class DeleteTrackCommandCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackSectionViewModel;

            if (vm == null)
                return;

            // confirmation?

            vm.Commands.RemoveAt(vm.SelectedTrackCommandIndex);
        }

        public override bool CanExecute(object parameter)
        {
            var vm = parameter as TrackSectionViewModel;

            if (vm == null)
                return false;

            return vm.Commands.Any();
        }
    }
}
