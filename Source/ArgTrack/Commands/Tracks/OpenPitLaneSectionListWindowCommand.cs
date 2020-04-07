using System.Windows;
using ArgTrack.ViewModels;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class OpenPitLaneSectionListWindowCommand : CommandBase<MainViewModel>
    {
        public override void Execute(MainViewModel parameter)
        {
            var viewModel = new TrackSectionDataListViewModel
            {
                TrackSections = parameter.TrackViewModel.PitLaneSections
            };

            var window = new PitLaneSectionListWindow
            {
                Owner = Application.Current.MainWindow,
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
