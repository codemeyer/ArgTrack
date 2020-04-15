using System.Windows;

namespace ArgTrack.Features.TrackEditing.Horizon
{
    /// <summary>
    /// Interaction logic for HorizonWindow.xaml
    /// </summary>
    public partial class HorizonWindow : Window
    {
        public HorizonWindow()
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
