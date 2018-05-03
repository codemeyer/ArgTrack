using System.Collections.Generic;
using System.Windows;
using ArgData.Entities;

namespace ArgTrack.ViewModels.Track
{
    public class BestLineSegmentViewModel : BaseViewModel
    {

        private byte _length;

        public byte Length
        {
            get { return _length; }
            set { SetProperty(ref _length, value, () => Length); }
        }


        private short _correction;

        public short Correction
        {
            get { return _correction; }
            set { SetProperty(ref _correction, value, () => Correction); }
        }


        private short _displacement;

        public short Displacement
        {
            get { return _displacement; }
            set { SetProperty(ref _displacement, value, () => Displacement); }
        }


        private short _radius;

        public short Radius
        {
            get { return _radius; }
            set { SetProperty(ref _radius, value, () => Radius); }
        }

        private short _highRadius;

        public short HighRadius
        {
            get { return _highRadius; }
            set { SetProperty(ref _highRadius, value, () => HighRadius); }
        }

        private short _lowRadius;

        public short LowRadius
        {
            get { return _lowRadius; }
            set { SetProperty(ref _lowRadius, value, () => LowRadius); }
        }


        private TrackBestLineSegmentType _segmentType;

        public TrackBestLineSegmentType SegmentType
        {
            get { return _segmentType; }
            set
            {
                SetProperty(ref _segmentType, value, () => SegmentType);
                Type = value.ToString();
                UpdateDisplayValues();
            }
        }


        private void UpdateDisplayValues()
        {
            switch (SegmentType)
            {
                case TrackBestLineSegmentType.Displacement:
                    ShowDisplacement = Visibility.Visible;
                    ShowRadius = Visibility.Visible;
                    ShowCorrection = Visibility.Visible;
                    ShowLowRadius = Visibility.Collapsed;
                    ShowHighRadius = Visibility.Collapsed;
                    break;
                case TrackBestLineSegmentType.Normal:
                    ShowDisplacement = Visibility.Collapsed;
                    ShowRadius = Visibility.Visible;
                    ShowCorrection = Visibility.Visible;
                    ShowLowRadius = Visibility.Collapsed;
                    ShowHighRadius = Visibility.Collapsed;
                    break;
                case TrackBestLineSegmentType.WideRadius:
                    ShowDisplacement = Visibility.Collapsed;
                    ShowRadius = Visibility.Collapsed;
                    ShowCorrection = Visibility.Collapsed; //?
                    ShowLowRadius = Visibility.Visible;
                    ShowHighRadius = Visibility.Visible;
                    break;
            }
        }


        private string _type;

        public string Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value, () => Type); }
        }



        private bool _canEditType;

        public bool CanEditType
        {
            get { return _canEditType; }
            set { SetProperty(ref _canEditType, value, () => CanEditType); }
        }


        public List<BestLineTypeBoundValue> AvailableBestLineTypes
        {
            get
            {
                var list = new List<BestLineTypeBoundValue>();

                list.Add(new BestLineTypeBoundValue(TrackBestLineSegmentType.Normal, "Normal"));
                list.Add(new BestLineTypeBoundValue(TrackBestLineSegmentType.WideRadius, "Wide Radius"));

                if (!CanEditType)
                {
                    list.Add(new BestLineTypeBoundValue(TrackBestLineSegmentType.Displacement, "Displacement"));
                }

                return list;
            }
        }



        // hmmm?
        public string Description
        {
            get
            {
                if (SegmentType == TrackBestLineSegmentType.Displacement)
                {
                    return $"Displacement: {Displacement}";
                    // more here?
                }

                if (SegmentType == TrackBestLineSegmentType.Normal)
                {
                    return $"Radius: {Radius}, Correction: {Correction}";
                    // more here?
                }

                if (SegmentType == TrackBestLineSegmentType.WideRadius)
                {
                    return $"High radius: {HighRadius}, Low radius: {LowRadius}";
                    // more here?
                }

                return string.Empty;
            }
        }


        //public TrackBestLineSegmentType SegmentType { get; set; }

        //// only used for first segment?
        //public short Displacement { get; set; }

        //public short Correction { get; set; }


        private Visibility _showRadius;

        public Visibility ShowRadius
        {
            get { return _showRadius; }
            set { SetProperty(ref _showRadius, value, () => ShowRadius); }
        }

        private Visibility _showDisplacement;

        public Visibility ShowDisplacement
        {
            get { return _showDisplacement; }
            set { SetProperty(ref _showDisplacement, value, () => ShowDisplacement); }
        }

        private Visibility _showCorrection;

        public Visibility ShowCorrection
        {
            get { return _showCorrection; }
            set { SetProperty(ref _showCorrection, value, () => ShowCorrection); }
        }

        private Visibility _showLowRadius;

        public Visibility ShowLowRadius
        {
            get { return _showLowRadius; }
            set { SetProperty(ref _showLowRadius, value, () => ShowLowRadius); }
        }

        private Visibility _showHighRadius;

        public Visibility ShowHighRadius
        {
            get { return _showHighRadius; }
            set { SetProperty(ref _showHighRadius, value, () => ShowHighRadius); }
        }



    }


    public class BestLineTypeBoundValue
    {
        public BestLineTypeBoundValue(TrackBestLineSegmentType value, string displayValue)
        {
            Value = value;
            DisplayValue = displayValue;
        }

        public TrackBestLineSegmentType Value { get; set; }
        public string DisplayValue { get; set; }
    }
}
