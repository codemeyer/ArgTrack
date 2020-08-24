using System.Windows;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    /// <summary>
    /// Interaction logic for EditTrackObjectShapePointWindow.xaml
    /// </summary>
    public partial class EditTrackObjectShapeVectorWindow : Window
    {
        public EditTrackObjectShapeVectorWindow()
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
