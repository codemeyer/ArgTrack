using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using ArgData.Entities;
using ArgTrack.ViewModels;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class ValidateTrackCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var trackTab = parameter as TrackTabViewModel;

            if (trackTab == null)
            {
                return;
            }

            var validator = new TrackValidator();
            var results = validator.Validate(trackTab.Track);

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
    }

    public class TrackValidator
    {
        public List<TrackValidationResult> Validate(TrackViewModel track)
        {
            var results = new List<TrackValidationResult>();

            // check for missing commands that NEED to be there
            // what else?

            results.Add(new TrackValidationResult { Message = "Test" });

            return results;
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
