using System.Collections.ObjectModel;
using System.Windows.Input;
using ArgTrack.Commands.Model;
using ArgTrack.Commands.Model.TrackSections;
using ArgTrack.Commands.Tracks;
using InsertTrackSectionCommand = ArgTrack.Commands.Model.TrackSections.InsertTrackSectionCommand;

namespace ArgTrack.ViewModels.Track
{
    public class TrackSectionViewModelCollection : ObservableCollection<TrackSectionViewModel>
    {
        private readonly TrackViewModel _trackViewModel;

        public TrackSectionViewModelCollection(TrackViewModel trackViewModel)
        {
            _trackViewModel = trackViewModel;
        }

        /// <summary>
        /// Child sections can call this method to force a recalculation of the track graphics,
        /// e.g. when a section has been added, removed or edited.
        /// </summary>
        public void RecalculateGraphics()
        {
            _trackViewModel.RecalculateLayout();
        }

        public int SelectedIndex { get; set; }

        public ICommand AddNewTrackSectionCommand { get; } = new AddTrackSectionCommand();
        public ICommand InsertNewTrackSectionBeforeCommand { get; } = new InsertTrackSectionCommand(RelativeLocation.Before);
        public ICommand InsertNewTrackSectionAfterCommand { get; } = new InsertTrackSectionCommand(RelativeLocation.After);
        public ICommand RemoveTrackSection { get; } = new RemoveTrackSectionCommand();
        public ICommand EditTrackSectionCommand { get; } = new EditTrackSectionCommand();
    }
}
