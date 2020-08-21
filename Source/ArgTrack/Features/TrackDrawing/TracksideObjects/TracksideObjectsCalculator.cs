using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using ArgTrack.Features.TrackEditing.ObjectSettings;

namespace ArgTrack.Features.TrackDrawing
{
    public static class TracksideObjectsCalculator
    {
        public static ObservableCollection<TrackShapeData> Calculate(List<RoadLayoutSectionData> roadSections, List<RoadLayoutSectionData> roadSectionsOneEach, ObservableCollection<TrackObjectSettingsViewModel> objectSettings, bool isTrack)
        {
            var list = new List<TrackSideObjectDrawingLayout>();

            int sectionStartAt = 0;

            foreach (var section in roadSections)
            {
                var trackSection = section.RelatedSection;

                foreach (var cmd in trackSection.Commands.Where(c => c.Command == 0x80))
                {
                    short fromSectionStart = cmd.Arguments[0].Value;
                    short objectOffset = cmd.Arguments[1].Value;

                    var objectSetting = objectSettings.SingleOrDefault(o => o.Offset == objectOffset);

                    if (objectSetting == null)
                    {
                        continue;
                    }

                    // if SectionIndex becomes larger than actual number sections, then "wrap" it
                    var sectionIndex = sectionStartAt + fromSectionStart;

                    if (sectionIndex > roadSectionsOneEach.Count - 1)
                    {
                        sectionIndex -= roadSectionsOneEach.Count;
                    }

                    var drawingObject = new TrackSideObjectDrawingLayout
                    {
                        SectionIndex = sectionIndex,
                        DistanceFromCenter = DrawingUnitFromTracksideObjectDistance(objectSetting.DistanceFromTrack, isTrack),
                        ObjectSetting = objectSetting
                    };

                    list.Add(drawingObject);
                }

                sectionStartAt += trackSection.Length;
            }

            var orderedList = list.OrderBy(o => o.SectionIndex).ToList();


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

                drawingObject.Points = objectPoints;
            }

            var shapeData = new ObservableCollection<TrackShapeData>();

            foreach (var drawingObject in orderedList)
            {
                var shape = PointsToShapesConverter.Convert(drawingObject.Points, drawingObject.GetDrawingLayout());

                // somewhat temp
                if (!isTrack)
                {
                    foreach(var p in shape)
                    {
                        p.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                        p.StrokeThickness = 0.25;
                    }
                }

                shapeData.AddRange(shape);
            }

            return shapeData;
        }

        private static double DrawingUnitFromTracksideObjectDistance(short distanceFromTrack, bool track)
        {
            // is there a more "exact" formula for this?
            if (track)
            {
                return distanceFromTrack / 180.0;
            }
            else
            {
                return distanceFromTrack / 360.0;
            }
        }
    }
}
