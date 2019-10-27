using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DS
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen p;
        bool f;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            numericUpDown1.Maximum = pictureBox1.Height;
            numericUpDown2.Maximum = pictureBox1.Height;
            pictureBox1.BackColor = Color.White;
            pictureBox2.BackColor = Color.Blue;
        }

        private void draw_Click(object sender, EventArgs e)
        {
            draw.Enabled = false;
            clear.Enabled = false;

            clear_Click(sender, e);
            int c = (int)Iterations.Value;
            p = new Pen(pictureBox2.BackColor, 2);
            g.DrawLine(p, 0, (int)numericUpDown1.Value, pictureBox1.Width, (int)numericUpDown2.Value);
            pictureBox1.Refresh();
            midpoint_displ((int)numericUpDown1.Value, 0, (int)numericUpDown2.Value, pictureBox1.Width, c);

            draw.Enabled = true;
            clear.Enabled = true;
        }

        private void midpoint_displ(int h1, int x1, int h2, int x2, int c)
        {
            if (c > 0)
            {
                --c;
                int dis_x = x2 - x1;
                int dis_h = h2 - h1;
                double l = Math.Sqrt(dis_x * dis_x + dis_h * dis_h);
                Random r = new Random();
                double h = (h1 + h2) / 2.0;
                double dis_hh = h1 - h;
                double l2 = l / 2.0;
                int x = x1 + (int)Math.Round(Math.Sqrt(l2 * l2 - dis_hh * dis_hh));
                int rand = r.Next((int)((-1) * numericUpDown4.Value * (int)Math.Round(l)), (int)(numericUpDown4.Value * (int)Math.Round(l)));
                h += rand;
                
                if (h < 0)
                    h = 10;
                if (h > pictureBox1.Height)
                    h = pictureBox1.Height - 10;

                Pen p1 = new Pen(Color.White, 2);
                g.DrawLine(p1, x1, h1, x2, h2);
                g.DrawLine(p, x1, h1, x, (int)Math.Ceiling(h));
                g.DrawLine(p, x, (int)Math.Ceiling(h), x2, h2);
                pictureBox1.Refresh();
                midpoint_displ(h1, x1, (int)Math.Ceiling(h), x, c);
                midpoint_displ((int)Math.Ceiling(h), x, h2, x2, c);
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Dispose();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.BackColor = colorDialog1.Color;
            }
        }
    }
}
