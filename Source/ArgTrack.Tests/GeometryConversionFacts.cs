using ArgTrack.Features.TrackDrawing;
using FluentAssertions;
using Xunit;

namespace ArgTrack.Tests
{
    public class GeometryConversionFacts
    {
        private const double Tolerance = 0.001;

        [Theory]
        [InlineData(90.0, 1.5708)]
        [InlineData(45.0, 0.7854)]
        [InlineData(-45.0, -0.7854)]
        public void DegreesToRadians(double degrees, double expectedRadians)
        {
            double radians = GeometryConversions.DegreesToRadians(degrees);

            radians.Should().BeApproximately(expectedRadians, Tolerance);
        }

        [Theory]
        [InlineData(1.5708, 90.0)]
        public void RadiansToDegrees(double radians, double expectedDegrees)
        {
            double degrees = GeometryConversions.RadiansToDegrees(radians);

            degrees.Should().BeApproximately(expectedDegrees, Tolerance);
        }


        [Theory]
        [InlineData(0, 0)]
        [InlineData(16384, 1.5708)]
        public void RadiansFromGameAngle(int gameAngle, double expectedRadians)
        {
            double angle = GeometryConversions.RadiansFromGameAngle(gameAngle);

            angle.Should().BeApproximately(expectedRadians, Tolerance);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(16384, 90.0)]
        public void DegreesFromGameAngle(int gameAngle, double expectedRadians)
        {
            double angle = GeometryConversions.DegreesFromGameAngle(gameAngle);

            angle.Should().BeApproximately(expectedRadians, Tolerance);
        }
    }
}
