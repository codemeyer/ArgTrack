using System.Windows;

namespace ArgTrack.Windows.Track
{
    /// <summary>
    /// Interaction logic for TrackCommandDetailsWindow.xaml
    /// </summary>
    public partial class TrackCommandDetailsWindow : Window
    {
        public TrackCommandDetailsWindow()
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
