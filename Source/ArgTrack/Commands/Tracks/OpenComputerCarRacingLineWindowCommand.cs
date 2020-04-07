using System.Windows;
using ArgTrack.ViewModels;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
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
                Owner = Application.Current.MainWindow,
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
