using System.Windows;

namespace ArgTrack.Windows.Track
{
    /// <summary>
    /// Interaction logic for TrackLayoutWindow.xaml
    /// </summary>
    public partial class TrackLayoutWindow : Window
    {
        public TrackLayoutWindow()
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
