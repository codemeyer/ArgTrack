using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.TrackSettings
{
    public class EditTrackLayoutSettingsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackSettingsViewModel;

            if (vm == null)
                return;

            var clone = new TrackSettingsViewModel();
            TrackSettingsMapper.MapTrackSettingsViewModel(vm, clone);

            var window = new TrackLayoutWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                bool needsRepainting = NeedsRepainting(vm, clone);

                TrackSettingsMapper.MapTrackSettingsViewModel(clone, vm);

                if (needsRepainting)
                {
                    // TODO: not global, raise event instead?
                    TrackViewModelPasserArounder.TrackViewModel.RecalculateLayout();
                }
            }
        }

        private bool NeedsRepainting(TrackSettingsViewModel original, TrackSettingsViewModel updated)
        {
            if (original.SurroundingArea != updated.SurroundingArea)
            {
                return true;
            }

            if (original.PitsSide != updated.PitsSide)
            {
                return true;
            }

            return false;
        }
    }
}
