using Microsoft.Win32;
using Modbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace CathaterTipInspection.classes
{
    public enum PlcReg
    {
        NgResult,
        SideAck,
        PlcComm,
        SoftReady
    }

    public static class PlcControl
    {

        static PlcControl()
        {

            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
            //timer.Interval = 200;
            //timer.Tick += Timer_Tick;
            //timer.Start();
        }

        private static void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                StartPlcComm();
                Thread.Sleep(200);
            }

        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            StartPlcComm();
        }

        static bool commthreadFlag;
        static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        static BackgroundWorker backgroundWorker = new BackgroundWorker();
        public static event Action<bool> PlcConnectionStatusAction;
        public static event Action<int> SideAckAction;
        
        static int _side = 0;
        public static int Side { get { return _side; } set 
            {
                int oldValue = _side;
                _side = value;
                if (oldValue != _side)
                {
                    SideAckAction?.Invoke(_side);

                }

            }
        }

        static bool _sensorOn = false;
        static bool SensorOn { get { return _sensorOn; } 
            set 
            {
                bool oldValue = _sensorOn;
                _sensorOn = value;

                if (oldValue != _sensorOn)
                {
                    PlcConnectionStatusAction?.Invoke(_sensorOn);
                }
            } 
        }



        private const int REGISTER_START = 153;
        public static int SoftwareReady = 0;
        
        private static void StartPlcComm()
        {
            SensorOn = ReadDataPLC(PlcReg.PlcComm) == 1;


            if (SensorOn)
            {
                Side = ReadDataPLC(PlcReg.SideAck);
            }
            //Thread.Sleep(3000);

        }
        

        public static int ReadDataPLC(int reg)
        {
            int intValue = 0;
            try
            {
                reg = reg + REGISTER_START + 4096;


                //string hexValue = value.ToString("X4");
                string regNum = reg.ToString("X4");
                clsInputValidation.function(3);
                string tx = string.Concat("01", "03", regNum, "0004");
                //Console.WriteLine("data sent {0}", tx);
                string resp = clsComms.Read(tx);
                //Console.WriteLine("data received{0}", resp);
                // lblResp.Text = resp;

                if (resp != "")
                {
                    string dataHex = resp.Substring(7, 4);
                    intValue = int.Parse(dataHex, System.Globalization.NumberStyles.HexNumber);
                    // lblIntVal.Text = intValue.ToString();
                }
            }
            catch (Exception exx)
            {
                Console.WriteLine(exx.Message);
                return 0;
            }
            return intValue;

        }

        public static int ReadDataPLC(PlcReg register)
        {
            int intValue = 0;
            try
            {
                int reg = Convert.ToInt32(register) + REGISTER_START + 4096;

                //string hexValue = value.ToString("X4");
                string regNum = reg.ToString("X4");
                clsInputValidation.function(3);
                string tx = string.Concat("01", "03", regNum, "0004");
                //Console.WriteLine("data sent {0}", tx);
                string resp = clsComms.Read(tx);
                //Console.WriteLine("data received{0}", resp);
                // lblResp.Text = resp;

                if (resp != "")
                {
                    string dataHex = resp.Substring(7, 4);
                    intValue = int.Parse(dataHex, System.Globalization.NumberStyles.HexNumber);
                    // lblIntVal.Text = intValue.ToString();
                }
            }
            catch (Exception exx)
            {
                Console.WriteLine(exx.Message);
                return 0;
            }
            return intValue;

        }
        public static string WriteDataPLC(int value, PlcReg register)
        {

            int reg = Convert.ToInt32(register) + REGISTER_START + 4096;

            string hexValue = value.ToString("X4");
            string regNum = reg.ToString("X4");
            clsInputValidation.function(6);
            string tx = string.Concat("01", "06", regNum, hexValue);
            // Console.WriteLine("data sent {0}", tx);
            string resp = clsComms.Read(tx);
            // lblResp.Text = resp;
            // Console.WriteLine("data received{0}", resp);
            return "all good";

        }

        public static string WriteDataPLC(int value, int reg)
        {
            reg = reg + REGISTER_START + 4096;

            string hexValue = value.ToString("X4");
            string regNum = reg.ToString("X4");
            clsInputValidation.function(6);
            string tx = string.Concat("01", "06", regNum, hexValue);
            // Console.WriteLine("data sent {0}", tx);
            string resp = clsComms.Read(tx);
            // lblResp.Text = resp;
            // Console.WriteLine("data received{0}", resp);
            return "all good";

        }


    }
}
