using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using ArgTrack.Features.Application.Shared;
using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectSettings
{
    public class TrackObjectSettingsViewModel : BaseViewModel
    {
        public TrackObjectSettingsViewModel()
        {
            UpdateDisplayValues(0);
        }

        private short _offset;

        public short Offset
        {
            get { return _offset; }
            set { SetProperty(ref _offset, value, () => Offset); }
        }

        private byte _id;

        public byte Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value, () => Id);

                UpdateDisplayValues(value);
            }
        }


        private void UpdateDisplayValues(byte id)
        {
            // TODO: update what stuff is shown, labels/descriptions, etc

            if (id == 5)
            {
                ShowAngleX = Visibility.Collapsed;
                ShowAngleY = Visibility.Collapsed;
                ShowBillboardObjectDropdown = Visibility.Visible;
                ShowId2 = Visibility.Collapsed;
                //ShowUnknown2 = Visibility.Collapsed;
                return;
            }

            //if (id >= 17)
            //{
            ShowAngleX = Visibility.Visible;
            ShowAngleY = Visibility.Visible;
            ShowBillboardObjectDropdown = Visibility.Collapsed;
            ShowId2 = Visibility.Visible;
            ShowUnknown2 = Visibility.Visible;

            //}

            // TODO: this is the "special case" ones, i.e. 0 to 16 except 5, what here?
            //ShowAngleY = Visibility.Visible;
            //ShowBillboardObjectDropdown = Visibility.Collapsed;
            //ShowId2 = Visibility.Visible;
            //ShowUnknown2 = Visibility.Visible;
        }




        private short _id2;

        public short Id2
        {
            get { return _id2; }
            set { SetProperty(ref _id2, value, () => Id2); }
        }

        private byte _detailLevel;

        public byte DetailLevel
        {
            get { return _detailLevel; }
            set { SetProperty(ref _detailLevel, value, () => DetailLevel); }
        }

        private short _unknown;

        public short Unknown
        {
            get { return _unknown; }
            set { SetProperty(ref _unknown, value, () => Unknown); }
        }

        private short _unknown2;

        public short Unknown2
        {
            get { return _unknown2; }
            set { SetProperty(ref _unknown2, value, () => Unknown2); }
        }

        private short _distanceFromTrack;

        public short DistanceFromTrack
        {
            get { return _distanceFromTrack; }
            set { SetProperty(ref _distanceFromTrack, value, () => DistanceFromTrack); }
        }

        private short _angleX;

        public short AngleX
        {
            get { return _angleX; }
            set { SetProperty(ref _angleX, value, () => AngleX); }
        }

        private short _angleY;

        public short AngleY
        {
            get { return _angleY; }
            set { SetProperty(ref _angleY, value, () => AngleY); }
        }

        private short _height;

        public short Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value, () => Height); }
        }




        private Visibility _showAngleY;

        public Visibility ShowAngleY
        {
            get { return _showAngleY; }
            set { SetProperty(ref _showAngleY, value, () => ShowAngleY); }
        }

        private Visibility _showAngleX;

        public Visibility ShowAngleX
        {
            get { return _showAngleX; }
            set { SetProperty(ref _showAngleX, value, () => ShowAngleX); }
        }

        private Visibility _showId2;

        public Visibility ShowId2
        {
            get { return _showId2; }
            set { SetProperty(ref _showId2, value, () => ShowId2); }
        }

        private Visibility _showUnknown2;

        public Visibility ShowUnknown2
        {
            get { return _showUnknown2; }
            set { SetProperty(ref _showUnknown2, value, () => ShowUnknown2); }
        }


        private Visibility _showBillboardObjectDropdown;

        public Visibility ShowBillboardObjectDropdown
        {
            get { return _showBillboardObjectDropdown; }
            set { SetProperty(ref _showBillboardObjectDropdown, value, () => ShowBillboardObjectDropdown); }
        }




        public List<ObjectIdBoundValue> AvailableObjectIds
        {
            get
            {
                var objs = TrackViewModelPasserArounder.TrackViewModel.TrackObjectShapes;
                var list = new List<ObjectIdBoundValue>();

                for (short i = 0; i <= 16; i++)
                {
                    list.Add(new ObjectIdBoundValue((byte)i, GetDescriptionForId(i)));
                }

                foreach (var obj in objs)
                {
                    list.Add(new ObjectIdBoundValue((byte)obj.Id, GetDescriptionForId(obj.Id)));
                }

                return list;
            }
        }

        private string GetDescriptionForId(int id)
        {
            if (id >= 17)
                return $"{id}: 3D Object";

            if (id == 5)
                return $"{id}: Tree, bush, etc";

            // TODO: special cases

            return $"{id}";
        }

        public List<BillboardObjectBoundValue> AvailableBillboardObjectIds
        {
            get { return BillboardObjects.List(); }
        }


        public ICommand Edit { get; } = new EditTrackObjectSettingsCommand();

        public ICommand HelpDocsCommand { get; } = new HelpDocsCommand("screens/object-settings-details-window/");
    }


    public class ObjectIdBoundValue
    {
        public ObjectIdBoundValue(byte value, string displayValue)
        {
            Value = value;
            DisplayValue = displayValue;
        }

        public byte Value { get; set; }
        public string DisplayValue { get; set; }
    }

}
