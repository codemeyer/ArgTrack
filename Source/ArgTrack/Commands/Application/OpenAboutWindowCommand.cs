using System.Windows;
using ArgTrack.ViewModels;
using ArgTrack.Windows;

namespace ArgTrack.Commands
{
    public class OpenAboutWindowCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var window = new AboutWindow();
            window.DataContext = new AboutViewModel();
            window.Owner = Application.Current.MainWindow;

            window.ShowDialog();
        }
    }
}
