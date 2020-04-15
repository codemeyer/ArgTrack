using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using ArgTrack.Features.Main;

namespace ArgTrack.Features.TrackEditing.TrackSection
{
    public sealed class TrackSectionViewModelCollection : ObservableCollection<TrackSectionViewModel>
    {
        private readonly TrackViewModel _trackViewModel;

        public TrackSectionViewModelCollection(TrackViewModel trackViewModel)
        {
            _trackViewModel = trackViewModel;

            this.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RecalculateIndices();

            // also recalculate graphics? or will that be too expensive?
        }

        private void RecalculateIndices()
        {
            for (var index = 0; index < Items.Count; index++)
            {
                Items[index].Index = index;
            }
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
