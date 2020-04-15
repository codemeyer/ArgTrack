using ArgData;
using ArgTrack.Features.Main;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.Persistence
{
    public class SaveTrackCommand : CommandBase
    {
        private readonly IFileDialog _fileDialog;

        public SaveTrackCommand(IFileDialog fileDialog)
        {
            _fileDialog = fileDialog;
        }

        public override void Execute(object parameter)
        {
            // TODO: MainViewModel directly?
            var main = parameter as MainViewModel;
            if (main == null)
                return;

            var vm = main.TrackViewModel;

            var path = _fileDialog.SelectDatFileToSave();

            if (string.IsNullOrWhiteSpace(path))
                return;

            var track = TrackMapper.MapEntireTrackViewModelToTrack(vm);

            new TrackWriter().Write(track, path);
        }

        public override bool CanExecute(object parameter)
        {
            var main = parameter as MainViewModel;
            if (main == null)
            {
                return false;
            }

            return main.HasLoadedTrack;
        }
    }
}
