using System;
using System.Windows;
using System.Windows.Input;
using ArgTrack.AppStuff;
using ArgTrack.Commands.Model.TrackSections;
using ArgTrack.Commands.Tracks;
using InsertTrackSectionCommand = ArgTrack.Commands.Tracks.InsertTrackSectionCommand;

namespace ArgTrack.ViewModels.Track
{
    [Obsolete("Can be deleted after moving completely to tree view?")]
    public class TrackTabViewModel : BaseViewModel
    {
        public TrackTabViewModel()
        {
            UpdateActionButtonsVisibility();
        }

        private TrackViewModel _track;

        [Obsolete("Should not be used, replace with some global instead")]
        public TrackViewModel Track
        {
            get { return _track; }
            set { SetProperty(ref _track, value, () => Track); }
        }


        private int _selectedTrackTabIndex;

        public int SelectedTrackTabIndex
        {
            get { return _selectedTrackTabIndex; }
            set
            {
                if (SetProperty(ref _selectedTrackTabIndex, value, () => SelectedTrackTabIndex))
                {
                    UpdateActionButtonsVisibility();
                }
            }
        }


        private void UpdateActionButtonsVisibility()
        {
            if (SelectedTrackTabIndex == 1)
                ShowTrackSectionActionButtons = Visibility.Visible;
            else
                ShowTrackSectionActionButtons = Visibility.Collapsed;

            if (SelectedTrackTabIndex == 2)
                ShowPitLaneSectionActionButtons = Visibility.Visible;
            else
                ShowPitLaneSectionActionButtons = Visibility.Collapsed;

            if (SelectedTrackTabIndex == 3)
                ShowBestLineActionButtons = Visibility.Visible;
            else
                ShowBestLineActionButtons = Visibility.Collapsed;

            if (SelectedTrackTabIndex == 4)
                ShowObjectSettingsActionButtons = Visibility.Visible;
            else
                ShowObjectSettingsActionButtons = Visibility.Collapsed;

            if (SelectedTrackTabIndex == 6)
                ShowHorizonActionButtons = Visibility.Visible;
            else
                ShowHorizonActionButtons = Visibility.Collapsed;
        }


        private Visibility _showTrackSectionActionButtons;

        public Visibility ShowTrackSectionActionButtons
        {
            get { return _showTrackSectionActionButtons; }
            set { SetProperty(ref _showTrackSectionActionButtons, value, () => ShowTrackSectionActionButtons); }
        }

        private Visibility _showPitLaneSectionActionButtons;

        public Visibility ShowPitLaneSectionActionButtons
        {
            get { return _showPitLaneSectionActionButtons; }
            set { SetProperty(ref _showPitLaneSectionActionButtons, value, () => ShowPitLaneSectionActionButtons); }
        }

        private Visibility _showBestLineActionButtons;

        public Visibility ShowBestLineActionButtons
        {
            get { return _showBestLineActionButtons; }
            set { SetProperty(ref _showBestLineActionButtons, value, () => ShowBestLineActionButtons); }
        }

        private Visibility _showObjectSettingsActionButtons;

        public Visibility ShowObjectSettingsActionButtons
        {
            get { return _showObjectSettingsActionButtons; }
            set { SetProperty(ref _showObjectSettingsActionButtons, value, () => ShowObjectSettingsActionButtons); }
        }

        private Visibility _showHorizonActionButtons;

        public Visibility ShowHorizonActionButtons
        {
            get { return _showHorizonActionButtons; }
            set { SetProperty(ref _showHorizonActionButtons, value, () => ShowHorizonActionButtons); }
        }


        public ICommand LoadTrackCommand { get; } = new LoadTrackCommand(new FileDialog());
        public ICommand SaveTrackCommand { get; } = new SaveTrackCommand(new FileDialog());
        public ICommand AddTrackSectionCommand { get; } = new AddTrackSectionCommand();
        public ICommand InsertTrackSectionCommand { get; } = new InsertTrackSectionCommand();
        public ICommand DeleteTrackSectionCommand { get; } = new DeleteTrackSectionCommand();
        public ICommand MoveTrackSectionUpCommand { get; } = new MoveTrackSectionUpCommand();
        public ICommand MoveTrackSectionDownCommand { get; } = new MoveTrackSectionDownCommand();
        public ICommand InsertPitLaneSectionCommand { get; } = new InsertPitLaneSectionCommand();
        public ICommand DeletePitLaneSectionCommand { get; } = new DeleteTrackSectionCommand(false);
        public ICommand MovePitLaneSectionUpCommand { get; } = new MovePitLaneSectionUpCommand();
        public ICommand MovePitLaneSectionDownCommand { get; } = new MovePitLaneSectionDownCommand();
        public ICommand AddBestLineSegmentCommand { get; } = new AddBestLineSegmentCommand();
        public ICommand InsertBestLineSegmentCommand { get; } = new InsertBestLineSegmentCommand();
        public ICommand DeleteBestLineSegmentCommand { get; } = new DeleteBestLineSegmentCommand();
        public ICommand MoveBestLineSegmentUpCommand { get; } = new MoveBestLineSegmentUpCommand();
        public ICommand MoveBestLineSegmentDownCommand { get; } = new MoveBestLineSegmentDownCommand();
        public ICommand AddObjectSettingsCommand { get; } = new AddObjectSettingsCommand();
        public ICommand UpdateHorizonFromFileCommand { get; } = new LoadHorizonFromFileCommand(new FileDialog());
        public ICommand SaveHorizonToFileCommand { get; } = new SaveHorizonToFileCommand(new FileDialog());
    }
}
