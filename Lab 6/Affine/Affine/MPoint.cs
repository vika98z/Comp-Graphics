using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affine
{
    class MPoint
    {
        public float X;
        public float Y;
        public float Z;

        public MPoint(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public MPoint(float x, float y)
        {
            X = x;
            Y = y;
            Z = 0;
        }

        public MPoint()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public MPoint Copy()
        {
            return (MPoint)this.MemberwiseClone();
        }
    }
}
