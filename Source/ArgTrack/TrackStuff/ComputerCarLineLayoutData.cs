using System;
using ArgTrack.ViewModels.Track;

namespace ArgTrack.TrackStuff
{
    public class ComputerCarLineLayoutData
    {
        public ComputerCarLineLayoutData(BestLineSegmentViewModel ccLine)
        {
            _ccLine = ccLine;

            if (_ccLine != null)
            {
                setType((int)ccLine.SegmentType);
            }
        }

        private BestLineSegmentViewModel _ccLine;



        /** Calculate the radius from raw params, depending on segment type.
        Scale radius to tlu units and store internally.
        Returns radius.
        */
        // factor for converting radius units to tlu
        static double s_dRADIUS_SCALE = 1 / 128.0;

        public double CalculateRadius()
        {
            double dRadius;
            if ((getType() & 0x40) != 0)
            {
                // 32Bit radius: lower 16 bits is UNSIGNED!
                //int nLower = getParam(2);
                int nLower = LowRadius;
                if (nLower < 0)
                    // make it an unsigned 16 bit value
                    nLower = nLower & 0x0FFFF;
                //dRadius = getParam(1) << 16 + nLower;
                dRadius = HighRadius << 16 + nLower;
            }
            else if ((getType() & 0x80) != 0)
            {
                // Type 0x80 segment (first in list) carries radius in param 2
                //dRadius = getParam(2);
                dRadius = Radius;
            }
            else
            {
                // normal type 0 segment
                //dRadius = getParam(1);
                dRadius = Radius;
            }
            // scale to tlu and store into segment.
            //dRadius = dRadius * s_dRADIUS_SCALE;
            dRadius = dRadius * s_dRADIUS_SCALE;
            setRadius(dRadius);
            return dRadius;
        }

        public void Calculate()
        {

        }



        public bool isStraight()
        {
            switch (getType())
            {
                case 0: //TrackBestLineSegmentType.Displacement:
                    return Radius == 0;
                case 1: //TrackBestLineSegmentType.Normal:
                    return Radius == 0;
                case 2: //TrackBestLineSegmentType.WideRadius:
                    return LowRadius == 0 && HighRadius == 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool turnsRight()
        {
            switch (getType())
            {
                case 0: //TrackBestLineSegmentType.Displacement:
                    return Radius > 0;
                case 1: //TrackBestLineSegmentType.Normal:
                    return Radius > 0;
                case 2: //TrackBestLineSegmentType.WideRadius:
                    return HighRadius > 0;  //?
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool turnsLeft()
        {
            if (isStraight())
                return false;

            return !turnsRight();
        }


        // 0x80 = displacement (0=displacement, 1=correction, 2=radius)
        // 0x00 = normal (0=Correction, 1=radius)
        // 0x40 = wide radius (0=correction, 1=high radius, 2=low radius)


        // instance data members
        protected int m_nType, m_nTlu;
        //protected int[] m_nParam;

        // calculated values
        private double m_dRadius;
        private double m_dPosXStart, m_dPosYStart;
        private double m_dPosXEnd, m_dPosYEnd;
        private double m_dPosXCenter, m_dPosYCenter;
        private double m_dAngleStart, m_dAngleEnd;
        private double m_dLength, m_dS; // not exactly sure if we need this (KS 12 Sep 06)
        private ComputerCarLineLayoutData m_segShift;   //hmm
        private bool m_bValid;


        // helper properties for prettier code
        public byte Length => _ccLine.Length;
        private short Correction => _ccLine.Correction;
        private short Radius => _ccLine.Radius;
        private short HighRadius => _ccLine.HighRadius;
        private short LowRadius => _ccLine.LowRadius;

        public short Displacement => _ccLine.Displacement;

        //public double ActualRadius { get; private set; }


        public int getTlu()
        { return m_nTlu; }
        public void setTlu(int nTlu)
        { m_nTlu = nTlu; }

        //public TrackBestLineSegmentType SegmentType => _ccLine.SegmentType;
        public int getType()
        { return m_nType; }
        public void setType(int nType)
        { m_nType = nType; }

        public int getShift()
        {
            return Correction;
            //int nShift;
            //if ((m_nType & 0x80) > 0)
            //    nShift = Correction;
            //else
            //    nShift = Correction;
            //return nShift;
        }

        public double getRadius()
        {
            return m_dRadius;
        }

        public void setRadius(double dRadius)
        {
            m_dRadius = dRadius;
        }

        public double getPosXStart()
        {
            return m_dPosXStart;
        }

        public void setPosXStart(double dPosXStart)
        {
            m_dPosXStart = dPosXStart;
        }

        public double getPosYStart()
        {
            return m_dPosYStart;
        }

        public void setPosYStart(double dPosYStart)
        {
            m_dPosYStart = dPosYStart;
        }

        public double getPosXEnd()
        {
            return m_dPosXEnd;
        }

        public void setPosXEnd(double dPosXEnd)
        {
            m_dPosXEnd = dPosXEnd;
        }

        public double getPosYEnd()
        {
            return m_dPosYEnd;
        }

        public void setPosYEnd(double dPosYEnd)
        {
            m_dPosYEnd = dPosYEnd;
        }

        public double getPosXCenter()
        {
            return m_dPosXCenter;
        }

        public void setPosXCenter(double dPosXCenter)
        {
            m_dPosXCenter = dPosXCenter;
        }

        public double getPosYCenter()
        {
            return m_dPosYCenter;
        }

        public void setPosYCenter(double dPosYCenter)
        {
            m_dPosYCenter = dPosYCenter;
        }

        public double getAngleStart()
        {
            return m_dAngleStart;
        }

        public void setAngleStart(double dAngleStart)
        {
            m_dAngleStart = dAngleStart;
        }

        public double getAngleEnd()
        {
            return m_dAngleEnd;
        }

        public void setAngleEnd(double dAngleEnd)
        {
            m_dAngleEnd = dAngleEnd;
        }

        public double getLength()
        {
            return m_dLength;
        }

        public void setLength(double dLength)
        {
            m_dLength = dLength;
        }

        public double getS()
        {
            return m_dS;
        }

        public void setS(double dS)
        {
            m_dS = dS;
        }


        /** Segment can be shifted by a (small) straight segment.
    This is used to move a curved CCline segment by a fraction of
    a TLU along the driving direction.
*/
        public void setShiftSegment(ComputerCarLineLayoutData seg)
        {
            m_segShift = seg;
        }

        public ComputerCarLineLayoutData getShiftSegment()
        {
            return m_segShift;
        }


        /** Segment can be invalidated to be excluded from display in the track map.
*/
        public bool isValid()
        {
            return m_bValid;
        }

        public void setValid(bool bValid)
        {
            m_bValid = bValid;
        }

    }
}
