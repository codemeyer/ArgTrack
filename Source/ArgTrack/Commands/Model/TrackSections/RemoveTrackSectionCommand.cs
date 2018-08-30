using System.Windows;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Model.TrackSections
{
    public class RemoveTrackSectionCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var section = GetParameterAs<TrackSectionViewModel>(parameter);
            var parentList = section.ParentList;

            // circular?
            var result = MessageBox.Show("Are you sure that you wish to remove the section?", "ArgTrack", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                parentList.Remove(section);
                parentList.RecalculateGraphics();
            }
        }
    }
}
