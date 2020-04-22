using System.Linq;
using System.Runtime.Serialization.Formatters;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.TrackSection
{
    public class EditTrackSectionCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackSectionViewModel;

            if (vm == null)
                return;

            var clone = new TrackSectionViewModel(vm.ParentList);
            TrackSectionMapper.MapTrackSectionViewModel(vm, clone);

            var window = new TrackSectionDetailsWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                bool needsRepainting = NeedsRepainting(vm, clone);

                // map clone to actual value
                TrackSectionMapper.MapTrackSectionViewModel(clone, vm);

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

            if (original.LeftVergeWidth != updated.LeftVergeWidth)
            {
                return true;
            }

            if (original.RightVergeWidth != updated.RightVergeWidth)
            {
                return true;
            }

            if (original.BridgedLeftFence != updated.BridgedLeftFence)
            {
                return true;
            }

            if (original.BridgedRightFence != updated.BridgedRightFence)
            {
                return true;
            }

            // added or removed track width change
            if (original.Commands.Count(c => c.Command == 0x85) != updated.Commands.Count(c => c.Command == 0x85))
            {
                return true;
            }

            if (original.Commands.Count(c => c.Command == 0x85) > 0 &&
                original.Commands.Count(c => c.Command == 0x85) == updated.Commands.Count(c => c.Command == 0x85))
            {
                // TODO: can there be multiple here?
                var originalCommand = original.Commands.Single(c => c.Command == 0x85);
                var updatedCommand = updated.Commands.Single(c => c.Command == 0x85);

                if (originalCommand.Arguments[0].Value != updatedCommand.Arguments[0].Value)
                {
                    return true;
                }

                if (originalCommand.Arguments[1].Value != updatedCommand.Arguments[1].Value)
                {
                    return true;
                }

                if (originalCommand.Arguments[2].Value != updatedCommand.Arguments[2].Value)
                {
                    return true;
                }
            }

            // added or removed object placement
            if (original.Commands.Count(c => c.Command == 0x80) != updated.Commands.Count(c => c.Command == 0x80))
            {
                return true;
            }

            // if there are any object placements in the updated version, take the easy way out and assume redraw
            if (updated.Commands.Count(c => c.Command == 0x80) > 0)
            {
                return true;
            }

            return false;
        }
    }
}
