using System.Windows;
using ArgTrack.ViewModels;

namespace ArgTrack.Commands
{
    public class CheckForUpdateCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var viewModel = parameter as AboutTabViewModel;

            if (viewModel == null) return;

            viewModel.VersionUnableToRetrieveLabel = Visibility.Collapsed;
            viewModel.VersionLatestLabel = Visibility.Collapsed;
            viewModel.VersionNewAvailable = Visibility.Collapsed;

            var checker = new VersionChecker();
            string latest = checker.GetLatestVersion();

            if (string.IsNullOrEmpty(latest))
            {
                viewModel.VersionUnableToRetrieveLabel = Visibility.Visible;
                return;
            }

            viewModel.LatestVersion = latest;

            bool hasNewer = checker.Compare(viewModel.CurrentVersion, latest);

            if (hasNewer)
            {
                viewModel.VersionNewAvailable = Visibility.Visible;
            }
            else
            {
                viewModel.VersionLatestLabel = Visibility.Visible;
            }
        }
    }
}
