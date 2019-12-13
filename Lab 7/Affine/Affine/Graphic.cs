using System;
using System.Collections.Generic;
using System.Drawing;

namespace Affine
{
    class Graphic : Polyhedron
    {
        public Func<double, double, double> F;
        public int X0 { get; }
        public int X1 { get; }
        public int Y0 { get; }
        public int Y1 { get; }
        public int CountOfSplits { get; }

        private int xx1, xx2, yy1, yy2;
        private int[] xx = new int[4];
        private int[] yy = new int[4];
        public int left=20;
        public int top=20;
        public int width=300;
        public int height=300;

        public double alfa=10, beta=12;
        public double x0=0, y0=0, z0=0;
        public double A=8;
        public bool f_show=false;

        public Graphic(int x0, int x1, int y0, int y1, int count, int func)
        {
            X0 = x0;
            X1 = x1;
            Y0 = y0;
            Y1 = y1;
            CountOfSplits = count;
            Polygons = new List<Polygon>();

            float dx = (Math.Abs(x0) + Math.Abs(x1)) / (float)count;
            float dy = (Math.Abs(y0) + Math.Abs(y1)) / (float)count;

            List<Point3D> points0 = new List<Point3D>();
            List<Point3D> points = new List<Point3D>();

            switch (func)
            {
                case 0:
                    F = (x, y) => x + y;
                    break;
                case 1:
                    F = (x, y) => (float)Math.Cos(x * x + y * y);
                    break;
                case 2:
                    F = (x, y) => (float)Math.Sin(x) * 3f + (float)Math.Cos(y) * 3f;
                    break;
                case 3:
                    F = (x, y) => (float)Math.Sin(x) * 5f;
                    break;
                default:
                    F = (x, y) => x + y;
                    break;
            }

            //for (float x = x0; x < x1; x += dx)
            //{
            //    for (float y = y0; y < y1; y += dy)
            //    {
            //        var xi = X0 + (x / count) * (X1 - X0);
            //        var yj = Y0 + (x / count) * (Y1 - Y0);
            //        float z = F(xi, yj);
            //    }
            //}

            for (float x = x0; x < x1; x += dx)
            {
                for (float y = y0; y < y1; y += dy)
                {
                    var z = F(x, y);
                    points.Add(new Point3D(x, y, (float)z));
                }

                if (points0.Count != 0)
                    for (int i = 1; i < points0.Count; ++i)
                    {
                        Polygons.Add(new Polygon(new List<Point3D>() 
                        {
                                new Point3D(points0[i - 1]), 
                                new Point3D(points[i - 1]),
                                new Point3D(points[i]), 
                                new Point3D(points0[i])
                        }));
                    }
                points0.Clear();
                points0 = points;
                points = new List<Point3D>();
            }

        }

        public void ShowG(Graphics g, Projection pr = 0, Pen pen = null)
        {
            const double h = 0.1;
            const double h0 = 0;

            System.Drawing.Rectangle r1 = new Rectangle(left, top, left + width, top + height);
            Pen p = new Pen(Color.Black);
            g.DrawRectangle(p, r1);

            // Создать шрифт
            Font font = new Font("Courier New", 12, FontStyle.Bold);
            SolidBrush b = new SolidBrush(Color.Blue);

            // рисование осей
            // ось X
            Zoom_XY(0, 0, 0, out xx1, out yy1);
            Zoom_XY(1.2, 0, 0, out xx2, out yy2);
            g.DrawLine(p, xx1, yy1, xx2, yy2);
            g.DrawString("X", font, b, xx2 + 3, yy2);

            // ось Y
            Zoom_XY(0, 0, 0, out xx1, out yy1);
            Zoom_XY(0, 1.2, 0, out xx2, out yy2);
            g.DrawLine(p, xx1, yy1, xx2, yy2);
            g.DrawString("Y", font, b, xx2 + 3, yy2);

            // ось Z
            Zoom_XY(0, 0, 0, out xx1, out yy1);
            Zoom_XY(0, 0, 1.2, out xx2, out yy2);
            g.DrawLine(p, xx1, yy1, xx2, yy2);
            g.DrawString("Z", font, b, xx2 + 3, yy2 - 3);

            // рисование поверхности
            p.Color = Color.Red;
            p.Width = 1;

            float dx = (Math.Abs(X0) + Math.Abs(X1)) / CountOfSplits;
            float dy = (Math.Abs(Y0) + Math.Abs(Y1)) / CountOfSplits;

            for (float j = X0; j <= X1; j += dx)
                for (float i = Y0; i <= Y1; i += dy)
                {
                    Zoom_XY(h0 + h * i, h0 + h * j, F(h0 + h * i, h0 + h * j),
                            out xx[0], out yy[0]);
                    Zoom_XY(h0 + h * i, h + h * j, F(h0 + h * i, h + h * j),
                            out xx[1], out yy[1]);
                    Zoom_XY(h + h * i, h + h * j, F(h + h * i, h + h * j),
                            out xx[2], out yy[2]);

                    Zoom_XY(h + h * i, h0 + h * j, F(h + h * i, h0 + h * j),
                            out xx[3], out yy[3]);

                    g.DrawLine(p, xx[0], yy[0], xx[1], yy[1]);
                    g.DrawLine(p, xx[1], yy[1], xx[2], yy[2]);
                    g.DrawLine(p, xx[2], yy[2], xx[3], yy[3]);
                    g.DrawLine(p, xx[3], yy[3], xx[0], yy[0]);
                }
        }

        private void Zoom_XY(double x, double y, double z, out int xx, out int yy)
        {
            double xn, yn, zn;
            double tx, ty, tz;

            tx = (x - x0) * Math.Cos(alfa) - (y - y0) * Math.Sin(alfa);
            ty = ((x - x0) * Math.Sin(alfa) + (y - y0) * Math.Cos(alfa)) * Math.Cos(beta) -
                 (z - z0) * Math.Sin(beta);
            tz = ((x - x0) * Math.Sin(alfa) + (y - y0) * Math.Cos(alfa)) * Math.Sin(beta) +
                 (z - z0) * Math.Cos(beta);

            xn = tx / (tz / A + 1);
            yn = ty / (ty / A + 1);

            xx = (int)(width * (xn - X0) / (X1 - X0));
            yy = (int)(height * (yn - Y1) / (Y0 - Y1));
        }
    }
}
