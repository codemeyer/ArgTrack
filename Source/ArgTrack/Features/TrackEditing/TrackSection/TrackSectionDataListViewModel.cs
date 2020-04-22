using System.Windows.Input;
using System.Windows.Media;
using ArgTrack.Features.Application.Shared;
using ArgTrack.Features.Main;
using ArgTrack.Features.TrackDrawing;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.TrackSection
{
    public class TrackSectionDataListViewModel : BaseViewModel
    {
        public TrackSectionViewModelCollection TrackSections { get; set; }


        private int _selectedTrackSectionIndex;

        public int SelectedTrackSectionIndex
        {
            get { return _selectedTrackSectionIndex; }
            set
            {
                SetProperty(ref _selectedTrackSectionIndex, value, () => SelectedTrackSectionIndex);

                TrackSections.SelectedIndex = value;
            }
        }

        private TrackSectionViewModel _selectedTrackSection;

        public TrackSectionViewModel SelectedTrackSection
        {
            get { return _selectedTrackSection; }
            set
            {
                if (SetProperty(ref _selectedTrackSection, value, () => SelectedTrackSection))
                {
                    if (SelectedTrackSection == null)
                    {
                        return;
                    }

                    // TODO: same as MainTreeWindow TreeView_OnSelectedItemChanged
                    var pointData = new TrackShapeData();

                    foreach (var point in SelectedTrackSection.SectionPoints)
                    {
                        pointData.Points.Add(point);
                    }

                    pointData.Fill = Brushes.Transparent;
                    pointData.Stroke = Brushes.Cyan;
                    pointData.StrokeThickness = 0.35;

                    MainViewModelPasserArounder.MainViewModel.TrackLayoutViewModel.SelectedTrackSection = pointData;
                }
            }
        }


        public ICommand AddTrackSectionCommand { get; } = new AddTrackSectionCommand();
        public ICommand InsertTrackSectionBeforeCommand { get; } = new InsertTrackSectionCommand(RelativeLocation.Before);
        public ICommand InsertTrackSectionAfterCommand { get; } = new InsertTrackSectionCommand(RelativeLocation.After);
        public ICommand RemoveTrackSection { get; } = new RemoveTrackSectionCommand();
        public ICommand EditTrackSectionCommand { get; } = new EditTrackSectionCommand();

        public ICommand MoveTrackSectionUpCommand { get;  } = new MoveTrackSectionUpCommand();
        public ICommand MoveTrackSectionDownCommand { get;  } = new MoveTrackSectionDownCommand();

        public ICommand HelpDocsCommandTrack { get; } = new HelpDocsCommand("screens/track-section-list-window/");
        public ICommand HelpDocsCommandPitLane { get; } = new HelpDocsCommand("screens/pit-lane-section-list-window/");
    }
}
