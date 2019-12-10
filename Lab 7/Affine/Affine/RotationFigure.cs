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

        public RotationFigure(List<Point3D> points)
        {
            Points = new List<Point3D>(points);
        }

        public RotationFigure(List<Point3D> startPoints, Axis axis, int count)
        {
            Polygons = new List<Polygon>();
            List<Point3D> rotatedPoints = new List<Point3D>();
            //Points = new List<Point3D>();
            //Polygons = new List<Polygon>();
            //Points.AddRange(startPoints);
            float angle = 360f / count;

            var rot_line = new Edge(new Point3D(0, 0, 0), new Point3D(0, 1, 0));

            float Ax = rot_line.First.X, Ay = rot_line.First.Y, Az = rot_line.First.Z;

            foreach (var p in startPoints)
            {
                //p.translate(-Ax, -Ay, -Az);
                rotatedPoints.Add(new Point3D(p.X, p.Y, p.Z));
            }
                

            for (int i = 0; i < count; ++i)
            {
                foreach (var p in rotatedPoints)
                    p.rotate(angle, axis);

                for (int j = 1; j < startPoints.Count; ++j)
                {
                    Polygon f = new Polygon(
                                    new List<Point3D>()
                                    { 
                                        new Point3D(startPoints[j - 1]), 
                                        new Point3D(rotatedPoints[j - 1]),
                                        new Point3D(rotatedPoints[j]), 
                                        new Point3D(startPoints[j])
                                    });

                    Polygons.Add(f);
                }

                foreach (var p in startPoints)
                    p.rotate(angle, axis);
            }


            //foreach (var f in Polygons)
            //    f.translate(Ax, Ay, Az);

            //Points = new List<Point3D>();
            //Polygons = new List<Polygon>();
            //Points.AddRange(startPoints);
            //List<Point3D> rotatedPoints = new List<Point3D>();

            //var startCount = startPoints.Count;

            //for (int i = 0; i < count; ++i)
            //{
            //    double angle = 360f / (count);
            //    for (int j = 0; j < Points.Count; j++)
            //    //for (int i1 = 0; i1 < startPoints.Count; i1++)
            //    {
            //        //if (j >= (Points.Count - startCount - 5))
            //        {
            //            Point3D point = Points[j];
            //            //Point3D point = startPoints[i1];
            //            var newPoint = point.rotateNewPoint(point, angle, axis);
            //            //point = point.rotateNewPoint(point, angle, axis);
            //            rotatedPoints.Add(newPoint);
            //            //startPoints[i1] = point;
            //        }
            //    }
            //    Points.AddRange(rotatedPoints);
            //    rotatedPoints.Clear();
            //}

            //Points.Add(Points[0]);
            //Points.Add(Points[1]);

            //var n = startPoints.Count;
            //for (int k = 0; k < n - 1; k++)
            //{
            //    for (int i = 0; i <= Points.Count; i += 4)
            //    {
            //        if (i != Points.Count - 2)// - 3)  -2
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
            //        //else
            //        //{
            //        //    Polygons.Add(
            //        //        new Polygon(
            //        //            new List<Point3D>
            //        //                {
            //        //                Points[i],
            //        //                Points[i+1],
            //        //                Points[0],
            //        //                Points[1],
            //        //                Points[1+1],
            //        //                Points[1]
            //        //                }
            //        //        )
            //        //     );
            //        //}
            //    }

            //}
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