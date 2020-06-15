using ArgTrack.Features.Main;
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
            var vm = GetTrackLayoutViewModel(parameter);

            if (vm == null)
            {
                return;
            }

            double newFactor = vm.ZoomFactor + _factor;

            if (newFactor <= 0)
            {
                return;
            }

            vm.ZoomFactor += _factor;
        }

        public override bool CanExecute(object parameter)
        {
            var vm = GetTrackLayoutViewModel(parameter);

            if (vm == null)
            {
                return false;
            }

            double newFactor = vm.ZoomFactor + _factor;

            if (newFactor <= 0)
            {
                return false;
            }

            return true;
        }

        private TrackLayoutViewModel GetTrackLayoutViewModel(object parameter)
        {
            var vm = parameter as TrackLayoutViewModel;

            if (vm == null)
            {
                // a bit of a hack since we receive null from MainTreeWindow Window_PreviewKeyDown
                vm = MainViewModelPasserArounder.MainViewModel.TrackLayoutViewModel;
            }

            return vm;
        }
    }
}
