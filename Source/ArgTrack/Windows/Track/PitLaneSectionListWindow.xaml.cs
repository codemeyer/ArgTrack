using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ArgTrack.Commands.Tracks;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.Windows.Track
{
    /// <summary>
    /// Interaction logic for PitLaneSectionListWindow.xaml
    /// </summary>
    public partial class PitLaneSectionListWindow : Window
    {
        public PitLaneSectionListWindow()
        {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // until I figure out how to do it with MouseBinding in XAML...

            var listView = sender as ListView;

            var selectedTrackSection = listView?.SelectedValue;

            var ts = selectedTrackSection as TrackSectionViewModel;

            if (ts == null)
                return;

            var command = new EditTrackSectionCommand();
            command.Execute(ts);
        }
    }
}
