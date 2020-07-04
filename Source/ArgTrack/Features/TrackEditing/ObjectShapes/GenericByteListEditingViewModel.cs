using System.Collections.ObjectModel;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class GenericByteListEditingViewModel : BaseViewModel
    {
        public void SetData(byte[] data)
        {
            var list = new ObservableCollection<EditableByteData>();

            int index = 0;

            foreach(var value in data)
            {
                var byteData = new EditableByteData(index, value);
                list.Add(byteData);

                index++;
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
