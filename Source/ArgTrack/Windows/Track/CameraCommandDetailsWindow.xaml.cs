using System.Windows;

namespace ArgTrack.Windows
{
    /// <summary>
    /// Interaction logic for CameraCommandDetailsWindow.xaml
    /// </summary>
    public partial class CameraCommandDetailsWindow : Window
    {
        public CameraCommandDetailsWindow()
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
