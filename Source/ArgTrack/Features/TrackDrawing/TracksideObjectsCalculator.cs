using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ArgTrack.Features.TrackEditing.ObjectSettings;

namespace ArgTrack.Features.TrackDrawing
{
    public static class TracksideObjectsCalculator
    {
        public static ObservableCollection<PointData> Calculate(List<RoadLayoutSectionData> roadSections, List<RoadLayoutSectionData> roadSectionsOneEach, ObservableCollection<TrackObjectSettingsViewModel> objectSettings)
        {
            var list = new List<TrackObjeSidoLayout>();

            int sectionStartAt = 0;

            foreach (var section in roadSections)
            {
                var trackSection = section.RelatedSection;

                foreach (var cmd in trackSection.Commands.Where(c => c.Command == 0x80))
                {
                    short distFromSectionStart = cmd.Arguments[0].Value;
                    short objectOffset = cmd.Arguments[1].Value;

                    var obj = objectSettings.SingleOrDefault(o => o.Offset == objectOffset);

                    int dist = obj.DistanceFromTrack;
                    // needs to be converted?

                    var tobj = new TrackObjeSidoLayout
                    {
                        SectionIndex = sectionStartAt + distFromSectionStart,
                        DistanceSidewaysFromCenter = dist * 5
                    };

                    list.Add(tobj);
                }

                sectionStartAt += trackSection.Length;
            }

            var l1 = list.OrderBy(o => o.SectionIndex).ToList();



            var poitnsio = new List<PointCollection>();

            foreach (var tobj in l1)
            {
                var points = new PointCollection();
                var sectionCenter = roadSectionsOneEach[tobj.SectionIndex];

                // TODO: calculate pos based on sectionCenter...
                double diffX = Math.Cos(Math.PI - sectionCenter.StartAngle * LayoutConstants.AngleScaleRadians) * tobj.DistanceSidewaysFromCenter / LayoutConstants.TrackWidthUnitsPerLengthUnit;
                double diffY = Math.Sin(Math.PI - sectionCenter.StartAngle * LayoutConstants.AngleScaleRadians) * tobj.DistanceSidewaysFromCenter / LayoutConstants.TrackWidthUnitsPerLengthUnit;

                var newX = sectionCenter.StartPosition.X + diffX;
                var newY = sectionCenter.StartPosition.Y + diffY;

                var point = new Point(newX, newY);



                // draw squareish
                //double diffStartX = Math.Cos(Math.PI - sectionCenter.StartAngle * LayoutConstants.AngleScaleRadians) * 20 / LayoutConstants.TrackWidthUnitsPerLengthUnit;
                //double diffStartY = Math.Sin(Math.PI - sectionCenter.StartAngle * LayoutConstants.AngleScaleRadians) * 20 / LayoutConstants.TrackWidthUnitsPerLengthUnit;

                //double diffEndX = Math.Cos(Math.PI - sectionCenter.EndAngle * LayoutConstants.AngleScaleRadians) * 20 / LayoutConstants.TrackWidthUnitsPerLengthUnit;
                //double diffEndY = Math.Sin(Math.PI - sectionCenter.EndAngle * LayoutConstants.AngleScaleRadians) * 20 / LayoutConstants.TrackWidthUnitsPerLengthUnit;

                // TODO: add 4 points


                var indicatorScale = 2000;

                double dXDiff = Math.Cos(Math.PI - sectionCenter.StartAngle * LayoutConstants.AngleScaleRadians) * indicatorScale / LayoutConstants.TrackWidthUnitsPerLengthUnit;
                double dYDiff = Math.Sin(Math.PI - sectionCenter.StartAngle * LayoutConstants.AngleScaleRadians) * indicatorScale / LayoutConstants.TrackWidthUnitsPerLengthUnit;

                // TODO: check this order...
                // 0?
                points.Add(new Point
                {
                    X = point.X - dXDiff,
                    Y = point.Y + dYDiff
                });
                // 1?
                points.Add(new Point
                {
                    X = point.X + dXDiff,
                    Y = point.Y - dYDiff
                });

                double dXDiff2 = Math.Cos(Math.PI - sectionCenter.StartAngle * LayoutConstants.AngleScaleRadians) * indicatorScale / LayoutConstants.TrackWidthUnitsPerLengthUnit;
                double dYDiff2 = Math.Sin(Math.PI - sectionCenter.StartAngle * LayoutConstants.AngleScaleRadians) * indicatorScale / LayoutConstants.TrackWidthUnitsPerLengthUnit;

                //2?
                points.Add(new Point
                {
                    X = point.X + dXDiff2,
                    Y = point.Y - dYDiff2
                });
                //3?
                points.Add(new Point
                {
                    X = point.X - dXDiff2,
                    Y = point.Y + dYDiff2
                });

                // add start point again to finish the shape off!
                //4
                points.Add(new Point
                {
                    X = point.X - dXDiff,
                    Y = point.Y + dYDiff
                });

                points.Add(point);

                poitnsio.Add(points);
            }


            var z = new ObservableCollection<PointData>();

            var drawingLayout = new DrawingLayout
            {
                StrokeThickness = 0.5,
                StrokeBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FillBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };

            foreach (var poco in poitnsio)
            {
                var y = LayoutToPointsConverter.Convert(poco, drawingLayout);

                z.AddRange(y);
            }

            return z;
        }

    }

    // TODO: name
    public class TrackObjeSidoLayout
    {
        public int SectionIndex { get; set; }

        public int DistanceSidewaysFromCenter { get; set; }
    }

}
