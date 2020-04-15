using System.Windows;

namespace ArgTrack.Features.TrackEditing.ComputerCarBehavior
{
    /// <summary>
    /// Interaction logic for TrackHeaderWindow.xaml
    /// </summary>
    public partial class ComputerCarBehaviorWindow : Window
    {
        public ComputerCarBehaviorWindow()
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
