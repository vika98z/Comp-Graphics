using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace Affine
{
    public class Polygon
    {
        public List<Point3D> Points { get; }
        public Point3D Center { get; set; } = new Point3D(0, 0, 0);
        public List<float> Normal { get; set; }
        public bool IsVisible { get; set; }
        public Polygon(Polygon face)
        {
            Points = face.Points.Select(pt => new Point3D(pt.X, pt.Y, pt.Z)).ToList();
            Center = new Point3D(face.Center);
        }

        public Polygon(string s)
        {
            Points = new List<Point3D>();

            var arr = s.Split(' ');

            for (int i = 0; i < arr.Length; i += 3)
            {
                if (string.IsNullOrEmpty(arr[i]))
                    continue;
                float x = (float)Math.Truncate(float.Parse(arr[i], CultureInfo.InvariantCulture));
                float y = (float)Math.Truncate(float.Parse(arr[i + 1], CultureInfo.InvariantCulture));
                float z = (float)Math.Truncate(float.Parse(arr[i + 2], CultureInfo.InvariantCulture));
                Point3D p = new Point3D(x, y, z);
                Points.Add(p);
            }
            UpdateCenter();
        }

        public Polygon(List<Point3D> pts = null)
        {
            if (pts != null)
            {
                Points = new List<Point3D>(pts);
                UpdateCenter();
            }
        }

        private void UpdateCenter()
        {
            Center.X = 0;
            Center.Y = 0;
            Center.Z = 0;
            foreach (Point3D p in Points)
            {
                Center.X += p.X;
                Center.Y += p.Y;
                Center.Z += p.Z;
            }
            Center.X /= Points.Count;
            Center.Y /= Points.Count;
            Center.Z /= Points.Count;
        }

        public void reflectX()
        {
            Center.X = -Center.X;
            if (Points != null)
                foreach (var p in Points)
                    p.reflectX();
        }
        public void reflectY()
        {
            Center.Y = -Center.Y;
            if (Points != null)
                foreach (var p in Points)
                    p.reflectY();
        }
        public void reflectZ()
        {
            Center.Z = -Center.Z;
            if (Points != null)
                foreach (var p in Points)
                    p.reflectZ();
        }

        public List<PointF> make_perspective(float k = 1000, float z_camera = 1000)
        {
            List<PointF> res = new List<PointF>();

            foreach (Point3D p in Points)
            {
                res.Add(p.make_perspective(k));
            }
            return res;
        }

        public List<PointF> make_isometric()
        {
            List<PointF> res = new List<PointF>();

            foreach (Point3D p in Points)
                res.Add(p.make_isometric());

            return res;
        }

        public List<PointF> make_orthographic(Axis a)
        {
            List<PointF> res = new List<PointF>();

            foreach (Point3D p in Points)
                res.Add(p.make_orthographic(a));

            return res;
        }

        public void Show(Graphics g, Projection pr = 0, Pen pen = null)
        {
            if (pen == null)
                pen = Pens.Black;

            List<PointF> pts;

            find_normal(Center, new Edge(new Point3D(0, 0, 500), new Point3D(0, 0, 500)));

            if (IsVisible)
            {
                switch (pr)
                {
                    case Projection.ISOMETRIC:
                        pts = make_isometric();
                        break;
                    case Projection.ORTHOGR_X:
                        pts = make_orthographic(Axis.AXIS_X);
                        break;
                    case Projection.ORTHOGR_Y:
                        pts = make_orthographic(Axis.AXIS_Y);
                        break;
                    case Projection.ORTHOGR_Z:
                        pts = make_orthographic(Axis.AXIS_Z);
                        break;
                    default:
                        pts = make_perspective(1000);
                        break;
                }

                if (pts.Count > 1)
                {
                    g.DrawLines(pen, pts.ToArray());
                    g.DrawLine(pen, pts[0], pts[pts.Count - 1]);
                }
                else if (pts.Count == 1)
                    g.DrawRectangle(pen, pts[0].X, pts[0].Y, 1, 1);
            }
        }
        
        public void translate(float x, float y, float z)
        {
            foreach (Point3D p in Points)
                p.translate(x, y, z);
            UpdateCenter();
        }

        public void rotate(double angle, Axis a, Edge line = null)
        {
            foreach (Point3D p in Points)
                p.rotate(angle, a, line);
            UpdateCenter();
        }

        public void scale(float kx, float ky, float kz)
        {
            foreach (Point3D p in Points)
                p.Scale(kx, ky, kz);
            UpdateCenter();
        }

        //NORMAL VECTOR
        public void find_normal(Point3D p_center, Edge camera)
        {


            //Point3D Q = Points[1], R = Points[2], S = Points[0];
            //List<float> QR = new List<float> { R.X - Q.X, R.Y - Q.Y, R.Z - Q.Z };
            //List<float> QS = new List<float> { S.X - Q.X, S.Y - Q.Y, S.Z - Q.Z };

            //Normal = new List<float> { QR[1] * QS[2] - QR[2] * QS[1],
            //                           -(QR[0] * QS[2] - QR[2] * QS[0]),
            //                           QR[0] * QS[1] - QR[1] * QS[0] };

            //List<float> CQ = new List<float> { Q.X - p_center.X, Q.Y - p_center.Y, Q.Z - p_center.Z };
            //if (Point3D.mul_matrix(Normal, 1, 3, CQ, 3, 1)[0] > 1E-6)
            //{
            //    Normal[0] *= -1;
            //    Normal[1] *= -1;
            //    Normal[2] *= -1;
            //}

            //Point3D E = camera.First;
            //Point3D E = new Point3D(0, 0, 100);
            //List<float> CE = new List<float> { E.X - Center.X, E.Y - Center.Y, E.Z - Center.Z };
            //float dot_product = Point3D.mul_matrix(Normal, 1, 3, CE, 3, 1)[0];
            //IsVisible = Math.Abs(dot_product) < 1E-6 || dot_product < 0;
        }
    }
}
