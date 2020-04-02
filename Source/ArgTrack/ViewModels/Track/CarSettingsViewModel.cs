namespace ArgTrack.ViewModels.Track
{
    public class CarSettingsViewModel : BaseViewModel
    {
        private short _acceleration;

        public short Acceleration
        {
            get { return _acceleration; }
            set { SetProperty(ref _acceleration, value, () => Acceleration); }
        }


        private short _airResistance;

        public short AirResistance
        {
            get { return _airResistance; }
            set { SetProperty(ref _airResistance, value, () => AirResistance); }
        }


        private short _fuelLoad;

        public short FuelLoad
        {
            get { return _fuelLoad; }
            set { SetProperty(ref _fuelLoad, value, () => FuelLoad); }
        }


        private short _gripFactor;

        public short GripFactor
        {
            get { return _gripFactor; }
            set { SetProperty(ref _gripFactor, value, () => GripFactor); }
        }


        private short _tyreWearNonQualifying;

        public short TyreWearNonQualifying
        {
            get { return _tyreWearNonQualifying; }
            set { SetProperty(ref _tyreWearNonQualifying, value, () => TyreWearNonQualifying); }
        }


        private short _tyreWearQualifying;

        public short TyreWearQualifying
        {
            get { return _tyreWearQualifying; }
            set { SetProperty(ref _tyreWearQualifying, value, () => TyreWearQualifying); }
        }
    }
}
