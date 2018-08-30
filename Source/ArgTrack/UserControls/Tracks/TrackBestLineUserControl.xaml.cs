using System;
using System.Windows.Controls;
using System.Windows.Input;
using ArgTrack.Commands.Tracks;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.UserControls.Tracks
{
    /// <summary>
    /// Interaction logic for TrackBestLineUserControl.xaml
    /// </summary>
    [Obsolete("Replaced by a window")]
    public partial class TrackBestLineUserControl : UserControl
    {
        public TrackBestLineUserControl()
        {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // until I figure out how to do it with MouseBinding in XAML...

            var listView = sender as ListView;

            var selectedTrackSection = listView?.SelectedValue;

            var ts = selectedTrackSection as BestLineSegmentViewModel;

            if (ts == null)
                return;

            var command = new EditBestLineCommand();
            command.Execute(ts);
        }
    }
}
