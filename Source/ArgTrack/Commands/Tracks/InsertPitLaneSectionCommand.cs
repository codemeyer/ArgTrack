using System.Windows;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Commands.Tracks
{
    public class InsertPitLaneSectionCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackViewModel;

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
                vm.PitLaneSections.Insert(vm.SelectedPitLaneSectionIndex, newSection);
                vm.RecalculatePitLaneSectionIndexz();
                vm.SelectedPitLaneSectionIndex--;
            }
        }
    }
}
