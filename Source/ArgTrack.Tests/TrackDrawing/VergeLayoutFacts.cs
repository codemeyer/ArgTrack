using System.Collections.Generic;
using ArgTrack.Features.Main;
using ArgTrack.Features.TrackDrawing;
using ArgTrack.Features.TrackEditing.TrackSection;
using FluentAssertions;
using Xunit;

namespace ArgTrack.Tests.TrackDrawing
{
    public class VergeLayoutFacts
    {
        // PHO first part
        // straight-turn-turn-turn-straight
        // if turns are joined,
        // verge points go from start of first turn to start of straight

        // test the various permutations
        // - no join, should have five VergeLayouts
        // - join first two, should be four VL
        // - join all, should be three VL (should move CenterPoint?)

        // NB: IT LOOKS like bridged sections always have straight verges(?) yes...

        [Fact]
        public void PhoenixFirstFive_NoJoining_Returns_Five_Right_VergeSections()
        {
            var track = GetPhoenixTrackStart();

            var verges = GetRightVerges(track);

            verges.Count.Should().Be(5);
        }

        [Fact]
        public void PhoenixFirstFive_NoJoining_Returns_Five_Left_VergeSections()
        {
            var track = GetPhoenixTrackStart();

            var verges = GetLeftVerges(track);

            verges.Count.Should().Be(5);
        }

        [Fact]
        public void PhoenixFive_Right()
        {
            var track = GetPhoenixTrackStart();

            var verges = GetRightVerges(track);

            verges[0].StartPosition.X.Should().Be(0.0);
            verges[0].StartPosition.Y.Should().Be(0.0);

            verges[0].OuterStartPosition.X.Should().BeApproximately(1.62, TrackDrawingFactConstants.Tolerance);
            verges[0].OuterStartPosition.Y.Should().BeApproximately(0.0, TrackDrawingFactConstants.Tolerance);

            verges[0].OuterEndPosition.X.Should().BeApproximately(1.62, TrackDrawingFactConstants.Tolerance);
            verges[0].OuterEndPosition.Y.Should().BeApproximately(126.0, TrackDrawingFactConstants.Tolerance);

            verges[1].OuterStartPosition.X.Should().BeApproximately(1.62, TrackDrawingFactConstants.Tolerance);
            verges[1].OuterStartPosition.Y.Should().BeApproximately(126.0, TrackDrawingFactConstants.Tolerance);
        }

        [Fact]
        public void PhoenixFive_Left()
        {
            var track = GetPhoenixTrackStart();

            var verges = GetLeftVerges(track);

            verges[0].StartPosition.X.Should().Be(0.0);
            verges[0].StartPosition.Y.Should().Be(0.0);

            verges[0].OuterStartPosition.X.Should().BeApproximately(-1.62, TrackDrawingFactConstants.Tolerance);
            verges[0].OuterStartPosition.Y.Should().BeApproximately(0.0, TrackDrawingFactConstants.Tolerance);

            verges[0].OuterEndPosition.X.Should().BeApproximately(-1.62, TrackDrawingFactConstants.Tolerance);
            verges[0].OuterEndPosition.Y.Should().BeApproximately(126.0, TrackDrawingFactConstants.Tolerance);

            verges[1].OuterStartPosition.X.Should().BeApproximately(-1.62, TrackDrawingFactConstants.Tolerance);
            verges[1].OuterStartPosition.Y.Should().BeApproximately(126.0, TrackDrawingFactConstants.Tolerance);
        }


        [Fact]
        public void PhoenixFirstFive_JoinCornerSections_Returns_Three_VergeSections()
        {
            var track = GetPhoenixTrackStart();
            track.TrackSections[1].BridgedRightFence = true;
            track.TrackSections[2].BridgedRightFence = true;
            track.TrackSections[3].BridgedRightFence = true;

            var verges = GetRightVergesOneToOne(track);

            verges.Count.Should().Be(5, "because one-to-one does not merge bridged items");

            // TODO: also check verges[1] startpoint and endpoint (or as a separate test)
        }


        [Fact]
        public void PhoenixFirstFive_BlaBao()
        {
            var track = GetPhoenixTrackStart();
            track.TrackSections[1].BridgedRightFence = true;
            track.TrackSections[2].BridgedRightFence = true;
            track.TrackSections[3].BridgedRightFence = true;

            var verges = GetRightVergesOneToOne(track);

            var verges2 = VergeLayoutMerger.MergeBridgedSections(verges, VergeSide.Right);

            verges2.Count.Should().Be(3);

            // TODO: also check verges[1] startpoint and endpoint (or as a separate test)
        }

        [Fact]
        public void PhoenixFirstFive_BlaBao2()
        {
            var track = GetPhoenixTrackStart();
            track.TrackSections[1].BridgedRightFence = true;
            track.TrackSections[2].BridgedRightFence = true;
            track.TrackSections[3].BridgedRightFence = true;

            var verges = GetRightVergesOneToOne(track);

            var verges2 = VergeLayoutMerger.MergeBridgedSections(verges, VergeSide.Right);

            verges[0].OuterEndPosition.X.Should().Be(verges2[0].OuterEndPosition.X);
            verges[0].OuterEndPosition.Y.Should().Be(verges2[0].OuterEndPosition.Y);

            // the two last verge sections should be ident
            verges[4].OuterEndPosition.X.Should().Be(verges2[2].OuterEndPosition.X);
            verges[4].OuterEndPosition.Y.Should().Be(verges2[2].OuterEndPosition.Y);
            verges[4].OuterStartPosition.X.Should().Be(verges2[2].OuterStartPosition.X);
            verges[4].OuterStartPosition.Y.Should().Be(verges2[2].OuterStartPosition.Y);

            verges2[0].DrawArc.Should().BeFalse();
            verges2[1].DrawArc.Should().BeFalse();
            verges2[2].DrawArc.Should().BeFalse();
            // TODO: also check verges[1] startpoint and endpoint (or as a separate test)
        }

        // TODO:

        [Fact]
        public void PhoenixFirstFive_JoinTwoFirstCorners_Returns_Four_VergeSections()
        {
            var track = GetPhoenixTrackStart();
            track.TrackSections[1].BridgedRightFence = true;
            track.TrackSections[2].BridgedRightFence = true;
            track.TrackSections[3].BridgedRightFence = false;

            var verges = GetRightVerges(track);

            verges.Count.Should().Be(4);
        }



        private List<VergeLayoutSectionData> GetLeftVerges(TrackViewModel track)
        {
            var centerPoints = CenterPointsCalculator.GetTrackLayoutSections(track);
            var road = RoadLayoutCalculator.CalculateRoadSections(centerPoints, track.TrackHeader.TrackStartWidth);

            return VergeLayoutCalculator.CalculateLeftVerges(road, track.TrackHeader.RightVergeStartWidth);
        }

        private List<VergeLayoutSectionData> GetRightVerges(TrackViewModel track)
        {
            var centerPoints = CenterPointsCalculator.GetTrackLayoutSections(track);
            var road = RoadLayoutCalculator.CalculateRoadSections(centerPoints, track.TrackHeader.TrackStartWidth);

            return VergeLayoutCalculator.CalculateRightVerges(road, track.TrackHeader.RightVergeStartWidth);
        }

        private List<VergeLayoutSectionData> GetRightVergesOneToOne(TrackViewModel track)
        {
            var centerPoints = CenterPointsCalculator.GetTrackLayoutSections(track);
            var road = RoadLayoutCalculator.CalculateRoadSections(centerPoints, track.TrackHeader.TrackStartWidth);

            return VergeLayoutCalculator.ToSectionListOneToOne(road, VergeSide.Right, track.TrackHeader.RightVergeStartWidth);
        }

        private TrackViewModel GetPhoenixTrackStart()
        {
            var track = new TrackViewModel();

            TrackViewModelPasserArounder.TrackViewModel = track;

            track.TrackHeader.TrackStartWidth = 1536;
            track.TrackHeader.LeftVergeStartWidth = 2;
            track.TrackHeader.RightVergeStartWidth = 2;

            var trasan = track.TrackSections;

            track.TrackSections.Add(new TrackSectionViewModel(trasan)
            {
                Length = 126,
                Curvature = 0,
                RightVergeWidth = 2,
                BridgedRightFence = false,
                LeftVergeWidth = 2,
                BridgedLeftFence = false
            });
            track.TrackSections.Add(new TrackSectionViewModel(trasan)
            {
                Length = 2,
                Curvature = 2731,
                RightVergeWidth = 2,
                BridgedRightFence = false,
                LeftVergeWidth = 2,
                BridgedLeftFence = false
            });
            track.TrackSections.Add(new TrackSectionViewModel(trasan)
            {
                Length = 2,
                Curvature = 2731,
                RightVergeWidth = 2,
                BridgedRightFence = false,
                LeftVergeWidth = 2,
                BridgedLeftFence = false
            });
            track.TrackSections.Add(new TrackSectionViewModel(trasan)
            {
                Length = 2,
                Curvature = 2731,
                RightVergeWidth = 2,
                BridgedRightFence = false,
                LeftVergeWidth = 2,
                BridgedLeftFence = false
            });
            track.TrackSections.Add(new TrackSectionViewModel(trasan)
            {
                Length = 38,
                Curvature = 0,
                RightVergeWidth = 2,
                BridgedRightFence = false,
                LeftVergeWidth = 2,
                BridgedLeftFence = false
            });

            return track;
        }
    }
}
