using System.Linq;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.TrackSection
{
    public class DeleteTrackSectionCommandCommand : CommandBase
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
