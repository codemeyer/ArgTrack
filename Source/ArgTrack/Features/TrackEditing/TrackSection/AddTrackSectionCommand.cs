using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.TrackSection
{
    public class AddTrackSectionCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var parentList = GetParameterAs<TrackSectionViewModelCollection>(parameter);

            var newSection = new TrackSectionViewModel(parentList);

            var window = new TrackSectionDetailsWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = newSection
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                parentList.Add(newSection);
                parentList.RecalculateGraphics();
            }
        }
    }
}
