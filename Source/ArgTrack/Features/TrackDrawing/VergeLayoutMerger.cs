using System.Collections.Generic;

namespace ArgTrack.Features.TrackDrawing
{
    public static class VergeLayoutMerger
    {
        public static List<VergeLayoutSectionData> MergeBridgedSections(List<VergeLayoutSectionData> rawVergeSections, VergeSide vergeSide)
        {
            var mergedSections = new List<VergeLayoutSectionData>();

            var allBridged = BridgedSectionIndexes(rawVergeSections, vergeSide);

            var workingVerge = new VergeLayoutSectionData();

            for (int i = 0; i < rawVergeSections.Count; i++)
            {
                var originalVerge = rawVergeSections[i];

                if (allBridged.Contains(i))
                {
                    if (allBridged.Contains(i) && !allBridged.Contains(i - 1))
                    {
                        // first bridged
                        CopyVergeStartInfo(originalVerge, workingVerge);
                    }

                    if (allBridged.Contains(i) && !allBridged.Contains(i + 1))
                    {
                        // last bridged, add end stuff and add to list
                        CopyVergeEndInfo(originalVerge, workingVerge);

                        workingVerge.DrawStraight = true;
                        workingVerge.DrawArc = false;

                        mergedSections.Add(workingVerge);

                        workingVerge = new VergeLayoutSectionData();
                    }
                }
                else
                {
                    CopyVergeStartInfo(originalVerge, workingVerge);
                    CopyVergeEndInfo(originalVerge, workingVerge);

                    workingVerge.DrawArc = workingVerge.StartAngle != workingVerge.EndAngle;
                    workingVerge.DrawStraight = workingVerge.StartAngle == workingVerge.EndAngle;

                    // not bridged, add end stuff and add to list
                    mergedSections.Add(workingVerge);

                    workingVerge = new VergeLayoutSectionData();
                }
            }

            return mergedSections;
        }

        private static void CopyVergeStartInfo(VergeLayoutSectionData source, VergeLayoutSectionData target)
        {
            target.StartPosition = source.StartPosition;
            target.StartAngle = source.StartAngle;
            target.StartWidth = source.StartWidth;
            target.LeftVergeStartWidthRaw = source.LeftVergeStartWidthRaw;
            target.RightVergeStartWidthRaw = source.RightVergeStartWidthRaw;
            target.LeftVergeEndWidthRaw = source.LeftVergeEndWidthRaw;
            target.RightVergeEndWidthRaw = source.RightVergeEndWidthRaw;

            target.Curvature = source.Curvature;
            target.CircleCenterForCurve = source.CircleCenterForCurve;
            target.Radius = source.Radius;
            target.OuterStartPosition = source.OuterStartPosition;
        }

        private static void CopyVergeEndInfo(VergeLayoutSectionData source, VergeLayoutSectionData target)
        {
            target.EndPosition = source.EndPosition;
            target.EndAngle = source.EndAngle;
            target.EndWidth = source.EndWidth;
            target.OuterEndPosition = source.OuterEndPosition;
        }

        /// <summary>
        /// Gets the indexes of all bridged section in the list.
        /// </summary>
        /// <param name="layoutSections"></param>
        /// <param name="vergeSide"></param>
        /// <returns></returns>
        private static List<int> BridgedSectionIndexes(List<VergeLayoutSectionData> layoutSections, VergeSide vergeSide)
        {
            var allBridged = new List<int>();

            for (int i = 0; i < layoutSections.Count; i++)
            {
                var layoutSection = layoutSections[i];

                if (vergeSide == VergeSide.Right && layoutSection.RelatedSection.BridgedRightFence)
                {
                    allBridged.Add(i);
                }

                if (vergeSide == VergeSide.Left && layoutSection.RelatedSection.BridgedLeftFence)
                {
                    allBridged.Add(i);
                }
            }

            return allBridged;
        }
    }
}
