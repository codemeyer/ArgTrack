using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using ArgTrack.Features.TrackEditing.ObjectSettings;

namespace ArgTrack.Features.TrackDrawing
{
    public static class TracksideObjectsCalculator
    {
        public static ObservableCollection<PointData> Calculate(List<RoadLayoutSectionData> roadSections, List<RoadLayoutSectionData> roadSectionsOneEach, ObservableCollection<TrackObjectSettingsViewModel> objectSettings)
        {
            // TODO: does not draw pitlane objects

            var list = new List<TrackSideObjectDrawingLayout>();

            int sectionStartAt = 0;

            foreach (var section in roadSections)
            {
                var trackSection = section.RelatedSection;

                foreach (var cmd in trackSection.Commands.Where(c => c.Command == 0x80))
                {
                    short fromSectionStart = cmd.Arguments[0].Value;
                    short objectOffset = cmd.Arguments[1].Value;

                    var objectSetting = objectSettings.Single(o => o.Offset == objectOffset);

                    // if SectionIndex becomes larger than actual number sections, then "wrap" it
                    var sectionIndex = sectionStartAt + fromSectionStart;

                    if (sectionIndex > roadSectionsOneEach.Count - 1)
                    {
                        sectionIndex -= roadSectionsOneEach.Count;
                    }

                    var drawingObject = new TrackSideObjectDrawingLayout
                    {
                        SectionIndex = sectionIndex,
                        DistanceFromCenter = DrawingUnitFromTracksideObjectDistance(objectSetting.DistanceFromTrack)
                    };

                    list.Add(drawingObject);
                }

                sectionStartAt += trackSection.Length;
            }

            var orderedList = list.OrderBy(o => o.SectionIndex).ToList();



            var pointCollections = new List<PointCollection>();

            foreach (var drawingObject in orderedList)
            {
                var section = roadSectionsOneEach[drawingObject.SectionIndex];

                var objectPoints = new PointCollection();

                var objectCenterPoint = Geometry.GetLegacyPointPerpendicular(section.StartPosition, GeometryConversions.RadiansFromGameAngle(section.StartAngle), drawingObject.DistanceFromCenter);

                objectPoints.Add(Geometry.GetPointPerpendicular(objectCenterPoint, 0, -1));
                objectPoints.Add(Geometry.GetPoint(objectCenterPoint, 0, 1));
                objectPoints.Add(Geometry.GetPointPerpendicular(objectCenterPoint, 0, 1));
                objectPoints.Add(Geometry.GetPoint(objectCenterPoint, 0, -1));
                objectPoints.Add(Geometry.GetPointPerpendicular(objectCenterPoint, 0, -1));

                pointCollections.Add(objectPoints);
            }

            var shapeData = new ObservableCollection<PointData>();

            foreach (var points in pointCollections)
            {
                var shape = LayoutToPointsConverter.Convert(points, DrawingLayout.ForTrackSideObjects());

                shapeData.AddRange(shape);
            }

            return shapeData;
        }

        private static double DrawingUnitFromTracksideObjectDistance(short distanceFromTrack)
        {
            // is there a more "exact" formula for this?
            return distanceFromTrack / 180.0;
        }
    }

    public class TrackSideObjectDrawingLayout
    {
        public int SectionIndex { get; set; }

        public double DistanceFromCenter { get; set; }
    }
}
