using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class AddObjectShapeScaleValueCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var objectShapeViewModel = parameter as TrackObjectShapeViewModel;

            if (objectShapeViewModel == null)
            {
                return;
            }

            var edit = new EditableScaleValueDataViewModel(0);

            var window = new EditTrackShapeScaleValueWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = edit
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                objectShapeViewModel.ScaleValues.Add(edit.Value);
                objectShapeViewModel.ShapeBuddy.ScaleValues.Add(edit.Value);
            }
        }
    }
}
