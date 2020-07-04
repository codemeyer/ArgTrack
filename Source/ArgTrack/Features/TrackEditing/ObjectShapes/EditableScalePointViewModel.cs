using System.Collections.Generic;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class EditableScalePointViewModel : BaseViewModel
    {
        private readonly List<short> _scaleValues;

        public EditableScalePointViewModel(List<short> scaleValues, short currentX, short currentY, ushort currentZ)
        {
            _scaleValues = scaleValues;

            foreach (var values in AvailablePointValues)
            {
                if (values.DisplayValue == currentX.ToString())
                {
                    XValue = values;
                }

                if (values.DisplayValue == currentY.ToString())
                {
                    YValue = values;
                }
            }

            ZValue = currentZ;
        }


        private List<PointValueBoundType> _availablePointValues;

        public List<PointValueBoundType> AvailablePointValues
        {
            get
            {
                if (_availablePointValues != null)
                {
                    return _availablePointValues;
                }

                var list = new List<PointValueBoundType>();

                list.Add(new PointValueBoundType(-1, 0, "0", false));

                short index = 0;

                foreach (var scaleValue in _scaleValues)
                {
                    list.Add(new PointValueBoundType(index, scaleValue, scaleValue.ToString(), false));
                    list.Add(new PointValueBoundType(index, (short)(scaleValue * -1), (scaleValue * -1).ToString(), true));

                    index++;
                }

                _availablePointValues = list;

                return list;
            }
        }



        private PointValueBoundType _xValue;

        public PointValueBoundType XValue
        {
            get { return _xValue; }
            set { SetProperty(ref _xValue, value, () => XValue); }
        }



        private PointValueBoundType _yValue;

        public PointValueBoundType YValue
        {
            get { return _yValue; }
            set { SetProperty(ref _yValue, value, () => YValue); }
        }



        private ushort _zValue;

        public ushort ZValue
        {
            get { return _zValue; }
            set { SetProperty(ref _zValue, value, () => ZValue); }
        }

    }


    public class PointValueBoundType
    {
        public PointValueBoundType(short index, short value, string displayValue, bool isNegative)
        {
            Value = this;
            DisplayValue = displayValue;
            // TODO: more

            Index = index;
            IsNegative = isNegative;
        }

        public PointValueBoundType Value { get; set; }
        public string DisplayValue { get; set; }

        public short Index { get; set; }
        public bool IsNegative { get; set; }
    }


    //public class KerbTypeBoundValue
    //{
    //    public KerbTypeBoundValue(KerbType value, string displayValue)
    //    {
    //        Value = value;
    //        DisplayValue = displayValue;
    //    }

    //    public KerbType Value { get; set; }
    //    public string DisplayValue { get; set; }
    //}


    public class PointReferenceValueBoundType
    {
        public PointReferenceValueBoundType(byte index, byte value, string displayValue)
        {
            Index = index;
            Value = value;
            DisplayValue = displayValue;
        }

        //public PointReferenceValueBoundType Value { get; set; }
        public byte Value { get; set; }
        public string DisplayValue { get; set; }

        public byte Index { get; set; }
    }
}
