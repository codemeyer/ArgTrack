using System.Windows;

namespace ArgTrack.Features.TrackEditing.ObjectSettings
{
    public partial class TrackObjectSettingsDetailsWindow : Window
    {
        public TrackObjectSettingsDetailsWindow()
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
