using System.Windows;
using ArgTrack.Mappers;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class EditTrackSectionCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackSectionViewModel;

            if (vm == null)
                return;

            var clone = new TrackSectionViewModel(vm.ParentList);
            TrackMapper.MapTrackSectionViewModel(vm, clone);

            var window = new TrackSectionDetailsWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = clone
                // TODO: also pass in original, to allow reseting all properties
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                bool needsRepainting = NeedsRepainting(vm, clone);

                // map clone to actual value
                TrackMapper.MapTrackSectionViewModel(clone, vm);

                if (needsRepainting)
                {
                    vm.ParentList.RecalculateGraphics();
                }
            }
        }

        private bool NeedsRepainting(TrackSectionViewModel original, TrackSectionViewModel updated)
        {
            if (original.Length != updated.Length)
            {
                return true;
            }

            if (original.Curvature != updated.Curvature)
            {
                return true;
            }

            // track width?

            return false;
        }
    }
}
