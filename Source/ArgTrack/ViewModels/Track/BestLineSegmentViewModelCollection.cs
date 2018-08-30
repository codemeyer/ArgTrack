using System.Collections.ObjectModel;
using System.Windows.Input;
using ArgTrack.Commands.Model;
using ArgTrack.Commands.Model.ComputerCarLineSegments;
using ArgTrack.Commands.Tracks;

namespace ArgTrack.ViewModels.Track
{
    public class BestLineSegmentViewModelCollection : ObservableCollection<BestLineSegmentViewModel>
    {
        private readonly TrackViewModel _viewModel;

        public BestLineSegmentViewModelCollection(TrackViewModel viewModel)
        {
            _viewModel = viewModel;
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
