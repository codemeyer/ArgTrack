using System.Diagnostics;

namespace ArgTrack.Commands
{
    public class OpenOtherSiteCommand : CommandBase
    {
        private readonly string _siteUrl;

        public OpenOtherSiteCommand(string siteUrl)
        {
            _siteUrl = siteUrl;
        }

        public override void Execute(object parameter)
        {
            Process.Start(_siteUrl);
        }
    }
}
