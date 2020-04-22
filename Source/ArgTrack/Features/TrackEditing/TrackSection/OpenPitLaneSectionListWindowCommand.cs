using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.TrackSection
{
    public class OpenPitLaneSectionListWindowCommand : CommandBase<MainViewModel>
    {
        public override void Execute(MainViewModel parameter)
        {
            if (MainViewModelPasserArounder.MainViewModel.WindowPitLaneSectionListOpen)
            {
                return;
            }

            var viewModel = new TrackSectionDataListViewModel
            {
                TrackSections = parameter.TrackViewModel.PitLaneSections
            };

            var window = new PitLaneSectionListWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = viewModel
            };

            MainViewModelPasserArounder.MainViewModel.WindowPitLaneSectionListOpen = true;

            window.Closed += (sender, e) =>
            {
                MainViewModelPasserArounder.MainViewModel.WindowPitLaneSectionListOpen = false;
            };

            window.Show();
        }

        public override bool CanExecute(MainViewModel parameter)
        {
            return parameter.HasLoadedTrack;
        }
    }
}
