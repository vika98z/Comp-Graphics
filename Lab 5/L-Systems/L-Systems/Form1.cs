using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L_Systems
{
    public partial class Form1 : Form
    {
        Graphics g;
        string axiom;
        double angle;

        string filename;
        SortedDictionary<char, string> rules;

        Stack<Tuple<double, double, double, double>> states;
        int iterations;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            rules = new SortedDictionary<char, string>();

            states = new Stack<Tuple<double, double, double, double>>();
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rules.Clear();
            string[] rule;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    filename = openFileDialog1.FileName;
                    string[] lines = File.ReadAllLines(filename);
                    string[] parameters = lines[0].Split(' ');
                    axiom = parameters[0];
                    angle = Convert.ToDouble(parameters[1]);                   
                    for (int i = 1; i < lines.Length; ++i)
                    {
                        rule = lines[i].Split('=');
                        rules[Convert.ToChar(rule[0])] = rule[1];
                    }
                    button2.Enabled = true;
                }
                catch
                {
                    button2.Enabled = false;
                    DialogResult result = MessageBox.Show("Ошибка открытия файла",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);

            iterations = (int)numericUpDown1.Value;
            List<double> xPoints = new List<double>();
            List<double> yPoints = new List<double>();
            List<Tuple<double, double, double, double>> lSystPoints =
                new List<Tuple<double, double, double, double>>();
            double x = pictureBox1.Width, y = pictureBox1.Height / 2, dy = 0,
                dx = -(pictureBox1.Width / Math.Pow(10, iterations + 1));
            
            if (filename.Contains("Куст"))
            {
                x = pictureBox1.Width / 2;
                y = 0;
                dy = pictureBox1.Height / Math.Pow(10, iterations + 1);
                dx = 0;
            }

            xPoints.Add(x);
            yPoints.Add(y);

            string prev = axiom;
            string next = axiom;
            int iter = 0;
            while (iter++ < iterations)
            {
                prev = next;
                next = "";
                for (int i = 0; i < prev.Length; ++i)
                {
                    if (rules.ContainsKey(prev[i]))
                        next += rules[prev[i]];
                    else
                        next += prev[i];
                }
            }
            
            double rx, ry;
            for (int i = 0; i < next.Length; ++i)
            {
                switch (next[i])
                {
                    case 'F':
                        lSystPoints.Add(
                            new Tuple<double, double, double, double>(x, y, x + dx, y + dy));
                        x += dx;
                        y += dy;
                        xPoints.Add(x);
                        yPoints.Add(y);
                        break;
                    case '+':
                        rx = dx;
                        ry = dy;
                        dx = rx * Math.Cos(angle * Math.PI / 180) - ry * Math.Sin(angle * Math.PI / 180);
                        dy = rx * Math.Sin(angle * Math.PI / 180) + ry * Math.Cos(angle * Math.PI / 180);
                        break;

                    case '-':
                        rx = dx;
                        ry = dy;
                        dx = rx * Math.Cos(-angle * Math.PI / 180) - ry * Math.Sin(-angle * Math.PI / 180);
                        dy = rx * Math.Sin(-angle * Math.PI / 180) + ry * Math.Cos(-angle * Math.PI / 180);
                        break;
                    case '[':
                        states.Push(new Tuple<double, double, double, double>(x, y, dx, dy));
                        break;

                    case ']':
                        Tuple<double, double, double, double> coords = states.Pop();
                        x = coords.Item1;
                        y = coords.Item2;
                        dx = coords.Item3;
                        dy = coords.Item4;
                        break;
                    default: break;
                }
            }

            double scale = Math.Max(xPoints.Max() - xPoints.Min(), yPoints.Max() - yPoints.Min());

            foreach (var p in lSystPoints)
                g.DrawLine(Pens.Blue, (float)((xPoints.Max() - p.Item1) / scale * pictureBox1.Width),
                    (float)((yPoints.Max() - p.Item2) / scale * pictureBox1.Height), (float)((xPoints.Max() - p.Item3) / scale * pictureBox1.Width),
                    (float)((yPoints.Max() - p.Item4) / scale * pictureBox1.Height));

            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pictureBox1.Invalidate();
        }
    }
}
