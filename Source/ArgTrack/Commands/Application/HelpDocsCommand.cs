using System.Diagnostics;
using ArgTrack.ViewModels;

namespace ArgTrack.Commands
{
    public class HelpDocsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as MainViewModel;

            if (vm == null)
            {
                OpenWebSitePage();
                return;
            }

            // TODO: other way of determining which page to open

            // if (vm.SelectedMainTabIndex == 0)       // track
            // ...

            // fallback
            OpenWebSitePage();
        }

        private void OpenWebSitePage(string relativeChildUrl = "")
        {
            var fullUrl = $"https://www.argtools.com/argtrack/{relativeChildUrl}";

            Process.Start(fullUrl);
        }
    }
}
