using ArgData.Entities;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class AddObjectShapeScalePointCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var objectShapeViewModel = parameter as TrackObjectShapeViewModel;

            if (objectShapeViewModel == null)
            {
                return;
            }

            var edi = new EditableScalePointViewModel(objectShapeViewModel.ShapeBuddy.ScaleValues, 0, 0, 0);

            var win = new EditTrackObjectShapeScalePointWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = edi
            };

            win.ShowDialog();

            if (win.DialogResult == true)
            {
                var point = new TrackObjectShapePointViewModel(new TrackObjectShapeScalePoint(objectShapeViewModel.ShapeBuddy), objectShapeViewModel.ShapeBuddy);

                point.XScaleValueIndex = edi.XValue.Index;
                point.XIsNegative = edi.XValue.IsNegative;

                point.YScaleValueIndex = edi.YValue.Index;
                point.YIsNegative = edi.YValue.IsNegative;

                point.Z = edi.ZValue;

                point.Index = objectShapeViewModel.Points.Count;

                objectShapeViewModel.Points.Add(point);

                objectShapeViewModel.CalculateSignature();
            }
        }
    }

    public class AddObjectShapeReferencePointCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var objectShapeViewModel = parameter as TrackObjectShapeViewModel;

            if (objectShapeViewModel == null)
            {
                return;
            }

            var edi = new EditableReferencePointViewModel(objectShapeViewModel.Points, 0, 0);

            var win = new EditTrackObjectShapeReferencePointWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = edi
            };

            win.ShowDialog();

            if (win.DialogResult == true)
            {
                var point = new TrackObjectShapePointViewModel(new TrackObjectShapeReferencePoint(objectShapeViewModel.ShapeBuddy), objectShapeViewModel.ShapeBuddy);

                point.PointIndex = edi.SelectedPointIndex;
                point.Z = edi.ZValue;

                point.Index = objectShapeViewModel.Points.Count;

                objectShapeViewModel.Points.Add(point);

                objectShapeViewModel.CalculateSignature();
            }
        }
    }
}
