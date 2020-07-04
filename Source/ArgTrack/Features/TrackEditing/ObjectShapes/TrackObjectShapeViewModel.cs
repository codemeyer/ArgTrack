using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Windows.Input;
using ArgData.Entities;
using ArgTrack.Features.Application.Shared;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class TrackObjectShapeViewModel : BaseViewModel
    {
        public TrackObjectShapeViewModel()
        {
            _scaleValues = new ObservableCollection<short>();
            _points = new ObservableCollection<TrackObjectShapePointViewModel>();
            _vectors = new ObservableCollection<TrackObjectShapeVectorViewModel>();
        }

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

        // TODO: rename
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

        // TODO: rename
        public short Offset3
        {
            get { return _offset3; }
            set { SetProperty(ref _offset3, value, () => Offset3); }
        }


        private short _offset4;

        // TODO: rename
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



        //private byte[] _offsetData1;

        //// ScaleValues, remove this?
        //public byte[] OffsetData1
        //{
        //    get { return _offsetData1; }
        //    set { SetProperty(ref _offsetData1, value, () => OffsetData1); }
        //}


        private byte[] _offsetData2;

        // GraphicElements?
        public byte[] OffsetData2
        {
            get { return _offsetData2; }
            set { SetProperty(ref _offsetData2, value, () => OffsetData2); }
        }


        //private byte[] _offsetData3;

        //// Points, remove this?
        //public byte[] OffsetData3
        //{
        //    get { return _offsetData3; }
        //    set { SetProperty(ref _offsetData3, value, () => OffsetData3); }
        //}


        //private byte[] _offsetData4;

        //// Vectors, remove this?
        //public byte[] OffsetData4
        //{
        //    get { return _offsetData4; }
        //    set { SetProperty(ref _offsetData4, value, () => OffsetData4); }
        //}


        private byte[] _offsetData5;

        // GraphicElementsList?
        public byte[] OffsetData5
        {
            get { return _offsetData5; }
            set { SetProperty(ref _offsetData5, value, () => OffsetData5); }
        }


        private void UpdateDescription()
        {
            Description = ObjectShapeDescriptions.GetDescription(Signature);
        }


        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value, () => Description); }
        }



        private ObservableCollection<short> _scaleValues;

        public ObservableCollection<short> ScaleValues
        {
            get { return _scaleValues; }
        }


        private ObservableCollection<TrackObjectShapePointViewModel> _points;

        public ObservableCollection<TrackObjectShapePointViewModel> Points
        {
            get { return _points; }
        }



        private TrackObjectShapePointViewModel _selectedPoint;

        public TrackObjectShapePointViewModel SelectedPoint
        {
            get { return _selectedPoint; }
            set { SetProperty(ref _selectedPoint, value, () => SelectedPoint); }
        }



        private ObservableCollection<TrackObjectShapeVectorViewModel> _vectors;

        public ObservableCollection<TrackObjectShapeVectorViewModel> Vectors
        {
            get { return _vectors; }
            set { SetProperty(ref _vectors, value, () => Vectors); }
        }


        private TrackObjectShapeVectorViewModel _selectedVector;

        public TrackObjectShapeVectorViewModel SelectedVector
        {
            get { return _selectedVector; }
            set { SetProperty(ref _selectedVector, value, () => SelectedVector); }
        }


        /// <summary>
        /// Calculates the shape "signature", which is a hash based on the points and vectors of the shape.
        /// </summary>
        public void CalculateSignature()
        {
            var bytes = new List<byte>();

            foreach (var point in Points)
            {
                bytes.AddRange(BitConverter.GetBytes(point.X));
                bytes.AddRange(BitConverter.GetBytes(point.Y));
                bytes.AddRange(BitConverter.GetBytes(point.Z));
            }

            foreach (var vector in Vectors)
            {
                bytes.Add(vector.From);
                bytes.Add(vector.To);
            }

            var byteArray = bytes.ToArray();
            var sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(byteArray);
            ulong sig = BitConverter.ToUInt64(hash, 0);

            Signature = sig;
        }


        private ulong _signature;

        public ulong Signature
        {
            get { return _signature; }
            set
            {
                if (SetProperty(ref _signature, value, () => Signature))
                {
                    UpdateDescription();
                }
            }
        }


        private int _selectedScaleValueIndex;

        public int SelectedScaleValueIndex
        {
            get { return _selectedScaleValueIndex; }
            set { SetProperty(ref _selectedScaleValueIndex, value, () => SelectedScaleValueIndex); }
        }




        public ICommand EditGenericByteDataCommand { get; } = new EditGenericByteDataCommand();

        public ICommand EditTrackObjectShapeScaleValueCommand { get; } = new EditTrackObjectShapeScaleValueCommand();

        public ICommand Edit { get; } = new EditTrackObjectShapeCommand();

        public ICommand HelpDocsCommand { get; } = new HelpDocsCommand("screens/object-shape-details-window/");

        public TrackObjectShape ShapeBuddy { get; set; }
    }


    public enum TrackObjectShapePointTypeEnum
    {
        Scale,
        Reference
    }
}
