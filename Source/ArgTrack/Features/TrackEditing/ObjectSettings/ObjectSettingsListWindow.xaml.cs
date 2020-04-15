using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArgTrack.Features.TrackEditing.ObjectSettings
{
    /// <summary>
    /// Interaction logic for ObjectSettingsListWindow.xaml
    /// </summary>
    public partial class ObjectSettingsListWindow : Window
    {
        public ObjectSettingsListWindow()
        {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // until I figure out how to do it with MouseBinding in XAML...

            var listView = sender as ListView;

            var selectedItem = listView?.SelectedValue;

            var ts = selectedItem as TrackObjectSettingsViewModel;

            if (ts == null)
                return;

            var command = new EditTrackObjectSettingsCommand();
            command.Execute(ts);
        }
    }
}
