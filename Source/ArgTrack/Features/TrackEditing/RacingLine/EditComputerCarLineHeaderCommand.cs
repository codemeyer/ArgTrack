using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.RacingLine
{
    public class EditComputerCarLineHeaderCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = GetParameterAs<TrackViewModel>(parameter);

            if (vm == null)
                return;

            var newViewModel = new ComputerCarLineHeaderViewModel
            {
                Displacement = vm.ComputerCarLineDisplacement
            };

            var window = new ComputerCarLineHeaderWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = newViewModel
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                vm.ComputerCarLineDisplacement = newViewModel.Displacement;
            }
        }
    }
}
