using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using ArgData.Entities;
using ArgTrack.Features.Application.Shared;
using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.RacingLine
{
    public class BestLineSegmentViewModel : BaseViewModel
    {
        public BestLineSegmentViewModelCollection ParentList { get; }

        public BestLineSegmentViewModel(BestLineSegmentViewModelCollection parentList)
        {
            ParentList = parentList;
        }



        private int _index;

        public int Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value, () => Index); }
        }


        private byte _length;

        public byte Length
        {
            get { return _length; }
            set
            {
                SetProperty(ref _length, value, () => Length);
                UpdateLengthDescription(value);
            }
        }


        private void UpdateLengthDescription(byte length)
        {
            LengthDescription = UnitConvert.TrackLengthToMetersDescription(length);
            //double meters = UnitConvert.TrackLengthToMeters(length);

            //var cult = new CultureInfo("en-US");
            //var meterz = meters.ToString("F", cult);

            //int feet = length * 16;
            //LengthDescription = $"~{meterz} meters ({feet} feet)";
        }

        private string _lengthDescription;

        public string LengthDescription
        {
            get { return _lengthDescription; }
            set { SetProperty(ref _lengthDescription, value, () => LengthDescription); }
        }


        private short _correction;

        public short Correction
        {
            get { return _correction; }
            set { SetProperty(ref _correction, value, () => Correction); }
        }


        private short _displacement;

        [Obsolete("Displacement has moved, use TrackViewModel.ComputerCarLineDisplacement until further notice")]
        public short Displacement
        {
            get { return _displacement; }
            set { SetProperty(ref _displacement, value, () => Displacement); }
        }


        private short _radius;

        public short Radius
        {
            get { return _radius; }
            set
            {
                if (SetProperty(ref _radius, value, () => Radius))
                {
                    UpdateIcon();
                }
            }
        }

        private short _highRadius;

        public short HighRadius
        {
            get { return _highRadius; }
            set
            {
                if (SetProperty(ref _highRadius, value, () => HighRadius))
                {
                    UpdateIcon();
                }
            }
        }

        private short _lowRadius;

        public short LowRadius
        {
            get { return _lowRadius; }
            set
            {
                if (SetProperty(ref _lowRadius, value, () => LowRadius))
                {
                    UpdateIcon();
                }
            }
        }


        private TrackComputerCarLineSegmentType _segmentType;

        public TrackComputerCarLineSegmentType SegmentType
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
                case TrackComputerCarLineSegmentType.Normal:
                    ShowRadius = Visibility.Visible;
                    ShowCorrection = Visibility.Visible;
                    ShowLowRadius = Visibility.Collapsed;
                    ShowHighRadius = Visibility.Collapsed;
                    break;
                case TrackComputerCarLineSegmentType.WideRadius:
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


        private TreeViewIconEnum _icon = TreeViewIconEnum.ComputerCarLineStraight;

        public TreeViewIconEnum Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value, () => Icon); }
        }


        private void UpdateIcon()
        {
            if (SegmentType == TrackComputerCarLineSegmentType.WideRadius)
            {
                if (HighRadius < 0)
                {
                    Icon = TreeViewIconEnum.ComputerCarLineLeft;
                }
                else if (HighRadius > 0)
                {
                    Icon = TreeViewIconEnum.ComputerCarLineRight;
                }
                else
                {
                    Icon = TreeViewIconEnum.ComputerCarLineStraight;
                }
            }
            else
            {
                if (Radius < 0)
                {
                    Icon = TreeViewIconEnum.ComputerCarLineLeft;
                }
                else if (Radius > 0)
                {
                    Icon = TreeViewIconEnum.ComputerCarLineRight;
                }
                else
                {
                    Icon = TreeViewIconEnum.ComputerCarLineStraight;
                }
            }
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

                list.Add(new BestLineTypeBoundValue(TrackComputerCarLineSegmentType.Normal, "Normal"));
                list.Add(new BestLineTypeBoundValue(TrackComputerCarLineSegmentType.WideRadius, "Wide Radius"));

                return list;
            }
        }


        // hmmm?
        public string Description
        {
            get
            {
                //if (SegmentType == TrackBestLineSegmentType.Displacement)
                //{
                //    return $"Displacement: {Displacement}";
                //    // more here?
                //}

                if (SegmentType == TrackComputerCarLineSegmentType.Normal)
                {
                    return $"Radius: {Radius}, Correction: {Correction}";
                    // more here?
                }

                if (SegmentType == TrackComputerCarLineSegmentType.WideRadius)
                {
                    return $"High radius: {HighRadius}, Low radius: {LowRadius}";
                    // more here?
                }

                return string.Empty;
            }
        }


        private Visibility _showRadius;

        public Visibility ShowRadius
        {
            get { return _showRadius; }
            set { SetProperty(ref _showRadius, value, () => ShowRadius); }
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


        // makes the current view model the selected one in its list
        public void SelectInList()
        {
            ParentList.SelectedIndex = Index;
        }


        public ICommand HelpDocsCommand { get; } = new HelpDocsCommand("screens/racing-line-segment-details-window/");
    }


    public class BestLineTypeBoundValue
    {
        public BestLineTypeBoundValue(TrackComputerCarLineSegmentType value, string displayValue)
        {
            Value = value;
            DisplayValue = displayValue;
        }

        public TrackComputerCarLineSegmentType Value { get; set; }
        public string DisplayValue { get; set; }
    }
}
