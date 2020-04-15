using System.Windows;

namespace ArgTrack.Features.TrackEditing.RacingLine
{
    public partial class TrackBestLineDetailsWindow : Window
    {
        public TrackBestLineDetailsWindow()
        {
            InitializeComponent();
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
