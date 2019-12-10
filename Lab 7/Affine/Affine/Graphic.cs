using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affine
{
    class Graphic : Polyhedron
    {
        public Func<float, float, float> F;
        public int X0 { get; }
        public int X1 { get; }
        public int Y0 { get; }
        public int Y1 { get; }
        public int CountOfSplits { get; }

        public Graphic(int x0, int x1, int y0, int y1, int count, int func)
        {
            X0 = x0;
            X1 = x1;
            Y0 = y0;
            Y1 = y1;
            CountOfSplits = count;
            Polygons = new List<Polygon>();

            float dx = (Math.Abs(x0) + Math.Abs(x1)) / count;
            float dy = (Math.Abs(y0) + Math.Abs(y1)) / count;

            List<Point3D> pts0 = new List<Point3D>();
            List<Point3D> pts1 = new List<Point3D>();

            switch (func)
            {
                case 0:
                    F = (x, y) => x + y;
                    break;
                case 1:
                    F = (x, y) => (float)Math.Cos(x * x + y * y)/(x * x + y * y + 1);
                    break;
                case 2:
                    F = (x, y) => (float)Math.Sin(x) + (float)Math.Cos(y);
                    break;
                default:
                    F = (x, y) => x + y;
                    break;
            }

            for (float x = x0; x < x1; x += dx)
            {
                for (float y = y0; y < y1; y += dy)
                {
                    float z = F(x, y);
                    pts1.Add(new Point3D(x, y, z));
                }

                if (pts0.Count != 0)
                    for (int i = 1; i < pts0.Count; ++i)
                    {
                        Polygons.Add(new Polygon(new List<Point3D>() {
                            new Point3D(pts0[i - 1]), new Point3D(pts1[i - 1]),
                            new Point3D(pts1[i]), new Point3D(pts0[i])
                        }));
                    }
                pts0.Clear();
                pts0 = pts1;
                pts1 = new List<Point3D>();
            }
        }
    }
}
