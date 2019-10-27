using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affine
{
    class Polyhedron
    {
        public List<Polygon> Edges;

        public Polyhedron()
        {
            Edges = new List<Polygon>();
        }

        public Polyhedron(List<Polygon> list)
        {
            Edges = new List<Polygon>();
            Edges = list;//????
        }
    }
}
