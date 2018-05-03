using System.Diagnostics;

namespace ArgTrack.Commands
{
    public class OpenDownloadPageCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            Process.Start(@"http://manicomio.se/argtrack/download");
        }
    }
}
