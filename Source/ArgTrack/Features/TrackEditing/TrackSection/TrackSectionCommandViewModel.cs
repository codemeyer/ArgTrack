using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ArgTrack.Features.Application.Shared;
using ArgTrack.Features.Main;
using ArgTrack.Features.TrackEditing.ObjectSettings;
using ArgTrack.Features.TrackEditing.ObjectShapes;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.TrackSection
{
    public class TrackSectionCommandViewModel : BaseViewModel
    {
        public TrackSectionCommandViewModel()
        {
            Arguments = new ObservableCollection<TrackSectionCommandArgumentViewModel>();
            Arguments.CollectionChanged += (sender, args) => UpdateShowArgs();
        }

        private byte _command;

        public byte Command
        {
            get { return _command; }
            set
            {
                if (SetProperty(ref _command, value, () => Command))
                {
                    UpdateCommandDetails();
                    UpdateViewVisibility();
                }
            }
        }



        private void UpdateViewVisibility()
        {
            if (Command == 0x80)
            {
                // special
                ShowObjectPlacementDetails = Visibility.Visible;
                ShowArgumentList = Visibility.Collapsed;
            }
            else
            {
                ShowObjectPlacementDetails = Visibility.Collapsed;
                ShowArgumentList = Visibility.Visible;
            }
        }


        public void UpdateCommandDetails()
        {
            CommandDisplay = $"0x{Command:X}";
            Description = TrackSectionCommandDescriptions.GetCommandDescription(Command);

            if (Command == 0x80 && Arguments.Count > 0)
            {
                var desc = GetDescription();
                Description = desc;
            }
        }


        private string _commandDisplay;

        public string CommandDisplay
        {
            get { return _commandDisplay; }
            set { SetProperty(ref _commandDisplay, value, () => CommandDisplay); }
        }


        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value, () => Description); }
        }

        private ObservableCollection<TrackSectionCommandArgumentViewModel> _arguments;

        public ObservableCollection<TrackSectionCommandArgumentViewModel> Arguments
        {
            get { return _arguments; }
            set
            {
                if (SetProperty(ref _arguments, value, () => Arguments))
                {
                    UpdateShowArgs();
                }
            }
        }



        private Visibility _showObjectPlacementDetails;

        public Visibility ShowObjectPlacementDetails
        {
            get { return _showObjectPlacementDetails; }
            set { SetProperty(ref _showObjectPlacementDetails, value, () => ShowObjectPlacementDetails); }
        }

        private Visibility _showArgumentList;

        public Visibility ShowArgumentList
        {
            get { return _showArgumentList; }
            set { SetProperty(ref _showArgumentList, value, () => ShowArgumentList); }
        }


        public List<TrackSectionSelectableObjectSetting> AvailableObjects
        {
            get
            {
                var list = new List<TrackSectionSelectableObjectSetting>();

                foreach (var objectSetting in TrackViewModelPasserArounder.TrackViewModel.TrackObjectSettings)
                {
                    string description = GetObjectPlacementDescription(objectSetting);

                    // TODO: Id != 5 and <17

                    if (!string.IsNullOrEmpty(description))
                    {
                        description = " - " + description;
                    }

                    list.Add(new TrackSectionSelectableObjectSetting(objectSetting.Offset, $"{objectSetting.Offset} {description}"));
                }

                return list;
            }
        }

        private string GetDescription()
        {
            if (Command == 0x80)
            {
                var objectSetting = TrackViewModelPasserArounder.TrackViewModel?.TrackObjectSettings.FirstOrDefault(s => s.Offset == Arguments[1].Value);
                var description = GetObjectPlacementDescription(objectSetting);

                if (string.IsNullOrEmpty(description))
                {
                    return TrackSectionCommandDescriptions.GetCommandDescription(Command);
                }
                else
                {
                    return TrackSectionCommandDescriptions.GetCommandDescription(Command) + " - " + description;
                }
            }

            return TrackSectionCommandDescriptions.GetCommandDescription(Command);
        }

        private string GetObjectPlacementDescription(TrackObjectSettingsViewModel objectSetting)
        {
            var description = "";

            if (objectSetting == null)
            {
                return string.Empty;
            }

            if (objectSetting.Id == 5 || objectSetting.Id == 1 || objectSetting.Id == 13)  // difference between these?
            {
                var billboardDescription = BillboardObjects.GetDescription(objectSetting.AngleY);

                if (!string.IsNullOrEmpty(billboardDescription))
                {
                    description = $"{billboardDescription} [id {objectSetting.AngleY}]";
                }
                else
                {
                    description = $"[id {objectSetting.AngleY}]";
                }
            }

            if (objectSetting.Id >= 17)
            {
                var shape = TrackViewModelPasserArounder.TrackViewModel.TrackObjectShapes.FirstOrDefault(s => s.Id == objectSetting.Id);

                if (shape != null)
                {
                    var shapeDescription = ObjectShapeDescriptions.GetDescription(shape.Signature);

                    if (!string.IsNullOrEmpty(shapeDescription))
                    {
                        description = $"{shapeDescription} [shape id {objectSetting.Id}]";
                    }
                    else
                    {
                        description = $"3D Object [shape id {objectSetting.Id}]";
                    }
                }
                else
                {
                    description = $"3D Object [shape id  {objectSetting.Id}]";
                }
            }

            // indicate side of track
            if (objectSetting.DistanceFromTrack > 0)
            {
                description += "   \u2206\u2192";
            }

            if (objectSetting.DistanceFromTrack < 0)
            {
                description += " \u2190\u2206";
            }

            return description;
        }


        private void UpdateShowArgs()
        {
            ShowArg0 = Arguments.Count > 0 ? Visibility.Visible : Visibility.Hidden;
            ShowArg1 = Arguments.Count > 1 ? Visibility.Visible : Visibility.Hidden;
            ShowArg2 = Arguments.Count > 2 ? Visibility.Visible : Visibility.Hidden;
            ShowArg3 = Arguments.Count > 3 ? Visibility.Visible : Visibility.Hidden;
            ShowArg4 = Arguments.Count > 4 ? Visibility.Visible : Visibility.Hidden;
            ShowArg5 = Arguments.Count > 5 ? Visibility.Visible : Visibility.Hidden;
        }



        private Visibility _showArg0 = Visibility.Hidden;

        public Visibility ShowArg0
        {
            get { return _showArg0; }
            set { SetProperty(ref _showArg0, value, () => ShowArg0); }
        }

        private Visibility _showArg1 = Visibility.Hidden;

        public Visibility ShowArg1
        {
            get { return _showArg1; }
            set { SetProperty(ref _showArg1, value, () => ShowArg1); }
        }

        private Visibility _showArg2 = Visibility.Hidden;

        public Visibility ShowArg2
        {
            get { return _showArg2; }
            set { SetProperty(ref _showArg2, value, () => ShowArg2); }
        }

        private Visibility _showArg3 = Visibility.Hidden;

        public Visibility ShowArg3
        {
            get { return _showArg3; }
            set { SetProperty(ref _showArg3, value, () => ShowArg3); }
        }

        private Visibility _showArg4 = Visibility.Hidden;

        public Visibility ShowArg4
        {
            get { return _showArg4; }
            set { SetProperty(ref _showArg4, value, () => ShowArg4); }
        }

        private Visibility _showArg5 = Visibility.Hidden;

        public Visibility ShowArg5
        {
            get { return _showArg5; }
            set { SetProperty(ref _showArg5, value, () => ShowArg5); }
        }

        public void PopulateArguments()
        {
            int count = ArgumentCount[Command];

            for (int i = 0; i < count; i++)
            {
                // TODO: special stuff like in mapper...
                var vm = GetIt(Command, i);
                vm.Description = TrackSectionCommandDescriptions.GetCommandArgumentDescription(Command, i);
                Arguments.Add(vm);
            }

            UpdateShowArgs();
        }

        // TODO: stolen from TrackMapper, do not duplicate!
        private static TrackSectionCommandArgumentViewModel GetIt(byte command, int argumentIndex)
        {
            if (command == 0x80 && argumentIndex == 1)
                return new TrackSectionCommand80Argument1ViewModel();
            else
                return new TrackSectionCommandArgumentViewModel();
        }

        // TODO: stolen without mercy from ArgData somewhere...
        private static Dictionary<byte, int> ArgumentCount = new Dictionary<byte, int>
        {
            {0x80, 2},
            {0x81, 2},
            {0x82, 2},
            {0x83, 1},
            {0x84, 1},
            {0x85, 3},
            {0x86, 1},
            {0x87, 1},
            {0x88, 2},
            {0x89, 2},
            {0x8a, 6},
            {0x8b, 6},
            {0x8c, 2},
            {0x8d, 2},
            {0x8e, 3},
            {0x8f, 3},
            {0x90, 2},
            {0x91, 2},
            {0x92, 2},
            {0x93, 2},
            {0x94, 2},
            {0x95, 2},
            {0x96, 1},
            {0x97, 1},
            {0x98, 2},
            {0x99, 2},
            {0x9a, 3},
            {0x9b, 1},
            {0x9c, 1},
            {0x9d, 1},
            {0x9e, 1},
            {0x9f, 1},
            {0xa0, 1},
            {0xa1, 1},
            {0xa2, 1},
            {0xa3, 1},
            {0xa4, 1},
            {0xa5, 1},
            {0xa6, 3},
            {0xa7, 3},
            {0xa8, 1},
            {0xa9, 2},
            {0xaa, 4},
            {0xab, 3},
            {0xac, 5}
        };


        public ICommand HelpDocsCommand { get; } = new HelpDocsCommand("screens/track-section-details-window/#commands");
    }



    public class TrackSectionSelectableObjectSetting
    {
        public TrackSectionSelectableObjectSetting(short value, string display)
        {
            Value = value;
            DisplayValue = display;
        }

        public short Value { get; set; }
        public string DisplayValue { get; set; }
    }



    public class TrackSectionCommandBoundValue
    {
        public TrackSectionCommandBoundValue(byte command, string displayValue)
        {
            Value = command;
            DisplayValue = displayValue;
        }

        public byte Value { get; set; }
        public string DisplayValue { get; set; }
    }


    public class TrackSectionCommandArgumentViewModel : BaseViewModel
    {
        private short _value;

        public short Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value, () => Value); }
        }


        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value, () => Description); }
        }
    }


    public class TrackSectionCommand80Argument1ViewModel : TrackSectionCommandArgumentViewModel
    {
        public TrackSectionCommand80Argument1ViewModel()
        {
            PropertyChanged += TrackCommand80Argument1ViewModel_PropertyChanged;
        }

        private void TrackCommand80Argument1ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                Offset = Value;
            }
        }

        private short _offset;

        public short Offset
        {
            get { return _offset; }
            set
            {
                if (SetProperty(ref _offset, value, () => Offset))
                {
                    SelectedTrackObjectSettingsViewModel = TrackViewModelPasserArounder.TrackViewModel?
                        .TrackObjectSettings.SingleOrDefault(os => os.Offset == value);
                }
            }
        }


        private TrackObjectSettingsViewModel _selectedTrackObjectSettingsViewModel;

        public TrackObjectSettingsViewModel SelectedTrackObjectSettingsViewModel
        {
            get { return _selectedTrackObjectSettingsViewModel; }
            set { SetProperty(ref _selectedTrackObjectSettingsViewModel, value, () => SelectedTrackObjectSettingsViewModel); }
        }
    }
}
