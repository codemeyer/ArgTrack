using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ArgTrack.Commands.Model.Cameras;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.Windows.Track
{
    /// <summary>
    /// Interaction logic for CameraCommandListWindow.xaml
    /// </summary>
    public partial class CameraCommandListWindow : Window
    {
        public CameraCommandListWindow()
        {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listView = sender as ListView;

            var selectedItem = listView?.SelectedValue;

            var ts = selectedItem as CameraCommandEditingViewModel;

            if (ts == null)
                return;

            var command = new EditCameraCommandCommand();
            command.Execute(ts);
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
