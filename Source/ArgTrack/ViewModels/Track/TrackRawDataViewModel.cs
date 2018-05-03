namespace ArgTrack.ViewModels.Track
{
    public class TrackRawDataViewModel : BaseViewModel
    {
        // TODO: currently not used, see TrackHeaderViewModel
        // ObservableCollection?

        private byte[] _dataAfterSetup;

        public byte[] DataAfterSetup
        {
            get { return _dataAfterSetup; }
            set { SetProperty(ref _dataAfterSetup, value, () => DataAfterSetup); }
        }

        private byte[] _finalData1;

        public byte[] FinalData1
        {
            get { return _finalData1; }
            set { SetProperty(ref _finalData1, value, () => FinalData1); }
        }


        private byte[] _finalData2;

        public byte[] FinalData2
        {
            get { return _finalData2; }
            set { SetProperty(ref _finalData2, value, () => FinalData2); }
        }
    }
}
