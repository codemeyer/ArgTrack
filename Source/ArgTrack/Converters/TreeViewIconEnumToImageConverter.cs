using System;
using System.Globalization;
using System.Windows.Data;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.Converters
{
    public class TreeViewIconEnumToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueEnum = (TreeViewIconEnum)value;

            if (valueEnum == null)
            {
                // throw?
                return null;
            }

            var path = "../Resources";

            switch (valueEnum)
            {
                case TreeViewIconEnum.ComputerCarLineLeft:
                    return $"{path}/ccline-left.png";
                case TreeViewIconEnum.ComputerCarLineStraight:
                    return $"{path}/ccline-straight.png";
                case TreeViewIconEnum.ComputerCarLineRight:
                    return $"{path}/ccline-right.png";
                case TreeViewIconEnum.PitLaneLeft:
                    return $"{path}/pitlane-left.png";
                case TreeViewIconEnum.PitLaneStraight:
                    return $"{path}/pitlane-straight.png";
                case TreeViewIconEnum.PitLaneRight:
                    return $"{path}/pitlane-right.png";
                case TreeViewIconEnum.TrackLeft:
                    return $"{path}/track-left.png";
                case TreeViewIconEnum.TrackStraight:
                    return $"{path}/track-straight.png";
                case TreeViewIconEnum.TrackRight:
                    return $"{path}/track-right.png";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
