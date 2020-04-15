using ArgData.Entities;

namespace ArgTrack.Features.TrackEditing.CarSetup
{
    public static class CarSetupMapper
    {
        public static void MapCarSetupToViewModel(Setup sourceData, SetupViewModel targetViewModel)
        {
            targetViewModel.FrontWing = sourceData.FrontWing;
            targetViewModel.RearWing = sourceData.RearWing;
            targetViewModel.BrakeBalance = sourceData.BrakeBalance;
            targetViewModel.GearRatio1 = sourceData.GearRatio1;
            targetViewModel.GearRatio2 = sourceData.GearRatio2;
            targetViewModel.GearRatio3 = sourceData.GearRatio3;
            targetViewModel.GearRatio4 = sourceData.GearRatio4;
            targetViewModel.GearRatio5 = sourceData.GearRatio5;
            targetViewModel.GearRatio6 = sourceData.GearRatio6;
            targetViewModel.TyreCompound = sourceData.TyreCompound;
        }

        public static void MapCarSetupViewModel(SetupViewModel source, SetupViewModel target)
        {
            target.FrontWing = source.FrontWing;
            target.RearWing = source.RearWing;
            target.BrakeBalance = source.BrakeBalance;
            target.GearRatio1 = source.GearRatio1;
            target.GearRatio2 = source.GearRatio2;
            target.GearRatio3 = source.GearRatio3;
            target.GearRatio4 = source.GearRatio4;
            target.GearRatio5 = source.GearRatio5;
            target.GearRatio6 = source.GearRatio6;
            target.TyreCompound = source.TyreCompound;
        }
    }
}
