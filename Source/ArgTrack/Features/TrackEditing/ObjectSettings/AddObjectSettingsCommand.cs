using System;
using System.Linq;
using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectSettings
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
                Owner = System.Windows.Application.Current.MainWindow,
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
