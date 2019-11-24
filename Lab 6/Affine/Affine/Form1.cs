using System;
using System.Drawing;
using System.Windows.Forms;

namespace Affine
{

    public enum Axis { AXIS_X, AXIS_Y, AXIS_Z, OTHER };
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
                figure.translate(offsetX, offsetY, offsetZ);
                g.Clear(Color.White);
                figure.show(g, projection);

                //ROTATE
                int rotateAngleX = (int)numericUpDown4.Value;
                figure.rotate(rotateAngleX, 0);

                int rotateAngleY = (int)numericUpDown5.Value;
                figure.rotate(rotateAngleY, Axis.AXIS_Y);

                int rotateAngleZ = (int)numericUpDown6.Value;
                figure.rotate(rotateAngleZ, Axis.AXIS_Z);

                //SCALE
                if (checkBox1.Checked)
                {
                    float old_x = figure.Center.X, old_y = figure.Center.Y, old_z = figure.Center.Z;
                    figure.translate(-old_x, -old_y, -old_z);

                    float kx = (float)numericUpDown9.Value, ky = (float)numericUpDown8.Value, kz = (float)numericUpDown7.Value;
                    figure.scale(kx, ky, kz);

                    figure.translate(old_x, old_y, old_z);
                }
                else
                {
                    float kx = (float)numericUpDown9.Value, ky = (float)numericUpDown8.Value, kz = (float)numericUpDown7.Value;
                    figure.scale(kx, ky, kz);
                }
            }
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
                    figure.make_tetrahedron();
                    figure.show(g, projection);
                    break;
                case 1:
                    //Hexahedron
                    g.Clear(Color.White);
                    figure = new Polyhedron();
                    figure.make_hexahedron();
                    figure.show(g, projection);
                    break;
                case 2:
                    //Oktahedron
                    g.Clear(Color.White);
                    figure = new Polyhedron();
                    figure.make_octahedron();
                    figure.show(g, projection);
                    break;
                case 3:
                    //Icosahedron
                    g.Clear(Color.White);
                    figure = new Polyhedron();
                    figure.make_icosahedron();
                    figure.show(g, projection);
                    break;
                case 4:
                    //Dodecahedron
                    g.Clear(Color.White);
                    figure = new Polyhedron();
                    figure.make_dodecahedron();
                    figure.show(g, projection);
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

        private void RotateAroundLine()
        {
            Edge rotateLine = new Edge(
                                new Point3d(
                                    (float)numericUpDown12.Value,
                                    (float)numericUpDown11.Value,
                                    (float)numericUpDown10.Value),
                                new Point3d(
                                    (float)numericUpDown15.Value,
                                    (float)numericUpDown14.Value,
                                    (float)numericUpDown13.Value));

            float Ax = rotateLine.First.X, Ay = rotateLine.First.Y, Az = rotateLine.First.Z;
            figure.translate(-Ax, -Ay, -Az);

            double angle = (double)numericUpDown16.Value;
            figure.rotate(angle, rotateLineMode, rotateLine);

            figure.translate(Ax, Ay, Az);

            g.Clear(Color.White);
            figure.show(g, projection);
        }

        //CAMERA PROJECTION
        private void button1_Click_1(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            if (figure != null)
                figure.show(g, projection);
        }

        //REVERT FUNCTIONS
        private void button3_Click(object sender, EventArgs e)
        {
            if (revertId == 0)
            {
                figure.reflectX();
                g.Clear(Color.White);
                figure.show(g, projection);
            }
            else if (revertId == 1)
            {
                figure.reflectY();
                g.Clear(Color.White);
                figure.show(g, projection);
            }
            else if (revertId == 2)
            {
                figure.reflectZ();
                g.Clear(Color.White);
                figure.show(g, projection);
            }
        }

        //ROTATE AROUND LINE
        private void button4_Click(object sender, EventArgs e) => RotateAroundLine();
    }
}