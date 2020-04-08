using System.IO;
using ArgTrack.AppStuff;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.Commands.Tracks
{
    public class SaveHorizonToFileCommand : CommandBase
    {
        private readonly IFileDialog _fileDialog;

        public SaveHorizonToFileCommand(IFileDialog fileDialog)
        {
            _fileDialog = fileDialog;
        }

        public override void Execute(object parameter)
        {
            var vm = parameter as HorizonEditingViewModel;

            if (vm == null)
                return;

            var path = _fileDialog.SelectHorizonFileToSave();

            if (string.IsNullOrWhiteSpace(path))
                return;

            File.WriteAllBytes(path, vm.HorizonBytes);
        }
    }
}
