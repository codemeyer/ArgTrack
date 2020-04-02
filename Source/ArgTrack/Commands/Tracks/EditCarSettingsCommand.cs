using System.Windows;
using ArgTrack.Mappers;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class EditCarSettingsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = GetParameterAs<CarSettingsViewModel>(parameter);

            if (vm == null)
                return;

            var clone = new CarSettingsViewModel();
            TrackMapper.MapCarSettingsViewModel(vm, clone);

            var window = new CarSettingsWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = clone
                // TODO: also pass in original, to allow reseting all properties
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                // map clone to actual value
                TrackMapper.MapCarSettingsViewModel(clone, vm);

                // TODO: repaint 3D track! ie LayoutCalculator etc
            }
        }
    }
}
