using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace VoltasBeko.Classes
{
    public class Pose
    {
        public Pose()
        {

        }

        public static void SendAllPoseToPlc()
        {
            for (int i = 0; i < AppData.Camera.Poses.Count; i++)
            {
                
                PlcControl.WriteValue(PlcControl.PlcReg.Xaxis + (i * 2), AppData.Camera.Poses[i].location.X * 100);
                PlcControl.WriteValue(PlcControl.PlcReg.Yaxis + (i * 2), AppData.Camera.Poses[i].location.Y * 100);
                PlcControl.WriteValue(PlcControl.PlcReg.Zaxis + (i * 2), AppData.Camera.Poses[i].location.Z * 100) ;

                Console.WriteLine($"Pose value sent to X: {AppData.Camera.Poses[i].location.X}");
                Console.WriteLine($"Pose value sent to Y: {AppData.Camera.Poses[i].location.Y}");
                Console.WriteLine($"Pose value sent to Z: {AppData.Camera.Poses[i].location.Z}");

            }

            PlcControl.WriteValue(PlcControl.PlcReg.TotalPose, AppData.Camera.Poses.Count);

        }


        public Pose(Pose pose)
        {
            location = new PointT(pose.location);
            focus = pose.focus;
            exposure = pose.exposure;
            number = pose.number;
            Id = pose.Id;
            image = pose.image;
            Selected = pose.Selected;
            PoseSelectionChanged = pose.PoseSelectionChanged;
        }
        public int number = 0;
        public string Id = "";
        public PointT location = new PointT(100, 100, 100);
        public int focus = 0;
        public int exposure = 0;
        private bool _selected = false;
        public static bool Activated = false;


        [JsonIgnore]
        public Bitmap image = null;
        [JsonIgnore]
        public bool Selected
        {
            get { return _selected; }
            set
            {
                bool oldValue = _selected;
                _selected = value;
                if (oldValue != _selected)
                {
                    PoseSelectionChanged?.Invoke(this, _selected);
                }
            }
        }

        //private async Task CheckIfPoseActivated()
        //{
        //    while (AppData.TisCamera.Zoom != zoom)
        //    {
        //        await Console.Out.WriteLineAsync($"Pose zoom: {AppData.TisCamera.Zoom} compare zoom {zoom}");
        //        await Task.Delay(10);
        //    }
        //    ConsoleExtension.WriteWithColor("Pose Activated.", ConsoleColor.Green);

        //    Activated = true;
        //}

        public event EventHandler<bool> PoseSelectionChanged;

    }
    public class PointT
    {
        public PointT()
        {
            
        }

        public PointT(PointT p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }

        public PointT(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        private int _x = 0;
        public int X
        {
            get { return _x; }
            set
            {
                if (value < 300)
                {
                    _x = value;
                }
            }
        }
        private int _y = 0;
        public int Y { get { return _y; } 
            set 
            {
                if (value < 340 || value > 9)
                {
                    _y = value;
                }
            }
        }

        private int _z = 0;
        public int Z
        {
            get { return _z; }
            set
            {
                if (value < 65)
                {
                    _z = value;
                }
            }
        }

    }
}
