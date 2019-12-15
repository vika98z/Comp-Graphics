using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Affine
{

    public enum Axis { AXIS_X, AXIS_Y, AXIS_Z, LINE };
    public enum Projection { PERSPECTIVE = 0, ISOMETRIC, ORTHOGR_X, ORTHOGR_Y, ORTHOGR_Z };
    public partial class Form1 : Form
    {
        Graphics g;
        Projection projection = 0;
        Axis rotateLineMode = 0;
        Polyhedron figure = null;
        int revertId = -1;

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            g.TranslateTransform(pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
            g.ScaleTransform(1, -1);
        }

        //TRANSLATION ROTATION SCALE
        private void button2_Click(object sender, EventArgs e)
        {
            if (figure == null)
            {
                MessageBox.Show("Неодбходимо выбрать фигуру!", "Ошибка!", MessageBoxButtons.OK);
            }
            else
            {
                //TRANSLATE
                int offsetX = (int)numericUpDown1.Value, offsetY = (int)numericUpDown2.Value, offsetZ = (int)numericUpDown3.Value;
                figure.Translate(offsetX, offsetY, offsetZ);
                
                //ROTATE
                int rotateAngleX = (int)numericUpDown4.Value;
                figure.Rotate(rotateAngleX, 0);

                int rotateAngleY = (int)numericUpDown5.Value;
                figure.Rotate(rotateAngleY, Axis.AXIS_Y);

                int rotateAngleZ = (int)numericUpDown6.Value;
                figure.Rotate(rotateAngleZ, Axis.AXIS_Z);

                //SCALE
                if (checkBox1.Checked)
                {
                    float old_x = figure.Center.X, old_y = figure.Center.Y, old_z = figure.Center.Z;
                    figure.Translate(-old_x, -old_y, -old_z);

                    float kx = (float)numericUpDown9.Value, ky = (float)numericUpDown8.Value, kz = (float)numericUpDown7.Value;
                    figure.Scale(kx, ky, kz);

                    figure.Translate(old_x, old_y, old_z);
                }
                else
                {
                    float kx = (float)numericUpDown9.Value, ky = (float)numericUpDown8.Value, kz = (float)numericUpDown7.Value;
                    figure.Scale(kx, ky, kz);
                }
            }

            g.Clear(Color.White);
            figure.Show(g, projection);
        }

        //DRAW FIGURE
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    //Tetrahedron
                    g.Clear(Color.White);
                    figure = new Polyhedron();
                    figure.Tetrahedron();
                    figure.Show(g, projection);
                    break;
                case 1:
                    //Hexahedron
                    g.Clear(Color.White);
                    figure = new Polyhedron();
                    figure.Hexahedron();
                    figure.Show(g, projection);
                    break;
                case 2:
                    //Oktahedron
                    g.Clear(Color.White);
                    figure = new Polyhedron();
                    figure.Octahedron();
                    figure.Show(g, projection);
                    break;
                case 3:
                    //Icosahedron
                    g.Clear(Color.White);
                    figure = new Polyhedron();
                    figure.Icosahedron();
                    figure.Show(g, projection);
                    break;
                case 4:
                    //Dodecahedron
                    g.Clear(Color.White);
                    figure = new Polyhedron();
                    figure.Dodecahedron();
                    figure.Show(g, projection);
                    break;
                default:
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) => projection = (Projection)comboBox2.SelectedIndex;
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) => revertId = comboBox3.SelectedIndex;
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            rotateLineMode = (Axis)comboBox4.SelectedIndex;
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

        //ROTATE AROUND LINE
        private void RotateAroundLine()
        {
            Edge rotateLine = new Edge(
                                new Point3D(
                                    (float)numericUpDown12.Value,
                                    (float)numericUpDown11.Value,
                                    (float)numericUpDown10.Value),
                                new Point3D(
                                    (float)numericUpDown15.Value,
                                    (float)numericUpDown14.Value,
                                    (float)numericUpDown13.Value));

            float Ax = rotateLine.First.X, Ay = rotateLine.First.Y, Az = rotateLine.First.Z;
            figure.Translate(-Ax, -Ay, -Az);

            double angle = (double)numericUpDown16.Value;
            figure.Rotate(angle, rotateLineMode, rotateLine);

            figure.Translate(Ax, Ay, Az);

            g.Clear(Color.White);
            figure.Show(g, projection);
        }

        //CAMERA PROJECTION
        private void button1_Click_1(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            if (figure != null)
                figure.Show(g, projection);
        }

        //REVERT FUNCTIONS
        private void button3_Click(object sender, EventArgs e)
        {
            if (revertId == 0)
            {
                figure.reflectX();
                g.Clear(Color.White);
                figure.Show(g, projection);
            }
            else if (revertId == 1)
            {
                figure.reflectY();
                g.Clear(Color.White);
                figure.Show(g, projection);
            }
            else if (revertId == 2)
            {
                figure.reflectZ();
                g.Clear(Color.White);
                figure.Show(g, projection);
            }
        }

        //INVOKE ROTATE AROUND LINE
        private void button4_Click(object sender, EventArgs e) => RotateAroundLine();

        //LOAD
        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);

            figure = new Polyhedron(fileText);
            g.Clear(Color.White);
            figure.Show(g, 0);
        }

        //SAVE
        private void button6_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            string text = "";

            if (figure != null)
            {
                text = figure.Save();                
            }
            System.IO.File.WriteAllText(filename, text);
        }

        //ROTATION FIGURE
        private void button7_Click(object sender, EventArgs e)
        {
            List<Point3D> points = new List<Point3D>();
            var lines = textBox1.Text.Split('\n');

            foreach (var p in lines)
            {
                var arr = ((string)p).Split(',');
                points.Add(new Point3D(float.Parse(arr[0]), float.Parse(arr[1]), float.Parse(arr[2])));
            }

            Axis axis = 0;
            switch (comboBox5.SelectedItem.ToString())
            {
                case "OX":
                        axis = 0;
                        break;
                case "OY":
                        axis = Axis.AXIS_Y;
                        break;
                case "OZ":
                        axis = Axis.AXIS_Z;
                        break;
                default:
                        axis = 0;
                        break;
            }

            RotationFigure rotateFigure = new RotationFigure(points, axis, (int)numericUpDown17.Value);

            figure = rotateFigure;

            g.Clear(Color.White);
            rotateFigure.Show(g, 0);
        }

        //GRAPHIC
        private void button8_Click(object sender, EventArgs e)
        {
            Graphic Graph = new Graphic((int)numericUpDown18.Value, (int)numericUpDown20.Value, (int)numericUpDown19.Value, 
                (int)numericUpDown21.Value, (int)numericUpDown22.Value, comboBox6.SelectedIndex);

            figure = Graph;

            g.Clear(Color.White);
            Graph.Show(g, 0);
        }
    }
}