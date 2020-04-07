using System.Windows;
using ArgTrack.Mappers;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class EditBestLineCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as BestLineSegmentViewModel;

            if (vm == null)
                return;

            var clone = new BestLineSegmentViewModel(vm.ParentList);
            TrackMapper.MapTrackBestLineViewModel(vm, clone);

            var window = new TrackBestLineDetailsWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                TrackMapper.MapTrackBestLineViewModel(clone, vm);
            }
        }
    }
}
