using ArgData;
using ArgTrack.ViewModels;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Tracks
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
            var vm = parameter as TrackTabViewModel;

            if (vm == null)
            {
                var main = parameter as MainViewModel;
                if (main == null)
                    return;

                vm = main.TrackTabViewModel;
            }

            var path = _fileDialog.SelectDatFileToSave();

            if (string.IsNullOrWhiteSpace(path))
                return;

            var track = Mappers.TrackMapper.MapEntireTrackViewModelToTrack(vm.Track);

            new TrackWriter().Write(track, path);
        }
    }
}
