using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class TrackObjectShapeVectorViewModel : BaseViewModel
    {
        private byte _from;

        public byte From
        {
            get { return _from; }
            set { SetProperty(ref _from, value, () => From); }
        }

        private byte _to;

        public byte To
        {
            get { return _to; }
            set { SetProperty(ref _to, value, () => To); }
        }
    }
}
