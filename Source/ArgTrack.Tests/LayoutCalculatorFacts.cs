using System.Linq;
using ArgTrack.TrackStuff;
using FluentAssertions;
using Xunit;

namespace ArgTrack.Tests
{
    public class LayoutCalculatorFacts
    {
        [Fact]
        public void BuildArc_90DegRight_CreatesKnownNumberOfPoints()
        {
            var points = LayoutCalculator.BuildArc(0, 0, 0, 90, 1, 2);  // adioius?

            points[0].X.Should().Be(1);
            points[0].Y.Should().Be(0);
            points.Last().X.Should().BeInRange(-0.897, -0.896);
            points.Last().Y.Should().BeInRange(-1.788, -1.787);
            points.Count.Should().Be(22);
        }

        [Fact]
        public void BuildArc_90DegRight_CreatesKnownNumberOfPoint2s()
        {
            var points = LayoutCalculator.BuildArc(0, 0, 0, 90, 2, 2);  // adioius?

            points[0].X.Should().Be(2, "the radius is 2 and the start point is 0");
            points[0].Y.Should().Be(0);
            points.Last().X.Should().BeInRange(-0.897, -0.896);
            points.Last().Y.Should().BeInRange(-1.788, -1.787);
            points.Count.Should().Be(22);
        }

        //[Fact]
        //public void BuildArc_90DegLeft_CreatesKnownNumberOfPoints()
        //{
        //    var points = LayoutCalculator.BuildArc(0, 0, 0, -90, 1, 2);  // adioius?

        //    points[0].X.Should().Be(1);
        //    points[0].Y.Should().Be(0);
        //    points.Last().X.Should().BeInRange(-0.897, -0.896);
        //    points.Last().Y.Should().BeInRange(1.787, 1.788);
        //    points.Count.Should().Be(22);
        //}
    }
}
