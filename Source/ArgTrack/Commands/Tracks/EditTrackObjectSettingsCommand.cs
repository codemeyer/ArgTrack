using System.Windows;
using ArgTrack.Mappers;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class EditTrackObjectSettingsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackObjectSettingsViewModel;

            if (vm == null)
                return;

            var clone = new TrackObjectSettingsViewModel();
            TrackMapper.MapTrackObjectSettingsViewModel(vm, clone);

            var window = new TrackObjectSettingsDetailsWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = clone
                // TODO: also pass in original, to allow reseting all properties
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                // map clone to actual value
                TrackMapper.MapTrackObjectSettingsViewModel(clone, vm);
            }
        }
    }
}
