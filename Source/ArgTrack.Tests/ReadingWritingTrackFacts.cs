using System.IO;
using ArgData;
using FluentAssertions;
using Xunit;

namespace ArgTrack.Tests
{
    public class ReadingWritingTrackFacts
    {
        [Fact]
        public void ReadMapWriteTrackIdentical()
        {
            var file = ExampleDataHelper.GetExampleDataPath("F1CT01.DAT", TestDataFileType.Tracks);
            var tempFile = ExampleDataHelper.GetTempFileName(".DAT");

            var originalTrack = new TrackReader().Read(file);
            var viewModel = Mappers.TrackMapper.MapEntireTrackToViewModel(originalTrack);
            var trackFromViewModel = Mappers.TrackMapper.MapEntireTrackViewModelToTrack(viewModel);
            new TrackWriter().Write(trackFromViewModel, tempFile);

            // compare original and new
            var originalBytes = File.ReadAllBytes(file);
            var newBytes = File.ReadAllBytes(tempFile);

            newBytes.Length.Should().Be(originalBytes.Length);
            originalBytes.Should().BeEquivalentTo(newBytes);

            // double-check :)
            for (int i = 0; i < originalBytes.Length; i++)
            {
                byte original = originalBytes[i];
                byte newByte = newBytes[i];

                newByte.Should().Be(original);
            }

            File.Delete(tempFile);
        }
    }
}
