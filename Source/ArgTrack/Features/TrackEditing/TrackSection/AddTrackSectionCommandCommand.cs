using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.TrackSection
{
    public class AddTrackSectionCommandCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackSectionViewModel;

            if (vm == null)
                return;

            var newCommand = new TrackSectionCommandViewModel();
            newCommand.Command = vm.SelectedTrackCommandValueToAdd;
            newCommand.PopulateArguments();

            var window = new TrackCommandDetailsWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = newCommand
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                vm.Commands.Add(newCommand);
            }
        }
    }
}
