using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Input;
using ArgTrack.Commands.Tracks;

namespace ArgTrack.ViewModels.Track
{
    public class TrackObjectShapeViewModel : BaseViewModel
    {


        private int _id;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value, () => Id); }
        }


        private int _headerIndex;

        public int HeaderIndex
        {
            get { return _headerIndex; }
            set { SetProperty(ref _headerIndex, value, () => HeaderIndex); }
        }


        private int _dataIndex;

        public int DataIndex
        {
            get { return _dataIndex; }
            set { SetProperty(ref _dataIndex, value, () => DataIndex); }
        }



        private short _offset1;

        public short Offset1
        {
            get { return _offset1; }
            set { SetProperty(ref _offset1, value, () => Offset1); }
        }


        private short _offset2;

        public short Offset2
        {
            get { return _offset2; }
            set { SetProperty(ref _offset2, value, () => Offset2); }
        }


        private short _offset3;

        public short Offset3
        {
            get { return _offset3; }
            set { SetProperty(ref _offset3, value, () => Offset3); }
        }


        private short _offset4;

        public short Offset4
        {
            get { return _offset4; }
            set { SetProperty(ref _offset4, value, () => Offset4); }
        }


        private short _offset5;

        public short Offset5
        {
            get { return _offset5; }
            set { SetProperty(ref _offset5, value, () => Offset5); }
        }


        private short _headerValue1;

        public short HeaderValue1
        {
            get { return _headerValue1; }
            set { SetProperty(ref _headerValue1, value, () => HeaderValue1); }
        }


        private short _headerValue2;

        public short HeaderValue2
        {
            get { return _headerValue2; }
            set { SetProperty(ref _headerValue2, value, () => HeaderValue2); }
        }


        private short _headerValue3;

        public short HeaderValue3
        {
            get { return _headerValue3; }
            set { SetProperty(ref _headerValue3, value, () => HeaderValue3); }
        }


        private short _headerValue4;

        public short HeaderValue4
        {
            get { return _headerValue4; }
            set { SetProperty(ref _headerValue4, value, () => HeaderValue4); }
        }


        private byte[] _headerValue5;

        public byte[] HeaderValue5
        {
            get { return _headerValue5; }
            set { SetProperty(ref _headerValue5, value, () => HeaderValue5); }
        }


        private byte[] _headerValue6;

        public byte[] HeaderValue6
        {
            get { return _headerValue6; }
            set { SetProperty(ref _headerValue6, value, () => HeaderValue6); }
        }



        private byte[] _offsetData1;

        public byte[] OffsetData1
        {
            get { return _offsetData1; }
            set { SetProperty(ref _offsetData1, value, () => OffsetData1); }
        }


        private byte[] _offsetData2;

        public byte[] OffsetData2
        {
            get { return _offsetData2; }
            set { SetProperty(ref _offsetData2, value, () => OffsetData2); }
        }


        private byte[] _offsetData3;

        public byte[] OffsetData3
        {
            get { return _offsetData3; }
            set { SetProperty(ref _offsetData3, value, () => OffsetData3); }
        }


        private byte[] _offsetData4;

        public byte[] OffsetData4
        {
            get { return _offsetData4; }
            set { SetProperty(ref _offsetData4, value, () => OffsetData4); }
        }


        private byte[] _offsetData5;

        public byte[] OffsetData5
        {
            get { return _offsetData5; }
            set { SetProperty(ref _offsetData5, value, () => OffsetData5); }
        }




        public void SetDescriptionBasedOnData()
        {
            // calculate checksum for data
            // match checksum against "known" values, somewhere

            var yo = new List<byte>();
            yo.AddRange(OffsetData1);
            yo.AddRange(OffsetData2);
            yo.AddRange(OffsetData3);
            yo.AddRange(OffsetData4);
            yo.AddRange(OffsetData5);

            var fulla = yo.ToArray();

            var sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(fulla);
            string hashtext = BitConverter.ToString(hash);
            hashtext = hashtext.Replace("-", "");

            ulong hashio = BitConverter.ToUInt64(hash, 0);
            //var hashtext = Encoding.ASCII.GetString(hash);

            // TODO: stuff...
        }



        public ICommand EditGenericByteDataCommand { get; } = new EditGenericByteDataCommand();
    }
}
