using System.Windows;

namespace ArgTrack.Features.TrackEditing.CarSettings
{
    /// <summary>
    /// Interaction logic for CarSettingsWindow.xaml
    /// </summary>
    public partial class CarSettingsWindow : Window
    {
        public CarSettingsWindow()
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
