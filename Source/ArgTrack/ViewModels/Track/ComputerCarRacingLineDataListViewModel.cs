using System.Windows.Input;
using ArgTrack.Commands.Model;
using ArgTrack.Commands.Model.ComputerCarLineSegments;
using ArgTrack.Commands.Tracks;

namespace ArgTrack.ViewModels.Track
{
    public class ComputerCarRacingLineDataListViewModel : BaseViewModel
    {
        public BestLineSegmentViewModelCollection BestLineSegments { get; set; }


        private int _selectedSegmentIndex;

        public int SelectedSegmentIndex
        {
            get { return _selectedSegmentIndex; }
            set
            {
                SetProperty(ref _selectedSegmentIndex, value, () => SelectedSegmentIndex);

                BestLineSegments.SelectedIndex = value;
            }
        }

        private BestLineSegmentViewModel _selectedSegment;

        public BestLineSegmentViewModel SelectedSegment
        {
            get { return _selectedSegment; }
            set { SetProperty(ref _selectedSegment, value, () => SelectedSegment); }
        }


        //public ICommand AddTrackSectionCommand { get; } = new AddTrackSectionCommand();
        //public ICommand InsertTrackSectionBeforeCommand { get; } = new Commands.Model.TrackSections.InsertTrackSectionCommand(RelativeLocation.Before);
        //public ICommand InsertTrackSectionAfterCommand { get; } = new Commands.Model.TrackSections.InsertTrackSectionCommand(RelativeLocation.After);
        //public ICommand RemoveTrackSection { get; } = new RemoveTrackSectionCommand();
        //public ICommand EditTrackSectionCommand { get; } = new EditTrackSectionCommand();

        //public ICommand MoveTrackSectionUpCommand { get;  } = new MoveTrackSectionUpCommand();
        //public ICommand MoveTrackSectionDownCommand { get;  } = new MoveTrackSectionDownCommand();

        public ICommand AddComputerCarLineSegmentCommand { get; } = new AddComputerCarLineSegmentCommand();
        public ICommand InsertNewComputerCarLineSegmentBeforeCommand { get; } = new InsertComputerCarLineSegmentCommand(RelativeLocation.Before);
        public ICommand InsertNewComputerCarLineSegmentAfterCommand { get; } = new InsertComputerCarLineSegmentCommand(RelativeLocation.After);
        public ICommand EditComputerCarLineSegmentCommand { get; } = new EditComputerCarLineSegmentCommand();
        public ICommand RemoveComputerCarLineSegmentCommand { get; } = new RemoveComputerCarLineSegmentCommand();

        public ICommand MoveComputerCarLineSegmentUpCommand { get; } = new MoveBestLineSegmentUpCommand();
        public ICommand MoveComputerCarLineSegmentDownCommand { get; } = new MoveBestLineSegmentDownCommand();
    }
}
