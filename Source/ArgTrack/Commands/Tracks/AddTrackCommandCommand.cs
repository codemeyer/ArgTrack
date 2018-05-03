using System.Windows;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class AddTrackCommandCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackSectionViewModel;

            if (vm == null)
                return;

            var newCommand = new TrackCommandViewModel();
            newCommand.Command = vm.SelectedTrackCommandValueToAdd;
            newCommand.PopulateArguments();

            var window = new TrackCommandDetailsWindow
            {
                Owner = Application.Current.MainWindow,
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
