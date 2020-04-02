using System.Windows;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class EditComputerCarLineHeaderCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = GetParameterAs<TrackViewModel>(parameter);

            if (vm == null)
                return;

            short original = vm.ComputerCarLineDisplacement;

            var window = new ComputerCarLineHeaderWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = vm
            };

            bool? update = window.ShowDialog();

            if (!update.HasValue || !update.Value)
            {
                // no change, restore value
                vm.ComputerCarLineDisplacement = original;
            }
        }
    }
}
