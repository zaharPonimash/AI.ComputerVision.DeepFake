using AI.ComputerVision.DeepFake.Core;
using System.Drawing;
using System.Drawing.Imaging;

namespace AI.ComputerVision.DeepFake
{
    public class ReplFaceImg
    {
        public static Bitmap Repl(Bitmap bitmap, Bitmap targFace)
        {
            Dlib.FaceData haar = new Dlib.FaceData();

            Rectangle rectangle = haar.AccurateFaceDetection(bitmap);
            Rectangle rectangle2 = haar.AccurateFaceDetection(targFace);

            Bitmap face = targFace.Clone(rectangle2, PixelFormat.Format32bppArgb);
            face = new Bitmap(face, rectangle.Width, rectangle.Height);

            Bitmap mask = new Bitmap(bitmap.Width, bitmap.Height);
            Bitmap dst = new Bitmap(bitmap.Width, bitmap.Height);


            Graphics g = Graphics.FromImage(dst);
            g.Clear(Color.Black);
            g.DrawImage(face, rectangle);


            Graphics g1 = Graphics.FromImage(mask);
            g1.Clear(Color.Black);
            g1.FillRectangle(Brushes.White,rectangle);

            Bitmap bitmap1 = ImgProcessing.GetBitmap(bitmap, dst, mask);



            return bitmap1;

        }
    }
}
