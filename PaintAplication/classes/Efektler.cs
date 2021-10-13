using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintAplication.classes
{
    class Efektler
    {

        public Bitmap griyap(Bitmap bmp)
        {


            for (int i = 0; i < bmp.Height-1; i++)
            {
                for (int j = 0; j < bmp.Width-1; j++)
                {

                    int deger = (bmp.GetPixel(j, i).R + bmp.GetPixel(j, i).G + bmp.GetPixel(j, i).B) / 3;
                    Color renk;

                    renk = Color.FromArgb(deger, deger, deger);
                    bmp.SetPixel(j, i, renk);
                }
            }

            return bmp;
        }


        public Bitmap sobel(Bitmap image)
        {
            Bitmap gri = griyap(image);
            Bitmap buffer = new Bitmap(gri.Width, gri.Height);
            Color renk;
            int valX, valY,gradient;
            int[,] GX = new int[3, 3];
            int[,] GY = new int[3, 3];

            // Yatay yönde kenar // horizontal sobel mask
            GX[0, 0] = 1; GX[0, 1] = 0; GX[0, 2] = -1;
            GX[1, 0] = 2; GX[1, 1] = 0; GX[1, 2] = -2;
            GX[2, 0] = 1; GX[2, 1] = 0; GX[2, 2] = -1;

            // Dikey yönde kenar // horizontal sobel mask
            GX[0, 0] = -1; GX[0, 1] = -2; GX[0, 2] = -1;
            GX[1, 0] = 0; GX[1, 1] = 0; GX[1, 2] = 0;
            GX[2, 0] = 1; GX[2, 1] = 2; GX[2, 2] = 1;


            for (int i = 0; i < gri.Height - 1; i++)
            {
                for (int j = 0; j < gri.Width - 1; j++)
                {

                    if (i == 0 || i == gri.Height -1 || j==0 ||j==gri.Width-1)
                    {
                        renk = Color.FromArgb(255,255,255);
                        buffer.SetPixel(j, i, renk);

                        valX = 0;
                        valY = 0;

                    }
                    else
                    {
                        valX = gri.GetPixel(j - 1, i - 1).R * GX[0, 0] +
                               gri.GetPixel(j, i - 1).R     * GX[0, 1] +
                               gri.GetPixel(j+1, i - 1).R   * GX[0, 2] +
                               gri.GetPixel(j - 1, i ).R    * GX[1, 0] +
                               gri.GetPixel(j , i ).R       * GX[1, 1] +
                               gri.GetPixel(j+1, i).R       * GX[1, 2] +
                               gri.GetPixel(j - 1, i+1).R   * GX[2, 0] +
                               gri.GetPixel(j, i + 1).R     * GX[2, 1] +
                               gri.GetPixel(j + 1, i + 1).R * GX[2, 2];

                        valY = gri.GetPixel(j - 1, i - 1).R * GY[0, 0] +
                               gri.GetPixel(j, i - 1).R     * GY[0, 1] +
                               gri.GetPixel(j + 1, i - 1).R * GY[0, 2] +
                               gri.GetPixel(j - 1, i).R     * GY[1, 0] +
                               gri.GetPixel(j, i).R         * GY[1, 1] +
                               gri.GetPixel(j + 1, i).R     * GY[1, 2] +
                               gri.GetPixel(j - 1, i + 1).R * GY[2, 0] +
                               gri.GetPixel(j, i + 1).R     * GY[2, 1] +
                               gri.GetPixel(j + 1, i + 1).R * GY[2, 2];

                        gradient = (int)(Math.Abs(valX) + Math.Abs(valY));

                        if (gradient < 0) { gradient = 0; }
                        if (gradient > 255) { gradient = 255; }

                        renk = Color.FromArgb(gradient, gradient, gradient);
                        buffer.SetPixel(j, i, renk);

                    }
                    
                }
            }

            return buffer;
        }

    }
}
