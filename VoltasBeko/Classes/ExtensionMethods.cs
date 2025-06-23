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

namespace VoltasBeko
{
    public static class TextBoxExtension
    {
        // int? is a nullable int
        public static dynamic Digit(this TextBox textBox)
        {
            int inValue;
            if (int.TryParse(textBox.Text, out inValue))
            {
                return inValue;
            }
            float floatValue;
            if (float.TryParse(textBox.Text, out floatValue))
            {
                return floatValue;
            }
            return null;
        }
    }


   

    public static class PictureBoxExtension
    {
        // int? is a nullable int
        public static void SetImage(this PictureBox pictureBox, Bitmap bitmap)
        {
            try
            {
                pictureBox.Invoke(new Action(() =>
                {
                    pictureBox.Image = bitmap;
                }));
            }
            catch (Exception ex)
            {
                ConsoleExtension.WriteWithColor(ex, ConsoleColor.Red);
            }
          
        }
    }

    public static class DataGridViewExtension
    {
        public static void SaveToCsv(this DataGridView dataGridView)
        {
            CsvExporter.ExportToCsv(dataGridView);
        }
    }

    public static class ButtonExtension
    {
        public static void FitImage(this Button button, string imagePath)
        {
            Bitmap bitmap;

            bitmap = new Bitmap(imagePath);

            var cam_ico_new = new Bitmap(bitmap, new Size(35, 35));
            button.Image = cam_ico_new;
            button.ForeColor = button.Parent.BackColor;
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

        public static Bitmap LoadImageSafe(string path)
        {
            using (var fs = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                var bmp = new Bitmap(fs);
                return (Bitmap)bmp.Clone();
            }

        }

    }
    public static class ComboBoxExtension
    {
        public static void LoadDirectoryNames(this ComboBox comboBox, string dirPath)
        {
            try
            {
                comboBox.Items.Clear();

                foreach (string dirName in Directory.GetDirectories(dirPath))
                {
                    comboBox.Items.Add(Path.GetFileName(dirName));
                }
                if (comboBox.Items.Count > 0)
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
