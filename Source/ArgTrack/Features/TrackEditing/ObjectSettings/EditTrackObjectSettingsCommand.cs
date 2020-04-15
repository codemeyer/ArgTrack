using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectSettings
{
    public class EditTrackObjectSettingsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var viewModel = parameter as TrackObjectSettingsViewModel;

            if (viewModel == null)
                return;

            var clone = new TrackObjectSettingsViewModel();

            ObjectSettingsMapper.CopyTrackObjectSettingsViewModel(viewModel, clone);

            var window = new TrackObjectSettingsDetailsWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                ObjectSettingsMapper.CopyTrackObjectSettingsViewModel(clone, viewModel);
            }
        }
    }
}
