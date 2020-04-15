using System.Diagnostics;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.Application.About
{
    public class OpenDownloadPageCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            Process.Start(@"https://www.argtools.com/argtrack/download/");
        }
    }
}
