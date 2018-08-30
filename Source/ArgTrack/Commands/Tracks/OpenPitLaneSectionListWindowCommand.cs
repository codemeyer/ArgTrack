using System.Windows;
using ArgTrack.ViewModels;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class OpenPitLaneSectionListWindowCommand : CommandBase<MainViewModel>
    {
        public override void Execute(MainViewModel parameter)
        {
            var window = new PitLaneSectionListWindow
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
