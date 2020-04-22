using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackDrawing
{
    public class RedrawViewCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var viewModel = parameter as TrackLayoutViewModel;

            if (viewModel == null)
            {
                return;
            }

            // TODO: fix
            TrackViewModelPasserArounder.TrackViewModel.RecalculateLayout();

            viewModel.RecalculateViewAndScrollbars();
        }
    }
}
