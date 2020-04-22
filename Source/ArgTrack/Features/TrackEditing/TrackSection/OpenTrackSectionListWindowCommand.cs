using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.TrackSection
{
    public class OpenTrackSectionListWindowCommand : CommandBase<MainViewModel>
    {
        public override void Execute(MainViewModel parameter)
        {
            if (MainViewModelPasserArounder.MainViewModel.WindowTrackSectionListOpen)
            {
                return;
            }

            var viewModel = new TrackSectionDataListViewModel
            {
                TrackSections = parameter.TrackViewModel.TrackSections
            };

            var window = new TrackSectionListWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = viewModel
            };

            MainViewModelPasserArounder.MainViewModel.WindowTrackSectionListOpen = true;

            window.Closed += (sender, e) =>
            {
                MainViewModelPasserArounder.MainViewModel.WindowTrackSectionListOpen = false;
            };

            window.Show();
        }

        public override bool CanExecute(MainViewModel parameter)
        {
            return parameter.HasLoadedTrack;
        }
    }
}
