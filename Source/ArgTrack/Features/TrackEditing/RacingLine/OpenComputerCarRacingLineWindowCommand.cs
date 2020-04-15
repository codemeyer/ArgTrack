using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.RacingLine
{
    public class OpenComputerCarRacingLineWindowCommand : CommandBase<MainViewModel>
    {
        public override void Execute(MainViewModel parameter)
        {
            var viewModel = new ComputerCarRacingLineDataListViewModel
            {
                BestLineSegments = parameter.TrackViewModel.BestLineSegments
            };

            var window = new ComputerCarRacingLineWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                //DataContext = parameter.TrackViewModel
                DataContext = viewModel
            };

            window.ShowDialog();
        }

        public override bool CanExecute(MainViewModel parameter)
        {
            return parameter.HasLoadedTrack;
        }
    }
}
