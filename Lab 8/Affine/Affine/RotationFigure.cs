using System.Collections.Generic;
using System.Drawing;

namespace Affine
{
    class RotationFigure: Polyhedron
    {
        public List<Point3D> Points { get; }

        public RotationFigure(List<Point3D> points)
        {
            Points = new List<Point3D>(points);
        }

        public RotationFigure(List<Point3D> startPoints, Axis axis, int count)
        {
            Polygons = new List<Polygon>();
            List<Point3D> rotatedPoints = new List<Point3D>();
            float angle = 360f / count;
            
            foreach (var p in startPoints)
            {
                rotatedPoints.Add(new Point3D(p.X, p.Y, p.Z));
            }                

            for (int i = 0; i < count; ++i)
            {
                foreach (var p in rotatedPoints)
                    p.rotate(angle, axis);

                for (int j = 1; j < startPoints.Count; ++j)
                {
                    Polygon p = new Polygon(
                                    new List<Point3D>()
                                    { 
                                        new Point3D(startPoints[j - 1]), 
                                        new Point3D(rotatedPoints[j - 1]),
                                        new Point3D(rotatedPoints[j]), 
                                        new Point3D(startPoints[j])
                                    });

                    Polygons.Add(p);
                }

                foreach (var p in startPoints)
                    p.rotate(angle, axis);
            }
        }

        public new void Show(Graphics g, Projection pr = 0, Pen pen = null)
        {
            foreach (Polygon f in Polygons)
            {
                f.Show(g, pr, pen);
            }
        }

    }
}

//-40,40,20
//-40,-40,20
//-40,-80,20

//40,80,0
//120,100,0
//40,120,0

//10,20,0
//10,50,0
//70,50,0
//70,20,0

//60,120,0
//40,100,0
//40,80,0
//60,60,0
//60,40,0
//40,20,0

//130,250,0
//80,200,0
//80,160,0
//140,140,0
//140,100,0
//80,60,0