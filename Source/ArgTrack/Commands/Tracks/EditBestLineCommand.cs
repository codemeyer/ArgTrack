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
                // TODO: also pass in original, to allow reseting all properties
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                // map clone to actual value
                TrackMapper.MapTrackBestLineViewModel(clone, vm);
            }
        }
    }
}
