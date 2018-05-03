using System.Collections.ObjectModel;
using System.Windows;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class AddTrackSectionCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            //var vm = parameter as TrackViewModel;
            var vm = parameter as ObservableCollection<TrackSectionViewModel>;

            if (vm == null)
                return;

            var newSection = new TrackSectionViewModel();

            var window = new TrackSectionDetailsWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = newSection
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                vm.Add(newSection);
            }
        }
    }
}
