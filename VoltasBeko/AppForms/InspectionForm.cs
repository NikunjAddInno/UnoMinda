using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using tryThresholds.IP_tools;
using tryThresholds;
using VoltasBeko;
using VoltasBeko.AppForms;
using VoltasBeko.Classes;
using VoltasBeko.CustomControl;
using Button = System.Windows.Forms.Button;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Http;
using System.Collections.Concurrent;
using System.Drawing.Imaging;
using VoltasBeko.Properties;
using VoltasBeko.classes;
using Measurement_AI.classes;
using static tryThresholds.IP_tools.resultFrontCam;
using System.CodeDom;
using System.Web;
namespace VoltasBeko
{
    public partial class InspectionForm : Form
    {
        int originalExStyle = -1;
        bool enableFormLevelDoubleBuffering = true;

        protected override CreateParams CreateParams
        {
            get
            {
                if (originalExStyle == -1)
                    originalExStyle = base.CreateParams.ExStyle;

                CreateParams cp = base.CreateParams;
                if (enableFormLevelDoubleBuffering)
                    cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                else
                    cp.ExStyle = originalExStyle;

                return cp;
            }
        }


        private void TurnOffFormLevelDoubleBuffering()
        {
            enableFormLevelDoubleBuffering = false;
            this.MaximizeBox = true;
        }




        Size iconSize = new Size(55, 55);
        List<Button> camIcons = new List<Button>();
        BindingList<(Bitmap, Pose)> poseImages = new BindingList<(Bitmap, Pose)>();

        ImageAssembler imageAssembler = new ImageAssembler();


        List<(int pcbIndex, bool result)> FinalResults = new List<(int, bool)>();

        int _okCount = 0;

        public int OkCount
        {
            get { return _okCount; }
            set
            {
                _okCount = value;
                Settings.Default.OkCount = _okCount;
                panelInspectionStats.Invoke(new Action(() =>
                {
                    labelOk.Text = _okCount.ToString();
                }));
            }
        }
        int _ngCount = 0;

        public int NgCount
        {
            get { return _ngCount; }
            set
            {
                _ngCount = value;
                Settings.Default.NgCount = _ngCount;
                panelInspectionStats.Invoke(new Action(() =>
                {
                    labelNg.Text = _ngCount.ToString();
                }));
            }
        }
        int _totalCount = 0;

        public int TotalCount
        {
            get { return _totalCount; }
            set
            {
                _totalCount = value;
                Settings.Default.TotalCount = _totalCount;
                labelTotal.Invoke(new Action(() =>
                {
                    labelTotal.Text = _totalCount.ToString();
                }));
            }
        }
        bool _pcbFinalResult = false;
        public bool PcbFinalResult
        {
            get { return _pcbFinalResult; }
            set
            {
                _pcbFinalResult = value;
                panelInspectionStats.Invoke(new Action(() =>
                {
                    if (_pcbFinalResult)
                    {
                        labelOk.Text = _okCount.ToString();
                        labelResult.BackColor = Color.LimeGreen;
                        labelResult.Text = "OK";
                    }
                    else
                    {
                        labelNg.Text = _ngCount.ToString();
                        labelResult.BackColor = Color.Red;
                        labelResult.Text = "NG";
                    }

                }));
            }
        }
        public InspectionForm()
        {
            InitializeComponent();
            camIcons = new List<Button>() { buttonCam1 };

            foreach (Button btn in camIcons)
            {
                btn.FitImage($"{AppData.ProjectDirectory}/Icons/camRed.png");
            }
        }

        private void Camera_BitmapRecievedEvent(Bitmap image, int posenumber)
        {
            poseImages.Add((image.DeepClone(), AppData.Camera.Poses[posenumber]));
            ConsoleExtension.WriteWithColor("Posenumber " + posenumber, ConsoleColor.Yellow);
            //PoseNumber++;
            zoomInOutPictureBox1.Invoke(new Action(() =>
            {
                zoomInOutPictureBox1.PictureBox1.Image = image.DeepClone();

            }));
            checkBoxSaveImg1.Invoke(new Action(() =>
            {
                if (checkBoxSaveImg1.Checked)
                {
                    string path = $@"{ModelPath}\Images";
                    Directory.CreateDirectory(path);
                    image.DeepClone().Save($@"{path}\{DateTime.Now:dd_MM_yyyy_HH_mm_ss}_{DateTime.Now.Millisecond}.bmp");
                }
            }));
        }

        private async void PoseImages_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (poseImages.Count > 0/* AppData.Camera.Poses.Count*/)
            {
                //foreach (Bitmap image in poseImages)
                //{
                //ProcessImage(poseImages[PoseNumber - 1].Item1.DeepClone());

                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

            }

            labelFinalResultCount.Invoke(new Action(() =>
            {
                labelFinalResultCount.Text = $"Count: {FinalResults.Count} Expected: {AppData.Camera.Poses.Count}";
            }));

            if (FinalResults.Count == AppData.Camera.Poses.Count)
            {

                zoomInOutPictureBoxFull.Invoke(new Action(() =>
                {
                    zoomInOutPictureBoxFull.PictureBox1.Image = imageAssembler.CropCanvas();

                    zoomInOutPictureBoxFull.Size = new Size(1687, 910);

                    pbClass.SetAspectRatio(ref zoomInOutPictureBoxFull.PictureBox1);
                    //zoomInOutPictureBoxFull.Width = zoomInOutPictureBoxFull.PictureBox1.Width + 10;
                    //zoomInOutPictureBoxFull.Height = zoomInOutPictureBoxFull.PictureBox1.Height + 10;
                    zoomInOutPictureBoxFull.PictureBoxSize = zoomInOutPictureBoxFull.PictureBox1.Size;
                    label3.Invoke(new Action(() =>
                    {
                        label3.Width = zoomInOutPictureBoxFull.Width;

                    }));
                    zoomInOutPictureBoxFull.PictureBox1.Invalidate();

                }));

                var pcbCount = FinalResults.Select(x => x.pcbIndex).Distinct();
                bool finalResult = !FinalResults.Any(x => x.result == false);
                TotalCount += pcbCount.Count();
                PcbFinalResult = finalResult;
                foreach (var item in pcbCount)
                {
                    //ConsoleExtension.WriteWithColor($"{item} {JsonConvert.SerializeObject(FinalResults, Formatting.Indented)}");
                    bool resultPcb = FinalResults.Any(x => x.pcbIndex == item && x.result == false);
                    NgCount += Convert.ToInt32(resultPcb);
                    OkCount += Convert.ToInt32(!resultPcb);


                    // sending pose done signal after all the inspection is completed
                    LogReportData logReportData = new LogReportData();
                    logReportData.Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    logReportData.Time = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                    logReportData.Result = resultPcb;
                    logReportData.ModelCode = item == 0 ? ModelCode : ModelCode2;
                    logReportData.ModelName = AppData.ModelName;
                    logReportData.InspectionCount = InspectionCount;

                    Database.InsertDataFinalReport(logReportData);
                }


                //bool result = item.result;

                //if (!result)
                //{
                //    NgCount++;
                //    ngFound = true;
                //    break;
                //}

                //if (!ngFound)
                //{
                //    OkCount++;
                //}



                panel2.Invoke(new Action(() =>
                {
                    radioButtonFull.Checked = true;
                }));


                PlcControl.WriteValue(PlcControl.PlcReg.PartResult, Convert.ToInt32(!finalResult) + 1);
                //PlcControl.WriteValue(PlcControl.PlcReg.PartResult, 2);// send force ng
                FinalResults.Clear();
            }

        }

        private void SetDelayBeforeGrabbing()
        {
            int totalDelayBeforeGrab = 1000;
            TimeSpan timeDifference = DateTime.Now - timeAtSetFocus;

            long millisecondsDifference = (long)timeDifference.TotalMilliseconds;

            ConsoleExtension.WriteWithColor($"Time diffrence {millisecondsDifference}", ConsoleColor.Yellow);

            if (millisecondsDifference < totalDelayBeforeGrab)
            {
                Thread.Sleep(totalDelayBeforeGrab - (int)millisecondsDifference);
                ConsoleExtension.WriteWithColor($"Delay set to {totalDelayBeforeGrab - (int)millisecondsDifference}", ConsoleColor.Yellow);
            }
        }

        private void InspectionForm_Load(object sender, EventArgs e)
        {
            //Database.CreateLogReportTableIfNotExists();

            buttonPlc.FitImage($"{AppData.ProjectDirectory}/Icons/plcRed.png");
            buttonApi.FitImage($"{AppData.ProjectDirectory}/Icons/apiRed.png");

            //AppData.Camera.SetExposure(500);

            AppData.AppMode = Mode.Idol;
            PlcControl.PlcConnected += PlcControl_PlcConnected;

            PlcControl.Connect();


            PlcControl.NewCycle += PlcControl_NewCycle;
            //Console.WriteLine(PlcControl.ReadValue(270));

            comboBoxModel.SelectedIndexChanged += ComboBoxModel_SelectedIndexChanged;
            comboBoxModel.LoadDirectoryNames($"{AppData.ProjectDirectory}/Models");

            UpdateConnectionStatusUI();
            AppData.AppModeChanged += AppData_AppModeChanged;
            AppData.Camera.BitmapRecievedEvent += Camera_BitmapRecievedEvent;
            poseImages.ListChanged += PoseImages_ListChanged;

            TotalCount = Settings.Default.TotalCount;
            OkCount = Settings.Default.OkCount;
            NgCount = Settings.Default.NgCount;
            AppData.AppMode = Mode.Idol;

            AppData.AppMode = Mode.Inspection;
            //backgroundWorkerProcessBack.RunWorkerAsync();

            //AppData.Camera.SetTriggerMode(false);

            backgroundWorker1.RunWorkerAsync();
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
            serialPort1.Open();
            serialPort1.DataReceived += SerialPort1_DataReceived;
            backgroundWorkerDatabase.RunWorkerAsync();
            PlcControl.WriteValue(PlcControl.PlcReg.SoftwareReady, 1);
            radioButtonSingleImage.Checked = true;
            //zoomInOutPictureBoxFull.PictureBox1.Image = imageAssembler.Canvas;
            //pbClass.setAspectRatio(ref zoomInOutPictureBoxFull.PictureBox1);
            //zoomInOutPictureBoxFull.Size = zoomInOutPictureBoxFull.PictureBox1.Size;
            //label3.Width = zoomInOutPictureBoxFull.Width;
            //zoomInOutPictureBoxFull.PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            panelInspectionStats.Invoke(new Action(() =>
            {
                labelResult.BackColor = Color.LightGray;
                labelResult.Text = "RESULT";
            }));

            ApiController.ApiConnectionEvent += ApiController_ApiConnectionEvent;
        }

        private void ApiController_ApiConnectionEvent(bool status)
        {
            if (buttonApi.IsHandleCreated)
            {
                buttonApi.Invoke(new Action(() =>
                {
                    if (status)
                    {
                        buttonApi.FitImage($"{AppData.ProjectDirectory}/Icons/apiGreen.png");
                    }
                    else
                    {
                        buttonApi.FitImage($"{AppData.ProjectDirectory}/Icons/apiRed.png");

                    }
                }));
            }


        }



        private void SerialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string s = serialPort1.ReadLine();
            ModelCode = s.Replace("\n", "").Replace("\r", "");
            Console.WriteLine($"Scanner data {s}");
        }

        private void PlcControl_NewCycle()
        {
            Console.WriteLine($"New Cycle started. FinalResults count {FinalResults.Count}");
            PoseNumber = 0;
            poseImages.Clear();
            FinalResults.Clear();
            logReports.Clear();
            HikCam.ImagesCaptured = 0;
            ModelCode = "Null";
            ModelCode2 = "Null";
            //qrIndex = new int[] { -1, -1 };

            arrayIndex = -1;
            flowLayoutPanel1.Invoke(new Action(() =>
            {
                flowLayoutPanel1.Controls.Clear();
                imageAssembler.ResetImage();
            }));
        }

        private void PlcControl_PlcConnected(bool status)
        {
            Console.WriteLine("Plc status " + status);
            if (status)
            {
                buttonPlc.FitImage($"{AppData.ProjectDirectory}/Icons/plcGreen.png");
            }
            else
            {
                buttonPlc.FitImage($"{AppData.ProjectDirectory}/Icons/plcRed.png");

            }
        }

        DateTime timeAtSetFocus = new DateTime();


        void loadColorsinCpp()
        {

            for (int k = 0; k < userColours.list_userColours.Count(); k++)
            {
                colourRange_hsv c = userColours.list_userColours[k];
                GlobalItems.algoC1.loadColours(k, c.id, c.name, c.H_low, c.S_low, c.V_low, c.H_high, c.S_high, c.V_high);
            }
            Console.WriteLine("*******************************************colour count in cs::" + userColours.list_userColours.Count().ToString());
        }
        int[] qrIndex = new int[2];
        Rectangle[] qrRect = new Rectangle[2];
        int arrayIndex = -1;
        int LoadCppModel()
        {
            try
            {
                qrIndex = new int[] { -1, -1 };
                globalVars.algo.Clear_MarkInspectioData();

                userColours.list_userColours = userColours.Load_from_file($@"{AppData.ModelPath}\" + "colours.json");
                ConsoleExtension.WriteWithColor($@"loading colour cpp: {AppData.ModelPath}\", ConsoleColor.Magenta);
                loadColorsinCpp();
                foreach (Pose pose in AppData.Camera.Poses)
                {
                    string filePath = $@"{AppData.ModelPath}\{pose.Id}\" + "MarkInsTools.json";

                    if (File.Exists(filePath))
                    {


                        applied_tools Obj_UserTools_applied = applied_tools.Load_from_file(filePath);

                        ConsoleExtension.WriteWithColor($@"Pose load data cpp: {AppData.ModelPath}\{pose.Id}\", ConsoleColor.Magenta);
                        //copy data in cpp
                        String allTools_Ser = Newtonsoft.Json.JsonConvert.SerializeObject(Obj_UserTools_applied, Formatting.Indented);

                        Console.WriteLine("Qr tools count: " + Obj_UserTools_applied.lst_QR_tools.Count);
                        if (Obj_UserTools_applied.lst_QR_tools.Count > 0)
                        {
                            arrayIndex++;
                            qrIndex[arrayIndex] = pose.number;
                            qrRect[arrayIndex] = Obj_UserTools_applied.lst_QR_tools[0].Rect_roi.ToRectangle();
                            ConsoleExtension.WriteWithColor(qrIndex.ToList().IndexOf(pose.number) + " Index of pose", ConsoleColor.Yellow);

                        }
                        globalVars.algo.Load_MarkInspectioData(allTools_Ser);
                    }
                }

                return 1;
            }
            catch (Exception exx)
            {
                MessageBox.Show("Unable to load Mark inspection data or image for editing" + exx.Message);
                return -1;
            }
        }
        //int load_model_forTesting()
        //{
        //    try
        //    {
        //        userColours.list_userColours = userColours.Load_from_file(AppSettings.currentModelPath + "/colours.json");
        //        loadColorsinCpp();
        //        Obj_UserTools_applied_C1 = applied_tools.Load_from_file(AppSettings.currentModelPath + "/MarkInsTools.json");
        //        Obj_UserTools_applied_C2 = applied_tools.Load_from_file(AppSettings.currentModelPath + "/c2" + "/MarkInsTools.json");

        //        applied_tools.deleteUnusedTemplates(AppSettings.currentModelPath);
        //        String allTools_SerC1 = Newtonsoft.Json.JsonConvert.SerializeObject(Obj_UserTools_applied_C1, Formatting.Indented);
        //        String allTools_SerC2 = Newtonsoft.Json.JsonConvert.SerializeObject(Obj_UserTools_applied_C2, Formatting.Indented);
        //        //
        //        //Load to cpp
        //        globalVars.algo.Clear_MarkInspectioData();
        //        globalVars.algo.Load_MarkInspectioData(allTools_SerC1);
        //        globalVars.algo.Load_MarkInspectioData(allTools_SerC2);


        //        //15july_kk
        //        //Console.WriteLine(allTools_SerC1);
        //        //Console.WriteLine(allTools_SerC2);
        //        //read rotated image and show + reflect all tools in UI

        //        return 1;
        //    }
        //    catch (Exception exx)
        //    {
        //        MessageBox.Show("Unable to load Mark inspection data or image for editing" + exx.Message);
        //        return -1;
        //    }

        //}
        int pcbIndex = -1;
        void InspectPose(Bitmap bitmap, Pose pose)
        {
            try
            {
                string filePath = $@"{AppData.ModelPath}\{pose.Id}\" + "MarkInsTools.json";

                if (File.Exists(filePath) && qrIndex.ToList().IndexOf(pose.number) == -1)
                {

                    Bitmap testImg = imageFns_bmp.get24BitDeepCopy(bitmap);
                    Bitmap defectImage = imageFns_bmp.get24BitDeepCopy(bitmap);
                    string resultJson = globalVars.algo.markIns_testMode(testImg, pose.number - 1);


                    tryThresholds.IP_tools.resultFrontCam result = JsonConvert.DeserializeObject<tryThresholds.IP_tools.resultFrontCam>(resultJson);
                    //ConsoleExtension.WriteWithColor($"Pose number: {pose.number} result.isROIonly: {result.isROIonly}", ConsoleColor.Magenta);
                    string json = JsonConvert.SerializeObject(result);

                    LogReportData logReportData = new LogReportData();
                    logReportData.Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    logReportData.Time = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                    logReportData.Defects = json;
                    logReportData.Result = result.finalResult;
                    logReportData.ModelCode = ModelCode;
                    logReportData.ModelName = AppData.ModelName;
                    logReportData.DefectImage = null;
                    logReportData.PoseNumber = pose.number;
                    logReportData.InspectionCount = InspectionCount;
                    //ConsoleExtension.WriteWithColor($"Pose number: {result.finalResult}", ConsoleColor.Yellow);
                    FinalResults.Add((pcbIndex, result.finalResult));

                    imageAssembler.DrawImageAtPosition(defectImage, 300 - AppData.Camera.Poses[logReportData.PoseNumber - 1].location.Y, AppData.Camera.Poses[logReportData.PoseNumber - 1].location.X, result.list_defectDetails);
                    if (result.finalResult == false)
                    {
                        logReportData.DefectImage = testImg.DeepClone();


                    }
                    logReports.Add(new LogReportData(logReportData));

                    PosePreview posePreview = null;

                    posePreview = new PosePreview(testImg.DeepClone(), result, pose.number);

                    posePreview.pictureBox.Visible = false;
                    posePreview.Size = new Size(186, 190);
                    posePreview.panel1.Dock = DockStyle.Fill;
                    flowLayoutPanel1.Invoke(new Action(() =>
                    {
                        flowLayoutPanel1.Controls.Add(posePreview);


                    }));

                    //zoomInOutPictureBox1.SetImage(testImg.DeepClone());
                }
                else if (File.Exists(filePath) && qrIndex.ToList().IndexOf(pose.number) != -1)
                {
                    int index = qrIndex.ToList().IndexOf(pose.number);
                    pcbIndex = index;
                    Bitmap qrImage = bitmap.Clone(qrRect[index], PixelFormat.Format24bppRgb);
                    string apiResult = "BLANK";

                    checkBoxQr.Invoke(new Action(() =>
                    {
                        if (checkBoxQr.Checked)
                        {
                            try
                            {
                                apiResult = ApiController.ProcessImage(qrImage, "get_code", "5003");
                            }
                            catch (Newtonsoft.Json.JsonReaderException ex)
                            {
                                Console.WriteLine(ex);
                                apiResult = ApiController.ProcessImage(qrImage, "get_code", "5003");
                            }
                            catch (Exception ex)
                            {
                                ConsoleExtension.WriteWithColor(ex, ConsoleColor.Red);
                                ConsoleExtension.WriteWithColor("Error in Getting code Reading. Try again");
                            }
                        }
                    }));

                    ConsoleExtension.WriteWithColor(apiResult, ConsoleColor.Yellow);


                    string code = apiResult.Replace("\n", "").Replace("\r", "").Replace(" ", "");
                    if (index == 0)
                    {
                        ModelCode = code;
                    }
                    else if (index == 1)
                    {
                        ModelCode2 = code;
                    }

                    Label label = new Label();
                    label.AutoSize = false;
                    label.Width = flowLayoutPanel1.Width - 5;
                    label.Dock = DockStyle.Top;
                    label.BackColor = Color.Black;
                    label.ForeColor = Color.White;
                    label.Text = code;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); ;
                    flowLayoutPanel1.Invoke(new Action(() =>
                    {
                        flowLayoutPanel1.Controls.Add(label);
                    }));
                    List<DefectDetails> list_defectDetails = new List<DefectDetails>();
                    imageAssembler.DrawImageAtPosition(bitmap, 300 - AppData.Camera.Poses[pose.number - 1].location.Y, AppData.Camera.Poses[pose.number - 1].location.X, list_defectDetails);
                    if (string.IsNullOrEmpty(code) || code.ToLower().Contains("found"))
                    {
                        FinalResults.Add((pcbIndex, false));

                    }
                    else
                    {
                        FinalResults.Add((pcbIndex, true));
                    }
                }
                else
                {
                    List<DefectDetails> list_defectDetails = new List<DefectDetails>();
                    imageAssembler.DrawImageAtPosition(bitmap, 300 - AppData.Camera.Poses[pose.number - 1].location.Y, AppData.Camera.Poses[pose.number - 1].location.X, list_defectDetails);
                    FinalResults.Add((pcbIndex, true));

                    //ConsoleExtension.WriteWithColor($"Pose number: {false}", ConsoleColor.Yellow);

                    //zoomInOutPictureBox1.SetImage(bitmap.DeepClone());
                }

            }
            catch (Exception ex)
            {
                List<DefectDetails> list_defectDetails = new List<DefectDetails>();
                imageAssembler.DrawImageAtPosition(bitmap, 300 - AppData.Camera.Poses[pose.number - 1].location.Y, AppData.Camera.Poses[pose.number - 1].location.X, list_defectDetails);
                FinalResults.Add((pcbIndex, false));

                //ConsoleExtension.WriteWithColor($"Pose number: {false}", ConsoleColor.Yellow);

            }

            //ConsoleExtension.WriteWithColor($"FinalResults count {FinalResults.Count}");
        }

        private void ProcessImage(Bitmap bitmap, Pose pose)
        {

            if (AppData.AppMode == Mode.Inspection)
            {
                InspectPose(bitmap.DeepClone(), pose);
            }
        }

        private static bool CompareEnergyLabelReading(LabelData labelData, LabelData labelData2)
        {
            if (labelData.EnergyConsumption != labelData2.EnergyConsumption
                || labelData.StarRating != labelData2.StarRating
                || labelData.Volume != labelData2.Volume)
            {
                return false;
            }
            return true;
        }

        private void Camera_ImageRecieved(object sender, Bitmap e)
        {
            if (AppData.AppMode == Mode.Inspection)
            {
                checkBoxSaveImg1.Invoke(new Action(() =>
                {
                    if (checkBoxSaveImg1.Checked)
                    {
                        string path = $@"{AppData.ModelPath}\Images\{PoseNumber}";
                        Directory.CreateDirectory(path);
                        e.DeepClone().Save($@"{path}\{DateTime.Now:dd_MM_yyyy_HH_mm_ss}.bmp");
                    }
                }));

                zoomInOutPictureBox1.PictureBox1.SetImage(e.DeepClone());

                poseImages.Add((e.DeepClone(), AppData.Camera.Poses[PoseNumber]));
                PoseNumber++;

                if (PoseNumber < AppData.Camera.Poses.Count)
                {
                    timeAtSetFocus = DateTime.Now;

                    //LoadCppModel(AppData.Camera.Poses[PoseNumber]);
                }
                ConsoleExtension.WriteWithColor("Image captured");

                // Do no send next pose signal when last pose image is grabbed.


                if (PoseNumber < AppData.Camera.Poses.Count)
                {
                    ConsoleExtension.WriteWithColor("Next Pose Signal sent", ConsoleColor.Yellow);

                }

                if (PoseNumber == AppData.Camera.Poses.Count)
                {
                    //PLCControl.WriteToPlc(1, PlcReg.PoseComplete);
                    ConsoleExtension.WriteWithColor("Pose complete signal sent.", ConsoleColor.Green);

                    // Activate first pose for next cycle

                }



            }
        }



        private void AppData_AppModeChanged(Mode obj)
        {
            if (labelAppMode.IsHandleCreated)
            {
                labelAppMode.Invoke(new Action(() => { labelAppMode.Text = $"App Mode: {obj}"; }));

            }

        }

        private string ModelPath
        {
            get
            {
                string path = $"{AppData.ProjectDirectory}/Models/{comboBoxModel.Text}";
                return path;
            }
        }


        private int _poseNumber = 0;
        public int PoseNumber
        {
            get { return _poseNumber; }
            set
            {

                flowLayoutPanel1.Invoke(new Action(() =>
                {

                    //ConsoleExtension.WriteWithColor($"{AppData.Camera.Poses.Count} {flowLayoutPanel1.Controls.Count}", ConsoleColor.Green);

                    if (value == 1)
                    {
                        //imageAssembler.ResetImage();
                        flowLayoutPanel1.Controls.Clear();
                    }

                    panel2.Invoke(new Action(() =>
                    {
                        radioButtonFull.Checked = false;
                    }));

                }));

                if (value >= AppData.Camera.Poses.Count)
                {
                    _poseNumber = 0;

                }
                else
                {
                    _poseNumber = value;
                }
            }

        }



        private void ComboBoxModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(ModelPath))
            {
                if (File.Exists($"{ModelPath}/CameraSetup.json"))
                {
                    AppData.ModelName = comboBoxModel.Text;
                    AppData.Camera.Poses = JsonConvert.DeserializeObject<BindingList<Pose>>(File.ReadAllText($"{ModelPath}/CameraSetup.json"));
                    HikCam.PoseCount = AppData.Camera.Poses.Count;
                    if (AppData.Camera.Poses.Count > 0)
                    {
                        LoadCppModel();

                        arrayIndex = -1;
                        Pose.SendAllPoseToPlc();
                        // Activate first pose of the camera for new cycle
                        AppData.Camera.SetExposure(AppData.Camera.Poses[0].exposure);
                        Console.WriteLine($"Exposure Time  :  {AppData.Camera.Poses[0].exposure}");
                        string csvPathFile = Path.Combine(ModelPath, "CsvPath.txt");
                        if (File.Exists(csvPathFile))
                        {
                            AppData.CsvPath = File.ReadAllText(csvPathFile);
                            Console.WriteLine("Csv Path: " + AppData.CsvPath);
                        }

                        backCameraModelLoaded = false;

                    }
                    else
                    {
                        labelCode1.Text = "Message: Model data incomplete. Recreate model.";
                    }
                }
            }
        }

        public int GetFolderCount(string path)
        {
            // Check if the path exists
            if (!Directory.Exists(path))
            {
                return 0;
            }

            // Get all directories in the specified path
            string[] directories = Directory.GetDirectories(path);

            // Return the count of directories
            return directories.Length;
        }

        private void UpdateConnectionStatusUI()
        {
            try
            {

                if (AppData.Camera.IsConnected)
                {
                    buttonCam1.FitImage($"{AppData.ProjectDirectory}/Icons/camGreen.png");
                }


            }
            catch (Exception ex)
            {
                ConsoleExtension.WriteWithColor(ex, ConsoleColor.Red);
            }
        }

        private void InspectionForm_Shown(object sender, EventArgs e)
        {
            TurnOffFormLevelDoubleBuffering();
            Refresh();
        }

        string _modelCode = "Null";
        string ModelCode
        {
            get { return _modelCode; }
            set
            {
                _modelCode = value;
                labelCode1.Invoke(new Action(() =>
                {
                    labelCode1.Text = $"Code 1: {_modelCode}";
                }));
            }
        }

        string _modelCode2 = "Null";
        string ModelCode2
        {
            get { return _modelCode2; }
            set
            {
                _modelCode2 = value;
                labelCode2.Invoke(new Action(() =>
                {
                    labelCode2.Text = $"Code 2: {_modelCode2}";
                }));
            }
        }
        static int InspectionCount = 1;


        public bool SearchAndLoadModelCode(string rootPath, string modelCode)
        {
            if (!Directory.Exists(rootPath))
            {
                Console.WriteLine("Root path does not exist.");
                return false;
            }

            try
            {
                string[] directories = Directory.GetDirectories(rootPath);

                Console.WriteLine($"Directories length {directories.Length}");

                foreach (string directory in directories)
                {
                    string modelFilePath = Path.Combine(directory, "ModelCode.txt");
                    //Console.WriteLine($"modelFilePath {modelFilePath}");

                    if (File.Exists(modelFilePath))
                    {
                        string fileContent = File.ReadAllText(modelFilePath);
                        //Console.WriteLine($"fileContent {fileContent} modelCode. {modelCode} ");

                        if (fileContent.Substring(0, 7) == modelCode.Substring(0, 7))
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(directory);

                            comboBoxModel.Invoke(new Action(() =>
                            {

                                for (int i = 0; i < comboBoxModel.Items.Count; i++)
                                {
                                    if (comboBoxModel.Items[i].ToString().Trim() == dirInfo.Name)
                                    {
                                        comboBoxModel.SelectedIndex = i;
                                        Thread.Sleep(800);
                                        break;
                                    }

                                }


                            }));
                            return true;
                        }

                    }
                }

                // If no matching model code found
                Console.WriteLine("Model Code not found in saved models: Please check if model is registered");

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        private void buttonFrontCamSetup_Click(object sender, EventArgs e)
        {
            Login_page login_Page = new Login_page();
           // login_Page.FormClosed += Login_Page_FormClosed1;
            login_Page.ShowDialog();
            if (!AppData.AdminUser)
            {
                MessageBox.Show("Enter correct Login details to proceed");
                return;
            }
            AppData.AdminUser = false;
            if (AppData.AppMode != Mode.Setup)
            {
                AppData.AppMode = Mode.Setup;
                AppData.Camera.SetTriggerMode(true);
                PlcControl.WriteValue(PlcControl.PlcReg.Light, 1);
                AppData.Camera.BitmapRecievedEvent -= Camera_BitmapRecievedEvent;
                CameraSetup cameraSetup = new CameraSetup();
                cameraSetup.Disposed += CameraSetup_Disposed;
                cameraSetup.Show();
                PlcControl.WriteValue(PlcControl.PlcReg.SetupMode, 1);

            }
            else
            {
                MessageBox.Show("Already in setup mode.", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void CameraSetup_Disposed(object sender, EventArgs e)
        {
            comboBoxModel.LoadDirectoryNames($"{AppData.ProjectDirectory}/Models");
            AppData.Camera.SetTriggerMode(true);

            AppData.Camera.BitmapRecievedEvent += Camera_BitmapRecievedEvent;
            PlcControl.WriteValue(PlcControl.PlcReg.SetupMode, 0);
            PlcControl.WriteValue(PlcControl.PlcReg.Light, 0);
        }


        private bool asyncCloseHack = true;

        private async void InspectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
            AppData.Camera.DisconnectCam();
            PlcControl.WriteValue(PlcControl.PlcReg.Light, 0);
            PlcControl.WriteValue(PlcControl.PlcReg.SetupMode, 0);
            PlcControl.WriteValue(PlcControl.PlcReg.SoftwareReady, 0);

            // turn off  light
            if (asyncCloseHack)
            {
                e.Cancel = true;
                //await ApiController.ShutdownServer(5008);
                //await ApiController.ShutdownServer(5001);
                // once task is completed we can start the form closing process
                asyncCloseHack = false;
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                Close();
                //Environment.Exit(0);
            }

            ConsoleExtension.WriteWithColor("Closing app", ConsoleColor.Red);



        }
        bool value = false;


        private void label17_Click(object sender, EventArgs e)
        {
            if (AppData.AppMode != Mode.Inspection)
            {
                AppData.AppMode = Mode.Inspection;

            }
            else
            {
                AppData.AppMode = Mode.Idol;

            }
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            ReportPage reportPage = new ReportPage();
            reportPage.Show();
        }


        bool backCameraModelLoaded = false;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    if (poseImages.Count > 0)
                    {
                        ProcessImage(poseImages.First().Item1, poseImages.First().Item2);
                        poseImages.RemoveAt(0);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                Thread.Sleep(100);

            }
        }

        private void buttonCam1_Click(object sender, EventArgs e)
        {

        }

        private void ResetCount()
        {
            Login_page login_Page = new Login_page();

            login_Page.ShowDialog();


            if (AppData.AdminUser == false)
            {
                return;
            }


            AppData.AdminUser = false;



            OkCount = 0;
            NgCount = 0;
            TotalCount = 0;
            labelResult.Invoke(new Action(() =>
            {
                labelResult.BackColor = Color.LightGray;
                labelResult.Text = "RESULT";
            }));
            Settings.Default.Save();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetCount();
        }

        private void buttonLight_Click(object sender, EventArgs e)
        {
            bool status = Convert.ToBoolean(PlcControl.ReadValue(PlcControl.PlcReg.Light));
            PlcControl.WriteValue(PlcControl.PlcReg.Light, Convert.ToInt32(!status));
        }
        List<LogReportData> logReports = new List<LogReportData>();
        private void backgroundWorkerDatabase_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {

                if (logReports.Count > 0 && string.IsNullOrEmpty(ModelCode) == false && ModelCode.ToLower() != "null" && ModelCode2.ToLower() != "null")
                {
                    LogReportData logReportData = logReports.First();

                    if (logReportData.PoseNumber < qrIndex[1] || qrIndex[1] == -1)
                    {
                        logReportData.ModelCode = ModelCode;

                    }
                    else
                    {
                        logReportData.ModelCode = ModelCode2;

                    }
                    Database.InsertDataIntoLogReport(logReportData);

                    logReports.RemoveAt(0);

                }

                Thread.Sleep(50);

            }
        }

        private void buttonPlc_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonFull_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFull.Checked)
            {
                label3.Visible = true;
                zoomInOutPictureBoxFull.Visible = true;
                label8.Visible = false;
                label2.Visible = false;
                zoomInOutPictureBox1.Visible = false;
                flowLayoutPanel1.Visible = false;
                zoomInOutPictureBoxFull.Invalidate();
            }
            else
            {
                label3.Visible = false;
                zoomInOutPictureBoxFull.Visible = false;
                label8.Visible = true;
                label2.Visible = true;
                zoomInOutPictureBox1.Visible = true;
                flowLayoutPanel1.Visible = true;
            }
        }

        private void labelResult_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("New Cycle started");
            //poseImages.Clear();
            //FinalResults.Clear();
            //PoseNumber = 0;
            //flowLayoutPanel1.Invoke(new Action(() =>
            //{
            //    flowLayoutPanel1.Controls.Clear();
            //    imageAssembler.ResetImage();
            //}));
            PlcControl.WriteValue(PlcControl.PlcReg.PartResult, 1);

        }

        private void labelNg_Click(object sender, EventArgs e)
        {
            //bool status = Convert.ToBoolean(PlcControl.ReadValue(PlcControl.PlcReg.NewCycle));
            //PlcControl.WriteValue(PlcControl.PlcReg.NewCycle, Convert.ToInt32(!status));
        }

        private void label19_Click(object sender, EventArgs e)
        {
            tryThresholds.IP_tools.resultFrontCam result = new tryThresholds.IP_tools.resultFrontCam();

            tryThresholds.IP_tools.resultFrontCam.DefectDetails defectDetails = new tryThresholds.IP_tools.resultFrontCam.DefectDetails();

            defectDetails.DefectName = "Name";
            defectDetails.CvRect = new cvRect(10, 20, 50, 60);
            defectDetails.Result = false;

            result.list_defectDetails.Add(defectDetails);
            result.list_defectDetails.Add(defectDetails);
            result.list_defectDetails.Add(defectDetails);
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonRecheck_Click(object sender, EventArgs e)
        {

            //Login_page login_Page = new Login_page();

            //login_Page.FormClosed += Login_Page_FormClosed;

            //login_Page.ShowDialog();
            PlcControl.WriteValue(PlcControl.PlcReg.ReCheck, 1);

        }

        private void Login_Page_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (AppData.AdminUser)
            {
                PlcControl.WriteValue(PlcControl.PlcReg.ReCheck, 1);

            }
            AppData.AdminUser = false;
        }

        private void buttonByPass_Click(object sender, EventArgs e)
        {
            Login_page login_Page = new Login_page();
            login_Page.FormClosed += Login_Page_FormClosed1;
            login_Page.ShowDialog();

        }

        private void Login_Page_FormClosed1(object sender, FormClosedEventArgs e)
        {
            if (AppData.AdminUser)
            {
                PlcControl.WriteValue(PlcControl.PlcReg.ByPass, 1);
            }
            AppData.AdminUser = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxModel_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }



        private void Login_Page_FormQrClosed(object sender, FormClosedEventArgs e)
        {
            if (AppData.AdminUser == false)
            {
                checkBoxQr.Checked = !checkBoxQr.Checked;
            }
            AppData.AdminUser = false;
        }

        private void checkBoxQr_Click(object sender, EventArgs e)
        {
            Login_page login_Page = new Login_page();

            login_Page.FormClosed += Login_Page_FormQrClosed;

            login_Page.ShowDialog();
        }

        private void labelCode2_Click(object sender, EventArgs e)
        {

        }
        bool[] defectsMarked = new bool[] { false, false };
        private void buttonFalseCall_Click(object sender, EventArgs e)                                                              
        {
            //Login_page login_Page = new Login_page();

            //login_Page.FormClosed += Login_Page_FormClosed2;

            //login_Page.ShowDialog();
            try
            {
               // ModelCode = "01961837-002157-BC24-Rev-001";
               // ModelCode2 = "01961837-002158-BC24-Rev-001";
                defectsMarked = new bool[] { false, false };
                AppData.DefectsMarkedOk = false;
                ReportDetailsForm reportDetailsForm = new ReportDetailsForm($@"{ModelCode}", true);
                reportDetailsForm.FormClosed += ReportDetailsForm_FormClosed;
                reportDetailsForm.ShowDialog();

                reportDetailsForm = new ReportDetailsForm($@"{ModelCode2}", true);
                reportDetailsForm.FormClosed += ReportDetailsForm_FormClosed1;
                reportDetailsForm.ShowDialog();

                PcbFinalResult = !defectsMarked.Any(x => x == false);

                if (PcbFinalResult)
                {
                    PlcControl.WriteValue(PlcControl.PlcReg.PartResult, 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void Login_Page_FormClosed2(object sender, FormClosedEventArgs e)
        //{
        //    if (AppData.AdminUser)
        //    {
        //        try
        //        {
        //            ModelCode = "01961837-002157-BC24-Rev-001";
        //            ModelCode2 = "01961837-002158-BC24-Rev-001";
        //            defectsMarked = new bool[] { false, false };
        //            AppData.DefectsMarkedOk = false;
        //            ReportDetailsForm reportDetailsForm = new ReportDetailsForm($@"""{ModelCode}""", true);
        //            reportDetailsForm.FormClosed += ReportDetailsForm_FormClosed;
        //            reportDetailsForm.ShowDialog();

        //            reportDetailsForm = new ReportDetailsForm($@"""{ModelCode2}""", true);
        //            reportDetailsForm.FormClosed += ReportDetailsForm_FormClosed1;
        //            reportDetailsForm.ShowDialog();

        //            PcbFinalResult = !defectsMarked.Any(x => x == false);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }

        //    AppData.AdminUser = false;

        //}

        private void ReportDetailsForm_FormClosed1(object sender, FormClosedEventArgs e)
        {
            if (AppData.DefectsMarkedOk == true)
            {
                Database.SetResultTrueByModelCode(ModelCode2);
                defectsMarked[1] = true;
                AppData.DefectsMarkedOk = false;
            }
        }

        private void ReportDetailsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (AppData.DefectsMarkedOk == true)
            {
                Database.SetResultTrueByModelCode(ModelCode);
                defectsMarked[0] = true;
                AppData.DefectsMarkedOk = false;

            }
        }
    }
}
