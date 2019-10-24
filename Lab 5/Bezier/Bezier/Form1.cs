using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bezier
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private List<PointF> points = new List<PointF>();
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            points.Add(e.Location);
            g.FillEllipse(new SolidBrush(Color.Red), e.X - 2.5f, e.Y - 2.5f, 5, 5);
            pictureBox1.Invalidate();
        }

        private int Fuctorial(int n)
        {
            int res = 1;
            for (int i = 1; i <= n; i++)
                res *= i;
            return res;
        }

        // Функция вычисления полинома Бернштейна
        float polinom(int i, int n, float t)
        {
            return ((float)Fuctorial(n) / (Fuctorial(i) * Fuctorial(n - i))) * (float)Math.Pow(t, i) * (float)Math.Pow(1 - t, n - i);
        }

        void thirdBezier(List<PointF> pointFs)
        {
            int j = 0;
            float step = 0.01f;

            PointF[] result = new PointF[101];
            for (float t = 0; t <= 1; t += step)
            {
                float ytmp = 0;
                float xtmp = 0;
                for (int i = 0; i < pointFs.Count; i++)
                {
                    float b = polinom(i, pointFs.Count - 1, t);
                    xtmp += pointFs[i].X * b;
                    ytmp += pointFs[i].Y * b;
                }
                result[j] = new PointF(xtmp, ytmp);
                j++;
            }
            g.DrawLines(new Pen(Color.Blue), result);
            pictureBox1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var b = SplitPoints(points, 3);
            for (int i = 0; i < b.Count(); i++)
            {
                List<PointF> tmp = new List<PointF>();
                foreach (var v in b.ElementAt(i))
                    tmp.Add(v);
                if (i + 1 < b.Count())
                    tmp.Add(b.ElementAt(i + 1).ElementAt(0));
                thirdBezier(tmp);
            }
        }

        private IEnumerable<IEnumerable<PointF>> SplitPoints(IEnumerable<PointF> source, int count)
        {
            return source.Select((x, y) => new { Index = y, Value = x })
              .GroupBy(x => x.Index / count)
              .Select(x => x.Select(y => y.Value).ToList())
              .ToList();
        }
    }
}
