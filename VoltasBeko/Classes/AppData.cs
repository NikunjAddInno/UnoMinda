using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VoltasBeko.Classes;

namespace VoltasBeko
{
    public static class AppData
    {

        static AppData()
        {
            Camera.connectListedCam();
            if (Camera.IsConnected)
            {
                Camera.startCamAcq();
            }
            //else
            //{
            //    int tries = 0;
            //    while (Camera.IsConnected == false)
            //    {
            //        Camera.DisconnectCam();
            //        Camera.connectListedCam();

            //        Thread.Sleep(600);
            //        tries++;
            //        ConsoleExtension.WriteWithColor($"Trying to reconnect camera. Trial number {tries}", ConsoleColor.Yellow);
            //        if (tries == 50)
            //        {
            //            break;
            //        }
            //    }
            //    if (Camera.IsConnected)
            //    {
            //        Camera.startCamAcq();

            //    }
            //}
        }

        public static HikCam Camera = new HikCam("MvsCamera", "DA2345241", 0, 5000);
        public static event Action UserRoleChanged;
        private static UserManager.UserRoles  _useRole = UserManager.UserRoles.Admin;

        public static UserManager.UserRoles UserRole { get { return _useRole; }
            set 
            {
                _useRole = value;
                UserRoleChanged?.Invoke();
            }
        }

        private static string globalProjectDirectory = Environment.CurrentDirectory;
        public static string ProjectDirectory = Directory.GetParent(globalProjectDirectory).Parent.Parent.FullName;
        public static event Action<Mode> AppModeChanged;
        private static Mode _appMode = Mode.Inspection;
        public static Mode AppMode { get { return _appMode; } set { _appMode = value; AppModeChanged?.Invoke(_appMode); } }
        public static bool InspectionStarted = false;
        public static string ModelName = "";
        public static string ModelPath
        {
            get { return $@"{ProjectDirectory}/Models/{ModelName}"; }
        }
        public static string SelectedModel = "";
        public static List<string> strings = new List<string> { "", "", ""};
        public static bool AdminUser = false;
        public static string CsvPath = "";
        public static bool DefectsMarkedOk = false;

    }
    public enum Mode
    {
        Idol,
        Inspection,
        Settings,
        Setup
    }
}
