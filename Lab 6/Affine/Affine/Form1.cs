using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affine
{
    public partial class Form1 : Form
    {
        Polyhedron Figure = new Polyhedron();
        Point Center = new Point(360,360);

        Graphics g;

        int projection = -1, revertId = -1, rotateAroundLine = -1;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        }

        void Tetrahedron()
        {
            int size = 150;
            float h = (float)(size * Math.Sqrt(3));
            List<Edge> Edges = new List<Edge>();
            MPoint p1 = new MPoint(-size, -h / 2, -h / 2);
            MPoint p2 = new MPoint(size, -h / 2, -h / 2);
            MPoint p3 = new MPoint(0, -h / 2, h / 2);
            MPoint p4 = new MPoint(0, h / 2, 0);

            Edge e1 = new Edge(p1, p2);
            Edge e2 = new Edge(p2, p3);
            Edge e3 = new Edge(p3, p1);
            Edge e4 = new Edge(p1, p4);
            Edge e5 = new Edge(p2, p4);
            Edge e6 = new Edge(p3, p4);

            Polygon f1 = new Polygon();
            f1.Edges.Add(e1);
            f1.Edges.Add(e2);
            f1.Edges.Add(e3);

            Polygon f2 = new Polygon();
            f2.Edges.Add(e1);
            f2.Edges.Add(e4);
            f2.Edges.Add(e5);

            Polygon f3 = new Polygon();
            f3.Edges.Add(e2);
            f3.Edges.Add(e5);
            f3.Edges.Add(e6);

            Polygon f4 = new Polygon();
            f4.Edges.Add(e3);
            f4.Edges.Add(e4);
            f4.Edges.Add(e6);

            List<Polygon> polygons = new List<Polygon>();
            polygons.Add(f1);
            polygons.Add(f2);
            polygons.Add(f3);
            polygons.Add(f4);

            Figure = new Polyhedron(polygons);

            DrawPolyhedron(Figure);
        }

        void Hexahedron()
        {
            int size = 300;

            MPoint p1 = new MPoint(-size / 2, -size / 2, -size / 2);
            MPoint p2 = new MPoint(size / 2, -size / 2, -size / 2);
            MPoint p3 = new MPoint(size / 2, -size / 2, size / 2);
            MPoint p4 = new MPoint(-size / 2, -size / 2, size / 2);
            MPoint p5 = new MPoint(-size / 2, size / 2, -size / 2);
            MPoint p6 = new MPoint(size / 2, size / 2, -size / 2);
            MPoint p7 = new MPoint(size / 2, size / 2, size / 2);
            MPoint p8 = new MPoint(-size / 2, size / 2, size / 2);

            Edge e1 = new Edge(p1, p2);
            Edge e2 = new Edge(p2, p3);
            Edge e3 = new Edge(p3, p4);
            Edge e4 = new Edge(p1, p4);
            Edge e5 = new Edge(p5, p6);
            Edge e6 = new Edge(p6, p7);
            Edge e7 = new Edge(p7, p8);
            Edge e8 = new Edge(p5, p8);
            Edge e9 = new Edge(p4, p8);
            Edge e10 = new Edge(p1, p5);
            Edge e11 = new Edge(p2, p6);
            Edge e12 = new Edge(p3, p7);

            Polygon f1 = new Polygon();
            f1.Edges.Add(e1);
            f1.Edges.Add(e2);
            f1.Edges.Add(e3);
            f1.Edges.Add(e4);

            Polygon f2 = new Polygon();
            f2.Edges.Add(e5);
            f2.Edges.Add(e6);
            f2.Edges.Add(e7);
            f2.Edges.Add(e8);

            Polygon f3 = new Polygon();
            f3.Edges.Add(e4);
            f3.Edges.Add(e10);
            f3.Edges.Add(e8);
            f3.Edges.Add(e9);

            Polygon f4 = new Polygon();
            f4.Edges.Add(e10);
            f4.Edges.Add(e1);
            f4.Edges.Add(e11);
            f4.Edges.Add(e5);

            Polygon f5 = new Polygon();
            f5.Edges.Add(e6);
            f5.Edges.Add(e11);
            f5.Edges.Add(e2);
            f5.Edges.Add(e12);

            Polygon f6 = new Polygon();
            f6.Edges.Add(e7);
            f6.Edges.Add(e12);
            f6.Edges.Add(e3);
            f6.Edges.Add(e9);

            List<Polygon> polygons = new List<Polygon>();
            polygons.Add(f1);
            polygons.Add(f2);
            polygons.Add(f3);
            polygons.Add(f4);
            polygons.Add(f5);
            polygons.Add(f6);

            Figure = new Polyhedron(polygons);

            DrawPolyhedron(Figure);
        }

        void Oktahedron()
        {
            int size = 300;
            MPoint p1 = new MPoint(-size / 2, 0, 0);
            MPoint p2 = new MPoint(0, 0, -size / 2);
            MPoint p3 = new MPoint(size / 2, 0, 0);
            MPoint p4 = new MPoint(0, 0, size / 2);
            MPoint p5 = new MPoint(0, size / 2, 0);
            MPoint p6 = new MPoint(0, -size / 2, 0);

            Edge e1 = new Edge(p1, p2);
            Edge e2 = new Edge(p2, p3);
            Edge e3 = new Edge(p3, p4);
            Edge e4 = new Edge(p1, p4);
            Edge e5 = new Edge(p1, p5);
            Edge e6 = new Edge(p2, p5);
            Edge e7 = new Edge(p3, p5);
            Edge e8 = new Edge(p4, p5);
            Edge e9 = new Edge(p1, p6);
            Edge e10 = new Edge(p2, p6);
            Edge e11 = new Edge(p3, p6);
            Edge e12 = new Edge(p4, p6);

            Polygon f1 = new Polygon();
            f1.Edges.Add(e1);
            f1.Edges.Add(e6);
            f1.Edges.Add(e5);

            Polygon f2 = new Polygon();
            f2.Edges.Add(e2);
            f2.Edges.Add(e7);
            f2.Edges.Add(e6);

            Polygon f3 = new Polygon();
            f3.Edges.Add(e3);
            f3.Edges.Add(e7);
            f3.Edges.Add(e8);

            Polygon f4 = new Polygon();
            f4.Edges.Add(e4);
            f4.Edges.Add(e8);
            f4.Edges.Add(e5);

            Polygon f5 = new Polygon();
            f5.Edges.Add(e1);
            f5.Edges.Add(e9);
            f5.Edges.Add(e10);

            Polygon f6 = new Polygon();
            f6.Edges.Add(e2);
            f6.Edges.Add(e10);
            f6.Edges.Add(e11);

            Polygon f7 = new Polygon();
            f7.Edges.Add(e3);
            f7.Edges.Add(e11);
            f7.Edges.Add(e12);

            Polygon f8 = new Polygon();
            f1.Edges.Add(e4);
            f1.Edges.Add(e9);
            f1.Edges.Add(e12);

            List<Polygon> polygons = new List<Polygon>();
            polygons.Add(f1);
            polygons.Add(f2);
            polygons.Add(f3);
            polygons.Add(f4);
            polygons.Add(f5);
            polygons.Add(f6);
            polygons.Add(f7);
            polygons.Add(f8);

            Figure = new Polyhedron(polygons);

            DrawPolyhedron(Figure);
        }

        private void Icosahedron()
        {
            float size = 200;
            var magicAngle = Math.PI * 26.565f / 180;
            var segmentAngle = Math.PI * 72 / 180;
            var currentAngle = 0f;

            var v = new MPoint[12];
            v[0] = new MPoint(0, size, 0);
            v[11] = new MPoint(0, -size, 0);

            for (var i = 1; i < 6; i++)
            {
                v[i] = new MPoint((float)(size * Math.Sin(currentAngle) * Math.Cos(magicAngle)),
                    (float)(size * Math.Sin(magicAngle)), (float)(size * Math.Cos(currentAngle) * Math.Cos(magicAngle)));
                currentAngle += (float)segmentAngle;
            }
            currentAngle = (float)Math.PI * 36 / 180;
            for (var i = 6; i < 11; i++)
            {
                v[i] = new MPoint((float)(size * Math.Sin(currentAngle) * Math.Cos(-magicAngle)),
                    (float)(size * Math.Sin(-magicAngle)), (float)(size * Math.Cos(currentAngle) * Math.Cos(-magicAngle)));
                currentAngle += (float)segmentAngle;
            }

            Edge e1 = new Edge(v[0], v[1]);
            Edge e2 = new Edge(v[1], v[2]);
            Edge e3 = new Edge(v[0], v[2]);

            Edge e4 = new Edge(v[0], v[2]);
            Edge e5 = new Edge(v[2], v[3]);
            Edge e6 = new Edge(v[0], v[3]);

            Edge e7 = new Edge(v[0], v[3]);
            Edge e8 = new Edge(v[3], v[4]);
            Edge e9 = new Edge(v[0], v[4]);

            Edge e10 = new Edge(v[0], v[4]);
            Edge e11 = new Edge(v[4], v[5]);
            Edge e12 = new Edge(v[0], v[5]);

            Edge e13 = new Edge(v[0], v[5]);
            Edge e14 = new Edge(v[5], v[1]);
            Edge e15 = new Edge(v[0], v[1]);

            Edge e16 = new Edge(v[11], v[7]);
            Edge e17 = new Edge(v[7], v[6]);
            Edge e18 = new Edge(v[11], v[6]);

            Edge e19 = new Edge(v[11], v[8]);
            Edge e20 = new Edge(v[8], v[7]);
            Edge e21 = new Edge(v[11], v[7]);

            Edge e22 = new Edge(v[11], v[9]);
            Edge e23 = new Edge(v[9], v[8]);
            Edge e24 = new Edge(v[11], v[8]);

            Edge e25 = new Edge(v[11], v[10]);
            Edge e26 = new Edge(v[10], v[9]);
            Edge e27 = new Edge(v[11], v[9]);

            Edge e28 = new Edge(v[11], v[6]);
            Edge e29 = new Edge(v[6], v[10]);
            Edge e30 = new Edge(v[11], v[10]);

            Edge e31 = new Edge(v[2], v[1]);
            Edge e32 = new Edge(v[1], v[6]);
            Edge e33 = new Edge(v[2], v[6]);

            Edge e34 = new Edge(v[3], v[2]);
            Edge e35 = new Edge(v[2], v[7]);
            Edge e36 = new Edge(v[3], v[7]);

            Edge e37 = new Edge(v[4], v[3]);
            Edge e38 = new Edge(v[3], v[8]);
            Edge e39 = new Edge(v[4], v[8]);

            Edge e40 = new Edge(v[5], v[4]);
            Edge e41 = new Edge(v[4], v[9]);
            Edge e42 = new Edge(v[5], v[9]);

            Edge e43 = new Edge(v[1], v[5]);
            Edge e44 = new Edge(v[5], v[10]);
            Edge e45 = new Edge(v[1], v[10]);

            Edge e46 = new Edge(v[6], v[7]);
            Edge e47 = new Edge(v[7], v[2]);
            Edge e48 = new Edge(v[6], v[2]);

            Edge e49 = new Edge(v[7], v[8]);
            Edge e50 = new Edge(v[8], v[3]);
            Edge e51 = new Edge(v[7], v[3]);

            Edge e52 = new Edge(v[8], v[9]);
            Edge e53 = new Edge(v[9], v[4]);
            Edge e54 = new Edge(v[8], v[4]);

            Edge e55 = new Edge(v[9], v[10]);
            Edge e56 = new Edge(v[10], v[5]);
            Edge e57 = new Edge(v[9], v[5]);

            Edge e58 = new Edge(v[10], v[6]);
            Edge e59 = new Edge(v[6], v[1]);
            Edge e60 = new Edge(v[10], v[1]);

            Polygon f1 = new Polygon();
            f1.Edges.Add(e1);
            f1.Edges.Add(e2);
            f1.Edges.Add(e3);

            Polygon f2 = new Polygon();
            f2.Edges.Add(e4);
            f2.Edges.Add(e5);
            f2.Edges.Add(e6);

            Polygon f3 = new Polygon();
            f3.Edges.Add(e7);
            f3.Edges.Add(e8);
            f3.Edges.Add(e9);

            Polygon f4 = new Polygon();
            f4.Edges.Add(e10);
            f4.Edges.Add(e11);
            f4.Edges.Add(e12);

            Polygon f5 = new Polygon();
            f5.Edges.Add(e13);
            f5.Edges.Add(e14);
            f5.Edges.Add(e15);

            Polygon f6 = new Polygon();
            f6.Edges.Add(e16);
            f6.Edges.Add(e17);
            f6.Edges.Add(e18);

            Polygon f7 = new Polygon();
            f7.Edges.Add(e19);
            f7.Edges.Add(e20);
            f7.Edges.Add(e21);

            Polygon f8 = new Polygon();
            f8.Edges.Add(e22);
            f8.Edges.Add(e23);
            f8.Edges.Add(e24);

            Polygon f9 = new Polygon();
            f9.Edges.Add(e25);
            f9.Edges.Add(e26);
            f9.Edges.Add(e27);

            Polygon f10 = new Polygon();
            f10.Edges.Add(e28);
            f10.Edges.Add(e29);
            f10.Edges.Add(e30);

            Polygon f11 = new Polygon();
            f11.Edges.Add(e31);
            f11.Edges.Add(e32);
            f11.Edges.Add(e33);

            Polygon f12 = new Polygon();
            f12.Edges.Add(e34);
            f12.Edges.Add(e35);
            f12.Edges.Add(e36);

            Polygon f13 = new Polygon();
            f13.Edges.Add(e37);
            f13.Edges.Add(e38);
            f13.Edges.Add(e39);

            Polygon f14 = new Polygon();
            f14.Edges.Add(e40);
            f14.Edges.Add(e41);
            f14.Edges.Add(e42);

            Polygon f15 = new Polygon();
            f15.Edges.Add(e43);
            f15.Edges.Add(e44);
            f15.Edges.Add(e45);

            Polygon f16 = new Polygon();
            f16.Edges.Add(e46);
            f16.Edges.Add(e47);
            f16.Edges.Add(e48);

            Polygon f17 = new Polygon();
            f17.Edges.Add(e49);
            f17.Edges.Add(e50);
            f17.Edges.Add(e51);

            Polygon f18 = new Polygon();
            f18.Edges.Add(e52);
            f18.Edges.Add(e53);
            f18.Edges.Add(e54);

            Polygon f19 = new Polygon();
            f19.Edges.Add(e55);
            f19.Edges.Add(e56);
            f19.Edges.Add(e57);

            Polygon f20 = new Polygon();
            f20.Edges.Add(e58);
            f20.Edges.Add(e59);
            f20.Edges.Add(e60);

            List<Polygon> polygons = new List<Polygon>();
            polygons.Add(f1);
            polygons.Add(f2);
            polygons.Add(f3);
            polygons.Add(f4);
            polygons.Add(f5);
            polygons.Add(f6);
            polygons.Add(f7);
            polygons.Add(f8);
            polygons.Add(f9);
            polygons.Add(f10);
            polygons.Add(f11);
            polygons.Add(f12);
            polygons.Add(f13);
            polygons.Add(f14);
            polygons.Add(f15);
            polygons.Add(f16);
            polygons.Add(f17);
            polygons.Add(f18);
            polygons.Add(f19);
            polygons.Add(f20);

            Figure = new Polyhedron(polygons);

            DrawPolyhedron(Figure);
        }

        void DrawPolyhedron(Polyhedron Pol)
        {
            g.Clear(Color.White);

            foreach (Polygon p in Pol.Facets)
            {
                DrawPolygon(p);
            }

            void DrawPolygon(Polygon p)
            {
                foreach (Edge e in p.Edges)
                {
                    g.DrawLine(new Pen(Color.Red), (int)e.First.X + Center.X,
                        (int)e.First.Y + Center.Y, (int)e.Second.X + Center.X,
                        (int)e.Second.Y + Center.Y);
                }
            }

            pictureBox1.Invalidate();
        }

        static public List<float> mulMatrix(List<float> matr1, int m1, int n1, List<float> matr2, int m2, int n2)
        {
            if (n1 != m2)
                return new List<float>();
            int l = m1;
            int m = n1;
            int n = n2;

            List<float> c = new List<float>();
            for (int i = 0; i < l * n; ++i)
                c.Add(0f);

            for (int i = 0; i < l; ++i)
                for (int j = 0; j < n; ++j)
                {
                    for (int r = 0; r < m; ++r)
                        c[i * l + j] += matr1[i * m1 + r] * matr2[r * n2 + j];
                }
            return c;
        }

        private MPoint Translate(MPoint Point, MPoint Offset)
        {
            int offsetX = (int)numericUpDown1.Value, offsetY = (int)numericUpDown2.Value, offsetZ = (int)numericUpDown3.Value;
            if (Offset.X != 0 && Offset.Y != 0 && Offset.Z != 0)
            {
                offsetX = (int)Offset.X;
                offsetY = (int)Offset.Y;
                offsetZ = (int)Offset.Z;
            }

            List<float> Matrix = new List<float> { 1, 0, 0, 0,
                                              0, 1, 0, 0,
                                              0, 0, 1, 0,
                                              offsetX, offsetY, offsetZ, 1 };
            List<float> xyz = new List<float> { Point.X, Point.Y, Point.Z, 1 };

            List<float> c = mulMatrix(xyz, 1, 4, Matrix, 4, 4);

            return new MPoint((float)c[0], (float)c[1], (float)c[2]);
        }

        private MPoint RotateOX( MPoint Point)
        {
            int rotateAngleX = (int)numericUpDown4.Value;
            var angleX = (rotateAngleX / 180D) * Math.PI;
            double[] resultVector = new double[4];

            List<float> MatrixOX = new List<float> { 1, 0,  0,  0,
                                              0,  (float)Math.Cos(angleX), (float)-Math.Sin(angleX),  0,
                                              0,  (float)Math.Sin(angleX),  (float)Math.Cos(angleX), 0,
                                              0,  0,  0,  1 };
            List<float> xyz = new List<float> { Point.X, Point.Y, Point.Z, 1 };

            List<float> c = mulMatrix(xyz, 1, 4, MatrixOX, 4, 4);

            return new MPoint((float)c[0], (float)c[1], (float)c[2]);
        }

        private MPoint RotateOY(MPoint Point)
        {
            int rotateAngleY = (int)numericUpDown5.Value;
            var angleY = (rotateAngleY / 180D) * Math.PI;

            List<float> MatrixOX = new List<float> { (float)Math.Cos(angleY), 0,  (float)Math.Sin(angleY),  0,
                                              0,  1, 0,  0,
                                              (float)-Math.Sin(angleY),  0,  (float)Math.Cos(angleY), 0,
                                              0,  0,  0,  1 };
            List<float> xyz = new List<float> { Point.X, Point.Y, Point.Z, 1 };

            List<float> c = mulMatrix(xyz, 1, 4, MatrixOX, 4, 4);

            return new MPoint((float)c[0], (float)c[1], (float)c[2]);
        }

        private MPoint RotateOZ(MPoint Point)
        {
            int rotateAngleZ = (int)numericUpDown6.Value;
            var angleZ = (rotateAngleZ / 180D) * Math.PI;

            
            List<float> MatrixOX = new List<float> { (float)Math.Cos(angleZ), (float)-Math.Sin(angleZ), 0,  0,
                                              (float)Math.Sin(angleZ),  (float)Math.Cos(angleZ), 0,  0,
                                              0,  0,  1, 0,
                                              0,  0,  0,  1 };
            List<float> xyz = new List<float> { Point.X, Point.Y, Point.Z, 1 };

            List<float> c = mulMatrix(xyz, 1, 4, MatrixOX, 4, 4);

            return new MPoint((float)c[0], (float)c[1], (float)c[2]);
        }

        private MPoint Scale(MPoint Point)
        {
            float kx = (float)numericUpDown9.Value;
            float ky = (float)numericUpDown8.Value;
            float kz = (float)numericUpDown7.Value;

            List<float> D = new List<float> { kx, 0,  0,  0,
                                              0,  ky, 0,  0,
                                              0,  0,  kz, 0,
                                              0,  0,  0,  1 };
            List<float> xyz = new List<float> { Point.X, Point.Y, Point.Z, 1 };

            List<float> c = mulMatrix(xyz, 1, 4, D, 4, 4);
            return new MPoint(c[0], c[1], c[2]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Figure.Facets.Count; i++)
            {
                Polygon p = Figure.Facets[i];
                for (int i1 = 0; i1 < p.Edges.Count; i1++)
                {
                    //TRANSLATION
                    Figure.Facets[i].Edges[i1] = new Edge(Translate(Figure.Facets[i].Edges[i1].First, new MPoint()),
                            Translate(Figure.Facets[i].Edges[i1].Second, new MPoint()));

                    //ROTATION
                    Figure.Facets[i].Edges[i1] = new Edge(RotateOX(Figure.Facets[i].Edges[i1].First),
                            RotateOX(Figure.Facets[i].Edges[i1].Second));
                    Figure.Facets[i].Edges[i1] = new Edge(RotateOY(Figure.Facets[i].Edges[i1].First),
                            RotateOY(Figure.Facets[i].Edges[i1].Second));
                    Figure.Facets[i].Edges[i1] = new Edge(RotateOZ(Figure.Facets[i].Edges[i1].First),
                            RotateOZ(Figure.Facets[i].Edges[i1].Second));

                    //SCALE
                    Figure.Facets[i].Edges[i1] = new Edge(Scale(Figure.Facets[i].Edges[i1].First),
                            Scale(Figure.Facets[i].Edges[i1].Second));
                }
            }

            Figure.PolyCenter.X += (int)numericUpDown1.Value;
            Figure.PolyCenter.Y += (int)numericUpDown2.Value;
            Figure.PolyCenter.Z += (int)numericUpDown3.Value;

            DrawPolyhedron(Figure);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Tetrahedron();
                    break;
                case 1:
                    Hexahedron();
                    break;
                case 2:
                    Oktahedron();
                    break;
                case 3:
                    Icosahedron();
                    break;
                default:
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) => projection = comboBox2.SelectedIndex;
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) => revertId = comboBox3.SelectedIndex;
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            rotateAroundLine = comboBox4.SelectedIndex;
            switch (comboBox4.SelectedIndex)
            {
                case 0:
                    checkNumeric(false);
                    break;
                case 1:
                    checkNumeric(false);
                    break;
                case 2:
                    checkNumeric(false);
                    break;
                case 3:
                    checkNumeric(true);
                    break;
                default:
                    break;
            }

            void checkNumeric(bool flag)
            {
                numericUpDown10.Enabled = flag;
                numericUpDown11.Enabled = flag;
                numericUpDown12.Enabled = flag;
                numericUpDown13.Enabled = flag;
                numericUpDown14.Enabled = flag;
                numericUpDown15.Enabled = flag;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        //CAMERA PROJECTION
        private void button1_Click_1(object sender, EventArgs e)
        {
            for (int i1 = 0; i1 < Figure.Facets.Count; i1++)
            {
                Polygon p = Figure.Facets[i1];
                for (int i2 = 0; i2 < p.Edges.Count; i2++)
                {
                    Edge ed = p.Edges[i2];

                    //ORTHOGRAPHIC OX
                    if (projection == 0)
                    {
                        List<float> P = new List<float>();
                        for (int i = 0; i < 16; ++i)
                        {
                            if (i % 5 == 0)
                                P.Add(1);
                            else
                                P.Add(0);
                        }
                        P[0] = 0;
                        P[12] = Figure.Facets[i1].Edges[i2].First.Z;
                        List<float> xyz = new List<float> { Figure.Facets[i1].Edges[i2].First.X, Figure.Facets[i1].Edges[i2].First.Y, Figure.Facets[i1].Edges[i2].First.Z, 1 };
                        List<float> c1 = mulMatrix(xyz, 1, 4, P, 4, 4);

                        P[12] = Figure.Facets[i1].Edges[i2].Second.Z;

                        List<float> xyz2 = new List<float> { Figure.Facets[i1].Edges[i2].Second.X, Figure.Facets[i1].Edges[i2].Second.Y, Figure.Facets[i1].Edges[i2].Second.Z, 1 };
                        List<float> c2 = mulMatrix(xyz2, 1, 4, P, 4, 4);

                        //Figure.Facets[i1].Edges[i2] = new Edge(new MPoint(c1[1], c1[2]),
                        //    new MPoint(c2[1], c2[2]));
                        Figure.Facets[i1].Edges[i2] = new Edge(new MPoint(c1[1], c1[2]),
                            new MPoint(c2[1], c2[2]));
                    }
                    //ORTHOGRAPHIC OY
                    else if (projection == 1)
                    {
                        List<float> P = new List<float>();

                        for (int i = 0; i < 16; ++i)
                        {
                            if (i % 5 == 0)
                                P.Add(1);
                            else
                                P.Add(0);
                        }
                        P[5] = 0;
                        List<float> xyz = new List<float> { Figure.Facets[i1].Edges[i2].First.X, Figure.Facets[i1].Edges[i2].First.Y, Figure.Facets[i1].Edges[i2].First.Z, 1 };
                        List<float> c1 = mulMatrix(xyz, 1, 4, P, 4, 4);

                        List<float> xyz2 = new List<float> { Figure.Facets[i1].Edges[i2].Second.X, Figure.Facets[i1].Edges[i2].Second.Y, Figure.Facets[i1].Edges[i2].Second.Z, 1 };
                        List<float> c2 = mulMatrix(xyz2, 1, 4, P, 4, 4);

                        Figure.Facets[i1].Edges[i2] = new Edge(new MPoint(c1[0], c1[2]),
                            new MPoint(c2[0], c2[2]));
                    }
                    //ORTHOGRAPHIC OZ
                    else if (projection == 2)
                    {
                        List<float> P = new List<float>();

                        for (int i = 0; i < 16; ++i)
                        {
                            if (i % 5 == 0)
                                P.Add(1);
                            else
                                P.Add(0);
                        }
                        P[10] = 0;
                        List<float> xyz = new List<float> { Figure.Facets[i1].Edges[i2].First.X, Figure.Facets[i1].Edges[i2].First.Y, Figure.Facets[i1].Edges[i2].First.Z, 1 };
                        List<float> c1 = mulMatrix(xyz, 1, 4, P, 4, 4);

                        List<float> xyz2 = new List<float> { Figure.Facets[i1].Edges[i2].Second.X, Figure.Facets[i1].Edges[i2].Second.Y, Figure.Facets[i1].Edges[i2].Second.Z, 1 };
                        List<float> c2 = mulMatrix(xyz2, 1, 4, P, 4, 4);

                        Figure.Facets[i1].Edges[i2] = new Edge(new MPoint(c1[0], c1[1]),
                            new MPoint(c2[0], c2[1]));
                    }
                    //ISOMETRIC
                    else if (projection == 3)
                    {
                        double r_phi = Math.Asin(Math.Tan(Math.PI * 30 / 180));
                        double r_psi = Math.PI * 45 / 180;
                        float cos_phi = (float)Math.Cos(r_phi);
                        float sin_phi = (float)Math.Sin(r_phi);
                        float cos_psi = (float)Math.Cos(r_psi);
                        float sin_psi = (float)Math.Sin(r_psi);

                        List<float> M = new List<float> { cos_psi,  sin_phi * sin_psi,   0,  0,
                                                 0,          cos_phi,        0,  0,
                                              sin_psi,  -sin_phi * cos_psi,  0,  0,
                                                 0,              0,          0,  1 };

                        List<float> xyz = new List<float> { Figure.Facets[i1].Edges[i2].First.X, Figure.Facets[i1].Edges[i2].First.Y, Figure.Facets[i1].Edges[i2].First.Z, 1 };
                        List<float> c = mulMatrix(xyz, 1, 4, M, 4, 4);

                        List<float> xyz2 = new List<float> { Figure.Facets[i1].Edges[i2].Second.X, Figure.Facets[i1].Edges[i2].Second.Y, Figure.Facets[i1].Edges[i2].Second.Z, 1 };
                        List<float> c2 = mulMatrix(xyz2, 1, 4, M, 4, 4);

                        Figure.Facets[i1].Edges[i2] = new Edge(new MPoint(c[0], c[1]),
                            new MPoint(c2[0], c2[1]));
                    }
                }
            }
            var a = 0;

            DrawPolyhedron(Figure);
        }

        //REVERT FUNCTIONS
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Figure.Facets.Count; i++)
            {
                for (int j = 0; j < Figure.Facets[i].Edges.Count; j++)
                {
                    if (revertId == 0) //RevertXY
                    {
                        double[] offsetVector = new double[4] { Figure.Facets[i].Edges[j].First.X, Figure.Facets[i].Edges[j].First.Y, Figure.Facets[i].Edges[j].First.Z, 1 };
                        double[] offsetVector2 = new double[4] { Figure.Facets[i].Edges[j].Second.X, Figure.Facets[i].Edges[j].Second.Y, Figure.Facets[i].Edges[j].Second.Z, 1 };
                        double[,] Matrix = new double[4, 4];
                        double[] resultVector = new double[4];
                        double[] resultVector2 = new double[4];

                        Matrix[0, 0] = 1; Matrix[0, 1] = 0; Matrix[0, 2] = 0; Matrix[0, 3] = 0; 
                        Matrix[1, 0] = 0; Matrix[1, 1] = 1; Matrix[1, 2] = 0; Matrix[1, 3] = 0; 
                        Matrix[2, 0] = 0; Matrix[2, 1] = 0; Matrix[2, 2] = -1; Matrix[2, 3] = 0; 
                        Matrix[3, 0] = 0; Matrix[3, 1] = 0; Matrix[3, 2] = 0; Matrix[3, 3] = 1;

                        for (int i1 = 0; i1 < 4; i1++)
                        {
                            for (int j1 = 0; j1 < 4; j1++)
                                resultVector[i1] += offsetVector[j1] * Matrix[i1, j1];
                        }

                        for (int i1 = 0; i1 < 4; i1++)
                        {
                            for (int j1 = 0; j1 < 4; j1++)
                                resultVector2[i1] += offsetVector2[j1] * Matrix[i1, j1];
                        }

                        Figure.Facets[i].Edges[j] = new Edge(new MPoint((float)resultVector[0], (float)resultVector[1], (float)resultVector[2]),
                            new MPoint((float)resultVector2[0], (float)resultVector2[1], (float)resultVector2[2]));
                    }
                    else if (revertId == 1) //RevertYZ
                    {
                        double[] offsetVector = new double[4] { Figure.Facets[i].Edges[j].First.X, Figure.Facets[i].Edges[j].First.Y, Figure.Facets[i].Edges[j].First.Z, 1 };
                        double[] offsetVector2 = new double[4] { Figure.Facets[i].Edges[j].Second.X, Figure.Facets[i].Edges[j].Second.Y, Figure.Facets[i].Edges[j].Second.Z, 1 };
                        double[,] Matrix = new double[4, 4];
                        double[] resultVector = new double[4];
                        double[] resultVector2 = new double[4];

                        Matrix[0, 0] = -1; Matrix[0, 1] = 0; Matrix[0, 2] = 0; Matrix[0, 3] = 0; 
                        Matrix[1, 0] = 0; Matrix[1, 1] = 1; Matrix[1, 2] = 0; Matrix[1, 3] = 0; 
                        Matrix[2, 0] = 0; Matrix[2, 1] = 0; Matrix[2, 2] = 1; Matrix[2, 3] = 0;
                        Matrix[3, 0] = 0; Matrix[3, 1] = 0;  Matrix[3, 2] = 0; Matrix[3, 3] = 1;

                        for (int i1 = 0; i1 < 4; i1++)
                        {
                            for (int j1 = 0; j1 < 4; j1++)
                                resultVector[i1] += offsetVector[j1] * Matrix[i1, j1];
                        }

                        for (int i1 = 0; i1 < 4; i1++)
                        {
                            for (int j1 = 0; j1 < 4; j1++)
                                resultVector2[i1] += offsetVector2[j1] * Matrix[i1, j1];
                        }

                        Figure.Facets[i].Edges[j] = new Edge(new MPoint((float)resultVector[0], (float)resultVector[1], (float)resultVector[2]),
                            new MPoint((float)resultVector2[0], (float)resultVector2[1], (float)resultVector2[2]));
                    }
                    else if (revertId == 2) //RevertZX
                    {
                        double[] offsetVector = new double[4] { Figure.Facets[i].Edges[j].First.X, Figure.Facets[i].Edges[j].First.Y, Figure.Facets[i].Edges[j].First.Z, 1 };
                        double[] offsetVector2 = new double[4] { Figure.Facets[i].Edges[j].Second.X, Figure.Facets[i].Edges[j].Second.Y, Figure.Facets[i].Edges[j].Second.Z, 1 };
                        double[,] Matrix = new double[4, 4];
                        double[] resultVector = new double[4];
                        double[] resultVector2 = new double[4];

                        Matrix[0, 0] = 1; Matrix[0, 1] = 0; Matrix[0, 2] = 0; Matrix[0, 3] = 0;
                        Matrix[1, 0] = 0; Matrix[1, 1] = -1; Matrix[1, 2] = 0;  Matrix[1, 3] = 0;
                        Matrix[2, 0] = 0; Matrix[2, 1] = 0; Matrix[2, 2] = 1; Matrix[2, 3] = 0;
                        Matrix[3, 0] = 0; Matrix[3, 1] = 0; Matrix[3, 2] = 0; Matrix[3, 3] = 1;

                        for (int i1 = 0; i1 < 4; i1++)
                        {
                            for (int j1 = 0; j1 < 4; j1++)
                                resultVector[i1] += offsetVector[j1] * Matrix[i1, j1];
                        }

                        for (int i1 = 0; i1 < 4; i1++)
                        {
                            for (int j1 = 0; j1 < 4; j1++)
                                resultVector2[i1] += offsetVector2[j1] * Matrix[i1, j1];
                        }

                        Figure.Facets[i].Edges[j] = new Edge(new MPoint((float)resultVector[0], (float)resultVector[1], (float)resultVector[2]),
                            new MPoint((float)resultVector2[0], (float)resultVector2[1], (float)resultVector2[2]));
                    }
                }
            }

            DrawPolyhedron(Figure);
        }
    }
}
