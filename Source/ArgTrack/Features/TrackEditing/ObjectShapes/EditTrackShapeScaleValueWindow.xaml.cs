using System.Windows;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public partial class EditTrackShapeScaleValueWindow : Window
    {
        public EditTrackShapeScaleValueWindow()
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
