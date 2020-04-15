using System.Windows;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
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
