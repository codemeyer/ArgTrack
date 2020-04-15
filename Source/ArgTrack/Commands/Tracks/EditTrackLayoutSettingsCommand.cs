using System.Windows;
using ArgTrack.Mappers;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class EditTrackLayoutSettingsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackSettingsViewModel;

            if (vm == null)
                return;

            var clone = new TrackSettingsViewModel();
            TrackMapper.MapTrackSettingsViewModel(vm, clone);

            var window = new TrackLayoutWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                bool needsRepainting = NeedsRepainting(vm, clone);

                TrackMapper.MapTrackSettingsViewModel(clone, vm);

                if (needsRepainting)
                {
                    // TODO: not global
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
