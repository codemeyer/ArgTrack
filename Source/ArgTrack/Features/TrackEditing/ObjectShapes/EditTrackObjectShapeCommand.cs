using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class EditTrackObjectShapeCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackObjectShapeViewModel;

            if (vm == null)
                return;

            var clone = new TrackObjectShapeViewModel();
            ObjectShapeMapper.MapTrackObjectShapeViewModel(vm, clone);

            var window = new TrackObjectShapeDetailsWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                ObjectShapeMapper.MapTrackObjectShapeViewModel(clone, vm);
                vm.CalculateSignature();
            }
        }
    }
}
