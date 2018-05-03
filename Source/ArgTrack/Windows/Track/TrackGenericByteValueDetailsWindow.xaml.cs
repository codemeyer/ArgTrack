using System.Windows;

namespace ArgTrack.Windows.Track
{
    public partial class TrackGenericByteValueDetailsWindow : Window
    {
        public TrackGenericByteValueDetailsWindow()
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
