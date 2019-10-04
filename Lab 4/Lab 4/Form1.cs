using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_4
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Pen penColor = Pens.BlueViolet;

        private bool isLine, isPolygon, isAroundCenter, isScaleAroundCenter;

        private PointF startPoint, endPoint = PointF.Empty;
        private PointF mainPoint = new PointF(0, 0);
        private PointF[] edge = new PointF[2];
        private PointF[] polygon = new PointF[0];

        private PointF minPolyPoint, maxPolyPoint, minEdgePoint, maxEdgePoint;

        private int offsetX = 0, offsetY = 0;
        double rotateAngle = 0, scaleX, scaleY;

        PointF intersection = new PointF(-1,-1);

        public Form1()
        {
            InitializeComponent();
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            startPoint = endPoint = Point.Empty;
            Array.Clear(edge, 0, edge.Length);
            Array.Clear(polygon, 0, polygon.Length);
            Array.Resize(ref edge, 2);
            Array.Resize(ref polygon, 0);
            pictureBox1.Invalidate();
            minEdgePoint = new Point(9999999, 9999999);
            maxEdgePoint = new Point(-1, -1);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            isLine = true;
            isPolygon = false;
        }

        private void intersec_Click(object sender, EventArgs e)
        {
            if (edge.Length > 4)
            {
                int n = edge.Length - 3;
                intersection = Intersection(edge[n - 3], edge[n - 2], edge[n - 1], edge[n]);
            }
            else
                intersection = new PointF(-1, -1);

            if (intersection.X == -1 && intersection.Y == -1)
                label9.Text = "Не сущ-ет";
            else
            {
                label9.Text = string.Format("X: {0:N2} Y: {1:N2}", intersection.X, intersection.Y);
                pictureBox1.Invalidate();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            isLine = false;
            isPolygon = true;
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isLine)
                {
                    startPoint = e.Location;
                }
                else if (isPolygon && polygon.Length == 0)
                {
                    minPolyPoint = maxPolyPoint = e.Location;
                    startPoint = e.Location;
                    Array.Resize(ref polygon, 1);
                    polygon[polygon.Length - 1] = startPoint;
                }
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isLine)
                {
                    endPoint = e.Location;
                    pictureBox1.Invalidate();
                }
                else if (isPolygon)
                {
                    endPoint = e.Location;
                    pictureBox1.Invalidate();
                }
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isLine)
                {
                    edge[edge.Length - 2] = new PointF(startPoint.X, startPoint.Y);
                    edge[edge.Length - 1] = new PointF(endPoint.X, endPoint.Y);
                    Array.Resize(ref edge, edge.Length + 2);

                    FindMin(ref minEdgePoint, endPoint);
                    FindMax(ref maxEdgePoint, endPoint);

                    FindMin(ref minEdgePoint, startPoint);
                    FindMax(ref maxEdgePoint, startPoint);

                    void FindMin(ref PointF myPoint, PointF point)
                    {
                        if (point.X < myPoint.X)
                            myPoint.X = point.X;
                        if (point.Y < myPoint.Y)
                            myPoint.Y = point.Y;
                    }

                    void FindMax(ref PointF myPoint, PointF point)
                    {
                        if (point.X > myPoint.X)
                            myPoint.X = point.X;
                        if (point.Y > myPoint.Y)
                            myPoint.Y = point.Y;
                    }
                }
                else if (isPolygon)
                {
                    Array.Resize(ref polygon, polygon.Length + 1);

                    if (endPoint.X < minPolyPoint.X)
                        minPolyPoint.X = endPoint.X;
                    if (endPoint.Y < minPolyPoint.Y)
                        minPolyPoint.Y = endPoint.Y;
                    if (endPoint.X > maxPolyPoint.X)
                        maxPolyPoint.X = endPoint.X;
                    if (endPoint.Y > maxPolyPoint.Y)
                        maxPolyPoint.Y = endPoint.Y;

                    polygon[polygon.Length - 1] = endPoint;
                    startPoint = endPoint;
                }
            }
            else
            {
                mainPoint.X = e.Location.X;
                mainPoint.Y = e.Location.Y;
            }


            pictureBox1.Invalidate();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            offsetX = (int)numericUpDown1.Value;
            offsetY = (int)numericUpDown2.Value;
            rotateAngle = (double)numericUpDown3.Value;
            isAroundCenter = checkBox1.Checked;
            isScaleAroundCenter = checkBox2.Checked;
            scaleX = (double)numericUpDown5.Value / 100d;
            scaleY = (double)numericUpDown4.Value / 100d;

            if (isLine)
            {
                for (int i = 0; i < edge.Length; i++)
                {
                    Translate(ref edge[i]);
                    Rotate(ref edge[i]);
                    Scale(ref edge[i]);
                }
            }
            else
            {
                for (int i = 0; i < polygon.Length; i++)
                {
                    Translate(ref polygon[i]);
                    Rotate(ref polygon[i]);
                    Scale(ref polygon[i]);
                }
            }

            startPoint = edge[0];
            endPoint = edge[1];
            pictureBox1.Invalidate();
        }


        private void Translate(ref PointF Point)
        {
            double[] offsetVector = new double[3] { Point.X, Point.Y, 1 };
            double[,] Matrix = new double[3, 3];

            double[] resultVector = new double[3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == j)
                        Matrix[i, j] = 1;
                    else if (i == 0 && j == 2)
                        Matrix[i, j] = offsetX;
                    else if (i == 1 && j == 2)
                        Matrix[i, j] = offsetY;
                    else
                        Matrix[i, j] = 0;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    resultVector[i] += Matrix[i, j] * offsetVector[j];
            }

            Point.X = (float)resultVector[0];
            Point.Y = (float)resultVector[1];
        }

        private void Rotate(ref PointF Point)
        {
            double pointA, pointB;

            if (isPolygon)
            {
                if (!isAroundCenter)
                {
                    pointA = -mainPoint.X * Math.Cos(rotateAngle) + mainPoint.Y * Math.Sin(rotateAngle) + mainPoint.X;
                    pointB = -mainPoint.X * Math.Sin(rotateAngle) - mainPoint.Y * Math.Cos(rotateAngle) + mainPoint.Y;
                }
                else
                {
                    var rotatePoint = new PointF((minPolyPoint.X + maxPolyPoint.X) / 2, (minPolyPoint.Y + maxPolyPoint.Y) / 2);
                    pointA = -rotatePoint.X * Math.Cos(rotateAngle) + rotatePoint.Y * Math.Sin(rotateAngle) + rotatePoint.X;
                    pointB = -rotatePoint.X * Math.Sin(rotateAngle) - rotatePoint.Y * Math.Cos(rotateAngle) + rotatePoint.Y;
                }
            }
            else
            {
                var rotatePoint = new PointF((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
                pointA = -rotatePoint.X * Math.Cos(rotateAngle) + rotatePoint.Y * Math.Sin(rotateAngle) + rotatePoint.X;
                pointB = -rotatePoint.X * Math.Sin(rotateAngle) - rotatePoint.Y * Math.Cos(rotateAngle) + rotatePoint.Y;
            }

            double[] offsetVector = new double[3] { Point.X, Point.Y, 1 };
            double[,] Matrix = new double[3, 3];
            double[] resultVector = new double[3];

            Matrix[0, 0] = Math.Cos(rotateAngle);
            Matrix[0, 1] = -Math.Sin(rotateAngle);
            Matrix[0, 2] = pointA;
            Matrix[1, 0] = Math.Sin(rotateAngle);
            Matrix[1, 1] = Math.Cos(rotateAngle);
            Matrix[1, 2] = pointB;
            Matrix[2, 0] = 0;
            Matrix[2, 1] = 0;
            Matrix[2, 2] = 1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    resultVector[i] += Matrix[i, j] * offsetVector[j];
            }


            Point.X = (float)resultVector[0];
            Point.Y = (float)resultVector[1];
        }

        private void Scale(ref PointF Point)
        {
            double[] offsetVector = new double[3] { Point.X, Point.Y, 1 };
            double[,] Matrix = new double[3, 3];
            double[] resultVector = new double[3];

            PointF scalePoint;
            if (isScaleAroundCenter)
            {
                scalePoint = new PointF((minPolyPoint.X + maxPolyPoint.X) / 2, (minPolyPoint.Y + maxPolyPoint.Y) / 2);
            }
            else
            {
                scalePoint = mainPoint;
            }

            Matrix[0, 0] = scaleX;
            Matrix[0, 1] = 0;
            Matrix[0, 2] = (1 - scaleX) * scalePoint.X;
            Matrix[1, 0] = 0;
            Matrix[1, 1] = scaleY;
            Matrix[1, 2] = (1 - scaleY) * scalePoint.Y;
            Matrix[2, 0] = 0;
            Matrix[2, 1] = 0;
            Matrix[2, 2] = 1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    resultVector[i] += Matrix[i, j] * offsetVector[j];
            }

            Point.X = (float)resultVector[0];
            Point.Y = (float)resultVector[1];
        }

        private PointF Intersection(PointF p0, PointF p1, PointF p2, PointF p3)
        {
            PointF i = new PointF(-1, -1);
            PointF s1 = new PointF();
            PointF s2 = new PointF();
            s1.X = p1.X - p0.X;
            s1.Y = p1.Y - p0.Y;
            s2.X = p3.X - p2.X;
            s2.Y = p3.Y - p2.Y;
            float s, t;
            s = (-s1.Y * (p0.X - p2.X) + s1.X * (p0.Y - p2.Y)) / (-s2.X * s1.Y + s1.X * s2.Y);
            t = (s2.X * (p0.Y - p2.Y) - s2.Y * (p0.X - p2.X)) / (-s2.X * s1.Y + s1.X * s2.Y);

            if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
                i.X = p0.X + (t * s1.X);
                i.Y = p0.Y + (t * s1.Y);

            }
            return i;
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < edge.Length; i += 2)
            {
                e.Graphics.DrawLine(penColor, edge[i], edge[i + 1]);
            }

            if (polygon.Length > 1)
                e.Graphics.DrawPolygon(penColor, polygon);

            e.Graphics.DrawLine(penColor, startPoint, endPoint);
            e.Graphics.DrawEllipse(Pens.Green, intersection.X - 2, intersection.Y - 2, 5, 5);
            e.Graphics.DrawEllipse(Pens.Red, mainPoint.X - 1, mainPoint.Y - 1, 3, 3);
        }
    }
}
