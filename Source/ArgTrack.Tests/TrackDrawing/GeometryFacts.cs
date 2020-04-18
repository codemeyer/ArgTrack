using System.Linq;
using System.Windows;
using ArgTrack.Features.TrackDrawing;
using FluentAssertions;
using Xunit;

namespace ArgTrack.Tests.TrackDrawing
{
    public class GeometryFacts
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


        [Fact]
        public void GetRadiusForCurvature()
        {
            int curvature = 2731;

            double radius = Geometry.GetRadiusForCurvature(curvature);

            radius.Should().BeApproximately(3.8192524, Tolerance);
        }


        [Fact]
        public void GetLegacyCircleCenter_Phoenix_Turn1_RightHand_Part1()
        {
            var position = new Point(0, 126);
            double radius = 3.8192524168694444;
            double angle = 0;

            var circleCenter = Geometry.GetLegacyCircleCenter(position, radius, angle);

            circleCenter.X.Should().BeApproximately(3.81925, Tolerance);
            circleCenter.Y.Should().BeApproximately(126, Tolerance);
        }

        [Fact]
        public void GetLegacyCircleCenter_Phoenix_Turn1_RightHand_Part2()
        {
            var position = new Point(0.51180, 127.9098);
            double radius = 3.8192524168694444;
            double angle = -0.52366269146446076;   // legacy east

            var circleCenter = Geometry.GetLegacyCircleCenter(position, radius, angle);

            circleCenter.X.Should().BeApproximately(3.81925, Tolerance);
            circleCenter.Y.Should().BeApproximately(126, Tolerance);
        }

        [Fact]
        public void GetLegacyCircleCenter_Phoenix_Turn2_LeftHand_Part1()
        {
            var position = new Point(41.819984050768213, 129.81196593796017);
            double radius = -3.8192524168694444;
            double angle = -1.5709880743933822;   // legacy east

            var circleCenter = Geometry.GetLegacyCircleCenter(position, radius, angle);

            circleCenter.X.Should().BeApproximately(41.82071, Tolerance);
            circleCenter.Y.Should().BeApproximately(133.6312, Tolerance);
        }



        [Fact]
        public void GetLegacyCurveEndpoint()
        {
            var circleCenterPosition = new Point(3.8192524168694368, 126);
            var radius = 3.8192524168694444;
            var endAngle = -0.52366269146446076;

            var endPosition = Geometry.GetLegacyCurveEndpoint(circleCenterPosition, radius, endAngle);

            endPosition.X.Should().BeApproximately(0.51180486, Tolerance);
            endPosition.Y.Should().BeApproximately(127.909837, Tolerance);
        }



        [Fact]
        public void BuildArc_90DegRight_CreatesKnownNumberOfPoints()
        {
            var points = Geometry.BuildArc(0, 0, 0, 90, 1, 2);

            points[0].X.Should().Be(1);
            points[0].Y.Should().Be(0);
            points.Last().X.Should().BeInRange(-0.897, -0.896);
            points.Last().Y.Should().BeInRange(-1.788, -1.787);
            points.Count.Should().Be(22);
        }

        [Fact]
        public void BuildArc_90DegRight_CreatesKnownNumberOfPoint2s()
        {
            var points = Geometry.BuildArc(0, 0, 0, 90, 2, 2);

            points[0].X.Should().Be(2, "the radius is 2 and the start point is 0");
            points[0].Y.Should().Be(0);
            points.Last().X.Should().BeInRange(-0.897, -0.896);
            points.Last().Y.Should().BeInRange(-1.788, -1.787);
            points.Count.Should().Be(22);
        }
    }
}
