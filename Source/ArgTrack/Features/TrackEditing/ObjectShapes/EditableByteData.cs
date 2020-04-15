using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class EditableByteData : BaseViewModel
    {
        public EditableByteData(byte value)
        {
            _value = value;
            UpdateHexValue();
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
