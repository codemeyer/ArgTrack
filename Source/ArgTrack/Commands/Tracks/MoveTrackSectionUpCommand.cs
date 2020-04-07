using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Tracks
{
    /// <summary>
    /// Moves a track section (or a pit lane section) up in the list.
    /// </summary>
    public class MoveTrackSectionUpCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var trackSection = parameter as TrackSectionViewModel;

            if (trackSection == null)
                return;

            trackSection.ParentList.Move(trackSection.Index, trackSection.Index - 1);
        }

        public override bool CanExecute(object parameter)
        {
            var trackSection = parameter as TrackSectionViewModel;

            if (trackSection == null)
                return false;

            return trackSection.Index > 0;
        }
    }
}
