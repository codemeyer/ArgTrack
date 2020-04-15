using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.WorldLocation
{
    public class EditTrackHeaderSettingsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackHeaderViewModel;

            if (vm == null)
                return;

            var clone = new TrackHeaderViewModel();
            WorldLocationMapper.CopyTrackHeaderViewModel(vm, clone);

            var window = new TrackHeaderWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                bool needsRepainting = NeedsRepainting(vm, clone);

                // map clone to actual value
                WorldLocationMapper.CopyTrackHeaderViewModel(clone, vm);

                if (needsRepainting)
                {
                    // TODO: not global
                    TrackViewModelPasserArounder.TrackViewModel.RecalculateLayout();
                }
            }
        }

        private bool NeedsRepainting(TrackHeaderViewModel original, TrackHeaderViewModel updated)
        {
            if (original.LeftVergeStartWidth != updated.LeftVergeStartWidth)
            {
                return true;
            }

            if (original.RightVergeStartWidth != updated.RightVergeStartWidth)
            {
                return true;
            }

            if (original.TrackStartWidth != updated.TrackStartWidth)
            {
                return true;
            }

            return false;
        }
    }
}
