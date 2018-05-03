using ArgData.Entities;
using System.ComponentModel;

namespace ArgTrack.ViewModels.Track
{
    public class SetupViewModel : BaseViewModel, IDataErrorInfo
    {
        public SetupViewModel(string group)
        {
            TyreCompoundRadioButtonGroup = group;
            TyreCompound = SetupTyreCompound.C;
        }

        public SetupViewModel() : this("DesignTime")
        {
        }

        private byte _frontWing = 40;

        public byte FrontWing
        {
            get { return _frontWing; }
            set { SetProperty(ref _frontWing, value, () => FrontWing); }
        }

        private byte _rearWing = 40;

        public byte RearWing
        {
            get { return _rearWing; }
            set { SetProperty(ref _rearWing, value, () => RearWing); }
        }

        private sbyte _brakeBalance = 0;

        public sbyte BrakeBalance
        {
            get { return _brakeBalance; }
            set { SetProperty(ref _brakeBalance, value, () => BrakeBalance); }
        }

        private byte _gearRatio1 = 24;

        public byte GearRatio1
        {
            get { return _gearRatio1; }
            set
            {
                if (SetProperty(ref _gearRatio1, value, () => GearRatio1))
                {
                    ValidateGearRatios();
                }
            }
        }

        private byte _gearRatio2 = 32;

        public byte GearRatio2
        {
            get { return _gearRatio2; }
            set
            {
                if (SetProperty(ref _gearRatio2, value, () => GearRatio2))
                {
                    ValidateGearRatios();
                }
            }
        }

        private byte _gearRatio3 = 39;

        public byte GearRatio3
        {
            get { return _gearRatio3; }
            set
            {
                if (SetProperty(ref _gearRatio3, value, () => GearRatio3))
                {
                    ValidateGearRatios();
                }
            }
        }

        private byte _gearRatio4 = 46;

        public byte GearRatio4
        {
            get { return _gearRatio4; }
            set
            {
                if (SetProperty(ref _gearRatio4, value, () => GearRatio4))
                {
                    ValidateGearRatios();
                }
            }
        }

        private byte _gearRatio5 = 53;

        public byte GearRatio5
        {
            get { return _gearRatio5; }
            set
            {
                if (SetProperty(ref _gearRatio5, value, () => GearRatio5))
                {
                    ValidateGearRatios();
                }
            }
        }

        private byte _gearRatio6 = 61;

        public byte GearRatio6
        {
            get { return _gearRatio6; }
            set
            {
                if (SetProperty(ref _gearRatio6, value, () => GearRatio6))
                {
                    ValidateGearRatios();
                }
            }
        }


        public bool IsValid
        {
            get
            {
                if (IncorrectGearRatio1 || IncorrectGearRatio2 || IncorrectGearRatio3 ||
                    IncorrectGearRatio4 || IncorrectGearRatio5 || IncorrectGearRatio6)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


        private void ValidateGearRatios()
        {
            // default
            IncorrectGearRatio1 = false;
            IncorrectGearRatio2 = false;
            IncorrectGearRatio3 = false;
            IncorrectGearRatio4 = false;
            IncorrectGearRatio5 = false;
            IncorrectGearRatio6 = false;

            if (GearRatio1 >= GearRatio2)
                IncorrectGearRatio1 = true;

            if (GearRatio2 >= GearRatio3)
                IncorrectGearRatio2 = true;

            if (GearRatio3 >= GearRatio4)
                IncorrectGearRatio3 = true;

            if (GearRatio4 >= GearRatio5)
                IncorrectGearRatio4 = true;

            if (GearRatio5 >= GearRatio6)
                IncorrectGearRatio5 = true;


            if (GearRatio2 <= GearRatio1)
                IncorrectGearRatio2 = true;

            if (GearRatio3 <= GearRatio2)
                IncorrectGearRatio3 = true;

            if (GearRatio4 <= GearRatio3)
                IncorrectGearRatio4 = true;

            if (GearRatio5 <= GearRatio4)
                IncorrectGearRatio5 = true;

            if (GearRatio6 <= GearRatio5)
                IncorrectGearRatio6 = true;
        }




        private bool _incorrectGearRatio1;

        public bool IncorrectGearRatio1
        {
            get { return _incorrectGearRatio1; }
            set { SetProperty(ref _incorrectGearRatio1, value, () => IncorrectGearRatio1); }
        }

        private bool _incorrectGearRatio2;

        public bool IncorrectGearRatio2
        {
            get { return _incorrectGearRatio2; }
            set { SetProperty(ref _incorrectGearRatio2, value, () => IncorrectGearRatio2); }
        }

        private bool _incorrectGearRatio3;

        public bool IncorrectGearRatio3
        {
            get { return _incorrectGearRatio3; }
            set { SetProperty(ref _incorrectGearRatio3, value, () => IncorrectGearRatio3); }
        }

        private bool _incorrectGearRatio4;

        public bool IncorrectGearRatio4
        {
            get { return _incorrectGearRatio4; }
            set { SetProperty(ref _incorrectGearRatio4, value, () => IncorrectGearRatio4); }
        }

        private bool _incorrectGearRatio5;

        public bool IncorrectGearRatio5
        {
            get { return _incorrectGearRatio5; }
            set { SetProperty(ref _incorrectGearRatio5, value, () => IncorrectGearRatio5); }
        }

        private bool _incorrectGearRatio6;

        public bool IncorrectGearRatio6
        {
            get { return _incorrectGearRatio6; }
            set { SetProperty(ref _incorrectGearRatio6, value, () => IncorrectGearRatio6); }
        }



        private string _tyreCompoundRadioButtonGroup;

        public string TyreCompoundRadioButtonGroup
        {
            get { return _tyreCompoundRadioButtonGroup; }
            set { SetProperty(ref _tyreCompoundRadioButtonGroup, value, () => TyreCompoundRadioButtonGroup); }
        }


        private SetupTyreCompound _tyreCompound;
        private bool _isTyreCompoundA;
        private bool _isTyreCompoundB;
        private bool _isTyreCompoundC;
        private bool _isTyreCompoundD;

        public SetupTyreCompound TyreCompound
        {
            get { return _tyreCompound; }
            set
            {
                if (SetProperty(ref _tyreCompound, value, () => TyreCompound))
                {
                    UpdateTyreCompoundFlags();
                }
            }
        }

        private void UpdateTyreCompoundFlags()
        {
            IsTyreCompoundA = _tyreCompound == SetupTyreCompound.A;
            IsTyreCompoundB = _tyreCompound == SetupTyreCompound.B;
            IsTyreCompoundC = _tyreCompound == SetupTyreCompound.C;
            IsTyreCompoundD = _tyreCompound == SetupTyreCompound.D;
        }

        public bool IsTyreCompoundA
        {
            get { return _isTyreCompoundA; }
            set
            {
                if (SetProperty(ref _isTyreCompoundA, value, () => IsTyreCompoundA))
                {
                    if (value)
                    {
                        TyreCompound = SetupTyreCompound.A;
                    }
                }
            }
        }

        public bool IsTyreCompoundB
        {
            get { return _isTyreCompoundB; }
            set
            {
                if (SetProperty(ref _isTyreCompoundB, value, () => IsTyreCompoundB))
                {
                    if (value)
                    {
                        TyreCompound = SetupTyreCompound.B;
                    }
                }
            }
        }

        public bool IsTyreCompoundC
        {
            get { return _isTyreCompoundC; }
            set
            {
                if (SetProperty(ref _isTyreCompoundC, value, () => IsTyreCompoundC))
                {
                    if (value)
                    {
                        TyreCompound = SetupTyreCompound.C;
                    }
                }
            }
        }

        public bool IsTyreCompoundD
        {
            get { return _isTyreCompoundD; }
            set
            {
                if (SetProperty(ref _isTyreCompoundD, value, () => IsTyreCompoundD))
                {
                    if (value)
                    {
                        TyreCompound = SetupTyreCompound.D;
                    }
                }
            }
        }


        public string this[string columnName]
        {
            get
            {
                return string.Empty;
            }
        }

        public string Error { get; }
    }
}
