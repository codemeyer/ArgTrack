using Microsoft.Win32;

namespace ArgTrack
{
    public interface IFileDialog
    {
        string SelectDatFileToOpen();
        string SelectDatFileToSave();
        string SelectHorizonFileToSave();
        string SelectAnyFileToOpen();
        string SelectAnyFileToSave();
    }

    public class FileDialog : IFileDialog
    {
        public string SelectDatFileToOpen()
        {
            var fileDialog = new OpenFileDialog
            {
                Title = "Select DAT File",
                DefaultExt = ".dat",
                Filter = "DAT files (*.dat)|.dat|All files (*.*)|*.*",
                FileName = "*.dat",
                CheckFileExists = true
            };

            return GetFilePathFromDialog(fileDialog);
        }

        public string SelectDatFileToSave()
        {
            var fileDialog = new SaveFileDialog
            {
                Title = "Select DAT File",
                DefaultExt = ".dat",
                Filter = "DAT files (*.dat)|.dat|All files (*.*)|*.*",
                FileName = "*.dat",
                AddExtension = false
            };

            return GetFilePathFromDialog(fileDialog);
        }

        public string SelectHorizonFileToSave()
        {
            var fileDialog = new SaveFileDialog
            {
                Title = "Select Horizon File",
                DefaultExt = ".arghoriz",
                Filter = "ArgHoriz files (*.arghoriz)|.arghoriz|All files (*.*)|*.*",
                FileName = "*.arghoriz",
                AddExtension = true
            };

            return GetFilePathFromDialog(fileDialog);
        }

        public string SelectAnyFileToOpen()
        {
            var fileDialog = new OpenFileDialog
            {
                Title = "Select File",
                DefaultExt = ".*",
                Filter = "All files (*.*)|*.*",
                FileName = "*.*",
                CheckFileExists = true
            };

            return GetFilePathFromDialog(fileDialog);
        }

        public string SelectAnyFileToSave()
        {
            var fileDialog = new SaveFileDialog
            {
                Title = "Select File",
                DefaultExt = "",
                Filter = "All files (*.*)|*.*",
                FileName = "*",
                AddExtension = false
            };

            return GetFilePathFromDialog(fileDialog);
        }

        private static string GetFilePathFromDialog(OpenFileDialog fileDialog)
        {
            bool? result = fileDialog.ShowDialog();

            if (result == true)
            {
                return fileDialog.FileName;
            }

            return string.Empty;
        }

        private static string GetFilePathFromDialog(SaveFileDialog fileDialog)
        {
            bool? result = fileDialog.ShowDialog();

            if (result == true)
            {
                return fileDialog.FileName;
            }

            return string.Empty;
        }
    }
}
