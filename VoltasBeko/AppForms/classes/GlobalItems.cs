using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement_AI.classes
{
    class GlobalItems
    {
        public static Size camImageSize = new Size(1024, 1280);// ;// new Size(3648, 5472);// new Size(3648, 5472);new Size(  5472, 3648 );// 
        // public static List<DrawShapes434OnpicBox> list_appliedTools= new List<DrawShapesOnpicBox>();
        //  public static List<MeasurementTools> list_toolCpp = new List<MeasurementTools>();
        public static  float zoomfactor = 1;
        public static float getImageAspectRatio()
        {
            return (float)camImageSize.Width / (float)camImageSize.Height;
        }
        public static float getImageAspectRatio(float width , float height)
        {
            return (float)width / (float)height;
        }

        //current ModelData
        public static String currentModelPath = "";
        public static String currentModelName = "";

        public static algorithmLib.Class1 algoC1 = new algorithmLib.Class1();
        public static algorithmLib.Class1 algoC2 = new algorithmLib.Class1();
    }
}
