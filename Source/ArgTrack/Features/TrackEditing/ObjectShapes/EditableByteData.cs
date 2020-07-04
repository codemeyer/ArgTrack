using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class EditableByteData : BaseViewModel
    {
        public EditableByteData(int index, byte value)
        {
            Index = index;
            _value = value;
            UpdateHexValue();
        }


        private int _index;

        public int Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value, () => Index); }
        }


        private byte _value;

        public byte Value
        {
            get { return _value; }
            set
            {
                if (SetProperty(ref _value, value, () => Value))
                {
                    UpdateHexValue();
                }
            }
        }


        private string _hexValue;

        public string HexValue
        {
            get { return _hexValue; }
            set { SetProperty(ref _hexValue, value, () => HexValue); }
        }

        // TODO: converter or something instead...

        private void UpdateHexValue()
        {
            HexValue = $"0x{Value:X2}";
        }
    }
}
