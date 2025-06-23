using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace CathaterTipInspection.classes
{
    class pbClass
    {
        public static void setAspectRatio(ref PictureBox pb)
        {

            if (pb.Image != null)
            {
                pb.Height = (int)((float)pb.Width * ((float)pb.Image.Height / (float)pb.Image.Width));
                //  this.Refresh();
            }

        }
        public static Bitmap get8BitImage(Bitmap imageIN)
        { 
             return  imageIN.Clone(new Rectangle(0, 0, imageIN.Width, imageIN.Height), System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

        }

    }
    class imageFns_bmp
    {
        public static Bitmap get24BitDeepCopy(Bitmap image)
        {
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            return image.Clone(rect, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        }
        public static Rectangle correctSize(Rectangle cropArea)
        {
            int adjX = cropArea.Width % 4;
            int adjY = cropArea.Height % 4;
            if (adjX != 0)
            {
                cropArea.Width = cropArea.Width + (4 - adjX);
            }
            if (adjY != 0)
            {
                cropArea.Height = cropArea.Height + (4 - adjY);
            }
            return cropArea;
        }
        public static Bitmap cropFromBitmap(Bitmap image, Rectangle cropArea)
        {
            //Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            int adjX = cropArea.Width % 4;
            int adjY = cropArea.Height % 4;
            if (adjX != 0)
            {
                cropArea.Width = cropArea.Width + (4 - adjX);
            }
            if (adjY != 0)
            {
                cropArea.Height = cropArea.Height + (4 - adjY);
            }
            return image.Clone(cropArea, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        }
        public static Rectangle getRectFromPoints(Point p1, Point p2)
        {
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int w = Math.Abs(p2.X - p1.X);
            int h = Math.Abs(p2.Y - p1.Y);
            Rectangle cropArea= new Rectangle(x, y, w, h);

            //size adjustment for cpp mod4 
            int adjX = cropArea.Width % 4;
            int adjY = cropArea.Height % 4;
            if (adjX != 0)
            {
                cropArea.Width = cropArea.Width + (4 - adjX);
            }
            if (adjY != 0)
            {
                cropArea.Height = cropArea.Height + (4 - adjY);
            }

            return cropArea;

        }

        public static Bitmap getCroppedImage(PictureBox pb, Point p1, Point p2)
        {
            Rectangle cropArea = getRectFromPoints(p1, p2);
            ////size adjustment for cpp mod4 
            //int adjX = cropArea.Width % 4;
            //int adjY = cropArea.Height % 4;
            //if (adjX != 0)
            //{
            //    cropArea.Width = cropArea.Width + (4-adjX);
            //}
            //if (adjY != 0)
            //{
            //    cropArea.Height = cropArea.Height + (4-adjY);
            //}
            //-------
            Console.WriteLine("rect crop ::" + cropArea.ToString());
            Rectangle rectScaled = new Rectangle(cropArea.X, cropArea.Y, cropArea.Width, cropArea.Height);
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

        public static Bitmap getCroppedImage(PictureBox pb, Rectangle r)
        {
            Rectangle cropArea = r;// getRectFromPoints(p1, p2);
            ////size adjustment for cpp mod4 
            //int adjX = cropArea.Width % 4;
            //int adjY = cropArea.Height % 4;
            //if (adjX != 0)
            //{
            //    cropArea.Width = cropArea.Width + (4-adjX);
            //}
            //if (adjY != 0)
            //{
            //    cropArea.Height = cropArea.Height + (4-adjY);
            //}
            //-------
            Console.WriteLine("rect crop ::" + cropArea.ToString());
            Rectangle rectScaled = new Rectangle(cropArea.X, cropArea.Y, cropArea.Width, cropArea.Height);
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

        public static int readImage_nonBlocking(String imagePath, ref Bitmap imgOut)
        {
            if (File.Exists(imagePath))
            {
                //Bitmap b = (Bitmap)Bitmap.FromFile(templatePathUpper).Clone();
                //    picModelImage.Image = b;
                //   picModelImage.Invalidate();
                Image img = null;
                using (Image imgTmp = Image.FromFile(imagePath))
                {
                    img = new Bitmap(imgTmp.Width, imgTmp.Height, imgTmp.PixelFormat);
                    Graphics gdi = Graphics.FromImage(img);
                    gdi.DrawImageUnscaled(imgTmp, 0, 0);
                    gdi.Dispose();
                    imgTmp.Dispose(); // just to make sure
                }
                imgOut = get24BitDeepCopy((Bitmap)img);
                return 1;
            }
            return -1;
        }
    }
}
