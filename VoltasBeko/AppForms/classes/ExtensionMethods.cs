using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CathaterTipInspection
{
    public static class TextBoxExtension
    {
        // int? is a nullable int
        public static dynamic Digit(this TextBox textBox)
        {
            int value;
            if (int.TryParse(textBox.Text, out value))
            {
                return value;
            }
            return null;
        }
    }

 
    public static class PictureBoxExtension
    {
        public static void SetImage(this PictureBox pictureBox, Bitmap bitmap)
        {
            if (pictureBox.Created)
            {
                pictureBox.Invoke(new Action(() => 
                {
                    pictureBox.Image = bitmap.DeepClone();
                }));
            }
        }
    }

    public static class ButtonExtension
    {
        public static void FitImage(this Button button, string imagePath, Size? size = null)
        {
            // Default to a size of 50x50 if no size is provided
            Size finalSize = size ?? new Size(55, 55);

            Bitmap bitmap;
            bitmap = new Bitmap(imagePath);

            var cam_ico_new = new Bitmap(bitmap, finalSize);
            button.ForeColor = button.Parent.BackColor;
            button.Image = cam_ico_new;
        }
    }

    public static class ConsoleExtension
    {
        public static void WriteWithColor(dynamic dynamic, ConsoleColor consoleColor = ConsoleColor.Blue)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(dynamic);
            Console.ResetColor();
        }
        public static void WriteError(dynamic dynamic, ConsoleColor consoleColor = ConsoleColor.Red)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(dynamic);
            Console.ResetColor();
        }
    }

    public static class BitmapExtension
    {
        public static Bitmap DeepClone(this Bitmap bitmap, PixelFormat pixelFormat = PixelFormat.Format24bppRgb)
        {
            return bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), pixelFormat);
        }
    }

    public static class ComboBoxExtension
    {
        public static void LoadDirectoryNames(this ComboBox comboBox, string dirPath, bool setIndex = true)
        {
            try
            {
                comboBox.Items.Clear();

                foreach (string dirName in Directory.GetDirectories(dirPath))
                {
                    comboBox.Items.Add(Path.GetFileName(dirName));
                }
                if (comboBox.Items.Count > 0 && setIndex)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
