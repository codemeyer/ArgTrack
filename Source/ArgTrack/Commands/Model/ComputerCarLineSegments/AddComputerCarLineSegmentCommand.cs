using System.Windows;
using ArgData.Entities;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Model.ComputerCarLineSegments
{
    public class AddComputerCarLineSegmentCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = GetParameterAs<BestLineSegmentViewModelCollection>(parameter);

            if (vm == null)
                return;

            var newSection = new BestLineSegmentViewModel(vm);
            newSection.CanEditType = true;
            newSection.SegmentType = TrackBestLineSegmentType.Normal;

            var window = new TrackBestLineDetailsWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = newSection
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                vm.Add(newSection);
            }
        }
    }
}
