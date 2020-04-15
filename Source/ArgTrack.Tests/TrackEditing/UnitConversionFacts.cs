using ArgTrack.Features.TrackEditing;
using FluentAssertions;
using Xunit;

namespace ArgTrack.Tests.TrackEditing
{
    public class UnitConversionFacts
    {
        [Fact]
        public void TrackLengthUnit_1_Is_4p9_Feet()
        {
            var meters = UnitConvert.TrackLengthToMeters(1);

            meters.Should().Be(4.9);
        }

        [Fact]
        public void TrackLengthToMetersDescription_Unit_1_Is_4p9_Feet()
        {
            var meters = UnitConvert.TrackLengthToMetersDescription(1);

            meters.Should().Be("~4.9 meters");
        }

        [Fact]
        public void TrackWidthToMeters()
        {
            var meters = UnitConvert.TrackWidthToMeters(210);

            meters.Should().Be(2.0);
        }

        [Fact]
        public void TrackWidthToMetersDescription()
        {
            var meters = UnitConvert.TrackWidthToMetersDescription(210);

            meters.Should().Be("~2.0 meters");
        }
    }
}
