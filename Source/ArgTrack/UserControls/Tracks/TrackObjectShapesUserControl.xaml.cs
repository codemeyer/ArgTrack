using System;
using System.Windows.Controls;
using System.Windows.Input;
using ArgTrack.Commands.Tracks;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.UserControls.Tracks
{
    [Obsolete("Replaced by a window")]
    public partial class TrackObjectShapesUserControl : UserControl
    {
        public TrackObjectShapesUserControl()
        {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // until I figure out how to do it with MouseBinding in XAML...

            var listView = sender as ListView;

            var selectedItem = listView?.SelectedValue;

            var ts = selectedItem as TrackObjectShapeViewModel;

            if (ts == null)
                return;

            var command = new EditTrackObjectShapeCommand();
            command.Execute(ts);
        }
    }
}
