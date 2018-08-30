using System.Windows;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Model.TrackSections
{
    public class AddTrackSectionCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var parentList = GetParameterAs<TrackSectionViewModelCollection>(parameter);

            var newSection = new TrackSectionViewModel(parentList);

            var window = new TrackSectionDetailsWindow
            {
                Owner = Application.Current.MainWindow,
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
