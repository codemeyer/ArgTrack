using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using ArgTrack.Commands.Model;
using ArgTrack.Commands.Model.ComputerCarLineSegments;

namespace ArgTrack.ViewModels.Track
{
    public sealed class BestLineSegmentViewModelCollection : ObservableCollection<BestLineSegmentViewModel>
    {
        private readonly TrackViewModel _viewModel;

        public BestLineSegmentViewModelCollection(TrackViewModel viewModel)
        {
            _viewModel = viewModel;

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
            // TODO: implement again when there's a CC line
            //_viewModel.RecalculateLayout();
        }

        public int SelectedIndex { get; set; }

        public ICommand AddComputerCarLineSegmentCommand { get; } = new AddComputerCarLineSegmentCommand();
        public ICommand InsertNewComputerCarLineSegmentBeforeCommand { get; } = new InsertComputerCarLineSegmentCommand(RelativeLocation.Before);
        public ICommand InsertNewComputerCarLineSegmentAfterCommand { get; } = new InsertComputerCarLineSegmentCommand(RelativeLocation.After);
        public ICommand EditComputerCarLineSegmentCommand { get; } = new EditComputerCarLineSegmentCommand();
        public ICommand RemoveComputerCarLineSegmentCommand { get; } = new RemoveComputerCarLineSegmentCommand();
    }
}
