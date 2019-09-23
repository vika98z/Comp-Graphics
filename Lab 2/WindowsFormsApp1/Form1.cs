using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		byte[] rgbValues;

        public Form1()
		{
			InitializeComponent();
		}

        private void rgb_Division()
        {
            Bitmap bmp = picture.Image as Bitmap;

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int counter = 0; counter < rgbValues.Length; counter += 3)
            {
                rgbValues[counter] = 0;
                rgbValues[counter + 2] = 0;
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);
        }


		private void buttonrgb_Click(object sender, EventArgs e)
		{
            Bitmap bmp = picture.Image as Bitmap;

            rgb_Division();

            picture.Refresh();
            this.buttongist.Enabled = false;
		}

		private void buttongist_Click(object sender, EventArgs e)
        {
            rgb_Division();
            for (int counter = 0; counter < rgbValues.Length; counter += 3)
            {
                if (counter % 1000 == 0)
                    this.chart1.Series["Green"].Points.Add(rgbValues[counter + 1]);
            }
            this.buttonrgb.Enabled = true;
            this.buttongist.Enabled = false;
        }
    }
}
