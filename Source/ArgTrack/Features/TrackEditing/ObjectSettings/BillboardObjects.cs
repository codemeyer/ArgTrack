using System.Collections.Generic;

namespace ArgTrack.Features.TrackEditing.ObjectSettings
{
    public static class BillboardObjects
    {
        public static List<BillboardObjectBoundValue> List()
        {
            var list = new List<BillboardObjectBoundValue>();

            foreach (var obj in _objects)
            {
                list.Add(new BillboardObjectBoundValue(obj.Key, $"{obj.Key}: {obj.Value}"));
            }

            return list;
        }

        public static string GetDescription(short id)
        {
            if (_objects.ContainsKey(id))
            {
                return _objects[id];
            }
            else
            {
                return string.Empty;
            }
        }

        private static readonly Dictionary<short, string> _objects = new Dictionary<short, string>
        {
            { 75, "100 sign" },
            { 76, "200 sign" },
            { 77, "300 sign" },
            { 78, "Arrow hard right sign" },
            { 79, "Arrow easy right sign" },
            { 80, "Arrow hard left sign" },
            { 81, "Arrow easy left sign" },
            { 82, "Monaco tunnel pillar" },
            { 83, "Yacht" },
            { 84, "Fir tree" },
            { 85, "Fir tree" },
            { 86, "Fir tree" },
            { 87, "Fir tree" },
            { 88, "Fir tree" },
            { 89, "Fir tree" },
            { 90, "Leaf tree" },
            { 91, "Large bush" },
            { 92, "Large bush" },
            { 93, "Large bush" },
            { 94, "Large bush" },
            { 95, "Large bush" },
            { 96, "Large bush" },
            { 97, "Tree" },
            { 98, "Tree" },
            { 99, "Tree" },
            { 100, "Tree" },
            { 101, "Tree" },
            { 102, "Tree" },
            { 103, "Tree" },
            { 104, "Tree" },
            { 105, "Huge bush/trees" },
            { 106, "Huge bush/trees" },
            { 107, "Huge bush/trees" },
            { 108, "Huge bush/trees" },
            { 109, "Mega huge bush/trees" },
            { 110, "Small bush" },
            { 111, "Small bush" },
            { 112, "Small bush" },
            { 113, "Small bush" },
            { 114, "Small bush" },
            { 115, "Small bush" },
            { 116, "Medium bush" },
            { 117, "Medium bush" },
            { 118, "Medium bush" },
            { 119, "Medium bush" },
            { 120, "Tree" },
            { 121, "Tree (small?)" },
            { 122, "Tree (small?)" },
            { 123, "Tree (small?)" },
            { 124, "Large bush" },
            { 125, "Large bush" },
            { 126, "Large bush" },
            { 127, "Large bush" },
            { 128, "Large bush" },
            { 129, "Large bush" },
            { 160, "Pit crew member, kneeling" },
            { 161, "Pit crew member, front tyre" },
            { 162, "Pit crew member with headset" },
            { 163, "Stack of thin tyres" },
            { 166, "Pit crew member" },
            { 167, "Pit crew member" },
            { 168, "Top of Hockenheim pit building" },
            { 169, "Pit crew member with jack" },
            { 170, "Hand with cloth" },
            { 171, "Brakes On sign" },
            { 172, "Palm tree" },
            { 173, "Palm tree" },
            { 174, "Palm tree" },
            { 175, "Timing info screen" },
            { 221, "Huge bush" },
            { 222, "Huge bush" },
            { 225, "Huge bush" },
        };
    }

    public class BillboardObjectBoundValue
    {
        public BillboardObjectBoundValue(short value, string displayValue)
        {
            Value = value;
            DisplayValue = displayValue;
        }

        public short Value { get; set; }
        public string DisplayValue { get; set; }

    }
}
