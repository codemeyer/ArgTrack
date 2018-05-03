using System.Collections.ObjectModel;
using System.Windows;
using ArgData.Entities;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class AddBestLineSegmentCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as ObservableCollection<BestLineSegmentViewModel>;

            if (vm == null)
                return;

            var newSection = new BestLineSegmentViewModel();
            newSection.CanEditType = true;
            newSection.SegmentType = TrackBestLineSegmentType.Normal;

            var window = new TrackBestLineDetailsWindow
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
