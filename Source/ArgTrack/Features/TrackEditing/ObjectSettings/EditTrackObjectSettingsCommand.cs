using ArgTrack.Features.Main;
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
                bool needsRepainting = NeedsRepainting(viewModel, clone);

                ObjectSettingsMapper.CopyTrackObjectSettingsViewModel(clone, viewModel);

                if (needsRepainting)
                {
                    // TODO: not so directly...
                    TrackViewModelPasserArounder.TrackViewModel.RecalculateLayout();
                }
            }
        }

        private bool NeedsRepainting(TrackObjectSettingsViewModel viewModel, TrackObjectSettingsViewModel clone)
        {
            if (clone.DistanceFromTrack != viewModel.DistanceFromTrack)
            {
                return true;
            }

            return false;
        }
    }
}
