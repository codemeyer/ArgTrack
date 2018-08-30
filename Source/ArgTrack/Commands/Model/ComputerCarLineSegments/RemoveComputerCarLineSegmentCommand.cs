using System.Windows;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Model.ComputerCarLineSegments
{
    public class RemoveComputerCarLineSegmentCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = GetParameterAs<BestLineSegmentViewModel>(parameter);

            if (vm == null)
            {
                return;
            }

            var result = MessageBox.Show("Are you sure that you wish to remove the segment?", "ArgTrack", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                vm.ParentList.Remove(vm);
                vm.ParentList.RecalculateGraphics();
            }
        }
    }
}
