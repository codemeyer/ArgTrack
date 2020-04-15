using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.TrackSection
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
