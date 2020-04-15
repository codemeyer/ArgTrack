using ArgTrack.Infrastructure.Mapping;

namespace ArgTrack.Features.TrackEditing.Camera
{
    public static class CameraCommandMapper
    {
        public static void MapCameraCommandViewModel(CameraCommandEditingViewModel source, CameraCommandEditingViewModel target)
        {
            PropertyCopier.Copy(source, target);
        }
    }
}
