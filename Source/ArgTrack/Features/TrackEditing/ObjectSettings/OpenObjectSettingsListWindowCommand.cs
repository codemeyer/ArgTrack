using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectSettings
{
    public class OpenObjectSettingsListWindowCommand : CommandBase<MainViewModel>
    {
        public override void Execute(MainViewModel parameter)
        {
            var window = new ObjectSettingsListWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
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
