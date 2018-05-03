using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ArgTrack.Commands.Tracks;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.Windows.Track
{
    public partial class TrackSectionDetailsWindow : Window
    {
        public TrackSectionDetailsWindow()
        {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listView = sender as ListView;

            var selectedTrackCommand = listView?.SelectedValue;

            var ts = selectedTrackCommand as TrackCommandViewModel;

            if (ts == null)
                return;

            var command = new EditTrackCommandCommand();
            command.Execute(ts);
        }

        private void OKButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
