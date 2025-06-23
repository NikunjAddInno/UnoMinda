using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.IO;


namespace VoltasBeko.Classes
{

    public class HikCam
    {
        public BindingList<Pose> Poses = new BindingList<Pose>();

        public HikCam(string Name, string SerialNo, int index, float Exposure)
        {
            this.Name = Name;
            this.SerialNo = SerialNo;
            this.Exposure = Exposure;
            this.Index = index;
            m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        }

        public string Name = "default";
        public string SerialNo = "010100101";
        public int Index = 0;
        public float Exposure = 5000.0F;
        bool ConnStatus = false;
        bool grabStatus = false;

        public bool IsConnected { get => ConnStatus; set => ConnStatus = value; }
        private bool IsGrabbing { get => grabStatus; set => grabStatus = value; }

        #region Camera Code
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        private static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);
        private MyCamera.MV_CC_DEVICE_INFO_LIST m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        public MyCamera m_pMyCamera;

        public delegate void BitmapReceivedHandler(Bitmap image, int posenumber);

        public event BitmapReceivedHandler BitmapRecievedEvent;

        public int SearchAllCameras()
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
               
            }
            return (int)m_stDeviceList.nDeviceNum;
        }

        public int connectListedCam()
        {
            SearchAllCameras();
            if (m_stDeviceList.nDeviceNum == 0)
            {
                ConsoleExtension.WriteError("No device, please select", 0);
                return 0;
            }
            MyCamera.MV_CC_DEVICE_INFO device;
            for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
            {
                // ch:获取选择的设备信息 | en:Get selected device information
                MyCamera.MV_CC_DEVICE_INFO deviceTmp = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i],
                                                                  typeof(MyCamera.MV_CC_DEVICE_INFO));

                if (deviceTmp.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(deviceTmp.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    if (SerialNo.Equals(gigeInfo.chSerialNumber) == false)
                    {
                        continue;
                    }
                }
                else if (deviceTmp.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(deviceTmp.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));

                    if (SerialNo.Equals(usbInfo.chSerialNumber) == false)
                    {
                        continue;
                    }
                }
                //get serial number from device data
                //Console.WriteLine("serial no of camera at index :" + i.ToString() + "  " + gigeInfo.chSerialNumber);
               
                    Console.WriteLine("camera match found :" + SerialNo + " " + Name);

                    device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i],
                                                                 typeof(MyCamera.MV_CC_DEVICE_INFO));
                    if (null == m_pMyCamera)
                    {
                        m_pMyCamera = new MyCamera();
                        if (null == m_pMyCamera)
                        {
                            IsConnected = false;
                            break;
                        }
                    }

                    int nRet = m_pMyCamera.MV_CC_CreateDevice_NET(ref device);
                    if (MyCamera.MV_OK != nRet)
                    {
                        IsConnected = false;
                        break;
                    }

                    nRet = m_pMyCamera.MV_CC_OpenDevice_NET();
                    if (MyCamera.MV_OK != nRet)
                    {
                        m_pMyCamera.MV_CC_DestroyDevice_NET();
                        ConsoleExtension.WriteError("Device open fail!");
                        IsConnected = false;
                        break;
                    }

                    IsConnected = true;
                    if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                    {
                        int nPacketSize = m_pMyCamera.MV_CC_GetOptimalPacketSize_NET();
                        if (nPacketSize > 0)
                        {
                            nRet = m_pMyCamera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
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
            // ch:打开设备 | en:Open device

            // ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)

            // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
            if (IsConnected)
            {
                m_pMyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", (uint)MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
                m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
                m_pMyCamera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);
                int nRet = m_pMyCamera.MV_CC_SetFloatValue_NET("ExposureTime", Exposure);
                SetTriggerMode(true);
                SetPixelFormat();
                if (nRet != MyCamera.MV_OK)
                {
                    ShowErrorMsg("Set Exposure Time Fail!", nRet);
                }
                else
                {
                    Console.WriteLine("Exposuere set successfully " + Name + "   " + Exposure.ToString());
                }
                IsGrabbing = true;



                return 1;
            }
            return 0;
        }

        // Grabbing Parameters
        MyCamera.cbOutputExdelegate cbImage;
        IntPtr[] m_hDisplayHandle = new IntPtr[3];
        int[] m_nFrames = new int[3];
        private UInt32[] m_nSaveImageBufSize = new UInt32[3] { 0, 0, 0 };
        private IntPtr[] m_pSaveImageBuf = new IntPtr[3] { IntPtr.Zero, IntPtr.Zero, IntPtr.Zero };
        private Object[] m_BufForSaveImageLock = new Object[3];
        MyCamera.MV_FRAME_OUT_INFO_EX[] m_stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX[3];

        Bitmap[] grabbedImage = new Bitmap[3];
        delegate void SetTextCallback(string text);

        public void startCamAcq()
        {
            m_nFrames = new int[3];

            cbImage = new MyCamera.cbOutputExdelegate(ImageCallBack);

            for (int i = 0; i < 3; ++i)
            {
                m_BufForSaveImageLock[i] = new Object();
            }

            if (IsConnected)
            {
                m_pMyCamera.MV_CC_RegisterImageCallBackEx_NET(cbImage, (IntPtr)0);
                int nRet = m_pMyCamera.MV_CC_StartGrabbing_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    Console.WriteLine("start grab failed");
                    IsGrabbing = false;
                    //ShowErrorMsg("Start Grabbing Fail!", nRet);
                }
                else
                {
                    IsGrabbing = true;

                }
            }
        }

        private static int GetPoseForImage(int number)
        {
            ConsoleExtension.WriteWithColor($"Frame number {number}");

            if (number < 1 || number > (PoseCount * 6))
            {
                throw new ArgumentOutOfRangeException("Number", "Number must be greater than 1 and less than 37");
            }
            
            //ConsoleExtension.WriteWithColor($"Frame number {number}");
            PoseNumberCamera = (number - 1) / 6;
            return PoseNumberCamera;
        }

        public static int PoseCount = 0;

        private static int _poseNumber = -1; 

        public static int PoseNumberCamera { get { return _poseNumber; }
            set 
            {
                int oldPose = _poseNumber;
                _poseNumber = value;
                if (oldPose != _poseNumber) 
                {
                    //int poseCount = AppData.Camera.Poses.Count;
                    //Parallel.ForEach(AppData.Camera.Poses[(_poseNumber + 1) % poseCount].CameraSetups, setupData =>
                    //{
                    //    AppData.Cameras[setupData.CameraIndex - 1].SetExposure(setupData.Exposure);
                    //    Console.WriteLine($"Camera: {setupData.CameraIndex - 1} Exposure: {setupData.Exposure} Pose: {_poseNumber}");

                    //});

                }
            }
        }

        private static int _imagesCaptured = 0;
        public static int ImagesCaptured
        {
            get { return _imagesCaptured; }
            set
            {
                _imagesCaptured = value;
                int poseNumber = (_imagesCaptured) % AppData.Camera.Poses.Count;

                ConsoleExtension.WriteWithColor($"POSENUMBER {poseNumber} Exposure {AppData.Camera.Poses[poseNumber].exposure}");
                AppData.Camera.SetExposure(AppData.Camera.Poses[poseNumber].exposure);

                if (ImagesCaptured >= PoseCount)
                {
                   
                    //PoseNumberCamera++;
                    _imagesCaptured = 1;

                }
            }
        }

        private void ImageCallBack(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {

            int nIndex = (int)pUser;

            // ch:抓取的帧数 | en:Aquired Frame Number
            ++m_nFrames[nIndex];
            //Console.WriteLine($"{m_nFrames[nIndex]} Index {nIndex}");
            //++ImagesCaptured;
            lock (m_BufForSaveImageLock[nIndex])
            {
                if (m_pSaveImageBuf[nIndex] == IntPtr.Zero || pFrameInfo.nFrameLen > m_nSaveImageBufSize[nIndex])
                {
                    if (m_pSaveImageBuf[nIndex] != IntPtr.Zero)
                    {
                        Marshal.Release(m_pSaveImageBuf[nIndex]);
                        m_pSaveImageBuf[nIndex] = IntPtr.Zero;
                    }

                    m_pSaveImageBuf[nIndex] = Marshal.AllocHGlobal((Int32)pFrameInfo.nFrameLen);
                    if (m_pSaveImageBuf[nIndex] == IntPtr.Zero)
                    {
                        return;
                    }
                    m_nSaveImageBufSize[nIndex] = pFrameInfo.nFrameLen;
                }

                m_stFrameInfo[nIndex] = pFrameInfo;
                CopyMemory(m_pSaveImageBuf[nIndex], pData, pFrameInfo.nFrameLen);
            }

            MyCamera.MV_DISPLAY_FRAME_INFO stDisplayInfo = new MyCamera.MV_DISPLAY_FRAME_INFO();
            stDisplayInfo.hWnd = m_hDisplayHandle[nIndex];
            stDisplayInfo.pData = pData;
            stDisplayInfo.nDataLen = pFrameInfo.nFrameLen;
            stDisplayInfo.nWidth = pFrameInfo.nWidth;
            stDisplayInfo.nHeight = pFrameInfo.nHeight;
            stDisplayInfo.enPixelType = pFrameInfo.enPixelType;

            //------------gray
            // try
            // {
            //     grabbedImage[nIndex] = new System.Drawing.Bitmap((int)pFrameInfo.nWidth,
            //(int)pFrameInfo.nHeight,
            //(int)pFrameInfo.nWidth * 1,
            //System.Drawing.Imaging.PixelFormat.Format8bppIndexed,
            //pData);
            //     System.Drawing.Imaging.ColorPalette palette = grabbedImage[nIndex].Palette;
            //     int nColors = 256;
            //     for (int ii = 0; ii < nColors; ii++)
            //     {
            //         uint Alpha = 0xFF;
            //         uint Intensity = (uint)(ii * 0xFF / (nColors - 1));
            //         palette.Entries[ii] = System.Drawing.Color.FromArgb(
            //          (int)Alpha,
            //          (int)Intensity,
            //          (int)Intensity,
            //          (int)Intensity);
            //     }
            //     grabbedImage[nIndex].Palette = palette;

            //     Rectangle rect0 = new Rectangle(0, 0, grabbedImage[nIndex].Width, grabbedImage[nIndex].Height);
            // }
            // catch (ArgumentException ex)
            // {
            //     Console.WriteLine("exception from camera class mono conversion");
            //     System.Console.Write(ex);
            //     return;
            // }
            //---------gray End---------
            //----color 
            grabbedImage[nIndex] = new System.Drawing.Bitmap((int)pFrameInfo.nWidth, (int)pFrameInfo.nHeight, (int)pFrameInfo.nWidth * 3, System.Drawing.Imaging.PixelFormat.Format24bppRgb, pData);
            //Console.WriteLine("image captured from Cam  " + nIndex.ToString() + "   frame no:" + m_nFrames[nIndex].ToString());
            //-----color end

            Rectangle rect = new Rectangle(0, 0, grabbedImage[nIndex].Width, grabbedImage[nIndex].Height);
            Bitmap grabbedImageColor = grabbedImage[nIndex].Clone(rect, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //Bitmap grabbedImageRet = grabbedImage[nIndex].Clone(rect, System.Drawing.Imaging.PixelFormat.Format24bppRgb);


            try
            {
                if (AppData.AppMode == Mode.Inspection)
                {
                    grabbedImageColor.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    BitmapRecievedEvent?.Invoke(grabbedImageColor.DeepClone(), (++ImagesCaptured) - 1);
                }
                if (AppData.AppMode == Mode.Setup)
                {
                    grabbedImageColor.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    BitmapRecievedEvent?.Invoke(grabbedImageColor.DeepClone(), 0);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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

        public int DisconnectCam()
        {
            
            try
            {

                BitmapRecievedEvent = null;
                SetTriggerMode(true);
                int nRet;
                if (IsGrabbing)
                {
                    m_pMyCamera.MV_CC_StopGrabbing_NET();
                    Console.WriteLine("Grabbing stopped.");
                }

                nRet = m_pMyCamera.MV_CC_CloseDevice_NET();

                if (MyCamera.MV_OK != nRet)
                {
                    return 0;
                }

                nRet = m_pMyCamera.MV_CC_DestroyDevice_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                ConsoleExtension.WriteError(ex);
            }
           
            return 1;
            
        }

        public void SetExposure(float Exposure)
        {
            if (IsConnected)
            {
                int nRet = m_pMyCamera.MV_CC_SetFloatValue_NET("ExposureTime", Exposure);
            }
        }

        public int GetExposure()
        {
            if (IsConnected)
            {
                MyCamera.MVCC_FLOATVALUE mVCC_FLOATVALUE = new MyCamera.MVCC_FLOATVALUE();
                 m_pMyCamera.MV_CC_GetFloatValue_NET("ExposureTime", ref mVCC_FLOATVALUE);
                return Convert.ToInt32(mVCC_FLOATVALUE);

            }
            else
            {
                return 0;
            }
        }

        public void SetGain(float Gain)
        {
            if (IsConnected)
            {
                int nRet = m_pMyCamera.MV_CC_SetFloatValue_NET("Gain", Gain);
            }
        }

        public void SetTriggerMode(bool On)
        {
            if (IsConnected)
            {
                if (On)
                {
                    m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
                }
                else
                {
                    m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);
                }
            }
        }

        public void SetPixelFormat()
        {
            if (IsConnected)
            {
                m_pMyCamera.MV_CC_SetEnumValue_NET("PixelFormat", (uint)MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed);
            }
        }

        public void SoftwareTrigger()
        {
            if (IsConnected)
            {
                // get original trigger source for the camera 
                MyCamera.MVCC_ENUMVALUE stEnumVal = new MyCamera.MVCC_ENUMVALUE();
                int oldTriggerSource = m_pMyCamera.MV_CC_GetEnumValue_NET("TriggerSource", ref stEnumVal);
                if (MyCamera.MV_OK != oldTriggerSource)
                {
                    Console.WriteLine("Get Trigger Mode failed:{0:x8}", oldTriggerSource);
                }

                // set trigger source to software and execute software trigger
                m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
                m_pMyCamera.MV_CC_TriggerSoftwareExecute_NET();

                // set original trigger source
                m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)oldTriggerSource);

            }
        }

        #endregion

    }
}
