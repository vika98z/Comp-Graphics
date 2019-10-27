using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affine
{
    class Polygon
    {
        public List<Edge> Edges;

        public Polygon()
        {
            Edges = new List<Edge>();
        }

        public Polygon(List<Edge> list)
        {
            Edges = new List<Edge>();
            Edges = list;//????
        }
    }
}
