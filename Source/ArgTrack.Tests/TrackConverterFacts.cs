using ArgTrack.TrackStuff;

namespace ArgTrack.Tests
{
    public class TrackConverterFacts
    {
        //[Fact]
        public void PerformTempConversion()
        {
            //var gp2Track = TrackReaderGp2.ReadGp2(@"C:\Temp\trackout\tracks\GP2\F1CT06.DAT");
            //var templateSourceTrack = TrackReader.Read(@"C:\Temp\trackout\tracks\F1GP\F1CT06.DAT");
            var gp2Track = Arg2Data.TrackReader.Read(@"C:\Manicomio\Solutions\ArgTrack\TestData\tracks\GP2-F1CT05.DAT");
            var templateSourceTrack = new ArgData.TrackReader().Read(@"C:\Manicomio\Solutions\ArgTrack\TestData\tracks\F1CT14.DAT");
            var outputPath = "c:\\Games\\gprix\\F1CT01.DAT";

            TrackConverter.ConvertGP2ToF1GP(gp2Track, templateSourceTrack, outputPath);

            // TODO: check what?
        }
    }
}
