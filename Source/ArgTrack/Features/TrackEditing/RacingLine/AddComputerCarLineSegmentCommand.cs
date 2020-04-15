using ArgData.Entities;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.RacingLine
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
            newSection.SegmentType = TrackComputerCarLineSegmentType.Normal;

            var window = new TrackBestLineDetailsWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
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
