using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        byte[] rgbOneValues;
        byte[] rgbTwoValues;
        byte[] rgbThreeValues;

        int i = 0;

        public Form2()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = pictureBox1.Image as Bitmap;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            rgbOneValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbOneValues, 0, bytes);

            Bitmap bmp2 = pictureBox2.Image as Bitmap;
            Rectangle rect2 = new Rectangle(0, 0, bmp2.Width, bmp2.Height);
            System.Drawing.Imaging.BitmapData bmpData2 =
                bmp2.LockBits(rect2, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp2.PixelFormat);
            IntPtr ptr2 = bmpData2.Scan0;
            int bytes2 = Math.Abs(bmpData2.Stride) * bmp2.Height;
            rgbTwoValues = new byte[bytes2];
            System.Runtime.InteropServices.Marshal.Copy(ptr2, rgbTwoValues, 0, bytes2);

            Bitmap bmp3 = pictureBox3.Image as Bitmap;
            Rectangle rect3 = new Rectangle(0, 0, bmp3.Width, bmp3.Height);
            System.Drawing.Imaging.BitmapData bmpData3 =
                bmp3.LockBits(rect3, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp3.PixelFormat);
            IntPtr ptr3 = bmpData3.Scan0;
            int bytes3 = Math.Abs(bmpData3.Stride) * bmp3.Height;
            rgbThreeValues = new byte[bytes3];
            System.Runtime.InteropServices.Marshal.Copy(ptr3, rgbThreeValues, 0, bytes3);

            float greyOneValue, greyTwoValue;

            float min = 0;

            for (int counter = 0; counter < rgbOneValues.Length; counter += 3)
            {
                float redValue = rgbOneValues[counter];
                float greenValue = rgbOneValues[counter + 1];
                float blueValue = rgbOneValues[counter + 2];

                greyOneValue = 0.3f * redValue + 0.59f * greenValue + 0.11f * blueValue;
                greyTwoValue = 0.21f * redValue + 0.72f * greenValue + 0.07f * blueValue;

                if (Math.Min((greyOneValue - greyTwoValue), (greyTwoValue - greyOneValue)) < 0)
                {
                    if ((greyOneValue - greyTwoValue) < min)
                        min = (greyOneValue - greyTwoValue);
                }
            }

            for (int counter = 0; counter < rgbOneValues.Length; counter += 3)
            {
               
                float redValue = rgbOneValues[counter];
                float greenValue = rgbOneValues[counter + 1];
                float blueValue = rgbOneValues[counter + 2];

                greyOneValue = 0.3f * redValue + 0.59f * greenValue + 0.11f * blueValue;
                greyTwoValue = 0.21f * redValue + 0.72f * greenValue + 0.07f * blueValue;

                if ((greyOneValue - greyTwoValue) < 0)
                    rgbThreeValues[counter] = rgbThreeValues[counter + 1] = rgbThreeValues[counter + 2] = (byte)(greyOneValue - greyTwoValue + Math.Abs(min));
                else
                    rgbThreeValues[counter] = rgbThreeValues[counter + 1] = rgbThreeValues[counter + 2] = (byte)(greyOneValue - greyTwoValue);

                rgbOneValues[counter] = rgbOneValues[counter + 1] = rgbOneValues[counter + 2] = (byte)greyOneValue;
                rgbTwoValues[counter] = rgbTwoValues[counter + 1] = rgbTwoValues[counter + 2] = (byte)greyTwoValue;
                

                if (counter % 1000 == 0)
                {
                    chart1.Series[0].Points.AddXY(i, greyOneValue);
                    chart2.Series[0].Points.AddXY(i, greyTwoValue);
                    i++;
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbOneValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            pictureBox1.Refresh();

            System.Runtime.InteropServices.Marshal.Copy(rgbTwoValues, 0, ptr2, bytes2);
            bmp2.UnlockBits(bmpData2);
            pictureBox2.Refresh();

            System.Runtime.InteropServices.Marshal.Copy(rgbThreeValues, 0, ptr3, bytes3);
            bmp3.UnlockBits(bmpData3);
            pictureBox3.Refresh();
        }
    }
}
