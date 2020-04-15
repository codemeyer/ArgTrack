using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.RacingLine
{
    public class EditBestLineCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as BestLineSegmentViewModel;

            if (vm == null)
                return;

            var clone = new BestLineSegmentViewModel(vm.ParentList);
            RacingLineMapper.MapTrackBestLineViewModel(vm, clone);

            var window = new TrackBestLineDetailsWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                RacingLineMapper.MapTrackBestLineViewModel(clone, vm);
            }
        }
    }
}
