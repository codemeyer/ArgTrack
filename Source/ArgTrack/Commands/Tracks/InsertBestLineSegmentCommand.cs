using System.Windows;
using ArgData.Entities;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class InsertBestLineSegmentCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            //var vm = parameter as ObservableCollection<BestLineSegmentViewModel>;
            var vm = parameter as TrackViewModel;

            if (vm == null)
                return;

            var newSection = new BestLineSegmentViewModel(vm.BestLineSegments);
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
                vm.BestLineSegments.Insert(vm.SelectedBestLineSectionIndex, newSection);
                vm.RecalculateBestLineSectionIndexz();
            }
        }
    }
}
