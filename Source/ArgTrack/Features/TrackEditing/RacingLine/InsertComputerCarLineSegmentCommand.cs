using ArgData.Entities;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.RacingLine
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
            newSection.CanEditType = true;
            newSection.SegmentType = TrackComputerCarLineSegmentType.Normal;

            var window = new TrackBestLineDetailsWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
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
            }
        }
    }
}
