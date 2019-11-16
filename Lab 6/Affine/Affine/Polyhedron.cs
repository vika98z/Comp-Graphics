using System.Collections.Generic;

namespace Affine
{
    class Polyhedron
    {
        public List<Polygon> Facets;
        public MPoint PolyCenter = new MPoint(360, 360, 0);

        public Polyhedron()
        {
            Facets = new List<Polygon>();
            PolyCenter = new MPoint(360, 360, 0);
        }

        public Polyhedron(List<Polygon> list)
        {
            Facets = new List<Polygon>();
            Facets = list;
            PolyCenter = new MPoint(360, 360, 0);
        }

        public List<MPoint> AllPoints()
        {
            List<MPoint> result = new List<MPoint>();

            foreach (var pol in Facets)
            {
                foreach (var e in pol.Edges)
                {
                    result.Add(e.First);
                    result.Add(e.Second);
                }
            }

            return result;
        }
    }
}
