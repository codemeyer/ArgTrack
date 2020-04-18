//using System;
//using System.Collections.Generic;

//namespace ArgTrack.Features.TrackDrawing
//{
//    public class RacingLineStuff
//    {

//        const double s_dANGLE_SCALE = (2 * Math.PI) / 65536;



//        public static void CalculateCCLine(List<ComputerCarLineLayoutData> ccLine, List<TrackSectionLayoutData> trackSectionLayoutData)
//        {
//            var m_TrackSegments = trackSectionLayoutData;
//            //ObservableCollection<TrackSectionViewModel> m_TrackSegments = TrackViewModelPasserArounder.TrackViewModel.TrackSections;
//            //ObservableCollection<BestLineSegmentViewModel> m_CCLine = TrackViewModelPasserArounder.TrackViewModel.BestLineSegments;
//            var m_CCLine = ccLine;

//            if (m_CCLine.Count == 0)
//                return;

//            // To calculate the CCline, both CCline data itself and
//            // track segment data is needed.
//            //BestLineSegmentViewModel ccLineSegment;
//            ComputerCarLineLayoutData ccLineSegment;
//            //TrackSectionViewModel trackSegment;
//            TrackSectionLayoutData trackSegment;


//            int nCumulatedTrackTlu = 0, nCumulatedCCLineTlu = 0;
//            //int nTrackSegmentIndex = 1, nCCLineSegmentIndex = 1;
//            int nTrackSegmentIndex = 0, nCCLineSegmentIndex = 0;
//            double dXPos, dYPos, dOffset, dAngleStart, dAngleEnd, dRadius;

//            // Get the first segments
//            ccLineSegment = m_CCLine[nCCLineSegmentIndex];
//            nCCLineSegmentIndex++;
//            nCumulatedCCLineTlu = ccLineSegment.Length;
//            trackSegment = m_TrackSegments[nTrackSegmentIndex];
//            nTrackSegmentIndex++;
//            //trackSegment = m_TrackSegments[nTrackSegmentIndex++];
//            nCumulatedTrackTlu = trackSegment.Length;

//            // First ccLine segment is related to the first track segment:
//            // starting point is on start/finish line, with an offset to the
//            // middle of the road.
//            //dXPos = trackSegment.getPosXStart();
//            //dYPos = trackSegment.getPosYStart();
//            dXPos = trackSegment.StartPosition.X;
//            dYPos = trackSegment.StartPosition.Y;
//            // calculate offset from middle of the track
//            //dOffset = ccLineSegment.getParam(0) / 1024.0; // given in width scales, i.e. 1/1024 tlu
//            //                                              // calculate starting angle in radiens
//            dOffset = ccLineSegment.Displacement / 1024.0; // given in width scales, i.e. 1/1024 tlu
//                                                           // calculate starting angle in radiens
//            dAngleStart = trackSegment.StartAngle * s_dANGLE_SCALE;
//            ccLineSegment.setAngleStart(dAngleStart);
//            ccLineSegment.setPosXStart(dXPos + Math.Cos(dAngleStart) * dOffset);
//            ccLineSegment.setPosYStart(dYPos + Math.Sin(dAngleStart) * dOffset);

//            do
//            {
//                // retrieve starting values
//                dXPos = ccLineSegment.getPosXStart();
//                dYPos = ccLineSegment.getPosYStart();
//                dAngleStart = ccLineSegment.getAngleStart();

//                // calculate radius
//                dRadius = ccLineSegment.CalculateRadius();

//                // calculate end point and end angle.
//                // TLU value from CCLine segment is used for TrackSegments lookup, not
//                // direct length calculations. Intersect CCLine segment with end
//                // of corresponding track segment to find end point. For curved segments,
//                // this calculation also gives the end angle.

//                // Find the track segment where the CCLine segment ends.
//                while (nCumulatedCCLineTlu > nCumulatedTrackTlu)
//                {
//                    trackSegment = m_TrackSegments[nTrackSegmentIndex++];
//                    if (trackSegment == null)
//                    {
//                        // end of track reached
//                        nCumulatedTrackTlu = nCumulatedCCLineTlu;
//                        // previous TrackSegmentIndex pointed already behind the list and
//                        // was incremented once. Last track segment is a dummy segment, so
//                        // subtract 3 to get the last real segment.
//                        nTrackSegmentIndex = nTrackSegmentIndex - 3;
//                        if (nTrackSegmentIndex < 1)
//                            nTrackSegmentIndex = 1;
//                        trackSegment = m_TrackSegments[nTrackSegmentIndex];
//                    }
//                    else
//                    {
//                        nCumulatedTrackTlu += trackSegment.Length;
//                    }
//                }

//                // Calculate angle of track segment border at the end of the ccLine segment.
//                double dTrackAngle = trackSegment.EndAngle
//                                     - (nCumulatedCCLineTlu - nCumulatedTrackTlu) * trackSegment.Curvature;
//                dTrackAngle = dTrackAngle * s_dANGLE_SCALE;

//                // Calculate point on the track segment border at the end of the ccLine segment.
//                double dTrackPosX;
//                double dTrackPosY;
//                if (trackSegment.Curvature == 0)
//                {
//                    // straight
//                    dTrackPosX = trackSegment.StartPosition.X
//                                 - (nCumulatedCCLineTlu - nCumulatedTrackTlu + trackSegment.Length) * Math.Sin(dTrackAngle);
//                    dTrackPosY = trackSegment.StartPosition.Y
//                                 + (nCumulatedCCLineTlu - nCumulatedTrackTlu + trackSegment.Length) * Math.Cos(dTrackAngle);
//                }
//                else
//                {
//                    // curved: use center and radius for calculations
//                    dTrackPosX = trackSegment.CircleCenterForCurve.X
//                                 - trackSegment.Radius * Math.Cos(dTrackAngle);
//                    dTrackPosY = trackSegment.CircleCenterForCurve.Y
//                                 - trackSegment.Radius * Math.Sin(dTrackAngle);
//                }

//                // Intersect CCLine with end of track segment. Sets values
//                // for end point and end angle in ccline segment.
//                intersect(ccLineSegment, dTrackPosX, dTrackPosY, dTrackAngle);

//                // get next ccline segment
//                if (nCCLineSegmentIndex < m_CCLine.Count)
//                {
//                    // get end values of last segment
//                    dXPos = ccLineSegment.getPosXEnd();
//                    dYPos = ccLineSegment.getPosYEnd();
//                    dAngleEnd = ccLineSegment.getAngleEnd();
//                    // get next segment
//                    ccLineSegment = m_CCLine[nCCLineSegmentIndex];
//                    nCCLineSegmentIndex++;
//                    // add tlu's to cumulated value
//                    nCumulatedCCLineTlu += ccLineSegment.getTlu();
//                    // transfer end values of previous segment to new one.
//                    ccLineSegment.setPosXStart(dXPos);
//                    ccLineSegment.setPosYStart(dYPos);
//                    ccLineSegment.setAngleStart(dAngleEnd);
//                }
//                else
//                    ccLineSegment = null;
//            } while (ccLineSegment != null);
//        }

//        /**
//    Intersect ccLine segment with end of track segment.
//    Parameters: CCline segment
//                point and angle of track at end of CCLine segment.
//    Calculate end point and angle for ccLineSegment and store into segment.
//*/
//        //public void intersect(CCLineSegment ccLineSegment, double dTrackPosX, double dTrackPosY, double dTrackAngle)
//        public static void intersect(ComputerCarLineLayoutData ccLineSegment, double dTrackPosX, double dTrackPosY, double dTrackAngle)
//        {
//            double dRadius = ccLineSegment.getRadius();
//            double dCClineAngle = ccLineSegment.getAngleStart();

//            // Calculations based on code from Kristof Kaly-Kulai (DOS Track editor)
//            if (ccLineSegment.isStraight())
//            {
//                ccLineSegment.setPosXCenter(0.0);
//                ccLineSegment.setPosYCenter(0.0);
//                // angle of segment can be modified by "shift" value
//                ccLineSegment.setAngleStart(ccLineSegment.getAngleStart()
//                                             - s_dANGLE_SCALE * ccLineSegment.getShift());
//                // straight segment can not be shifted in driving direction
//                ccLineSegment.setShiftSegment(null);
//                double dAux;
//                dAux = Math.Sin(ccLineSegment.getAngleStart()) * Math.Sin(dTrackAngle)
//                       + Math.Cos(ccLineSegment.getAngleStart()) * Math.Cos(dTrackAngle);

//                if (Math.Abs(dAux) < 1e-10)
//                {
//                    ccLineSegment.setS(100.0); // sense???
//                    ccLineSegment.setLength(ccLineSegment.getTlu());
//                }
//                else
//                {
//                    ccLineSegment.setLength((Math.Cos(dTrackAngle) * (dTrackPosY - ccLineSegment.getPosYStart()))
//                                             - (Math.Sin(dTrackAngle) * (dTrackPosX - ccLineSegment.getPosXStart())));
//                    ccLineSegment.setS((-Math.Sin(ccLineSegment.getAngleStart()) * (dTrackPosY - ccLineSegment.getPosYStart())
//                                         - Math.Cos(ccLineSegment.getAngleStart()) * (dTrackPosX - ccLineSegment.getPosXStart()))
//                                        / dAux);
//                }
//                ccLineSegment.setAngleEnd(ccLineSegment.getAngleStart());
//                ccLineSegment.setPosXEnd(dTrackPosX + ccLineSegment.getS() * Math.Cos(dTrackAngle));
//                ccLineSegment.setPosYEnd(dTrackPosY + ccLineSegment.getS() * Math.Sin(dTrackAngle));
//            }
//            else
//            {
//                int i;
//                double dS = 0.0;

//                // ccLineSegment is curved
//                // Check if shift value is present
//                if (ccLineSegment.getShift() > 0)
//                {
//                    // Shift segment along driving direction. Insert small auxiliary
//                    // segment to fill the gap.
//                    var ccLineAux = new ComputerCarLineLayoutData(null); // always special type 1 (aux)
//                                                                         // Shift segment is always straight
//                    ccLineAux.setRadius(0.0);
//                    ccLineAux.setPosXCenter(0.0);
//                    ccLineAux.setPosYCenter(0.0);
//                    // starts at starting position of current ccline segment
//                    ccLineAux.setPosXStart(ccLineSegment.getPosXStart());
//                    ccLineAux.setPosYStart(ccLineSegment.getPosYStart());
//                    ccLineAux.setAngleStart(ccLineSegment.getAngleStart());
//                    ccLineAux.setAngleEnd(ccLineSegment.getAngleStart());
//                    // length is calculated from shift value
//                    double dLength = ccLineSegment.getShift() / 256.0;
//                    ccLineSegment.setLength(dLength);
//                    ccLineSegment.setS(0.0);
//                    // calculate end point
//                    ccLineAux.setPosXEnd(ccLineAux.getPosXStart() - dLength * Math.Sin(ccLineAux.getAngleStart()));
//                    ccLineAux.setPosYEnd(ccLineAux.getPosYStart() + dLength * Math.Cos(ccLineAux.getAngleStart()));
//                    // store into current segment
//                    ccLineSegment.setShiftSegment(ccLineAux);
//                    // move current segment to end of shift segment
//                    ccLineSegment.setPosXStart(ccLineAux.getPosXEnd());
//                    ccLineSegment.setPosYStart(ccLineAux.getPosYEnd());
//                }
//                else
//                {
//                    // no shift segment
//                    ccLineSegment.setShiftSegment(null);
//                }

//                // Calculate center of circle
//                ccLineSegment.setPosXCenter(ccLineSegment.getPosXStart()
//                                             + ccLineSegment.getRadius() * Math.Cos(ccLineSegment.getAngleStart()));
//                ccLineSegment.setPosYCenter(ccLineSegment.getPosYStart()
//                                             + ccLineSegment.getRadius() * Math.Sin(ccLineSegment.getAngleStart()));

//                // calculate end points and angles
//                double[] adAlfa = new double[2];  // variable names are terrible, but I cant figure out what they mean, so
//                                                  // I kept the names of the DOS version.
//                double[] adS = new double[2];
//                double dCotTrackAngle, dA, dC, dF, dF2;
//                if (Math.Abs(Math.Cos(dTrackAngle)) < Math.Abs(Math.Sin(dTrackAngle)))
//                {
//                    dCotTrackAngle = Math.Cos(dTrackAngle) / Math.Sin(dTrackAngle);
//                    dA = 1.0 / Math.Sqrt(1 + dCotTrackAngle * dCotTrackAngle);
//                    dC = dA * (dTrackPosX - ccLineSegment.getPosXCenter()
//                                + dCotTrackAngle * (ccLineSegment.getPosYCenter() - dTrackPosY))
//                            / ccLineSegment.getRadius();
//                    dC = -dC; // negate sign
//                    dF = Math.Atan2(-dA, dCotTrackAngle * dA);
//                    if (Math.Abs(dC) > 1.0)
//                    {
//                        ccLineSegment.setS(100.0); // @@@ sense of value S??
//                        ccLineSegment.setAngleEnd(ccLineSegment.getAngleStart());
//                        ccLineSegment.setLength(1.0); // @@@ sense of length??
//                        ccLineSegment.setPosXEnd(ccLineSegment.getPosXStart() - Math.Sin(ccLineSegment.getAngleStart()));
//                        ccLineSegment.setPosYEnd(ccLineSegment.getPosYStart() + Math.Cos(ccLineSegment.getAngleStart()));
//                        // Segment and all following segments get "invalid" marker (?)
//                        ccLineSegment.setValid(false);
//                        return;
//                    }
//                    else
//                    {
//                        dF2 = Math.Asin(dC);
//                    }
//                    adAlfa[0] = -dF2 - dF;
//                    adS[0] = (ccLineSegment.getPosYCenter() - dTrackPosY
//                               - ccLineSegment.getRadius() * Math.Sin(adAlfa[0]))
//                              / Math.Sin(dTrackAngle);
//                    adAlfa[1] = Math.PI + dF2 - dF;
//                    adS[1] = (ccLineSegment.getPosYCenter() - dTrackPosY
//                               - ccLineSegment.getRadius() * Math.Sin(adAlfa[1]))
//                              / Math.Sin(dTrackAngle);
//                }
//                else
//                {
//                    double dTanTrackAngle;
//                    dTanTrackAngle = Math.Tan(dTrackAngle);
//                    dA = 1.0 / Math.Sqrt(1 + dTanTrackAngle * dTanTrackAngle);
//                    dC = dA * (dTrackPosY - ccLineSegment.getPosYCenter()
//                                + dTanTrackAngle * (ccLineSegment.getPosXCenter() - dTrackPosX))
//                            / ccLineSegment.getRadius();
//                    dC = -dC; // negate sign
//                    dF = Math.Atan2(dTanTrackAngle * dA, -dA);
//                    if (Math.Abs(dC) > 1.0)
//                    {
//                        // ccLine left track???
//                        ccLineSegment.setS(100.0);
//                        ccLineSegment.setLength(1.0);
//                        ccLineSegment.setAngleEnd(ccLineSegment.getAngleStart());
//                        ccLineSegment.setPosXEnd(ccLineSegment.getPosXStart() - Math.Sin(ccLineSegment.getAngleStart()));
//                        ccLineSegment.setPosYEnd(ccLineSegment.getPosYStart() + Math.Cos(ccLineSegment.getAngleStart()));
//                        // Mark segment and all followers as invalid (see above)
//                        ccLineSegment.setValid(false);
//                        return;
//                    }
//                    dF2 = Math.Asin(dC);
//                    adAlfa[0] = -dF2 - dF;
//                    adS[0] = (ccLineSegment.getPosXCenter() - dTrackPosX
//                                 - ccLineSegment.getRadius() * Math.Cos(adAlfa[0]))
//                               / Math.Cos(dTrackAngle);
//                    adAlfa[1] = Math.PI + dF2 - dF;
//                    adS[1] = (ccLineSegment.getPosXCenter() - dTrackPosX
//                                 - ccLineSegment.getRadius() * Math.Cos(adAlfa[1]))
//                               / Math.Cos(dTrackAngle);
//                }

//                // Possibly modify angles
//                for (i = 0; i < 2; i++)
//                {
//                    while ((adAlfa[i] - ccLineSegment.getAngleStart()) > (2.0 * Math.PI))
//                    {
//                        adAlfa[i] -= 2.0 * Math.PI;
//                    }
//                    while ((adAlfa[i] - ccLineSegment.getAngleStart()) <= (-2.0 * Math.PI))
//                    {
//                        adAlfa[i] += 2.0 * Math.PI;
//                    }
//                    if (ccLineSegment.turnsRight())
//                    {
//                        while (-adAlfa[i] <= -ccLineSegment.getAngleStart())
//                        {
//                            adAlfa[i] -= 2.0 * Math.PI;
//                        }
//                    }
//                    else
//                    {
//                        // turns left
//                        while (adAlfa[i] <= ccLineSegment.getAngleStart())
//                        {
//                            adAlfa[i] += 2.0 * Math.PI;
//                        }
//                    }
//                }

//                if (((ccLineSegment.getPosYStart() - dTrackPosY) * Math.Cos(dTrackAngle)
//                       - (ccLineSegment.getPosXStart() - dTrackPosX) * Math.Sin(dTrackAngle)) > 0.0)
//                {
//                    if ((ccLineSegment.turnsRight() && (-adAlfa[0] < -adAlfa[1]))
//                         || (ccLineSegment.turnsLeft() && (adAlfa[0] < adAlfa[1])))
//                    {
//                        ccLineSegment.setAngleEnd(adAlfa[1]);
//                        ccLineSegment.setS(adS[1]);
//                    }
//                    else
//                    {
//                        ccLineSegment.setAngleEnd(adAlfa[0]);
//                        ccLineSegment.setS(adS[0]);
//                    }
//                }
//                else
//                {
//                    if ((ccLineSegment.turnsRight() && (-adAlfa[0] < -adAlfa[1]))
//                         || (ccLineSegment.turnsLeft() && (adAlfa[0] < adAlfa[1])))
//                    {
//                        ccLineSegment.setAngleEnd(adAlfa[0]);
//                        ccLineSegment.setS(adS[0]);
//                    }
//                    else
//                    {
//                        ccLineSegment.setAngleEnd(adAlfa[1]);
//                        ccLineSegment.setS(adS[1]);
//                    }
//                }
//                // calculate length
//                ccLineSegment.setLength(Math.Abs((ccLineSegment.getAngleEnd() - ccLineSegment.getAngleStart()) * ccLineSegment.getRadius()));

//                // calculate end points once again from "S" value.
//                // Places end point on end of track subsegment???
//                ccLineSegment.setPosXEnd(dTrackPosX + ccLineSegment.getS() * Math.Cos(dTrackAngle));
//                ccLineSegment.setPosYEnd(dTrackPosY + ccLineSegment.getS() * Math.Sin(dTrackAngle));
//            }
//        }

//    }
//}
