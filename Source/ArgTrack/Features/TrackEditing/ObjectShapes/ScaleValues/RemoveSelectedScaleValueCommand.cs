using System.Linq;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class RemoveSelectedScaleValueCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var objectShapeViewModel = parameter as TrackObjectShapeViewModel;

            if (objectShapeViewModel == null)
            {
                return;
            }

            var scaleValueToRemoveIndex = objectShapeViewModel.SelectedScaleValueIndex;

            objectShapeViewModel.ScaleValues.RemoveAt(scaleValueToRemoveIndex);
            objectShapeViewModel.ShapeBuddy.ScaleValues.RemoveAt(scaleValueToRemoveIndex);

            foreach (var po in objectShapeViewModel.Points.Where(po => po.PointType == TrackObjectShapePointTypeEnum.Scale))
            {
                if (po.XScaleValueIndex > scaleValueToRemoveIndex)
                {
                    po.XScaleValueIndex--;
                }

                if (po.YScaleValueIndex > scaleValueToRemoveIndex)
                {
                    po.YScaleValueIndex--;
                }
            }

            objectShapeViewModel.CalculateSignature();
        }

        public override bool CanExecute(object parameter)
        {
            var objectShapeViewModel = parameter as TrackObjectShapeViewModel;

            if (objectShapeViewModel == null)
            {
                return false;
            }

            if (objectShapeViewModel.SelectedScaleValueIndex < 0)
            {
                return false;
            }

            return !IsScaleValueInUse(objectShapeViewModel.SelectedScaleValueIndex, objectShapeViewModel);
        }

        private bool IsScaleValueInUse(int scaleValueIndex, TrackObjectShapeViewModel objectShapeViewModel)
        {
            foreach (var po in objectShapeViewModel.Points)
            {
                if (po.PointType == TrackObjectShapePointTypeEnum.Scale)
                {
                    if (po.XScaleValueIndex == scaleValueIndex)
                    {
                        return true;
                    }

                    if (po.YScaleValueIndex == scaleValueIndex)
                    {
                        return true;
                    }
                }

                if (po.PointType == TrackObjectShapePointTypeEnum.Reference)
                {
                    // not relevant?
                }
            }

            return false;
        }
    }
}
