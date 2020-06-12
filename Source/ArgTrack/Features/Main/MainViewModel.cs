using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ArgTrack.Features.Application.About;
using ArgTrack.Features.Application.Shared;
using ArgTrack.Features.Persistence;
using ArgTrack.Features.TrackConversion;
using ArgTrack.Features.TrackDrawing;
using ArgTrack.Features.TrackEditing.ObjectSettings;
using ArgTrack.Features.TrackEditing.ObjectShapes;
using ArgTrack.Features.TrackEditing.RacingLine;
using ArgTrack.Features.TrackEditing.TrackSection;
using ArgTrack.Features.TrackValidation;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.Main
{
    /// <summary>
    /// Application-wide view model.
    /// </summary>
    public class MainViewModel : BaseViewModel, IDataErrorInfo
    {
        public ICommand LoadTrackCommand { get; } = new LoadTrackCommand(new FileDialog());
        public ICommand SaveTrackCommand { get; } = new SaveTrackCommand(new FileDialog());
        public ICommand ExitApplicationCommand { get; } = new ExitApplicationCommand();

        public ICommand HelpDocsCommand { get; } = new HelpDocsCommand();

        public ICommand ValidateTrackCommand { get; } = new ValidateTrackCommand();
        public ICommand ConvertTrackCommand { get; } = new ConvertTrackCommand();

        public ICommand OpenAboutWindowCommand { get; } = new OpenAboutWindowCommand();

        public ICommand ShowWarningMessageCommand { get; } = new ShowWarningMessageCommand();
        public ICommand ShowErrorMessageCommand { get; } = new ShowErrorMessageCommand();
        public ICommand ShowMessageCommand { get; } = new ShowMessageCommand();

        public ICommand OpenTrackSectionListWindowCommand { get; } = new OpenTrackSectionListWindowCommand();
        public ICommand OpenPitLaneSectionListWindowCommand { get; } = new OpenPitLaneSectionListWindowCommand();
        public ICommand OpenComputerCarRacingLineWindowCommand { get; } = new OpenComputerCarRacingLineWindowCommand();
        public ICommand OpenObjectSettingsListWindowCommand { get; } = new OpenObjectSettingsListWindowCommand();
        public ICommand OpenObjectShapesListWindowCommand { get; } = new OpenObjectShapesListWindowCommand();


        private bool _hasLoadedTrack;

        public bool HasLoadedTrack
        {
            get { return _hasLoadedTrack; }
            set
            {
                if (SetProperty(ref _hasLoadedTrack, value, () => HasLoadedTrack))
                {
                    UpdateTreeViewVisibility();
                }
            }
        }

        private void UpdateTreeViewVisibility()
        {
            VisibleAtStartup = HasLoadedTrack ? Visibility.Hidden : Visibility.Visible;
            VisibleWhenTrackHasBeenLoaded = HasLoadedTrack ? Visibility.Visible : Visibility.Hidden;

            if (TrackLayoutViewModel != null)
            {
                //TrackLayoutViewModel.VisibleAtStartup = VisibleAtStartup;
                //TrackLayoutViewModel.VisibleWhenTrackHasBeenLoaded = VisibleAtStartup;
            }
        }

        private Visibility _visibleAtStartup = Visibility.Visible;

        /// <summary>
        /// Apply this to UI elements that should be visible when the application starts.
        /// </summary>
        public Visibility VisibleAtStartup
        {
            get { return _visibleAtStartup; }
            set { SetProperty(ref _visibleAtStartup, value, () => VisibleAtStartup); }
        }

        /// <summary>
        /// Apply this to UI elements that should only be visible when a track has been loaded.
        /// </summary>
        private Visibility _visibleWhenTrackHasBeenLoaded = Visibility.Hidden;

        public Visibility VisibleWhenTrackHasBeenLoaded
        {
            get { return _visibleWhenTrackHasBeenLoaded; }
            set { SetProperty(ref _visibleWhenTrackHasBeenLoaded, value, () => VisibleWhenTrackHasBeenLoaded); }
        }


        private TrackViewModel _trackViewModel;

        public TrackViewModel TrackViewModel
        {
            get { return _trackViewModel; }
            set { SetProperty(ref _trackViewModel, value, () => TrackViewModel); }
        }


        private TrackLayoutViewModel _trackLayoutViewModel;

        public TrackLayoutViewModel TrackLayoutViewModel
        {
            get { return _trackLayoutViewModel; }
            set { SetProperty(ref _trackLayoutViewModel, value, () => TrackLayoutViewModel); }
        }


        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                return "";
            }
        }



#if DEBUG
        private Visibility _displayDebugHackMenu = Visibility.Visible;
#else
        private Visibility _displayDebugHackMenu = Visibility.Hidden;
#endif

        public Visibility DisplayDebugHackMenu
        {
            get { return _displayDebugHackMenu; }
            set
            {
                SetProperty(ref _displayDebugHackMenu, value, () => DisplayDebugHackMenu);
            }
        }



        private bool _drawTrack = true;

        public bool DrawTrack
        {
            get { return _drawTrack; }
            set
            {
                if (SetProperty(ref _drawTrack, value, () => DrawTrack))
                {
                    TrackLayoutViewModel.SetDisplayTrackShapeData(value);
                }
            }
        }

        private bool _drawPitLane = true;

        public bool DrawPitLane
        {
            get { return _drawPitLane; }
            set
            {
                if (SetProperty(ref _drawPitLane, value, () => DrawPitLane))
                {
                    TrackLayoutViewModel.SetDisplayPitLaneShapeData(value);
                }
            }
        }

        private bool _drawVerges = true;

        public bool DrawVerges
        {
            get { return _drawVerges; }
            set
            {
                if (SetProperty(ref _drawVerges, value, () => DrawVerges))
                {
                    TrackLayoutViewModel.SetDisplayVergeData(value);
                }
            }
        }


        private bool _drawTrackSideObjects = true;

        public bool DrawTrackSideObjects
        {
            get { return _drawTrackSideObjects; }
            set
            {
                if (SetProperty(ref _drawTrackSideObjects, value, () => DrawTrackSideObjects))
                {
                    TrackLayoutViewModel.SetDisplayTrackSideObjectData(value);
                }
            }
        }


        private bool _drawCircleCenterIndicators;

        public bool DrawCircleCenterIndicators
        {
            get { return _drawCircleCenterIndicators; }
            set
            {
                if (SetProperty(ref _drawCircleCenterIndicators, value, () => DrawCircleCenterIndicators))
                {
                    TrackLayoutViewModel.SetDisplayDebugCircleCenterIndicatorsData(value);
                }
            }
        }


        private bool _drawLineBetweenSectionCenters;

        public bool DrawLineBetweenSectionCenters
        {
            get { return _drawLineBetweenSectionCenters; }
            set
            {
                if (SetProperty(ref _drawLineBetweenSectionCenters, value, () => DrawLineBetweenSectionCenters))
                {
                    TrackLayoutViewModel.SetDisplayDebugLineBetweenSectionCentersData(value);
                }
            }
        }

        private bool _drawLineAlongTrackCenter;

        public bool DrawLineAlongTrackCenter
        {
            get { return _drawLineAlongTrackCenter; }
            set
            {
                if (SetProperty(ref _drawLineAlongTrackCenter, value, () => DrawLineAlongTrackCenter))
                {
                    TrackLayoutViewModel.SetDisplayDebugLineAlongTrackCenterData(value);
                }
            }
        }

        public bool WindowTrackSectionListOpen { get; set; }
        public bool WindowPitLaneSectionListOpen { get; set; }
        //public bool WindowRacingLineListOpen { get; set; }
        //public bool WindowObjectSettingsListOpen { get; set; }
        //public bool WindowObjectShapesListOpen { get; set; }

        public void ResetDrawingElements()
        {
            DrawTrack = true;
            DrawPitLane = true;
            DrawVerges = true;
            DrawTrackSideObjects = true;

            DrawCircleCenterIndicators = false;
            DrawLineAlongTrackCenter = false;
            DrawLineBetweenSectionCenters = false;
        }
    }
}
