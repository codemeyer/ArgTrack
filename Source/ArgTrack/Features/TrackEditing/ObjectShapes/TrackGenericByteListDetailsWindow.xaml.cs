using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public partial class TrackGenericByteListDetailsWindow : Window
    {
        public TrackGenericByteListDetailsWindow()
        {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listView = sender as ListView;

            var selectedValue = listView?.SelectedValue;

            var byteData = selectedValue as EditableByteData;

            if (byteData == null)
                return;

            var command = new EditSingleByteValueCommand();
            command.Execute(byteData);
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
