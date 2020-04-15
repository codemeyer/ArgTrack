using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackDrawing
{
    public class ZoomTrackViewCommand : CommandBase
    {
        private readonly double _factor;

        public ZoomTrackViewCommand(double factor)
        {
            _factor = factor;
        }

        public override void Execute(object parameter)
        {
            var vm = parameter as TrackLayoutViewModel;

            if (vm == null)
                return;

            vm.ZoomFactor += _factor;
        }

        public override bool CanExecute(object parameter)
        {
            // TODO: should have loaded track
            // TODO: vm.ZoomFactor must always stay above 0
            return true;
        }
    }
}
