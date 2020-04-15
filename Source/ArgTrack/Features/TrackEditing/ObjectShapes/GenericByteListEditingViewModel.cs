using System.Collections.ObjectModel;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class GenericByteListEditingViewModel : BaseViewModel
    {
        public void SetData(byte[] data)
        {
            var list = new ObservableCollection<EditableByteData>();

            foreach(var value in data)
            {
                var byteData = new EditableByteData(value);
                list.Add(byteData);
            }

            ByteData = list;
        }


        private ObservableCollection<EditableByteData> _byteData;

        public ObservableCollection<EditableByteData> ByteData
        {
            get { return _byteData; }
            set { SetProperty(ref _byteData, value, () => ByteData); }
        }
    }
}
