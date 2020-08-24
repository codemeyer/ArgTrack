using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class RemoveSelectedPointCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var objectShapeViewModel = parameter as TrackObjectShapeViewModel;

            if (objectShapeViewModel == null)
            {
                return;
            }

            var pointToRemove = objectShapeViewModel.SelectedPoint;

            objectShapeViewModel.Points.Remove(pointToRemove);

            objectShapeViewModel.CalculateSignature();
        }

        public override bool CanExecute(object parameter)
        {
            var objectShapeViewModel = parameter as TrackObjectShapeViewModel;

            var pointToRemove = objectShapeViewModel?.SelectedPoint;

            if (pointToRemove == null)
            {
                return false;
            }

            return !IsPointInUse(pointToRemove, objectShapeViewModel);
        }

        private bool IsPointInUse(TrackObjectShapePointViewModel pointToRemove, TrackObjectShapeViewModel objectShapeViewModel)
        {
            int pointToRemoveIndex = objectShapeViewModel.Points.IndexOf(pointToRemove);

            foreach (var vector in objectShapeViewModel.Vectors)
            {
                if (vector.From == pointToRemoveIndex || vector.To == pointToRemoveIndex)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
