using System;
using System.Linq;
using System.Windows;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class AddObjectSettingsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var track = parameter as TrackViewModel;

            if (track == null)
                return;


            var currentMaxOffset = track.TrackObjectSettings.Max(o => o.Offset);



            var newSetting = new TrackObjectSettingsViewModel();

            newSetting.Offset = Convert.ToInt16(currentMaxOffset + 16);

            // TODO:
            var window = new TrackObjectSettingsDetailsWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = newSetting
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                track.TrackObjectSettings.Add(newSetting);
            }
        }
    }
}
