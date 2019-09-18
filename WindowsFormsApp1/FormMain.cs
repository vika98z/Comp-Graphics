using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form ifrm = new Form1();
            ifrm.Show(); // отображаем Form1
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form ifrm = new Form2();
            ifrm.Show(); // отображаем Form2
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form ifrm = new Form3();
            ifrm.Show(); // отображаем Form3
        }
    }
}
