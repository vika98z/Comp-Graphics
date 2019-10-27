using System;
using System.Drawing;

namespace Affine
{
    class Edge
    {
        public MPoint First;
        public MPoint Second;

        public Edge(MPoint p1, MPoint p2)
        {
            First = p1;
            Second = p2;
        }

        public Edge(Edge e)
        {
            First = e.First;
            Second = e.Second;
        }

        Edge()
        {
            First = new MPoint();
            Second = new MPoint();
        }
    }
}
