using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace raster_algorithms
{
    public partial class Form1 : Form
    {
        private Graphics g;
        Point lastPoint = Point.Empty;
        bool isMouseDown = false;
        int penThickness = 1;
        Color borderColor = Color.FromArgb(255, 0, 0, 0);
        Pen borderPen;
        Point mouseCoord;
        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);

            borderColorPan.BackColor = borderColor;
            update_pens();

            radioPen.Checked = true;

            SetBorders();
        }

        private void SetBorders()
        {
            Pen blackPen = new Pen(borderColor, 1);
            Rectangle rect = new Rectangle(1, 1, pictureBox1.Width - 3, pictureBox1.Height - 3);
            g.DrawRectangle(blackPen, rect);
        }

        private void update_pens()
        {
            borderPen = new Pen(borderColor, penThickness);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = e.Location;
            isMouseDown = true;
            mouseCoord = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown && radioPen.Checked && lastPoint != null)
            {
                    g.DrawLine(borderPen, lastPoint, e.Location);
                    lastPoint = e.Location; 
                    pictureBox1.Invalidate();    
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            lastPoint = Point.Empty;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (radioSelectBorder.Checked)
            {
                selectBorder(Color.Red);
            }
            pictureBox1.Invalidate();
        }

        private void chooseBorderColorBtn_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = borderColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            { 
                borderColor = colorDlg.Color;
                borderColorPan.BackColor = colorDlg.Color;
                update_pens();
            }
        }

        private Color getColorAt(Point point)
        {
            if (pictureBox1.ClientRectangle.Contains(point))
                return ((Bitmap)pictureBox1.Image).GetPixel(point.X, point.Y);
            else
                return Color.Black;
        }

        private void radioSelectBorder_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSelectBorder.Checked)
                pictureBox1.Cursor = Cursors.Cross;
            else
                pictureBox1.Cursor = Cursors.Default;
        }

        // найти точку, принадлежащую границе
        private Point findStartPoint()
        {
            int x = mouseCoord.X;
            int y = mouseCoord.Y;

            Color bgColor = bmp.GetPixel(mouseCoord.X, mouseCoord.Y);
            Color currColor = bgColor;           
            while (x < bmp.Width - 2 && currColor.ToArgb() == bgColor.ToArgb())
            {
                x++;
                currColor = bmp.GetPixel(x, y);
            }
             
            return new Point(x, y);
        }

        // выделить границу
        private void selectBorder(Color c)
        {
            List<Point> pixels = new List<Point>();
            Point curr = findStartPoint();
            Point start = curr;
            pixels.Add(start);
            Color borderColor = bmp.GetPixel(curr.X, curr.Y);

            Point next = new Point();
            int currDir = 6;
            int nextDir = -1;
            int moveTo = 0;
            do
            {
                moveTo = (currDir - 2 + 8) % 8;
                int mt = moveTo;
                do
                {
                    next = curr;
                    switch (moveTo)
                    {
                        case 0: next.X++; nextDir = 0; break;
                        case 1: next.X++; next.Y--; nextDir = 1; break;
                        case 2: next.Y--; nextDir = 2; break;
                        case 3: next.X--; next.Y--; nextDir = 3; break;
                        case 4: next.X--; nextDir = 4; break;
                        case 5: next.X--; next.Y++; nextDir = 5; break;
                        case 6: next.Y++; nextDir = 6; break;
                        case 7: next.X++; next.Y++; nextDir = 7; break;
                    }

                    if (next == start)
                        break;

                    if (bmp.GetPixel(next.X, next.Y) == borderColor)
                    {
                        pixels.Add(next);
                        curr = next;
                        currDir = nextDir;
                        break;
                    }
                    moveTo = (moveTo + 1) % 8;
                } while (moveTo != mt);
            } while (next != start);

            foreach (var p in pixels)
                bmp.SetPixel(p.X, p.Y, c);

            pixels.Sort(compareY);
            HashSet<Point> pixset = new HashSet<Point>();

            foreach (var p in pixels)
            {
                pixset.Add(p);
            }


            Console.WriteLine(pixset.Count);
            Console.WriteLine(pixels.Count);
        }

        private int compareY(Point p1, Point p2)
        {
            if (p1.Y < p2.Y)
                return 1;
            else if (p1.Y == p2.Y)
                return 0;
            return -1;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pictureBox1.Invalidate();
            SetBorders();
        }
    }
}
