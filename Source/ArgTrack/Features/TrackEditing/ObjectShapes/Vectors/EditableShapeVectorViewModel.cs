using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class EditableShapeVectorViewModel : BaseViewModel
    {
        public EditableShapeVectorViewModel(ObservableCollection<TrackObjectShapePointViewModel> points)
        {
            _points = points;
        }

        private byte _from;

        public byte From
        {
            get { return _from; }
            set { SetProperty(ref _from, value, () => From); }
        }

        private byte _to;

        public byte To
        {
            get { return _to; }
            set { SetProperty(ref _to, value, () => To); }
        }


        private ObservableCollection<TrackObjectShapePointViewModel> _points;

        private List<PointReferenceValueBoundType> _availablePoints;

        public List<PointReferenceValueBoundType> AvailablePoints
        {
            get
            {
                if (_availablePoints != null)
                {
                    return _availablePoints;
                }

                var list = new List<PointReferenceValueBoundType>();


                byte index = 0;

                foreach (var point in _points)
                {
                    var boundPoint = new PointReferenceValueBoundType(index, index, $"[{index}] X:{point.X}, Y:{point.Y}, Z:{point.Z}");

                    list.Add(boundPoint);

                    index++;
                }

                _availablePoints = list;

                return list;
            }
        }
    }
}
