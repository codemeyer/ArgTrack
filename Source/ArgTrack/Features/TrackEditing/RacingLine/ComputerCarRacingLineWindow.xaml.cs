using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArgTrack.Features.TrackEditing.RacingLine
{
    /// <summary>
    /// Interaction logic for ComputerCarRacingLineWindow.xaml
    /// </summary>
    public partial class ComputerCarRacingLineWindow : Window
    {
        public ComputerCarRacingLineWindow()
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
