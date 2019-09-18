using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        byte[] rgbValues;
        int hueSlider = 0, saturSlider = 0, valueSlider = 0;

        public Form3() => InitializeComponent();

        private void TrackBar1_Scroll_1(object sender, EventArgs e) => hueSlider = trackBar1.Value;

        private void TrackBar2_Scroll_1(object sender, EventArgs e) => saturSlider = trackBar2.Value;

        private void TrackBar3_Scroll_1(object sender, EventArgs e) => valueSlider = trackBar3.Value;

        private void Button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = picture.Image as Bitmap;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            float hue = 0, satur = 0, value = 0;

            for (int counter = 0; counter < rgbValues.Length; counter += 3)
            {
                //RED
                float redValue = rgbValues[counter];
                //GREEN
                float greenValue = rgbValues[counter + 1];
                //BLUE
                float blueValue = rgbValues[counter + 2];
                //Получим значения в процентах
                redValue /= (float)255;
                greenValue /= (float)255;
                blueValue /= (float)255;

                float Max = Math.Max((Math.Max(redValue, greenValue)), blueValue);
                float Min = Math.Min((Math.Min(redValue, greenValue)), blueValue);

                float deltaRgb = Max - Min;

                value = Max;

                //Перевод в HSV

                if (Max != 0f)
                    satur = deltaRgb / Max;
                else
                    satur = 0f;
                if (satur < 0f)
                    hue = -1f;
                else
                {
                    if (redValue == Max)
                        hue = (greenValue - blueValue) / deltaRgb;
                    else if (greenValue == Max)
                        hue = 2f + (blueValue - redValue) / deltaRgb;
                    else if (blueValue == Max)
                        hue = 4f + (redValue - greenValue) / deltaRgb;

                    hue *= 60f;
                    if (hue < 0f)
                        hue += 360f;
                    hue /= 360f;
                }

                //Получаем новые значения со слайдеров

                hue += hueSlider / 360f;
                satur += saturSlider / 100f;
                value += valueSlider / 100f;

                hue = (hue > 1f) ? 1f : hue;
                satur = (satur > 1f) ? 1f : satur;
                value = (value > 1f) ? 1f : value;


                //Перевод обратно в RGB

                int k;
                float aa, bb, cc, f;

                if (satur < 0f)
                    redValue = greenValue = blueValue = value;
                else
                {
                    if (hue == 1f)
                        hue = 0f;
                    hue *= 6f;
                    k = (int)Math.Floor(hue);
                    f = hue - k;
                    aa = value * (1f - satur);
                    bb = value * (1f - (satur * f));
                    cc = value * (1f - (satur * (1f - f)));

                    switch (k)
                    {
                        case 0:
                            redValue = value;
                            greenValue = cc;
                            blueValue = aa;
                            break;
                        case 1:
                            redValue = bb;
                            greenValue = value;
                            blueValue = aa;
                            break;
                        case 2:
                            redValue = aa;
                            greenValue = value;
                            blueValue = cc;
                            break;
                        case 3:
                            redValue = aa;
                            greenValue = bb;
                            blueValue = value;
                            break;
                        case 4:
                            redValue = cc;
                            greenValue = aa;
                            blueValue = value;
                            break;
                        case 5:
                            redValue = value;
                            greenValue = aa;
                            blueValue = bb;
                            break;
                        default:
                            break;
                    }
                }
                redValue *= 255;
                greenValue *= 255;
                blueValue *= 255;

                rgbValues[counter] = (byte)redValue;
                rgbValues[counter + 1] = (byte)greenValue;
                rgbValues[counter + 2] = (byte)blueValue;
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            // Unlock the bits.
            bmp.UnlockBits(bmpData);
            picture.Refresh();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            if (picture.Image != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Сохранить изображение как...";
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;

                sfd.Filter = "Image Files(*.JPG)|*.JPG";

                sfd.ShowHelp = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        picture.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }
                }
            }
        }
    }
}
