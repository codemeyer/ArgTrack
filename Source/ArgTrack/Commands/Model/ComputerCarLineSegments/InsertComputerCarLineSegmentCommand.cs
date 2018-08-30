using System.Windows;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Model.ComputerCarLineSegments
{
    public class InsertComputerCarLineSegmentCommand : CommandBase
    {
        private readonly RelativeLocation _location;

        public InsertComputerCarLineSegmentCommand(RelativeLocation location)
        {
            _location = location;
        }

        public override void Execute(object parameter)
        {
            var parentList = GetParameterAs<BestLineSegmentViewModelCollection>(parameter);

            var newSection = new BestLineSegmentViewModel(parentList);

            var window = new TrackBestLineDetailsWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = newSection
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                int newIndex = _location == RelativeLocation.Before
                    ? parentList.SelectedIndex
                    : parentList.SelectedIndex + 1;

                parentList.Insert(newIndex, newSection);
                parentList.RecalculateGraphics();

                // TODO: ?
                //vm.RecalculateTrackSectionIndexz();
                //vm.SelectedTrackSectionIndex--;
            }
        }
    }
}
