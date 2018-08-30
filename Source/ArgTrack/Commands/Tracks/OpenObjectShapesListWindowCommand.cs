using System.Windows;
using ArgTrack.ViewModels;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class OpenObjectShapesListWindowCommand : CommandBase<MainViewModel>
    {
        public override void Execute(MainViewModel parameter)
        {
            var window = new ObjectShapesListWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = parameter.TrackViewModel
            };

            window.ShowDialog();
        }

        public override bool CanExecute(MainViewModel parameter)
        {
            return parameter.HasLoadedTrack;
        }
    }
}
