using System.Diagnostics;

namespace ArgTrack.Commands
{
    public class OpenArgDataPageCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            Process.Start(@"https://www.argtools.com/argdata");
        }
    }
}
