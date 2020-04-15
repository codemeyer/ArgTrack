using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    /// <summary>
    /// Interaction logic for ObjectShapesListWindow.xaml
    /// </summary>
    public partial class ObjectShapesListWindow : Window
    {
        public ObjectShapesListWindow()
        {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // until I figure out how to do it with MouseBinding in XAML...

            var listView = sender as ListView;

            var selectedItem = listView?.SelectedValue;

            var ts = selectedItem as TrackObjectShapeViewModel;

            if (ts == null)
                return;

            var command = new EditTrackObjectShapeCommand();
            command.Execute(ts);
        }
    }
}
