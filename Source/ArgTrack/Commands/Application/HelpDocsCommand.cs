using System.Diagnostics;

namespace ArgTrack.Commands
{
    public class HelpDocsCommand : CommandBase
    {
        private readonly string _helpPath;

        public HelpDocsCommand()
        {
            _helpPath = string.Empty;
        }

        public HelpDocsCommand(string helpPath)
        {
            _helpPath = helpPath;
        }

        public override void Execute(object parameter)
        {
            OpenWebSitePage(_helpPath);
        }

        private void OpenWebSitePage(string relativeChildUrl = "")
        {
            var fullUrl = $"https://www.argtools.com/argtrack/{relativeChildUrl}";

            Process.Start(fullUrl);
        }
    }
}
