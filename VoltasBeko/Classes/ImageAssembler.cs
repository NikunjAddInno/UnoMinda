using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using tryThresholds.IP_tools;
using VoltasBeko;
using static tryThresholds.IP_tools.resultFrontCam;

public class ImageAssembler
{
    private int baseWidth = 2048;
    private int baseHeight = 2448;
    private int fullWidth;
    private int fullHeight;
    private float mmToPixelX;
    private float mmToPixelY;
    public Bitmap Canvas;
    private string outputPath = "final_output.bmp"; 

    public ImageAssembler()
    {
        fullWidth = baseWidth * 7;  
        fullHeight = baseHeight * 6; 

        mmToPixelX = baseWidth / 44.0f;  // Pixels per mm in width
        mmToPixelY = baseHeight / 53.0f; // Pixels per mm in height

        Canvas = new Bitmap(fullWidth, fullHeight);
        using (Graphics g = Graphics.FromImage(Canvas))
        {
            g.Clear(Color.Black);
        }
    }
    public void ResetImage()
    {
        Canvas = new Bitmap(fullWidth, fullHeight);
        using (Graphics g = Graphics.FromImage(Canvas))
        {
            g.Clear(Color.Black);
        }
        points.Clear();
        defects.Clear();
    }
    //public void LoadJsonAndDrawImages(string jsonPath, string imageFolder)
    //{
    //    if (!File.Exists(jsonPath))
    //    {
    //        Console.WriteLine("JSON file not found!");
    //        return;
    //    }

    //    string jsonData = File.ReadAllText(jsonPath);
    //    var imageEntries = JsonConvert.DeserializeObject<List<ImageEntry>>(jsonData);

    //    foreach (var entry in imageEntries)
    //    {
    //        string imagePath = Path.Combine(imageFolder, entry.Id + ".bmp");
    //        if (File.Exists(imagePath))
    //        {
    //            using (Bitmap smallImage = new Bitmap(imagePath))
    //            {
    //                DrawImageAtPosition(smallImage,340- entry.Location.Y, entry.Location.X);
    //            }
    //        }
    //        else
    //        {
    //            Console.WriteLine($"Image not found: {imagePath}");
    //        }
    //    }

    //    SaveCanvas();
    //}
    private List<Point> points = new List<Point>();
    private List<DefectData> defects = new List<DefectData>();

    public class DefectData
    {
        public Rectangle rect = new Rectangle();
        public bool result = false;
        public string name = "";

    }
    public void DrawImageAtPosition(Bitmap smallImage, float x_mm, float y_mm, List<DefectDetails> details)
    {
        int x_px = (int)(x_mm * mmToPixelX);
        int y_px = (int)(y_mm * mmToPixelX);

        //Console.WriteLine($"Drawing Image: X (mm) = {x_mm}, Y (mm) = {y_mm} -> X (px) = {x_px}, Y (px) = {y_px}");


        if (x_px < 0 || y_px < 0 || x_px + smallImage.Width > fullWidth || y_px + smallImage.Height > fullHeight)
        {
            //return;
            throw new ArgumentOutOfRangeException("Position is out of canvas bounds.");
        }

        points.Add(new Point(x_px, y_px));

        for (int i = 0; i < details.Count; i++)
        {
            cvRect cvRect = new cvRect(details[i].CvRect.X + x_px, details[i].CvRect.Y + y_px, details[i].CvRect.Width, details[i].CvRect.Height);
            DefectData defectData = new DefectData { rect = cvRect.ToRectangle(), result = details[i].Result, name = details[i].DefectName };
            defects.Add(defectData);
        }

       

        using (Graphics g = Graphics.FromImage(Canvas))
        {
            g.DrawImage(smallImage,x_px, y_px, smallImage.Width, smallImage.Height);
        }
    }

    public Bitmap CropCanvas()
    {
        if (points.Count > 0)
        {


            Bitmap bitmap;

            int maxX = points.Max(p => p.X);
            int maxY = points.Max(p => p.Y);
            int minX = points.Min(p => p.X);
            int minY = points.Min(p => p.Y);

            //ConsoleExtension.WriteWithColor($"{JsonConvert.SerializeObject(points, Formatting.Indented)}");

            foreach (DefectData item in defects)
            {
                using (Graphics g = Graphics.FromImage(Canvas))
                {
                    Pen p;
                    if (item.result == true)
                    {
                        p = new Pen(Color.Green, 25);
                    }
                    else
                    {
                        p = new Pen(Color.Red, 25);
                    }
                    g.DrawRectangle(p, item.rect);
                    Font font =  new System.Drawing.Font("Nirmala UI", 44, System.Drawing.FontStyle.Regular);
                    g.DrawString(item.name, font, Brushes.Yellow, item.rect);
                }
            }

            Rectangle rectangle = new Rectangle(minX, minY, (maxX - minX) + baseWidth, (maxY - minY) + baseHeight);
            bitmap = Canvas.Clone(rectangle, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //Console.WriteLine($"Canvas size {bitmap.Size}");
            //bitmap.DeepClone();

            points.Clear();
            defects.Clear();
            return bitmap;
        }
        else
        {
            return null;
        }
    }

    private void SaveCanvas()
    {
        try
        {
            Canvas.Save(outputPath);
            Console.WriteLine($"Canvas saved successfully at {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving canvas: {ex.Message}");
        }
    }

    private class ImageEntry
    {
        public int Number { get; set; }
        public string Id { get; set; }
        public ImageLocation Location { get; set; }
    }

    private class ImageLocation
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; } // Not used in drawing
    }
}
