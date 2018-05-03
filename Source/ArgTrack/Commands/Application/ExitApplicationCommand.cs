using System.Windows;

namespace ArgTrack.Commands
{
    public class ExitApplicationCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
