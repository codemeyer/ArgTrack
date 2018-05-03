using System.Collections.ObjectModel;

namespace ArgTrack.ViewModels.Track
{
    public class GenericByteListEditingViewModel : BaseViewModel
    {
        public GenericByteListEditingViewModel()
        {
        }


        // TODO: bool CanAddRemoveBytes ?

        // ObservableCollectino =?

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
