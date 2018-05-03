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

            if (vm.SelectedMainTabIndex == 0)       // track
            {
            //    if (vm.SelectedCarSetTabIndex == 0)
            //    {
            //        OpenWebSitePage("carset-screen/#teams-and-drivers-tab");
            //        return;
            //    }

            //    if (vm.SelectedCarSetTabIndex == 1)
            //    {
            //        OpenWebSitePage("carset-screen/#performance-tab");
            //        return;
            //    }

            //    if (vm.SelectedCarSetTabIndex == 2)
            //    {
            //        OpenWebSitePage("carset-screen/#cars-tab");
            //        return;
            //    }


            //    if (vm.SelectedCarSetTabIndex == 3)
            //    {
            //        OpenWebSitePage("carset-screen/#helmets-tab");
            //        return;
            //    }


            //    if (vm.SelectedCarSetTabIndex == 4)
            //    {
            //        OpenWebSitePage("carset-screen/#points-system-tab");
            //        return;
            //    }

                // TODO: implement specific tab ref
                OpenWebSitePage("track-screen/");
                return;
            }

            //if (vm.SelectedMainTabIndex == 1)       // game settings
            //{
            //    OpenWebSitePage("settings-screen/");
            //    return;
            //}

            //if (vm.SelectedMainTabIndex == 2)       // setups
            //{
            //    // TODO: implement selected setups tab
            //    OpenWebSitePage("setups-screen/");
            //    return;
            //}
                

            if (vm.SelectedMainTabIndex == 1)       // help/about
            {
                OpenWebSitePage("help-about-screen/");
                return;
            }

            // fallback
            OpenWebSitePage();
        }

        private void OpenWebSitePage(string relativeChildUrl = "")
        {
            var fullUrl = $"http://www.argtools.com/argtrack/{relativeChildUrl}";

            Process.Start(fullUrl);
        }
    }
}
