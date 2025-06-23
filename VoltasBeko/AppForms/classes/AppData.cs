using CathaterTipInspection.CustomControls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CathaterTipInspection.classes
{
    static class AppData
    {
        static AppData()
        {
            Init();     
        }

        public static List<CameraDetailsDTO> cameraDetailsDTO = new List<CameraDetailsDTO>();
        private static string globalProjectDirectory = Environment.CurrentDirectory;
        public static string ProjectDirectory = Directory.GetParent(globalProjectDirectory).Parent.Parent.FullName;
        public static Mode appMode = Mode.Idol;
        public static bool InspectionStarted = false;
        public static string ModelName = "";
        public static string SelectedModel = "";
        public static int ColumnCount = 0;
        public static int RowCount = 0;
        public static DockStyle PanelDockStyle = DockStyle.None;
        private static void Init()
        {
            cameraDetailsDTO = JsonConvert.DeserializeObject<List<CameraDetailsDTO>>(File.ReadAllText($@"{AppData.ProjectDirectory}/Settings/CameraList.json"));
        }


    }

   

    public enum Mode
    {
        Idol,
        Inspection,
        Settings,
        CreateModel
    }
}
