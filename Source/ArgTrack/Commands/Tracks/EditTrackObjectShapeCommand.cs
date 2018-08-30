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
                // TODO: also pass in original, to allow reseting all properties
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                // map clone to actual value
                TrackMapper.MapTrackObjectShapeViewModel(clone, vm);
            }
        }
    }

    public class EditHorizonCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            // TODO: in command?
            var horizWindow = new HorizonWindow();
            horizWindow.DataContext = MainViewModelPasserArounder.MainViewModel.TrackViewModel;
            horizWindow.ShowDialog();


        }
    }
}
