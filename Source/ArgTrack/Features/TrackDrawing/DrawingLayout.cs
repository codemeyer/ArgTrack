using System.Windows.Media;
using ArgData.Entities;

namespace ArgTrack.Features.TrackDrawing
{
    public class DrawingLayout
    {
        public Brush StrokeBrush { get; set; }
        public Brush FillBrush { get; set; }
        public double StrokeThickness { get; set; }

        public static DrawingLayout ForTrack()
        {
            return new DrawingLayout
            {
                StrokeThickness = 0.1,
                StrokeBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FillBrush = new SolidColorBrush(Color.FromRgb(0x89, 0x89, 0x85))
            };
        }

        public static DrawingLayout ForPitLane()
        {
            return new DrawingLayout
            {
                StrokeThickness = 0.1,
                StrokeBrush = new SolidColorBrush(Color.FromRgb(255, 255, 0)),
                FillBrush = new SolidColorBrush(Color.FromRgb(0x89, 0x89, 0x85))
            };
        }

        public static DrawingLayout ForVerges(SurroundingArea surroundingArea)
        {
            var surroundingColor = Color.FromRgb(0x55, 0x87, 0x39);

            if (surroundingArea != SurroundingArea.Green)
            {
                surroundingColor = Color.FromRgb(0x8e, 0x8e, 0x92);
            }

            return new DrawingLayout
            {
                StrokeThickness = 0.025,
                StrokeBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FillBrush = new SolidColorBrush(surroundingColor)
            };
        }

        public static DrawingLayout ForTrackSideObjects()
        {
            return new DrawingLayout
            {
                StrokeThickness = 0.1,
                StrokeBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FillBrush = new SolidColorBrush(Color.FromRgb(0, 128, 255))
            };
        }

        public static DrawingLayout ForTrackSideObjectsId5()
        {
            return new DrawingLayout
            {
                StrokeThickness = 0.1,
                StrokeBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FillBrush = new SolidColorBrush(Color.FromRgb(0, 145, 38))
            };
        }

        public static DrawingLayout ForTechCenterPointLine()
        {
            return new DrawingLayout
            {
                StrokeThickness = 0.2,
                StrokeBrush = new SolidColorBrush(Color.FromRgb(0, 200, 255)),
                FillBrush = new SolidColorBrush(Color.FromRgb(0, 200, 255))
            };
        }

        public static DrawingLayout ForTechCenterArcLine()
        {
            return new DrawingLayout
            {
                StrokeThickness = 0.2,
                StrokeBrush = new SolidColorBrush(Color.FromRgb(200, 0, 255)),
                FillBrush = new SolidColorBrush(Color.FromRgb(200, 0, 255))
            };
        }

        public static DrawingLayout ForTechCircleCenter()
        {
            return new DrawingLayout
            {
                StrokeThickness = 0.2,
                StrokeBrush = new SolidColorBrush(Color.FromRgb(0, 255, 165)),
                FillBrush = new SolidColorBrush(Color.FromRgb(0, 255, 165))
            };
        }
    }
}
