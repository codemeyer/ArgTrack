using System.Collections.Generic;

namespace ArgTrack.Features.TrackDrawing
{
    public class CenterPointsCalculationResult
    {
        public CenterPointsCalculationResult()
        {
            TrackSections = new List<LayoutSectionData>();
            PitLaneSections = new List<LayoutSectionData>();
        }

        public List<LayoutSectionData> TrackSections { get; set; }

        public List<LayoutSectionData> PitLaneSections { get; set; }
    }
}
