using ArgData.Entities;
using ArgTrack.Infrastructure.Mapping;

namespace ArgTrack.Features.TrackEditing.CarSettings
{
    public static class CarSettingsMapper
    {
        public static void MapCarSettingsToViewModel(TrackCarSettings settings, CarSettingsViewModel viewModel)
        {
            viewModel.Acceleration = settings.Acceleration;
            viewModel.AirResistance = settings.AirResistance;
            viewModel.FuelLoad = settings.FuelLoad;
            viewModel.GripFactor = settings.GripFactor;
            viewModel.TyreWearNonQualifying = settings.TyreWearNonQualifying;
            viewModel.TyreWearQualifying = settings.TyreWearQualifying;
        }

        public static void MapCarSettingsViewModel(CarSettingsViewModel source, CarSettingsViewModel target)
        {
            PropertyCopier.Copy(source, target);
        }
    }
}
