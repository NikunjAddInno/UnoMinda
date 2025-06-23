using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Measurement_AI.classes
{
    class cls_picboxCrop
    {public static Bitmap get24BitDeepCopy(Bitmap image)
        {
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            return image.Clone(rect, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        }

        public static Rectangle getRectFromPoints(Point p1, Point p2)
        {
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int w = Math.Abs(p2.X - p1.X);
            int h = Math.Abs(p2.Y - p1.Y);

            return new Rectangle(x, y, w, h);
           
        }

        public static Bitmap getCroppedImage(PictureBox pb, Point p1, Point p2)
        {
            Rectangle cropArea = getRectFromPoints(p1, p2);
            Console.WriteLine("rect crop ::" + cropArea.ToString());
            Rectangle rectScaled = new Rectangle(cropArea.X, cropArea.Y,  cropArea.Width,  cropArea.Height);
            Bitmap _img = new Bitmap(rectScaled.Width, rectScaled.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Console.WriteLine("rectangle ::" + rectScaled.ToString());
            if (pb.Image == null)
            {
                return _img;
            }
            Bitmap OriginalImage = new Bitmap(pb.Image, pb.Image.Width, pb.Image.Height);// list_captured[0];// 
            Graphics g = Graphics.FromImage(_img);
            g.DrawImage(OriginalImage, 0, 0, rectScaled, GraphicsUnit.Pixel);
            Console.WriteLine("cropped image size ::" + _img.Size.ToString());

            Bitmap returnImg = _img.Clone(new Rectangle(0, 0, _img.Width, _img.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            return returnImg;
        }
    }
}
