using System;
using System.Drawing;
using System.Windows.Forms;

namespace RasterAlgorithms
{
    public partial class Form1 : Form
    {
        Color BorderColor = Color.Black;
        Color FillColor = Color.Green;
        bool isPressed = false;
        Point CurrentPoint;
        Point PrevPoint;
        Graphics g;
        //658; 426
        int Height = 426;
        int Width = 658;



        bool isFilling = false;

        Bitmap DrawArea;
        Bitmap PictureFillArea;

        bool isPictureFilling;

        public Form1()
        {
            InitializeComponent();

            PictureFillArea = (Bitmap)pictureBox1.Image;

            panel1.Image = null;
            panel1.Refresh();

            DrawArea = new Bitmap(panel1.Size.Width, panel1.Size.Height);
            panel1.Image = DrawArea;
            g = Graphics.FromImage(DrawArea);
            SetBorders();

            isPictureFilling = false;
        }

        private void SetBorders()
        {
            Pen blackPen = new Pen(BorderColor, 1);
            Rectangle rect = new Rectangle(1, 1, panel1.Width - 3, panel1.Height - 3);
            g.DrawRectangle(blackPen, rect);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK)
                FillColor = colorDialog1.Color;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            panel1.Image = null;
            panel1.Refresh();

            DrawArea = new Bitmap(panel1.Size.Width, panel1.Size.Height);
            panel1.Image = DrawArea;
            g = Graphics.FromImage(DrawArea);

            SetBorders();

            isFilling = false;
            button5.Text = "Выбрать точку для заливки";

            isPictureFilling = false;
            button3.Text = "Заливка изображением";
        }

        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isFilling)
            {
                fill(Convert.ToInt32(e.X), Convert.ToInt32(e.Y));
                panel1.Image = DrawArea;
            }
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isFilling)
            {
                isPressed = true;
                CurrentPoint = e.Location;
            }
            
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isFilling)
            {
                if (isPressed)
                {
                    PrevPoint = CurrentPoint;
                    CurrentPoint = e.Location;
                    paint_simple();
                }
            }
        }

        private void paint_simple()
        {
            Pen p = new Pen(BorderColor, 4);
            g.DrawLine(p, PrevPoint, CurrentPoint);
            panel1.Image = DrawArea;
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
        }

        private int lineFill(int x, int y, int dir, int prevXl, int prevXr)
        {
            int xl = x;
            int xr = x;
            Color c;

            

            if (!isPictureFilling)
            {
                do
                {
                    c = DrawArea.GetPixel(--xl, y);
                } while ((c.ToArgb() != BorderColor.ToArgb()) && (c.ToArgb() != FillColor.ToArgb()) && (xl > 0));

                do
                {
                    c = DrawArea.GetPixel(++xr, y);
                } while ((c.ToArgb() != BorderColor.ToArgb()) && (c.ToArgb() != FillColor.ToArgb()) && (xr < panel1.Width - 1));

            }
            else
            {
                do
                {
                    c = DrawArea.GetPixel(--xl, y);
                } while ((c.ToArgb() != BorderColor.ToArgb()) && (c.ToArgb() != PictureFillArea.GetPixel(xl, y).ToArgb()) && (xl > 0));

                do
                {
                    c = DrawArea.GetPixel(++xr, y);
                } while ((c.ToArgb() != BorderColor.ToArgb()) && (c.ToArgb() != PictureFillArea.GetPixel(xr, y).ToArgb()) && (xr < panel1.Width - 1));
            }

            xl++;
            xr--;

            if (!isPictureFilling)
            {
                Pen p = new Pen(FillColor);
                g.DrawLine(p, xl, y, xr, y);
            }
            else
            {
                for (int i = xl; i <= xr; i++)
                {

                    DrawArea.SetPixel(i, y, PictureFillArea.GetPixel(i, y));
                }
            }

            if (!isPictureFilling)
            {
                for (x = xl; x <= xr; x++)
                {
                    c = DrawArea.GetPixel(x, y + dir);
                    if ((c.ToArgb() != BorderColor.ToArgb()) && (c.ToArgb() != FillColor.ToArgb()) && (y + dir < panel1.Height - 1))
                    {
                        x = lineFill(x, y + dir, dir, xl, xr);
                    }
                }

                for (x = xl; x < prevXl; x++)
                {
                    c = DrawArea.GetPixel(x, y - dir);
                    if ((c.ToArgb() != BorderColor.ToArgb()) && (c.ToArgb() != FillColor.ToArgb()) && (y - dir > 0))
                    {
                        x = lineFill(x, y - dir, -dir, xl, xr);
                    }
                }

                for (x = prevXr; x < xr; x++)
                {
                    c = DrawArea.GetPixel(x, y - dir);
                    if ((c.ToArgb() != BorderColor.ToArgb()) && (c.ToArgb() != FillColor.ToArgb()) && (y - dir > 0))
                    {
                        x = lineFill(x, y - dir, -dir, xl, xr);
                    }
                }
            }
            else
            {
                for (x = xl; x <= xr; x++)
                {
                    c = DrawArea.GetPixel(x, y + dir);
                    if ((c.ToArgb() != BorderColor.ToArgb()) && (c.ToArgb() != PictureFillArea.GetPixel(x, y + dir).ToArgb()) && (y + dir < panel1.Height - 1))
                    {
                        x = lineFill(x, y + dir, dir, xl, xr);
                    }
                }

                for (x = xl; x < prevXl; x++)
                {
                    c = DrawArea.GetPixel(x, y - dir);
                    if ((c.ToArgb() != BorderColor.ToArgb()) && (c.ToArgb() != PictureFillArea.GetPixel(x, y - dir).ToArgb()) && (y - dir > 0))
                    {
                        x = lineFill(x, y - dir, -dir, xl, xr);
                    }
                }

                for (x = prevXr; x < xr; x++)
                {
                    c = DrawArea.GetPixel(x, y - dir);
                    if ((c.ToArgb() != BorderColor.ToArgb()) && (c.ToArgb() != PictureFillArea.GetPixel(x, y + dir).ToArgb()) && (y - dir > 0))
                    {
                        x = lineFill(x, y - dir, -dir, xl, xr);
                    }
                }
            }

            return xr;
        }

        private void fill(int x, int y)
        {
            lineFill(x, y, 1, x, x);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            isFilling = !isFilling;
            if (isFilling)
                button5.Text = "Рисовать далее";
            else
                button5.Text = "Выбрать точку для заливки";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            isPictureFilling = !isPictureFilling;
            if (isPictureFilling)
                button3.Text = "Заливка цветом";
            else
                button3.Text = "Заливка изображением";
        }
    }
}
