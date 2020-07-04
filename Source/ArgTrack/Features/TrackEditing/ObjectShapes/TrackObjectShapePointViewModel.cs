using ArgData.Entities;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class TrackObjectShapePointViewModel : BaseViewModel
    {
        private readonly TrackObjectShape _parentOriginalShape;

        public TrackObjectShapePointViewModel(ITrackObjectShapePoint relatedPoint, TrackObjectShape parentOriginalShape)
        {
            _parentOriginalShape = parentOriginalShape;
            RelatedPoint = relatedPoint;

            if (relatedPoint is TrackObjectShapeScalePoint scalePoint)
            {
                PointType = TrackObjectShapePointTypeEnum.Scale;
                XScaleValueIndex = scalePoint.XScaleValueIndex;
                XIsNegative = scalePoint.XIsNegative;
                YScaleValueIndex = scalePoint.YScaleValueIndex;
                YIsNegative = scalePoint.YIsNegative;
                Z = scalePoint.Z;
            }

            if (relatedPoint is TrackObjectShapeReferencePoint refPoint)
            {
                PointType = TrackObjectShapePointTypeEnum.Reference;
                PointIndex = refPoint.PointIndex;
                Z = refPoint.Z;
            }
        }


        public ITrackObjectShapePoint RelatedPoint { get; }


        private TrackObjectShapePointTypeEnum _pointType;

        public TrackObjectShapePointTypeEnum PointType
        {
            get { return _pointType; }
            set { SetProperty(ref _pointType, value, () => PointType); }
            // should be readonly?
        }


        private int _index;

        public int Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value, () => Index); }
        }


        private short _xScaleValueIndex;

        public short XScaleValueIndex
        {
            get { return _xScaleValueIndex; }
            set
            {
                if (SetProperty(ref _xScaleValueIndex, value, () => XScaleValueIndex))
                {
                    (RelatedPoint as TrackObjectShapeScalePoint).XScaleValueIndex = value;
                    Recalculate();
                }
            }
        }


        private bool _xIsNegative;

        public bool XIsNegative
        {
            get { return _xIsNegative; }
            set
            {
                if (SetProperty(ref _xIsNegative, value, () => XIsNegative))
                {
                    (RelatedPoint as TrackObjectShapeScalePoint).XIsNegative = value;
                    Recalculate();
                }
            }
        }


        private short _yScaleValueIndex;

        public short YScaleValueIndex
        {
            get { return _yScaleValueIndex; }
            set
            {
                if (SetProperty(ref _yScaleValueIndex, value, () => YScaleValueIndex))
                {
                    (RelatedPoint as TrackObjectShapeScalePoint).YScaleValueIndex = value;
                    Recalculate();
                }
            }
        }


        private bool _yIsNegative;

        public bool YIsNegative
        {
            get { return _yIsNegative; }
            set
            {
                if (SetProperty(ref _yIsNegative, value, () => YIsNegative))
                {
                    (RelatedPoint as TrackObjectShapeScalePoint).YIsNegative = value;
                    Recalculate();
                }
            }
        }


        private ushort _z;

        public ushort Z
        {
            get { return _z; }
            set
            {
                if (SetProperty(ref _z, value, () => Z))
                {
                    if (PointType == TrackObjectShapePointTypeEnum.Scale)
                    {
                        (RelatedPoint as TrackObjectShapeScalePoint).Z = value;
                    }
                    if (PointType == TrackObjectShapePointTypeEnum.Reference)
                    {
                        (RelatedPoint as TrackObjectShapeReferencePoint).Z = value;
                    }

                    Recalculate();
                }
            }
        }


        private byte _pointIndex;

        public byte PointIndex
        {
            get { return _pointIndex; }
            set
            {
                if (SetProperty(ref _pointIndex, value, () => PointIndex))
                {
                    (RelatedPoint as TrackObjectShapeReferencePoint).PointIndex = value;
                    Recalculate();
                }
            }
        }


        private short _x;

        public short X
        {
            get { return _x; }
        }


        private short _y;

        public short Y
        {
            get { return _y; }
        }


        public void Recalculate()
        {
            _x = RelatedPoint.X;
            OnPropertyChanged(nameof(X));

            _y = RelatedPoint.Y;
            OnPropertyChanged(nameof(Y));
        }
    }
}
