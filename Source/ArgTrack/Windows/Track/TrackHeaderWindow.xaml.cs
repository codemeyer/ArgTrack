using System.Windows;

namespace ArgTrack.Windows.Track
{
    /// <summary>
    /// Interaction logic for TrackHeaderWindow.xaml
    /// </summary>
    public partial class TrackHeaderWindow : Window
    {
        public TrackHeaderWindow()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
