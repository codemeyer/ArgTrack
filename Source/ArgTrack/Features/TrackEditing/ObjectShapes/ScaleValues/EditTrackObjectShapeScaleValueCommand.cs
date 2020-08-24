using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class EditTrackObjectShapeScaleValueCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackObjectShapeViewModel;

            if (vm == null)
                return;

            var value = vm.ScaleValues[vm.SelectedScaleValueIndex];

            var edit = new EditableScaleValueDataViewModel(value);

            var window = new EditTrackShapeScaleValueWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = edit
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                vm.ScaleValues[vm.SelectedScaleValueIndex] = edit.Value;
                vm.ShapeBuddy.ScaleValues[vm.SelectedScaleValueIndex] = edit.Value;

                foreach (var point in vm.Points)
                {
                    point.Recalculate();

                    if (point.PointType  == TrackObjectShapePointTypeEnum.Scale)
                    {
                    }
                }

                // TODO: update other stuff?
            }
        }
    }
}
