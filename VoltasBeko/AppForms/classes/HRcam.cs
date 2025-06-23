using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace Measurement_AI.classes
{
    class HRcam
    {
        public string name = "default";
       public string serialNo = "010100101";
       public float expTime = 12000.0F;
        bool ConnStatus = false;
        bool grabStatus = false;

        public bool isConnected { get => ConnStatus; set => ConnStatus = value; }
        public bool isGrabbing { get => grabStatus; set => grabStatus = value; }
    }

    class HRcamFns
    {
        public  List<HRcam> listOfCamObs_u = new List<HRcam>();
        public  MyCamera.MV_CC_DEVICE_INFO_LIST m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        int op_pulseDuration_us = 100000;
        void HRCamFns() //init blank lists
        {
            listOfCamObs_u = new List<HRcam>();
            m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        }
        private void ShowErrorMsg(string csMessage, int nErrorNum)
        {
            string errorMsg;
            if (nErrorNum == 0)
            {
                errorMsg = csMessage;
            }
            else
            {
                errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
            }

            switch (nErrorNum)
            {
                case MyCamera.MV_E_HANDLE: errorMsg += " Error or invalid handle "; break;
                case MyCamera.MV_E_SUPPORT: errorMsg += " Not supported function "; break;
                case MyCamera.MV_E_BUFOVER: errorMsg += " Cache is full "; break;
                case MyCamera.MV_E_CALLORDER: errorMsg += " Function calling order error "; break;
                case MyCamera.MV_E_PARAMETER: errorMsg += " Incorrect parameter "; break;
                case MyCamera.MV_E_RESOURCE: errorMsg += " Applying resource failed "; break;
                case MyCamera.MV_E_NODATA: errorMsg += " No data "; break;
                case MyCamera.MV_E_PRECONDITION: errorMsg += " Precondition error, or running environment changed "; break;
                case MyCamera.MV_E_VERSION: errorMsg += " Version mismatches "; break;
                case MyCamera.MV_E_NOENOUGH_BUF: errorMsg += " Insufficient memory "; break;
                case MyCamera.MV_E_UNKNOW: errorMsg += " Unknown error "; break;
                case MyCamera.MV_E_GC_GENERIC: errorMsg += " General error "; break;
                case MyCamera.MV_E_GC_ACCESS: errorMsg += " Node accessing condition error "; break;
                case MyCamera.MV_E_ACCESS_DENIED: errorMsg += " No permission "; break;
                case MyCamera.MV_E_BUSY: errorMsg += " Device is busy, or network disconnected "; break;
                case MyCamera.MV_E_NETER: errorMsg += " Network error "; break;
            }

            MessageBox.Show(errorMsg, "PROMPT");
        }
        public int SearchAllCameras() //DeviceListAcq
        {
            // ch:创建设备列表 | en:Create Device List
            System.GC.Collect();
          //  cbDeviceList.Items.Clear();
            m_stDeviceList.nDeviceNum = 0;
            int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_stDeviceList);
            if (0 != nRet)
            {
                ShowErrorMsg("Enumerate devices fail! class", 0);
                return 0;
            }

            // ch:在窗体列表中显示设备名 | en:Display device name in the form list
            for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));

                    if (gigeInfo.chUserDefinedName != "")
                    {
                        Console.WriteLine("GEV: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                    else
                    {
                     Console.WriteLine("GEV: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                }
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    if (usbInfo.chUserDefinedName != "")
                    {
                      Console.WriteLine("U3V: " + usbInfo.chUserDefinedName + " (" + usbInfo.chSerialNumber + ")");
                    }
                    else
                    {
                      Console.WriteLine("U3V: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
                    }
                }
            }

            // ch:选择第一项 | en:Select the first item
            if (m_stDeviceList.nDeviceNum != 0)
            {
                Console.WriteLine("total cameras detected : " + m_stDeviceList.nDeviceNum.ToString());
            }
            else 
            {
                Console.WriteLine("no cameras detected");
                MessageBox.Show("No camera found. Check camera cables and restart the system.");
            }
            return (int)m_stDeviceList.nDeviceNum;
        }

        public int connectListedCam(ref MyCamera m_MyCamera,int camNum)
        {
            if (m_stDeviceList.nDeviceNum == 0 )
            {
                ShowErrorMsg("No device, please select", 0);
                return 0;
            }
            MyCamera.MV_CC_DEVICE_INFO device;
            for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
            {
                // ch:获取选择的设备信息 | en:Get selected device information
                MyCamera.MV_CC_DEVICE_INFO  deviceTmp = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i],
                                                                  typeof(MyCamera.MV_CC_DEVICE_INFO));
                //get serial number from device data
                String serTemp = "0000";
                if (deviceTmp.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(deviceTmp.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    serTemp = gigeInfo.chSerialNumber;
                }
                else
                {
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(deviceTmp.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    serTemp = usbInfo.chSerialNumber;
                }
                    //Console.WriteLine("serial no of camera at index :" + i.ToString() + "  " + gigeInfo.chSerialNumber);
                if (listOfCamObs_u[camNum].serialNo.Equals(serTemp))
                {
                    Console.WriteLine("camera match found :" + listOfCamObs_u[camNum].name);

                    device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i],
                                                                 typeof(MyCamera.MV_CC_DEVICE_INFO));
                    if (null == m_MyCamera)
                    {
                        m_MyCamera = new MyCamera();
                        if (null == m_MyCamera)
                        {
                            listOfCamObs_u[camNum].isConnected = false;
                            break;
                        }
                    }

                    int nRet = m_MyCamera.MV_CC_CreateDevice_NET(ref device);
                    if (MyCamera.MV_OK != nRet)
                    {
                        listOfCamObs_u[camNum].isConnected = false;
                        break;
                    }

                    nRet = m_MyCamera.MV_CC_OpenDevice_NET();
                    if (MyCamera.MV_OK != nRet)
                    {
                        m_MyCamera.MV_CC_DestroyDevice_NET();
                        ShowErrorMsg("Device open fail!", nRet);
                        listOfCamObs_u[camNum].isConnected = false;
                        break;
                    }
               
                    listOfCamObs_u[camNum].isConnected = true;
                    if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                    {
                        int nPacketSize = m_MyCamera.MV_CC_GetOptimalPacketSize_NET();
                        if (nPacketSize > 0)
                        {
                            nRet = m_MyCamera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                            if (nRet != MyCamera.MV_OK)
                            {
                                ShowErrorMsg("Set Packet Size failed!", nRet);
                            }
                        }
                        else
                        {
                            ShowErrorMsg("Get Packet Size failed!", nPacketSize);
                        }
                    }

                }
                else
                {
                  //  Console.WriteLine("EXPECTED CAMERA SERIAL :" + listOfCamObs_u[camNum].serialNo);
                }
            }
            // ch:打开设备 | en:Open device

            //    int nRet = m_MyCamera.MV_CC_CreateDevice_NET(;


            // ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)


            // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
            if (listOfCamObs_u[camNum].isConnected)
            {
                m_MyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", (uint)MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
              //  m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);

                ConfigureOutLine(ref m_MyCamera, op_pulseDuration_us, 1);
               // ConfigureGPIOLine5v(ref m_MyCamera, 5*op_pulseDuration_us, 2);
                m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
                m_MyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
                m_MyCamera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);
                int nRet = m_MyCamera.MV_CC_SetFloatValue_NET("ExposureTime", listOfCamObs_u[camNum].expTime);
                if (nRet != MyCamera.MV_OK)
                {
                    ShowErrorMsg("Set Exposure Time Fail!", nRet);
                }
                else
                {
                    Console.WriteLine("Exposuere set successfully " + listOfCamObs_u[camNum].name + "   "+ listOfCamObs_u[camNum].expTime.ToString());
                }
                listOfCamObs_u[camNum].isGrabbing = true;
                return 1;

            }
            return 0;
        }

        public  int disconnectCam(ref MyCamera m_MyCamera)
        {
            int nRet;

            nRet = m_MyCamera.MV_CC_CloseDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return 0;
            }

            nRet = m_MyCamera.MV_CC_DestroyDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return 0;
            }
            return 1;
            // ch:取流标志位清零 | en:Zero setting grabbing flag bit
            // m_bGrabbing = false;
        }

        public void setTriggerMode(ref MyCamera m_MyCamera, bool softwareTrig)
        {
            m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
           // m_MyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
            if (softwareTrig)
            {
                m_MyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
       
            }
            else
            {
                m_MyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
            }
        }

        public void setCamExposure(ref MyCamera m_MyCamera, float exposureTime)
        {
            if (m_MyCamera != null)
            {
                m_MyCamera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);
                int nRet = m_MyCamera.MV_CC_SetFloatValue_NET("ExposureTime", exposureTime);

            }
        }
        public int triggerSoftwareCam(ref MyCamera m_pMyCamera)
        {
            int nRet;

            
                nRet = m_pMyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
                if (MyCamera.MV_OK != nRet)
                {
                    Console.WriteLine("software trigger error:  "+ String.Format("No.{0} Set software trigger failed! nRet=0x{1}\r\n", ( 1).ToString(), nRet.ToString("X")));
                }
            return nRet;
        }
        public void setCamStatusIndicator(ref Panel p, int camIndex)
        {
            if (listOfCamObs_u[camIndex].isGrabbing)
            {
                p.BackColor = Color.LimeGreen;
                return;
            }
            if (listOfCamObs_u[camIndex].isConnected)
            {
                p.BackColor = Color.Yellow;
                return;
            }
            else
            {
                p.BackColor = Color.Red;
                return;
            }

        }
        public void ConfigureOutLine(ref MyCamera m_MyCamera,int pulseDuration_us=10000, int lineNo=1)
        {
            int nRet;
            Console.WriteLine("s1 LineSelector");
            nRet = m_MyCamera.MV_CC_SetEnumValue_NET("LineSelector", (uint)lineNo);

    
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Set Fail!", nRet);
                return;
            }

           // Console.WriteLine("s2 LineMode");
            //nRet = m_MyCamera.MV_CC_SetEnumValue_NET("LineMode", 8);

            //if (MyCamera.MV_OK != nRet)
            //{
            //    ShowErrorMsg("Set Fail!", nRet);
            //    return;
            //}
            //nRet = m_MyCamera.MV_CC_SetBoolValue_NET("LineInverter", true);
            //if (MyCamera.MV_OK != nRet)
            //{
            //    ShowErrorMsg("Set line inverter Fail!", nRet);
            //    return;
            //}

            Console.WriteLine("s3 LineSource");
            nRet = m_MyCamera.MV_CC_SetEnumValue_NET("LineSource", 5);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Set Fail!", nRet);
                return;
            }

            Console.WriteLine("s4 StrobeEnable");
            nRet = m_MyCamera.MV_CC_SetBoolValue_NET("StrobeEnable", true);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Set Fail!", nRet);
                return;
            }

            Console.WriteLine("s5 StrobeLineDuration");
            nRet = m_MyCamera.MV_CC_SetIntValue_NET("StrobeLineDuration",(uint) pulseDuration_us);

            if (MyCamera.MV_OK != nRet)               
            {
                ShowErrorMsg("Set Fail!", nRet);
                return;
            }

            Console.WriteLine("s6 Done");
        }

        public void ConfigureGPIOLine5v(ref MyCamera m_MyCamera, int pulseDuration_us = 10000, int lineNo = 2)
        {
            int nRet;
            Console.WriteLine("line2 LineSelector");
            nRet = m_MyCamera.MV_CC_SetEnumValue_NET("LineSelector", (uint)lineNo);


            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Set Fail!", nRet);
                return;
            }
            nRet = m_MyCamera.MV_CC_SetEnumValue_NET("LineMode", (uint)8);//strobe
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Set Linemode Fail!", nRet);
                return;
            }
            nRet = m_MyCamera.MV_CC_SetBoolValue_NET("LineInverter", false);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Set line inverter Fail!", nRet);
                return;
            }
            //nRet = m_MyCamera.MV_CC_SetEnumValue_NET("LineMode", 8);

            //if (MyCamera.MV_OK != nRet)
            //{
            //    ShowErrorMsg("Set Fail!", nRet);
            //    return;
            //}

            Console.WriteLine("s3 LineSource");
            nRet = m_MyCamera.MV_CC_SetEnumValue_NET("LineSource", 5);//software
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Set Fail!", nRet);
                return;
            }

            Console.WriteLine("s4 StrobeEnable");
            nRet = m_MyCamera.MV_CC_SetBoolValue_NET("StrobeEnable", true);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Set Fail!", nRet);
                return;
            }

            Console.WriteLine("s5 StrobeLineDuration");
            nRet = m_MyCamera.MV_CC_SetIntValue_NET("StrobeLineDuration", (uint)pulseDuration_us);

            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Set Fail!", nRet);
                return;
            }

            Console.WriteLine("gpio setup Done");
        }
        public void SetOutputPulse(ref MyCamera m_MyCamera,int pulseCount ,uint lineNo, double pulseDuration_us =10000)
        {
            int nRet;
            nRet = m_MyCamera.MV_CC_SetEnumValue_NET("LineSelector", lineNo);
            for (int i = 0; i < pulseCount; i++)
            {
                nRet = m_MyCamera.MV_CC_SetCommandValue_NET("LineTriggerSoftware");
               // System.Threading.Thread.Sleep((int) (pulseDuration_us/1000));
            }

            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Set op pulse Fail!", nRet);
                return;
            }
        }

 
    }
}
