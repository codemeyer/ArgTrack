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
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                TrackMapper.MapCarSettingsViewModel(clone, vm);
            }
        }
    }
}
