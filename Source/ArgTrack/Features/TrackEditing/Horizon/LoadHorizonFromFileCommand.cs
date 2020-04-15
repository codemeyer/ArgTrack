using System;
using System.IO;
using ArgTrack.Features.Persistence;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.Horizon
{
    public class LoadHorizonFromFileCommand : CommandBase
    {
        private readonly IFileDialog _fileDialog;

        public LoadHorizonFromFileCommand(IFileDialog fileDialog)
        {
            _fileDialog = fileDialog;
        }

        public override void Execute(object parameter)
        {
            var vm = parameter as HorizonEditingViewModel;

            if (vm == null)
                return;

            var path = _fileDialog.SelectAnyFileToOpen();

            if (string.IsNullOrWhiteSpace(path))
                return;

            var fileInfo = new FileInfo(path);

            if (fileInfo.Length < 4096)
            {
                // TODO: display message to user
                return;
            }

            var bytes = File.ReadAllBytes(path);
            var hzbytes = new byte[4096];

            Array.Copy(bytes, 0, hzbytes, 0, 4096);

            vm.HorizonBytes = bytes;
        }
    }
}
