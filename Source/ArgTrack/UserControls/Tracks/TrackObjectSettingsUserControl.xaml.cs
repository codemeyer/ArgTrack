using System;
using System.Windows.Controls;
using System.Windows.Input;
using ArgTrack.Commands.Tracks;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.UserControls.Tracks
{
    [Obsolete("Has been replaced by a window")]
    public partial class TrackObjectSettingsUserControl : UserControl
    {
        public TrackObjectSettingsUserControl()
        {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // until I figure out how to do it with MouseBinding in XAML...

            var listView = sender as ListView;

            var selectedItem = listView?.SelectedValue;

            var ts = selectedItem as TrackObjectSettingsViewModel;

            if (ts == null)
                return;

            var command = new EditTrackObjectSettingsCommand();
            command.Execute(ts);
        }
    }
}
