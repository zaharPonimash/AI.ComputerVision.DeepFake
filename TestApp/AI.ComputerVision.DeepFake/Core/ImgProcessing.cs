using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace AI.ComputerVision.DeepFake.Core
{
    public class ImgProcessing
    {
        public static Bitmap BlendImages(Bitmap src, Bitmap ins, Rectangle r)
        {
           
            Bitmap bitmap = new Bitmap(src.Width, src.Height);
            Bitmap mask2 = new Bitmap(ins.Width, ins.Height);

            Vector centr = new double[] { r.Width / 2, r.Height / 2}.ToVector();


            for (int i = 0; i < ins.Width; i++)
            {
                for (int j = 0; j < ins.Height; j++)
                {
                    Color color = ins.GetPixel(i, j);

                    int k = coef(centr, i, j, r.Height, r.Width);
                    mask2.SetPixel(i, j, Color.FromArgb(k, color));
                }
            }



            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(src, 0, 0);
            g.DrawImage(mask2, r.X, r.Y);

            return bitmap;
        }

        private static int coef(Vector centr, int i, int j, double h, double w)
        {
            double r = Math.Pow(Math.Pow((centr[0] - i) / (1.2*w), 2) + Math.Pow((centr[1] - j) / h, 2), 3);
            r = Math.Exp(-190 * r);
            return (int)(255 * r);
        }

        private static Rectangle GetRectangleMask(Bitmap mask)
        {

            Matrix matr = ImgConverter.BmpToMatr(mask);

            int startX = 0, endX = 0, startY = 1 , endY = mask.Height;

            for (int i = 1; i < mask.Width; i++)
            {
                for (int j = startY; j < endY; j++)
                {
                    if (matr[j, i-1] < 0.1 && matr[j, i] > 0.1)
                    {
                        startX = i;
                    }

                    if (matr[j, i - 1] > 0.1 && matr[j, i] < 0.1)
                    {
                        endX = i;
                        goto r;
                    } 
                    if (matr[j-1, i] < 0.1 && matr[j, i] > 0.1)
                    {
                        startY = j;
                    }

                    if (matr[j-1, i] > 0.1 && matr[j, i] < 0.1)
                    {
                        endY = j;
                    }
                }
            }

            r:

            return new Rectangle(startX, startY, endX - startX, endY - startY);
        }

        /// <summary>
        /// Перенос цвета
        /// </summary>
        /// <param name="src">Исходное изображение</param>
        /// <param name="ins">Встраиваемое</param>
        /// <returns>Изображение региона</returns>
        static public Bitmap NewColor(Bitmap src, Bitmap ins, Rectangle r)
        {

            List<Vector> srV = new List<Vector>();
            List<Vector> inV = new List<Vector>(); ;


            for (int i = r.X; i < r.X + r.Width-2; i+=3)
            {
                for (int j = r.Y; j < r.Y + r.Height-3; j+=4)
                {
                    Color color = src.GetPixel(i, j);
                    Color color2 = ins.GetPixel(i, j);
                    srV.Add(new double[] { color.R, color.G, color.B }.ToVector());
                    inV.Add(new double[] { color2.R, color2.G, color2.B }.ToVector());
                }
            }

            Vector meanSrc = Vector.Mean(srV.ToArray()) / 255;
            Vector meanInk = Vector.Mean(inV.ToArray()) / 255;

            Tensor tensor = ImgConverter.BmpToTensor(ins.Clone(r, PixelFormat.Format32bppArgb));


            tensor = tensor.DivD(meanInk);
            tensor = tensor.PlusD(meanSrc);

            tensor = tensor.TransformTensor(x =>
            {
                if (x < 0)
                {
                    x = 0;
                }

                if (x > 1)
                {
                    x = 1;
                }

                return x;
            });

            return ImgConverter.TensorToBitmap(tensor);
        }



        public static Bitmap GetBitmap(Bitmap src, Bitmap ins, Bitmap mask)
        {
            Stopwatch stopwatch = new Stopwatch();
            string data = "";

            stopwatch.Start();
            Rectangle r = GetRectangleMask(mask);
            stopwatch.Stop();
            data += "region: " + stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            Bitmap bitmap = NewColor(src, ins, r);
            stopwatch.Stop();
            data += "\ncolor: " + stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            bitmap = BlendImages(src, bitmap, r);
            stopwatch.Stop();
            data += "\nblend: " + stopwatch.ElapsedMilliseconds;


            Console.WriteLine(data);
            return bitmap;
        }


    }
}
