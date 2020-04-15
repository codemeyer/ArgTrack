using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.TrackSection
{
    public class EditTrackSectionCommandCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as TrackSectionCommandViewModel;

            if (vm == null)
                return;

            var clone = new TrackSectionCommandViewModel();
            TrackSectionMapper.MapTrackCommandViewModel(vm, clone);

            var window = new TrackCommandDetailsWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                TrackSectionMapper.MapTrackCommandViewModel(clone, vm);
            }
        }
    }
}
