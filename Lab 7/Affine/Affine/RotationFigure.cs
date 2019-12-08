using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affine
{
    class RotationFigure: Polyhedron
    {
        public List<Point3D> Points { get; }
        public new List<Polygon> Polygons { get; }

        public RotationFigure(List<Point3D> points)
        {
            Points = new List<Point3D>(points);
        }

        public RotationFigure(List<Point3D> startPoints, Axis axis, int density)
        {
            Points = new List<Point3D>();
            Polygons = new List<Polygon>();
            Points.AddRange(startPoints);
            List<Point3D> rotatedPoints = new List<Point3D>();

            for (int i = 0; i < density; ++i)
            {
                double angle = 360f / (density);
                //foreach (var point in Points)
                for (int i1 = 0; i1 < startPoints.Count; i1++)
                {
                    Point3D point = startPoints[i1];
                    //var newPoint = point.rotateNewPoint(point, angle, axis);
                    point = point.rotateNewPoint(point, angle, axis);
                    rotatedPoints.Add(point);
                    startPoints[i1] = point;
                }
                Points.AddRange(rotatedPoints);
                rotatedPoints.Clear();
            }

            //Points.Add(Points[0]);
            //Points.Add(Points[1]);


            //for (int k = 0; k < n - 1; k++)
            //{
            //    for (int i = 0; i <= Points.Count; i += 4)
            //    {
            //        if (i != Points.Count - 2)
            //        {
            //            Polygons.Add(
            //                new Polygon(
            //                    new List<Point3D>
            //                        {
            //                        Points[i],
            //                        Points[i+1],
            //                        Points[i+2],
            //                        Points[i+3],
            //                        Points[i+1],
            //                        Points[i+3]
            //                        }
            //                )
            //             );
            //        }
            //    }
            //}

            var n = startPoints.Count;
            for (int i = 0; i < density; ++i)
                for (int j = 0; j < n - 1; ++j)
                    Polygons.Add(new Polygon(new List<Point3D> {
                        Points[i * n + j], Points[(i + 1) % (density - 1) * n + j],
                        Points[(i + 1) % (density - 1) * n + j + 1], Points[i * n + j + 1] }));
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


//else
//{
//    Polygons.Add(
//        new Polygon(
//            new List<Point3D>
//                {
//                    Points[i],
//                    Points[i+1],
//                    Points[0],
//                    Points[1],
//                    Points[1+1],
//                    Points[1]
//                }
//        )
//     );
//}