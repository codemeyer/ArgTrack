using System.Windows;

namespace ArgTrack.Features.TrackEditing.RacingLine
{
    /// <summary>
    /// Interaction logic for ComputerCarLineHeaderWindow.xaml
    /// </summary>
    public partial class ComputerCarLineHeaderWindow : Window
    {
        public ComputerCarLineHeaderWindow()
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
