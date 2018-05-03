using System;
using System.IO;

namespace ArgTrack.Tests
{
    internal class ExampleDataContext : IDisposable
    {
        public string FilePath { get; }

        private ExampleDataContext(string exePath)
        {
            FilePath = exePath;
        }

        public static ExampleDataContext GetTempFileName(string extFileName)
        {
            string filePath = Path.GetTempPath() + Path.GetRandomFileName() + extFileName;
            return new ExampleDataContext(filePath);
        }

        public void Dispose()
        {
            File.Delete(FilePath);
        }
    }

    internal static class ExampleDataHelper
    {
        private static string GetCopyOfExampleData(string fileName, TestDataFileType dataFileType)
        {
            string originalLocation = GetExampleDataPath(fileName, dataFileType);
            string tempFile = GetTempFileName(fileName);
            File.Copy(originalLocation, tempFile);

            return tempFile;
        }

        internal static string GetExampleDataPath(string fileName, TestDataFileType dataFileType)
        {
            return Path.Combine(GetExampleDataBaseFolder() + dataFileType, fileName);
        }

        internal static string GetExampleDataBaseFolder()
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory;

            return Path.Combine(directory, @"..\..\..\..\TestData\");
        }

        internal static string GetTempFileName(string extFileName)
        {
            return Path.GetTempPath() + Path.GetRandomFileName() + extFileName;
        }

        internal static byte[] ReadBytes(string path, int position, int count)
        {
            using (var br = new BinaryReader(new FileStream(path, FileMode.Open)))
            {
                br.BaseStream.Position = position;
                byte[] bytes = br.ReadBytes(count);

                return bytes;
            }
        }
    }

    internal enum TestDataFileType
    {
        Exe,
        Names,
        Prefs,
        Saves,
        Tracks,
        Setups
    }
}
