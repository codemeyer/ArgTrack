using System.Windows;

namespace ArgTrack.Features.TrackEditing.WorldLocation
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
