using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoltasBeko
{
    internal static class Program
    {
        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern bool SetConsoleCtrlHandler(Action handler, bool add);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //SetConsoleCtrlHandler(OnConsoleExit, true);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InspectionForm());
        }

        private static void OnConsoleExit()
        {
           // Cleanup();
        }

        private static void Cleanup()
        {
            //AppData.Camera.DisconnectCam();
        }
    }
}
