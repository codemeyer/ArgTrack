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
            var viewModel = parameter as TrackObjectSettingsViewModel;

            if (viewModel == null)
                return;

            var clone = new TrackObjectSettingsViewModel();

            ViewModelCopyMapper.CopyTrackObjectSettingsViewModel(viewModel, clone);

            var window = new TrackObjectSettingsDetailsWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                ViewModelCopyMapper.CopyTrackObjectSettingsViewModel(clone, viewModel);
            }
        }
    }
}
