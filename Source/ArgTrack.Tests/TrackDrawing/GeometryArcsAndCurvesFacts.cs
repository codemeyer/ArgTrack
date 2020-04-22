using System.Windows;
using ArgTrack.Features.TrackDrawing;
using FluentAssertions;
using Xunit;

namespace ArgTrack.Tests.TrackDrawing
{
    public class GeometryArcsAndCurvesFacts
    {
        // TODO: fix tests using Geometry.BuildArc

        private const double Tolerance = 0.001;

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



        //[Fact]
        //public void BuildArc_90DegRight_CreatesKnownNumberOfPoints()
        //{
        //    var points = Geometry.BuildArc(new Point(0, 0), 0, 90, 1, 2);

        //    points[0].X.Should().Be(1);
        //    points[0].Y.Should().Be(0);
        //    points.Last().X.Should().BeInRange(-0.897, -0.896);
        //    points.Last().Y.Should().BeInRange(-1.788, -1.787);
        //    points.Count.Should().Be(22);
        //}

        //[Fact]
        //public void BuildArc_90DegRight_CreatesKnownNumberOfPoint2s()
        //{
        //    var points = Geometry.BuildArc(new Point(0, 0), 0, 90, 2, 2);

        //    points[0].X.Should().Be(2, "the radius is 2 and the start point is 0");
        //    points[0].Y.Should().Be(0);
        //    points.Last().X.Should().BeInRange(-0.897, -0.896);
        //    points.Last().Y.Should().BeInRange(-1.788, -1.787);
        //    points.Count.Should().Be(22);
        //}

        //[Fact]
        //public void BuildArc_Interlagos_Left_TrackWidth_Change_Outer()
        //{
        //    var circleCenter = new Point(-95.590916505681676, -112.47272908855938);
        //    double startAngle = -0.86832899974251543;
        //    double endAngle = -1.5844104062873816;
        //    double outRadStart = -14.111381959455747;
        //    double outRadEnd = -13.611381959455747;

        //    // TODO: legacy, have to send params in wrong order for left hand corners
        //    var points = Geometry.BuildArc(circleCenter, endAngle, startAngle, outRadEnd, outRadStart);

        //    points.Count.Should().Be(9);
        //    points[0].X.Should().BeApproximately(-95.40561, Tolerance);
        //    points[0].Y.Should().BeApproximately(-126.08284, Tolerance);
        //    points[8].X.Should().BeApproximately(-104.70832, Tolerance);
        //    points[8].Y.Should().BeApproximately(-123.24324, Tolerance);
        //}

        [Fact]
        public void BuildArcLeftInner()
        {
            var circleCenter = new Point(-95.590916505681676, -112.47272908855938);
            int startAngle = 41825;
            int endAngle = 49294;
            double radius = -15.361381959455747;

            // legacy, have to send params in wrong order for left hand corners
            var points = Geometry.BuildTrackArcLeftInner(circleCenter, startAngle, endAngle, radius, 1280, 1792);

            points.Count.Should().Be(9);
            points[0].X.Should().BeApproximately(-104.70832, Tolerance);
            points[0].Y.Should().BeApproximately(-123.24324, Tolerance);
            points[8].X.Should().BeApproximately(-95.40561, Tolerance);
            points[8].Y.Should().BeApproximately(-126.08284, Tolerance);
        }


        //[Fact]
        //public void BuildArc_Interlagos_Left_TrackWidth_Change_Inner()
        //{
        //    var circleCenter = new Point(-95.590916505681676, -112.47272908855938);
        //    double startAngle = -0.86832899974251543;
        //    double endAngle = -1.5844104062873816;
        //    double innerRadStart = -16.611381959455748;
        //    double innerRadEnd = -17.111381959455748;

        //    // legacy, have to send params in wrong order for left hand corners
        //    var points = Geometry.BuildArc(circleCenter, endAngle, startAngle, innerRadEnd, innerRadStart);

        //    points.Count.Should().Be(9);
        //    points[0].X.Should().BeApproximately(-95.35796, Tolerance);
        //    points[0].Y.Should().BeApproximately(-129.58252, Tolerance);
        //    points[8].X.Should().BeApproximately(-106.32357, Tolerance);
        //    points[8].Y.Should().BeApproximately(-125.15137, Tolerance);
        //}

        [Fact]
        public void BuildArcLeftOuter()
        {
            var circleCenter = new Point(-95.590916505681676, -112.47272908855938);
            int startAngle = 41825;
            int endAngle = 49294;
            double radius = -15.361381959455747;

            var points = Geometry.BuildTrackArcLeftOuter(circleCenter, startAngle, endAngle, radius, 1280, 1792);

            points.Count.Should().Be(9);
            points[0].X.Should().BeApproximately(-106.32357, Tolerance);
            points[0].Y.Should().BeApproximately(-125.15137, Tolerance);
            points[8].X.Should().BeApproximately(-95.35796, Tolerance);
            points[8].Y.Should().BeApproximately(-129.58252, Tolerance);
        }
    }
}
