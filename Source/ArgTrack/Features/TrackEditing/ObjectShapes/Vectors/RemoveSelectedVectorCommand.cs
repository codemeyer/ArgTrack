using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class RemoveSelectedVectorCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var objectShapeViewModel = parameter as TrackObjectShapeViewModel;

            if (objectShapeViewModel == null)
            {
                return;
            }

            var vectorToRemove = objectShapeViewModel.SelectedVector;

            objectShapeViewModel.Vectors.Remove(vectorToRemove);

            objectShapeViewModel.CalculateSignature();
        }

        public override bool CanExecute(object parameter)
        {
            var objectShapeViewModel = parameter as TrackObjectShapeViewModel;

            var vectorToRemove = objectShapeViewModel?.SelectedVector;

            return vectorToRemove != null;
        }
    }
}
