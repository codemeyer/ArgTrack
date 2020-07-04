namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public static class ObjectShapeDescriptions
    {
        // TODO: all of these have become useless due to new way of calculating the signature
        public static string GetDescription(ulong signature)
        {
            switch (signature)
            {
                case 6624903810283278943: // US id 17?
                    return "Banner across track";
                case 14970757426583874831: // US id 18?
                    return "Banner across track";
                case 1230275162513647522: // US id 19
                    return "Building (long)";
                case 6977028658734253094: // US id 20 / 24 (one byte differs in OffsetData2...)
                    return "Building (tall)";
                case 7316695676700425945: // US id 21
                    return "Grandstand";
                case 6356977822502709836: // US id 22
                    return "Building (small) with windows";
                case 14345021413802830991: // US id 23
                    return "Bridge across track";
                case 15326233214005092590: // US id 25
                    return "Building (very long)";
                case 14984806071309391578: // US id 29
                    return "Pit building (base)";
                case 5592583758035079564: // US id 33
                    return "Church";
                case 7199742214909562774: // US id 36
                    return "Building (large, split roof)";
                case 2563460218542671741: // US id 37
                    return "Pit building (top)";
                case 1804671341350184658: // US id 41
                    return "Ad billboard (bar/diamond)";
                case 17359901276272088923: // US id 42
                    return "Ad billboard (hexagon)";
                case 5329891602170143700: // US id 43, SM id 41
                    return "Ad billboard (bar)";
                case 5495746349407109625: // US id 44
                    return "Windows";
                case 11573734072028963379: // US id 45
                    return "Windows";
                case 3291240858580297062: // US id 50
                    return "Windows";
                case 6057621248717762868: // US id 51
                    return "Windows";

                case 2632240749037042027: // SM id 34
                    return "Truck with crane";
                case 1191500967637494501:  // SM id 42
                    return "Ad billboard (large)";
                case 15259344655518611953: // SM id 43
                    return "Ad billboard (large)";

                case 5055906966707036665: // BRA id 17
                    return "Large building (pits?)";
                case 15028448486459629909: // BRA id 18
                    return "Bridge across track";
                case 4759217810388681850: // BRA id 19
                    return "Grandstand (small?)";
                case 4106320217738035985: // BRA id 20
                    return "Building (small) with windows";
                case 14116735515219089482: // BRA id 21
                    return "Grandstand (large with boxes)";
                case 1608283847208275492: // BRA id 22
                    return "Grandstand with roof";
                case 16678417183751682472: // BRA id 23
                    return "Banner/bridge across track";
                case 7048856883403442386: // BRA id 24 / MON id 33
                    return "Crane";
                case 6887974985522457908: // BRA id 25
                    return "Tent";
                case 14321256069509944364: // BRA id 26
                    return "Banner across track";
                case 14333565343114687342: // BRA id 27, not used
                    return "Ad billboard (single color)"; // ?
                case 9223284702271895899: // BRA id 28
                    return "Grandstand";
                case 5038354109337657879: // BRA id 29
                    return "Grandstand with roof";
                case 15770409077761807238: // BRA id 30
                    return "Grandstand (large, curved?)";
                case 8275984794335530646: // BRA id 31
                    return "Grandstand (large, curved?)";
                case 3635407304475344541: // BRA id 32
                    return "Grandstand";
                //case 10223380612919348542: // misc
                //    return "Pit crew (changing tyres)";
                //// 34 Pit crew (in race?)
                case 12916237433653170567: // BRA id 36
                    return "Billboard (single color)";
                case 13198397056088793595: // BRA id 37
                    return "Hill-side?";
                case 13662008444155641876: // BRA id 38
                    return "Hill-side? (incl. floating trees)";
                case 12713651048445316047: // BRA id 39
                    return "Hill-side?";
                case 13400539226430841274: // BRA id 40
                    return "Hill-side? (incl. floating trees)";

                case 2170688574809177066: // MON id 27
                    return "Grandstand";
                case 14278161885720524500: // MON id 26
                    return "Tall building (hotel/apartments)";
                case 9636630661675774017: // MON id 18
                    return "Bridge across track";
                case 12626597983087592520: // MON id 52
                    return "Boat mast";
                case 11961745097493225378: // MON id 53
                    return "Window";
                case 12364076771326422815: // MON id 36 - correct?
                    return "Grandstand";
                case 8471607909527252827: // MON id 20
                    return "Building (white with green roof)";
                case 13805113913099489531: // MON id 21
                    return "Zebra crossing";
                case 16844604713665088233: // MON id 22
                    return "One-way arrow track marking";
                case 11751572302259235244: // MON id 44
                    return "Yachts in water";
                case 3830947099239554759: // MON id 58
                    return "Distant body of water";
                case 669871304369725128: // MON id 17
                    return "Building above tunnel entrance";
                case 5649851585120645548: // MON id 39
                    return "Building (large)";

                case 14122902849771295415: // CAN id 19
                    return "Bridge across track (blocky)";
                case 1262238233145081085: // CAN id 25
                    return "Bridge across track (large)";
                case 14535631027562295601: // CAN id 35
                    return "Small wall";

                case 14437752569008329573: // MEX id 17
                    return "Dome-shaped building";

                default:
                    return string.Empty;
            }
        }
    }
}
