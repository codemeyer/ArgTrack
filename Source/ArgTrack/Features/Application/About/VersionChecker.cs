using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ArgTrack.Features.Application.About
{
    public class VersionChecker
    {
        public string GetLatestVersion()
        {
            var webClient = new WebClient();
            try
            {
                byte[] latestVersionData = webClient.DownloadData("https://www.argtools.com/downloads/argtrack/latest-version.json");
                string latestVersionText = Encoding.UTF8.GetString(latestVersionData);

                using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(latestVersionText)))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LatestVersionClass));
                    var latestVersion = (LatestVersionClass)serializer.ReadObject(ms);

                    return latestVersion.LatestVersion;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public bool Compare(string thisVersion, string latestVersion)
        {
            string[] thisParts = thisVersion.Split('.');
            long thisNumber = Convert.ToInt64(VersionNumber(thisParts));

            string[] latestParts = latestVersion.Split('.');
            long latestNumber = Convert.ToInt64(VersionNumber(latestParts));

            return latestNumber > thisNumber;
        }

        private string VersionNumber(string[] v)
        {
            return v[0] + v[1].PadLeft(5, '0') + v[2].PadLeft(5, '0') + v[3].PadLeft(5, '0');
        }
    }

    [DataContract]
    public class LatestVersionClass
    {
        [DataMember(Name="latest-version")]
        public string LatestVersion { get; set; }
    }
}
