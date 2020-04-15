using ArgTrack.Infrastructure;

namespace ArgTrack.Features.Application.About
{
    public class OpenAboutWindowCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var window = new AboutWindow();
            window.DataContext = new AboutViewModel();
            window.Owner = System.Windows.Application.Current.MainWindow;

            window.ShowDialog();
        }
    }
}
