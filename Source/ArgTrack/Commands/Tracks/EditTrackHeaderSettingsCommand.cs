using System.Windows;
using ArgTrack.Mappers;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class EditTrackHeaderSettingsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackHeaderViewModel;

            if (vm == null)
                return;

            var clone = new TrackHeaderViewModel();
            ViewModelCopyMapper.CopyTrackHeaderViewModel(vm, clone);

            var window = new TrackHeaderWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                // map clone to actual value
                ViewModelCopyMapper.CopyTrackHeaderViewModel(clone, vm);

                // TODO: repaint 3D track! ie LayoutCalculator etc
            }
        }
    }
}
