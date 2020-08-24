using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class EditableReferencePointViewModel : BaseViewModel
    {
        private readonly ObservableCollection<TrackObjectShapePointViewModel> _points;

        public EditableReferencePointViewModel(ObservableCollection<TrackObjectShapePointViewModel> points, byte current, ushort z)
        {
            _points = points;
            SelectedPointIndex = current;
            ZValue = z;
        }

        private List<PointReferenceValueBoundType> _availableScalePoints;

        public List<PointReferenceValueBoundType> AvailableScalePoints
        {
            get
            {
                if (_availableScalePoints != null)
                {
                    return _availableScalePoints;
                }

                var list = new List<PointReferenceValueBoundType>();

                byte index = 0;

                foreach (var point in _points)
                {
                    if (point.PointType == TrackObjectShapePointTypeEnum.Scale)
                    {
                        var scalePoint = new PointReferenceValueBoundType(index, index, $"[{index}] {point.X}, {point.Y}");

                        list.Add(scalePoint);
                    }

                    index++;
                }

                _availableScalePoints = list;

                return list;
            }
        }


        private byte _selectedPointIndex;

        public byte SelectedPointIndex
        {
            get { return _selectedPointIndex; }
            set { SetProperty(ref _selectedPointIndex, value, () => SelectedPointIndex); }
        }



        private ushort _zValue;

        public ushort ZValue
        {
            get { return _zValue; }
            set { SetProperty(ref _zValue, value, () => ZValue); }
        }

    }
}
