using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.CarSettings
{
    public class EditCarSettingsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = GetParameterAs<CarSettingsViewModel>(parameter);

            if (vm == null)
                return;

            var clone = new CarSettingsViewModel();
            CarSettingsMapper.MapCarSettingsViewModel(vm, clone);

            var window = new CarSettingsWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                CarSettingsMapper.MapCarSettingsViewModel(clone, vm);
            }
        }
    }
}
