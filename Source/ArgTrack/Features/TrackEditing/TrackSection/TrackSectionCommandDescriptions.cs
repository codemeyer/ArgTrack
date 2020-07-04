using System.Collections.Generic;
using System.Linq;

namespace ArgTrack.Features.TrackEditing.TrackSection
{
    public static class TrackSectionCommandDescriptions
    {
        public static string GetCommandDescription(byte command)
        {
            return Descriptions[command];
        }

        public static List<byte> ListCommands()
        {
            return Descriptions.Keys.ToList();
        }

        public static string GetCommandArgumentDescription(byte command, int argumentIndex)
        {
            if (ArgumentDescriptions.ContainsKey(command) && ArgumentDescriptions[command].ContainsKey(argumentIndex))
            {
                return ArgumentDescriptions[command][argumentIndex];
            }
            else
            {
                return $"Arg{argumentIndex} ?";
            }
        }

        private static readonly Dictionary<byte, string> Descriptions
            = new Dictionary<byte, string>
            {
                {0x80, "Object placement"},
                {0x81, "Display distance forwards"},
                {0x82, "Display distance backwards"},
                {0x83, "Disable drawing of horizon"},
                {0x84, "Enable drawing of horizon"},
                {0x85, "Track width change"},
                {0x86, "Connect start of pit lane to track"},
                {0x87, "Connect end of pit lane to track"},
                {0x88, "Pit parking zone marking left-hand pit lane"},
                {0x89, "Pit parking zone marking right-hand pit lane"},
                {0x8a, "Track markings"},
                {0x8b, "Starting grid markings"},
                {0x8c, "Changes unknown element on the left"},
                {0x8d, "Changes unknown element on the right"},
                {0x8e, "Left kerb starts"},
                {0x8f, "Right kerb starts"},
                {0x90, "Reverse object placement?"},
                {0x91, "Unknown Monaco command"},
                {0x92, "Sector in which unknown action 1 is taken?"},
                {0x93, "Sector in which unknown action 2 is taken?"},
                {0x94, "Computer car coaching left"},
                {0x95, "Computer car coaching right"},
                {0x96, "Pit lane start"},
                {0x97, "Pit lane end"},
                {0x98, "Left fence height change"},
                {0x99, "Right fence height change"},
                {0x9a, "Custom fence height"},
                {0x9b, "Unknown pitlane marker 1"},
                {0x9c, "Unknown pitlane marker 2"},
                {0x9d, "Unknown pitlane marker 3"},
                {0x9e, "Unknown pitlane marker 4"},
                {0x9f, "Pit lane fences start"},
                {0xa0, "Pit lane fences end"},
                {0xa1, "Pit lane entry, join right pit lane fence"},
                {0xa2, "Pit lane entry, join left pit lane fence"},
                {0xa3, "Pit lane exit, join right pit lane fence"},
                {0xa4, "Pit lane exit, join left pit lane fence"},
                {0xa5, "Change the sign of the first two sector arguments"},
                {0xa6, "Set unknown segment flags 1"},
                {0xa7, "Set unknown segment flags 2"},
                {0xa8, "Section that triggers the chequered flag"},
                {0xa9, "Pit lane view distance"},
                {0xaa, "Computer cars pitlane modifier?"},
                {0xab, "Unknown, may be unused"},
                {0xac, "Palette change"}
            };

        private static readonly Dictionary<byte, Dictionary<int, string>> ArgumentDescriptions
            = new Dictionary<byte, Dictionary<int, string>>
            {
                {
                    0x80, new Dictionary<int, string>
                    {
                        { 0, "Distance from start of section" },
                        { 1, "Object description offset" }
                    }
                },
                {
                    0x81, new Dictionary<int, string>
                    {
                        { 0, "Unknown" },
                        { 1, "Draw distance?" }
                    }
                },
                {
                    0x82, new Dictionary<int, string>
                    {
                        { 0, "Unknown" },
                        { 1, "Draw distance?" }
                    }
                },
                {
                    0x83, new Dictionary<int, string>
                    {
                        { 0, "Unknown" }
                    }
                },
                {
                    0x84, new Dictionary<int, string>
                    {
                        { 0, "Unknown" }
                    }
                },
                {
                    0x85, new Dictionary<int, string>
                    {
                        { 0, "Distance from start of section?" },
                        { 1, "Transition length" },
                        { 2, "New width" }
                    }
                },
                {
                    0x86, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" }
                    }
                },
                {
                    0x87, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" }
                    }
                },
                {
                    0x88, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" },
                        { 1, "Length of zone marking?" }
                    }
                },
                {
                    0x89, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" },
                        { 1, "Length of zone marking?" }
                    }
                },
                {
                    0x8A, new Dictionary<int, string>
                    {
                        { 0, "Unknown" },
                        { 1, "Unknown" },
                        { 2, "Length of zone marking?" },
                        { 3, "Unknown" },
                        { 4, "Unknown" },
                        { 5, "Unknown" }
                    }
                },
                {
                    0x8B, new Dictionary<int, string>
                    {
                        { 0, "Unknown" },
                        { 1, "Unknown" },
                        { 2, "Width or length of grid markings?" },
                        { 3, "Unknown" },
                        { 4, "Unknown" },
                        { 5, "Unknown" }
                    }
                },
                {
                    0x8C, new Dictionary<int, string>
                    {
                        { 0, "Unknown?" },
                        { 1, "Length?" }
                    }
                },
                {
                    0x8D, new Dictionary<int, string>
                    {
                        { 0, "Unknown?" },
                        { 1, "Length?" }
                    }
                },
                {
                    0x8E, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" },
                        { 1, "Offset into sector" },
                        { 2, "Length" }
                    }
                },
                {
                    0x8F, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" },
                        { 1, "Offset into sector" },
                        { 2, "Length" }
                    }
                },
                {
                    0x90, new Dictionary<int, string>
                    {
                        { 0, "Unknown" },
                        { 1, "Unknown" }
                    }
                },
                {
                    0x91, new Dictionary<int, string>
                    {
                        { 0, "Unknown" },
                        { 1, "Unknown" }
                    }
                },
                {
                    0x92, new Dictionary<int, string>
                    {
                        { 0, "Unknown" },
                        { 1, "Unknown" }
                    }
                },
                {
                    0x93, new Dictionary<int, string>
                    {
                        { 0, "Unknown" },
                        { 1, "Unknown" }
                    }
                },
                {
                    0x94, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" },
                        { 1, "Coaching factor" }
                    }
                },
                {
                    0x95, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" },
                        { 1, "Coaching factor" }
                    }
                },
                {
                    0x96, new Dictionary<int, string>
                    {
                        { 0, "Unknown" }
                    }
                },
                {
                    0x97, new Dictionary<int, string>
                    {
                        { 0, "Unknown" }
                    }
                },
                {
                    0x98, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" },
                        { 1, "Height modification" }
                    }
                },
                {
                    0x99, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" },
                        { 1, "Height modification" }
                    }
                },
                {
                    0x9A, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" },
                        { 1, "Height identifier" },
                        { 2, "New height value" }
                    }
                },
                {
                    0x9B, new Dictionary<int, string>
                    {
                        { 0, "Unknown" }
                    }
                },
                {
                    0x9C, new Dictionary<int, string>
                    {
                        { 0, "Unknown" }
                    }
                },
                {
                    0x9D, new Dictionary<int, string>
                    {
                        { 0, "Unknown" }
                    }
                },
                {
                    0x9E, new Dictionary<int, string>
                    {
                        { 0, "Unknown" }
                    }
                },
                {
                    0x9F, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" }
                    }
                },
                {
                    0xA0, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" }
                    }
                },
                {
                    0xA1, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" }
                    }
                },
                {
                    0xA2, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" }
                    }
                },
                {
                    0xA3, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" }
                    }
                },
                {
                    0xA4, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" }
                    }
                },
                {
                    0xA5, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" }
                    }
                },
                {
                    0xA6, new Dictionary<int, string>
                    {
                        { 0, "Unknown" },
                        { 1, "Unknown" },
                        { 2, "Unknown" }
                    }
                },
                {
                    0xA7, new Dictionary<int, string>
                    {
                        { 0, "Unknown" },
                        { 1, "Unknown" },
                        { 2, "Unknown" }
                    }
                },
                {
                    0xA8, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" }
                    }
                },
                {
                    0xa9, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" },
                        { 1, "Distance" }
                    }
                },
                {
                    0xAA, new Dictionary<int, string>
                    {
                        { 0, "Unknown/unused?" },
                        { 1, "Length for computer cars to angle towards pitlane (backwards)?" },
                        { 2, "Length for computer cars to angle out of pits?" },
                        { 3, "Computer car pit lane speed" }
                    }
                },
                {
                    0xAB, new Dictionary<int, string>
                    {
                        { 0, "Unknown" },
                        { 1, "Unknown" },
                        { 2, "Unknown" }
                    }
                },
                {
                    0xac, new Dictionary<int, string>
                    {
                        { 0, "Unknown" },
                        { 1, "Palette index (18=grass or 26=asphalt)" },
                        { 2, "Red color value (0-63?)" },
                        { 3, "Green color value (0-63?)" },
                        { 4, "Blue color value (0-63?)" },
                    }
                }
            };
    }
}
