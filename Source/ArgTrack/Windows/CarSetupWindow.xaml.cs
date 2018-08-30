using System.Windows;

namespace ArgTrack.Windows
{
    /// <summary>
    /// Interaction logic for CarSetupWindow.xaml
    /// </summary>
    public partial class CarSetupWindow : Window
    {
        public CarSetupWindow()
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
