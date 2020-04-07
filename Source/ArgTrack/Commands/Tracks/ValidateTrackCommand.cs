using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ArgTrack.ViewModels;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Tools;

namespace ArgTrack.Commands.Tracks
{
    public class ValidateTrackCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var mvm = parameter as MainViewModel;
            var track = mvm.TrackViewModel;

            var validator = new TrackValidator();
            var results = validator.Validate(track);

            var viewModel = new TrackValidationViewModel();

            foreach (var result in results)
            {
                var resultViewModel = new TrackValidationResultViewModel();
                resultViewModel.Message = result.Message;

                viewModel.Results.Add(resultViewModel);
            }

            var window = new TrackValidationWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = viewModel
            };

            window.ShowDialog();
        }

        public override bool CanExecute(object parameter)
        {
            var mvm = parameter as MainViewModel;

            return mvm.HasLoadedTrack;
        }
    }

    public class TrackValidator
    {
        public List<TrackValidationResult> Validate(TrackViewModel track)
        {
            var results = new List<TrackValidationResult>();

            // check for missing commands that NEED to be there
            // what else?

            var allTrackSectionCommands = track.TrackSections.SelectMany(t => t.Commands).ToList();
            var allPitLaneSectionCommand = track.PitLaneSections.SelectMany(p => p.Commands).ToList();

            CheckForExactlyOneCommandInTrackSections(0x86, allTrackSectionCommands, results);
            CheckForExactlyOneCommandInTrackSections(0x87, allTrackSectionCommands, results);
            CheckForExactlyOneCommandInPitLaneSections(0x96, allPitLaneSectionCommand, results);
            CheckForExactlyOneCommandInPitLaneSections(0x97, allPitLaneSectionCommand, results);
            CheckForExactlyOneCommandInPitLaneSections(0x9B, allPitLaneSectionCommand, results);
            CheckForExactlyOneCommandInPitLaneSections(0x9C, allPitLaneSectionCommand, results);
            CheckForExactlyOneCommandInPitLaneSections(0x9D, allPitLaneSectionCommand, results);
            CheckForExactlyOneCommandInPitLaneSections(0x9E, allPitLaneSectionCommand, results);
            CheckForExactlyOneCommandInPitLaneSections(0x9F, allPitLaneSectionCommand, results);
            CheckForExactlyOneCommandInPitLaneSections(0xA0, allPitLaneSectionCommand, results);
            CheckForExactlyOneCommandInTrackSections(0xA1, allTrackSectionCommands, results);
            CheckForExactlyOneCommandInTrackSections(0xA2, allTrackSectionCommands, results);
            CheckForExactlyOneCommandInTrackSections(0xA3, allTrackSectionCommands, results);
            CheckForExactlyOneCommandInTrackSections(0xA4, allTrackSectionCommands, results);
            CheckForExactlyOneCommandInTrackSections(0xA8, allTrackSectionCommands, results);

            // more detailed:
            // 0x9b unknown pitlane marker - must be 1 in first sector of pitlane
            // 0x9e must be 1 in pitlane, after 0x9d
            // 0xa1 (warn if too far from pitlane start?)
            // 0xa2 (warn if too far from pitlane start?)
            // 0xa8 (warn if not somewhere around the end of the track?)

            if (results.Count == 0)
            {
                results.Add(new TrackValidationResult { Message = "No validation errors found!" });
            }

            return results;
        }

        private void CheckForExactlyOneCommandInTrackSections(byte command, List<TrackCommandViewModel> allCommands, List<TrackValidationResult> results)
        {
            if (allCommands.Count(c => c.Command == command) != 1)
            {
                results.Add(new TrackValidationResult { Message = BuildMustHaveExactlyOneMessage(command) });
            }
        }

        private void CheckForExactlyOneCommandInPitLaneSections(byte command, List<TrackCommandViewModel> allCommands, List<TrackValidationResult> results)
        {
            if (allCommands.Count(c => c.Command == command) != 1)
            {
                results.Add(new TrackValidationResult { Message = BuildMustHaveExactlyOneMessagePL(command) });
            }
        }

        private string BuildMustHaveExactlyOneMessage(byte command)
        {
            var description = CommandDescriptions.GetCommandDescription(command);
            var message = $"Track sections must have a section with exactly one (1) 0x{command:X2}: {description}";

            return message;
        }

        private string BuildMustHaveExactlyOneMessagePL(byte command)
        {
            var description = CommandDescriptions.GetCommandDescription(command);
            var message = $"Pit lane sections must have a section with exactly one (1) 0x{command:X2}: {description}";

            return message;
        }
    }

    public class TrackValidationResult
    {
        public string Message { get; set; }
    }

    public class TrackValidationViewModel : BaseViewModel
    {
        public TrackValidationViewModel()
        {
            Results = new ObservableCollection<TrackValidationResultViewModel>();
        }

        private ObservableCollection<TrackValidationResultViewModel> _results;

        public ObservableCollection<TrackValidationResultViewModel> Results
        {
            get { return _results; }
            set { SetProperty(ref _results, value, () => Results); }
        }
    }

    public class TrackValidationResultViewModel : BaseViewModel
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value, () => Message); }
        }
    }
}
