using System.Windows;
using ArgTrack.Features.TrackDrawing;
using FluentAssertions;
using Xunit;

namespace ArgTrack.Tests.TrackDrawing
{
    public class TrackDrawingFactConstants
    {
        public const double Tolerance = 0.001;
    }

    public class GeometryPointFacts
    {
        //   Y increases
        //       N
        //       ^
        //       |
        //       |
        //  ------------>  E   X increases
        //       |
        //       |
        //       |

        private const double Tolerance = 0.001;

        [Fact]
        public void GetLegacyPoint()
        {
            var startPosition = new Point(3.8199847493439014, 129.81925234665795);
            var angle = -1.5709880743933822;   // so negative is "east"...
            var distance = 38;

            var point = Geometry.GetLegacyPoint(startPosition, angle, distance);

            point.Y.Should().BeApproximately(129.81196593796014, Tolerance);
            point.X.Should().BeApproximately(41.81998, Tolerance);
        }

        // TODO: more legacy tests?


        [Fact]
        public void GetPoint_NorthFacing_Source_Move_Increases_Y()
        {
            var startPosition = new Point(0, 0);
            var angle = 0;  // north
            var distance = 10;

            var newPoint = Geometry.GetPoint(startPosition, angle, distance);

            newPoint.X.Should().BeApproximately(0.0, Tolerance);
            newPoint.Y.Should().BeApproximately(10.0, Tolerance);
        }

        [Fact]
        public void GetPoint_SouthFacing_Source_Move_Decreases_Y()
        {
            var startPosition = new Point(0, 0);
            var angle = GeometryConversions.DegreesToRadians(180);  // south
            var distance = 10;

            var newPoint = Geometry.GetPoint(startPosition, angle, distance);

            newPoint.X.Should().BeApproximately(0.0, Tolerance);
            newPoint.Y.Should().BeApproximately(-10.0, Tolerance);
        }

        [Fact]
        public void GetPoint_EastFacing_Source_Move_Increases_X()
        {
            var startPosition = new Point(0, 0);
            var angle = GeometryConversions.DegreesToRadians(90);  // east
            var distance = 10;

            var newPoint = Geometry.GetPoint(startPosition, angle, distance);

            newPoint.X.Should().BeApproximately(10.0, Tolerance);
            newPoint.Y.Should().BeApproximately(0.0, Tolerance);
        }

        [Fact]
        public void GetPoint_WestFacing_Source_Move_Increases_X()
        {
            var startPosition = new Point(0, 0);
            var angle = GeometryConversions.DegreesToRadians(270);  // west
            var distance = 10;

            var newPoint = Geometry.GetPoint(startPosition, angle, distance);

            newPoint.X.Should().BeApproximately(-10.0, Tolerance);
            newPoint.Y.Should().BeApproximately(0.0, Tolerance);
        }


        [Fact]
        public void GetPointPerpendicularLegacy_EastFacing_Source_Move_Huh_Y()
        {
            var startPosition = new Point(0, 0);
            var angle = -1.5708;  // legacy way, negative is East
            var distance = 10;  // right

            var newPoint = Geometry.GetLegacyPointPerpendicular(startPosition, angle, distance);

            newPoint.X.Should().BeApproximately(0.0, Tolerance);
            newPoint.Y.Should().BeApproximately(-10.0, Tolerance);
        }

        [Fact]
        public void GetPointPerpendicularLegacy_NorthFacing_Source_Move_Huh_Y()
        {
            var startPosition = new Point(0, 0);
            var angle = 0;
            var distance = 10;  // right

            var newPoint = Geometry.GetLegacyPointPerpendicular(startPosition, angle, distance);

            newPoint.X.Should().BeApproximately(10.0, Tolerance);
            newPoint.Y.Should().BeApproximately(0.0, Tolerance);
        }


        [Fact]
        public void GetPointPerpendicular_NorthFacing_Source_Move_Increases_X()
        {
            var startPosition = new Point(0, 0);
            var angle = 0; // north
            var distance = 10;  // right

            var newPoint = Geometry.GetPointPerpendicular(startPosition, angle, distance);

            newPoint.X.Should().BeApproximately(10.0, Tolerance);
            newPoint.Y.Should().BeApproximately(0.0, Tolerance);
        }

        [Fact]
        public void GetPointPerpendicular_SouthFacing_Source_Move_Increases_X()
        {
            var startPosition = new Point(0, 0);
            var angle = GeometryConversions.DegreesToRadians(180);  // south
            var distance = 10;  // right

            var newPoint = Geometry.GetPointPerpendicular(startPosition, angle, distance);

            newPoint.X.Should().BeApproximately(-10.0, Tolerance);
            newPoint.Y.Should().BeApproximately(0.0, Tolerance);
        }

        [Fact]
        public void GetPointPerpendicular_EastFacing_Source_Move_Huh_Y()
        {
            var startPosition = new Point(0, 0);
            var angle = GeometryConversions.DegreesToRadians(90);
            var distance = 10;  // right

            var newPoint = Geometry.GetPointPerpendicular(startPosition, angle, distance);

            newPoint.X.Should().BeApproximately(0.0, Tolerance);
            newPoint.Y.Should().BeApproximately(-10.0, Tolerance);
        }

        [Fact]
        public void GetPointPerpendicular_WestFacing_Source_Move_Huh_Y()
        {
            var startPosition = new Point(0, 0);
            var angle = GeometryConversions.DegreesToRadians(270);  // west
            var distance = 10;  // right

            var newPoint = Geometry.GetPointPerpendicular(startPosition, angle, distance);

            newPoint.X.Should().BeApproximately(0.0, Tolerance);
            newPoint.Y.Should().BeApproximately(10.0, Tolerance);
        }

        [Fact]
        public void GetPointPerpendicular_NorthEastFacing_Source_Move_Increases_X_And_Decreases_Y()
        {
            var startPosition = new Point(0, 0);
            var angle = GeometryConversions.DegreesToRadians(45);  // N-E
            var distance = 10;

            var newPoint = Geometry.GetPointPerpendicular(startPosition, angle, distance);

            newPoint.X.Should().BeApproximately(7.071, Tolerance);
            newPoint.Y.Should().BeApproximately(-7.071, Tolerance);
        }


    }
}
