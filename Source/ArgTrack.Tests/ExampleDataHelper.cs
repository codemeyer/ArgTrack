using System;
using System.IO;

namespace ArgTrack.Tests
{
    internal static class ExampleDataHelper
    {
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
    }

    internal enum TestDataFileType
    {
        Tracks,
    }
}
