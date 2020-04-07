using System.Windows;
using ArgTrack.ViewModels;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class OpenTrackSectionListWindowCommand : CommandBase<MainViewModel>
    {
        public override void Execute(MainViewModel parameter)
        {
            var viewModel = new TrackSectionDataListViewModel
            {
                TrackSections = parameter.TrackViewModel.TrackSections
            };

            var window = new TrackSectionListWindow
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
