using System.Windows;
using ArgTrack.Mappers;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class EditTrackLayoutSettingsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            //var vm = parameter as TrackHeaderViewModel;
            var vm = parameter as TrackSettingsViewModel;

            if (vm == null)
                return;

            var clone = new TrackSettingsViewModel();
            TrackMapper.MapTrackSettingsViewModel(vm, clone);

            var window = new TrackLayoutWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = clone
                // TODO: also pass in original, to allow reseting all properties
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                // map clone to actual value
                //TrackMapper.MapTrackHeaderViewModel(clone, vm);
                TrackMapper.MapTrackSettingsViewModel(clone, vm);

                // TODO: repaint 3D track! ie LayoutCalculator etc
            }
        }
    }
}
