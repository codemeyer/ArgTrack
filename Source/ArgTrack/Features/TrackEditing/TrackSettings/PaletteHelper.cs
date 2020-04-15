using System.Collections.Generic;
using System.Windows.Media;

namespace ArgTrack.Features.TrackEditing.TrackSettings
{
    public static class PaletteHelper
    {
        public static Color GetKerbColor(int index)
        {
            var drawingColor = KerbPalette.GetColor(index);
            return Color.FromRgb(drawingColor.R, drawingColor.G, drawingColor.B);
        }
    }

    public static class KerbPalette
    {
        private static Dictionary<int, System.Drawing.Color> _palette;

        /// <summary>
        /// Get the color at the specified index in the palette.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        internal static System.Drawing.Color GetColor(int index)
        {
            return _palette[index];
        }

        static KerbPalette()
        {
            SetupPalette();
        }

        private static void SetupPalette()
        {
            _palette = new Dictionary<int, System.Drawing.Color>
            {
                {0, System.Drawing.Color.FromArgb(0, 0, 0)},
                {1, System.Drawing.Color.FromArgb(202, 20, 26)},
                {2, System.Drawing.Color.FromArgb(184, 19, 26)},
                {3, System.Drawing.Color.FromArgb(241, 226, 30)},
                {4, System.Drawing.Color.FromArgb(23, 137, 29)},
                {5, System.Drawing.Color.FromArgb(23, 11, 145)},
                {6, System.Drawing.Color.FromArgb(31, 16, 226)},
                {7, System.Drawing.Color.FromArgb(142, 142, 142)},
                {8, System.Drawing.Color.FromArgb(255, 254, 238)},
                {9, System.Drawing.Color.FromArgb(238, 24, 30)},
                {10, System.Drawing.Color.FromArgb(142, 142, 142)},
                {11, System.Drawing.Color.FromArgb(19, 81, 22)},
                {12, System.Drawing.Color.FromArgb(158, 158, 158)},
                {13, System.Drawing.Color.FromArgb(223, 222, 210)},
                {14, System.Drawing.Color.FromArgb(239, 238, 222)},
                {15, System.Drawing.Color.FromArgb(22, 108, 26)}
            };
        }
    }
}
