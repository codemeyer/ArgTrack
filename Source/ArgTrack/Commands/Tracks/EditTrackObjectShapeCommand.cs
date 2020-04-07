using System.Windows;
using ArgTrack.Mappers;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class EditTrackObjectShapeCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackObjectShapeViewModel;

            if (vm == null)
                return;

            var clone = new TrackObjectShapeViewModel();
            TrackMapper.MapTrackObjectShapeViewModel(vm, clone);

            // temp
            clone.SetDescriptionBasedOnData();

            var window = new TrackObjectShapeDetailsWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                TrackMapper.MapTrackObjectShapeViewModel(clone, vm);
            }
        }
    }
}
