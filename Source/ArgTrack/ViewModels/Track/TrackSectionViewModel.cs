using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using ArgData.Entities;
using ArgTrack.Commands.Tracks;
using ArgTrack.TrackStuff;

namespace ArgTrack.ViewModels.Track
{
    public class TrackSectionViewModel : BaseViewModel
    {
        public TrackSectionViewModelCollection ParentList { get; }

        public TrackSectionViewModel(TrackSectionViewModelCollection parentList)
        {
            ParentList = parentList;
            SelectedTrackCommandValueToAdd = 0x80;
            UpdateCurvatureDescription();
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
            double meters = UnitConvert.TrackLengthToMeters(length);

            var cult = new CultureInfo("en-US");
            var meterz = meters.ToString("F", cult);

            int feet = length * 16;
            LengthDescription = $"~{meterz} meters ({feet} feet)";
        }


        private string _lengthDescription;

        public string LengthDescription
        {
            get { return _lengthDescription; }
            set { SetProperty(ref _lengthDescription, value, () => LengthDescription); }
        }


        private short _curvature;

        public short Curvature
        {
            get { return _curvature; }
            set
            {
                if (SetProperty(ref _curvature, value, () => Curvature))
                {
                    UpdateCurvatureDescription();
                }
            }
        }



        // TODO: how to tell Track from PitLane here?

        private void UpdateCurvatureDescription()
        {
            if (Curvature < 0)
            {
                CurvatureDescription = "Left";
                Icon = TreeViewIconEnum.TrackLeft;
            }
            else if (Curvature > 0)
            {
                CurvatureDescription = "Right";
                Icon = TreeViewIconEnum.TrackRight;
            }
            else
            {
                CurvatureDescription = "Straight";
                Icon = TreeViewIconEnum.TrackStraight;
            }
        }


        private string _curvatureDescription;

        public string CurvatureDescription
        {
            get { return _curvatureDescription; }
            set { SetProperty(ref _curvatureDescription, value, () => CurvatureDescription); }
        }


        private short _height;

        public short Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value, () => Height); }
        }



        private byte _leftVergeWidth;

        public byte LeftVergeWidth
        {
            get { return _leftVergeWidth; }
            set { SetProperty(ref _leftVergeWidth, value, () => LeftVergeWidth); }
        }

        private byte _rightVergeWidth;

        public byte RightVergeWidth
        {
            get { return _rightVergeWidth; }
            set { SetProperty(ref _rightVergeWidth, value, () => RightVergeWidth); }
        }


        private KerbHeight _kerbHeight;

        public KerbHeight KerbHeight
        {
            get { return _kerbHeight; }
            set { SetProperty(ref _kerbHeight, value, () => KerbHeight); }
        }



        private ObservableCollection<TrackCommandViewModel> _commands = new ObservableCollection<TrackCommandViewModel>();

        public ObservableCollection<TrackCommandViewModel> Commands
        {
            get { return _commands; }
            set { SetProperty(ref _commands, value, () => Commands); }
        }


        private bool _pitlaneEntrance;

        public bool PitLaneEntrance
        {
            get { return _pitlaneEntrance; }
            set { SetProperty(ref _pitlaneEntrance, value, () => PitLaneEntrance); }
        }


        private bool _pitlaneExit;

        public bool PitLaneExit
        {
            get { return _pitlaneExit; }
            set { SetProperty(ref _pitlaneExit, value, () => PitLaneExit); }
        }


        private bool _bridgedLeftFence;

        public bool BridgedLeftFence
        {
            get { return _bridgedLeftFence; }
            set { SetProperty(ref _bridgedLeftFence, value, () => BridgedLeftFence); }
        }

        private bool _bridgedRightFence;

        public bool BridgedRightFence
        {
            get { return _bridgedRightFence; }
            set { SetProperty(ref _bridgedRightFence, value, () => BridgedRightFence); }
        }

        private bool _hasLeftKerb;

        public bool HasLeftKerb
        {
            get { return _hasLeftKerb; }
            set { SetProperty(ref _hasLeftKerb, value, () => HasLeftKerb); }
        }

        private bool _hasRightKerb;

        public bool HasRightKerb
        {
            get { return _hasRightKerb; }
            set { SetProperty(ref _hasRightKerb, value, () => HasRightKerb); }
        }



        private bool _removeLeftWall;

        public bool RemoveLeftWall
        {
            get { return _removeLeftWall; }
            set { SetProperty(ref _removeLeftWall, value, () => RemoveLeftWall); }
        }

        private bool _removeRightWall;

        public bool RemoveRightWall
        {
            get { return _removeRightWall; }
            set { SetProperty(ref _removeRightWall, value, () => RemoveRightWall); }
        }


        private bool _roadSignArrow;

        public bool RoadSignArrow
        {
            get { return _roadSignArrow; }
            set { SetProperty(ref _roadSignArrow, value, () => RoadSignArrow); }
        }

        private bool _roadSignArrow100;

        public bool RoadSignArrow100
        {
            get { return _roadSignArrow100; }
            set { SetProperty(ref _roadSignArrow100, value, () => RoadSignArrow100); }
        }

        private bool _roadSigns;

        public bool RoadSigns
        {
            get { return _roadSigns; }
            set { SetProperty(ref _roadSigns, value, () => RoadSigns); }
        }



        private bool _unknown1;

        public bool Unknown1
        {
            get { return _unknown1; }
            set { SetProperty(ref _unknown1, value, () => Unknown1); }
        }


        private bool _unknown2;

        public bool Unknown2
        {
            get { return _unknown2; }
            set { SetProperty(ref _unknown2, value, () => Unknown2); }
        }

        private bool _unknown3;

        public bool Unknown3
        {
            get { return _unknown3; }
            set { SetProperty(ref _unknown3, value, () => Unknown3); }
        }

        private bool _unknown4;

        public bool Unknown4
        {
            get { return _unknown4; }
            set { SetProperty(ref _unknown4, value, () => Unknown4); }
        }



        private TreeViewIconEnum _icon;

        public TreeViewIconEnum Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value, () => Icon); }
        }





        // hmmm?
        public string Description
        {
            get
            {
                var desc = new List<string>();
                if (Commands.Any(c => c.Command == 0x86))
                {
                    desc.Add("Pitlane Start");
                }

                if (Commands.Any(c => c.Command == 0x87))
                {
                    desc.Add("Pitlane End");
                }

                // only in pit lane?
                //if (PitLaneEntrance)
                //    desc.Add("Pitlane Entrance flag");

                //if (PitLaneExit)
                //    desc.Add("Pitlane Exit flag");


                if (Commands.Any(c => c.Command == 0x80))
                {
                    desc.Add($"{Commands.Count(c => c.Command == 0x80)} objects");
                }

                if (Commands.Any(c => c.Command == 0x85))
                {
                    desc.Add("Track width change");
                }


                if (Commands.Any(c => c.Command == 0x98 || c.Command == 0x99 || c.Command == 0x9A))
                {
                    desc.Add("Wall height change");
                }

                if (Commands.Any(c => c.Command == 0xAC))
                {
                    desc.Add("Palette change");
                }


                return string.Join(", ", desc.ToArray());
            }
        }



        public List<KerbHeightBoundValue> AvailableKerbHeights
        {
            get
            {
                var list = new List<KerbHeightBoundValue>
                {
                    new KerbHeightBoundValue(KerbHeight.High, "High"),
                    new KerbHeightBoundValue(KerbHeight.Low, "Low")
                };

                return list;
            }
        }


        private int _selectedTrackCommandIndex;

        public int SelectedTrackCommandIndex
        {
            get { return _selectedTrackCommandIndex; }
            set { SetProperty(ref _selectedTrackCommandIndex, value, () => SelectedTrackCommandIndex); }
        }



        private byte _selectedTrackCommandValue;

        public byte SelectedTrackCommandValueToAdd
        {
            get { return _selectedTrackCommandValue; }
            set { SetProperty(ref _selectedTrackCommandValue, value, () => SelectedTrackCommandValueToAdd); }
        }

        public List<TrackCommandBoundValue> AvailableTrackCommands
        {
            get
            {
                var list = new List<TrackCommandBoundValue>();

                foreach (var key in CommandDescriptions.ListCommands())
                {
                    list.Add(new TrackCommandBoundValue(key, $"0x{key:X} {CommandDescriptions.GetCommandDescription(key)}"));
                }

                return list;
            }
        }



        private TrackSectionLayoutData _layout;

        public TrackSectionLayoutData Layout
        {
            get { return _layout; }
            set { SetProperty(ref _layout, value, () => Layout); }
        }


        public ICommand AddTrackCommandCommand { get; } = new AddTrackCommandCommand();
        public ICommand DeleteTrackCommandCommand { get; } = new DeleteTrackCommandCommand();

        public ICommand EditThis { get; } = new EditTrackSectionCommand();
    }


    public class KerbHeightBoundValue
    {
        public KerbHeightBoundValue(KerbHeight value, string displayValue)
        {
            Value = value;
            DisplayValue = displayValue;
        }

        public KerbHeight Value { get; set; }
        public string DisplayValue { get; set; }
    }
}
