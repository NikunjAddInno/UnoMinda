using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoltasBeko.CustomControl;
using System.IO;
using Newtonsoft.Json;
using VoltasBeko.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;
using Application = System.Windows.Forms.Application;
using tryThresholds.IP_tools;
namespace VoltasBeko.AppForms
{
    public partial class CameraSetup : Form
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

        public CameraSetup()
        {
            InitializeComponent();
            //Bitmap bitmap = new Bitmap(@"D:\Add Inno\Projects\tryThresholds\tryThresholds\bin\x64\Release\m3.bmp");
            //zoomInOutPictureBox1.PictureBox1.Image = bitmap;
            comboBoxModel.SelectedIndexChanged += ComboBoxModel_SelectedIndexChanged;
            comboBoxModel.LoadDirectoryNames($"{AppData.ProjectDirectory}/Models");
            Directory.CreateDirectory(ModelPath);
            AppData.Camera.SetTriggerMode(false);
            AppData.Camera.Poses.ListChanged += Poses_ListChanged;

            Poses_ListChanged(null, null);
            StartScanner();
        }

        public void StartScanner()
        {

        }

       

        private void ComboBoxModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(ModelPath))
            {
                if (File.Exists($"{ModelPath}/CameraSetup.json"))
                {
                    AppData.ModelName = comboBoxModel.Text;
                    flowLayoutPanelPose.Controls.Clear();
                    AppData.Camera.Poses = JsonConvert.DeserializeObject<BindingList<Pose>>(File.ReadAllText($"{ModelPath}/CameraSetup.json"));
                    AppData.Camera.Poses.ListChanged += Poses_ListChanged;
                    labelPoseCount.Text = $"Total Poses: {AppData.Camera.Poses.Count}";

                    foreach (Pose pose in AppData.Camera.Poses)
                    {

                        pose.image?.Dispose();


                        String templatePathUpper = $@"{ModelPath}\{pose.Id}.bmp";
                        if (File.Exists(templatePathUpper))
                        {
                            //Bitmap b = (Bitmap)Bitmap.FromFile(templatePathUpper).Clone();
                            //    picModelImage.Image = b;
                            //   picModelImage.Invalidate();
                            Image img = null;
                            using (Image imgTmp = Image.FromFile(templatePathUpper))
                            {
                                img = new Bitmap(imgTmp.Width, imgTmp.Height, imgTmp.PixelFormat);
                                Graphics gdi = Graphics.FromImage(img);
                                gdi.DrawImageUnscaled(imgTmp, 0, 0);
                                gdi.Dispose();
                                imgTmp.Dispose(); // just to make sure
                            }
                            pose.image = new Bitmap(img);
                        }

                        //using (Bitmap bmp = new Bitmap($@"{ModelPath}\{pose.Id}.bmp", true))
                        //{
                        //    pose.image = bmp.DeepClone();
                        //}

                        pose.PoseSelectionChanged += CameraPose_PoseSelectionChanged;
                        PoseControl poseControl = new PoseControl(pose, pose.number);
                        flowLayoutPanelPose.Controls.Add(poseControl);
                    }

                    if (File.Exists($"{ModelPath}/CsvPath.txt"))
                    {
                        string code = File.ReadAllText($"{ModelPath}/CsvPath.txt");
                        labelCode.Text = code;
                    }
                    else
                    {
                        csvPath = $"{AppData.ProjectDirectory}/CSV/";
                        labelCode.Text = $"CSV Path: {csvPath}";
                        File.WriteAllText($"{ModelPath}/CsvPath.txt", csvPath);
                    }
                }
            }
        }

        Pose cameraPose = new Pose();

        private string ModelPath
        {
            get
            {
                string path = $"{AppData.ProjectDirectory}/Models/{comboBoxModel.Text}";
                return path;
            }
        }

        private void Poses_ListChanged(object sender, ListChangedEventArgs e)
        {
            poseControl1.labelPoseNumber.Text = $"Pose Number: {AppData.Camera.Poses.Count + 1}";
            labelPoseCount.Text = $"Total Poses: {AppData.Camera.Poses.Count}";
        }


        private void CameraSetup_Load(object sender, EventArgs e)
        {
            AppData.Camera.BitmapRecievedEvent += Camera_BitmapRecievedEvent;
            
            poseControl1.RefreshCameraValues();
            AppData.Camera.SetTriggerMode(false);
            //zoomPictureBoxPose.PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            timer1.Start();
        }

        private void Camera_BitmapRecievedEvent(Bitmap image, int posenumber)
        {
            try
            {
                if (zoomInOutPictureBox1.IsHandleCreated == true && zoomInOutPictureBox1.IsDisposed == false)
                {
                    zoomInOutPictureBox1.Invoke(new Action(() =>
                    {
                        zoomInOutPictureBox1.PictureBox1.Image = image.DeepClone();

                    }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
           
        }

        private void buttonSavePose_Click(object sender, EventArgs e)
        {
            Saved = false;
            Directory.CreateDirectory(ModelPath);
            poseControl1.UpdateCameraValues();
            poseControl1.pose.PoseSelectionChanged += CameraPose_PoseSelectionChanged;
            poseControl1.pose.Id = DateTime.Now.ToString("yyyyMMddHHmmss");
            cameraPose = new Pose(poseControl1.pose);

            Bitmap bmp = new Bitmap(zoomInOutPictureBox1.PictureBox1.Image);

            cameraPose.image = bmp.DeepClone();
            AppData.Camera.Poses.Add(new Pose(cameraPose));

            bmp.Save($"{ModelPath}/{cameraPose.Id}.bmp");
            bmp.Dispose();

            PoseControl poseControl = new PoseControl(AppData.Camera.Poses.Last(), AppData.Camera.Poses.Count);
            flowLayoutPanelPose.Controls.Add(poseControl);

        }

        private void CameraPose_PoseSelectionChanged(object sender, bool selected)
        {
            if (selected)
            {
                Pose pose = (Pose)sender;
                zoomPictureBoxPose.PictureBox1.Image = pose.image;
                PlcControl.MoveLocation(pose.location);

            }
        }

        private async void buttonAutoFocus_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            await Task.Delay(TimeSpan.FromSeconds(4));
            Application.UseWaitCursor = false;
            Cursor.Current = Cursors.Default;

        }

        bool Saved = false;

        private void buttonSaveSetup_Click(object sender, EventArgs e)
        {
            if (AppData.Camera.Poses.Count > 0)
            {
                Directory.CreateDirectory(ModelPath);
                File.WriteAllText($"{ModelPath}/CameraSetup.json", JsonConvert.SerializeObject(AppData.Camera.Poses));
                File.WriteAllText($"{ModelPath}/CsvPath.txt",csvPath);
                buttonClear.PerformClick();
                Saved = true;
            }
            else
            {
                MessageBox.Show("Model pose data empty. Can not save data.", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CameraSetup_Shown(object sender, EventArgs e)
        {
            TurnOffFormLevelDoubleBuffering();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PointT point = new PointT();
            point = PlcControl.ReadLocation();
            labelActPos.Text = $"Actuator current position X: {point.X} Y: {point.Y} Z: {point.Z}";
        }

        private void CameraSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Saved)
            {
                DialogResult dialogResult = MessageBox.Show("Changes not Saved. Close without saving ?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }

            }
            AppData.AppMode = Mode.Idol;
            AppData.Camera.BitmapRecievedEvent -= Camera_BitmapRecievedEvent;
            AppData.Camera.SetTriggerMode(true);
            Thread.Sleep(1500);
            PlcControl.WriteValue(PlcControl.PlcReg.SetupMode, 0);

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            AppData.Camera.Poses.Clear();
            flowLayoutPanelPose.Controls.Clear();
        }

        private void comboBoxModel_TextChanged(object sender, EventArgs e)
        {
            AppData.ModelName = comboBoxModel.Text;
        }
        private string csvPath = $"{AppData.ProjectDirectory}/CSV/";
        private void buttonScanCode_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a folder";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderDialog.SelectedPath;
                    csvPath = selectedPath;
                    labelCode.Text = $"CSV Path: {csvPath}";
                    File.WriteAllText($"{ModelPath}/CsvPath.txt", csvPath);

                    Console.WriteLine("Selected Folder Path: " + selectedPath);
                }
                else
                {
                    Console.WriteLine("No folder selected.");
                }
            }
        }

        private void poseControl1_Load(object sender, EventArgs e)
        {

        }
        public static void ReplaceInJsonFile(string filePath, string oldValue, string newValue)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found: " + filePath);
                return;
            }

            try
            {
                // Read all content from the JSON file
                string jsonContent = File.ReadAllText(filePath);

                // Replace all occurrences of the old value with the new value
                string updatedJsonContent = jsonContent.Replace(oldValue, newValue);

                // Write the updated content back to the file
                File.WriteAllText(filePath, updatedJsonContent);

                Console.WriteLine("Replacement done successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing the file: " + ex.Message);
            }
        }
        private void buttonStartCopy_Click(object sender, EventArgs e)
        {
            Task.Run(async () => 
            {
                try
                {
                    this.Invoke(new Action(async () => 
                    {
                        if (flowLayoutPanelPose.Controls.Count > 0 && string.IsNullOrEmpty(textBoxOffsetY.Text) == false && textBoxOffsetY.Digit() != null)
                        {
                            int offsetValue = textBoxOffsetY.Digit();

                            if (AppData.Camera.Poses.Any(p => p.location.Y + offsetValue > 340 || p.location.Y + offsetValue < 15) == true)
                            {
                                MessageBox.Show("Offset value limit exceeded.", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }


                            // create a new pose with off set value
                            Saved = false;

                            int pcbPoseCount = AppData.Camera.Poses.Count;

                            for (int i = 0; i < pcbPoseCount; i++)
                            {
                                string previousId = AppData.Camera.Poses[i].Id;
                                Pose pose = new Pose(AppData.Camera.Poses[i]);
                                pose.location.Y += offsetValue;

                                Directory.CreateDirectory(ModelPath);
                                poseControl1.pose = pose;
                                AppData.Camera.SetExposure(pose.exposure);

                                PlcControl.MoveLocation(pose.location);

                                poseControl1.pose.PoseSelectionChanged += CameraPose_PoseSelectionChanged;
                                poseControl1.pose.Id = DateTime.Now.ToString("yyyyMMddHHmmss");
                                cameraPose = new Pose(poseControl1.pose);
                                await Task.Delay(1500);

                                Bitmap bmp = new Bitmap(zoomInOutPictureBox1.PictureBox1.Image);

                                cameraPose.image = bmp.DeepClone();
                                AppData.Camera.Poses.Add(new Pose(cameraPose));

                                bmp.Save($"{ModelPath}/{cameraPose.Id}.bmp");
                                string filePath = $@"{AppData.ModelPath}\{previousId}\" + "MarkInsTools.json";

                                applied_tools Obj_UserTools_applied = applied_tools.Load_from_file(filePath);

                                Directory.CreateDirectory($@"{AppData.ModelPath}\{pose.Id}");
                                Obj_UserTools_applied.glass_templateFileName = Obj_UserTools_applied.glass_templateFileName.Replace(previousId, pose.Id);
                                Obj_UserTools_applied.rotatedROIFileName = Obj_UserTools_applied.rotatedROIFileName.Replace(previousId, pose.Id);
                                bmp.Save($@"{Obj_UserTools_applied.glass_templateFileName}");
                                bmp.Save($@"{Obj_UserTools_applied.rotatedROIFileName}");

                                if (Obj_UserTools_applied.AppliedToolCnt > 0)
                                {
                                    for (int j = 0; j < Obj_UserTools_applied.lst_Fixture_tools.Count; j++)
                                    {
                                        Rectangle rectangle = Obj_UserTools_applied.lst_Fixture_tools[j].RectRoi_T1.ToRectangle();

                                        Bitmap temp1 = bmp.Clone(rectangle, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                                        string temp1Path = Obj_UserTools_applied.lst_Fixture_tools[j].Template_1_Name.Replace(previousId, pose.Id);

                                        Obj_UserTools_applied.lst_Fixture_tools[j].Template_1_Name = temp1Path;

                                        temp1.Save(temp1Path);

                                        Rectangle rectangle2 = Obj_UserTools_applied.lst_Fixture_tools[j].RectRoi_T2.ToRectangle();

                                        Bitmap temp2 = bmp.Clone(rectangle2, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                                        string temp2Path = Obj_UserTools_applied.lst_Fixture_tools[j].Template_2_Name.Replace(previousId, pose.Id);
                                        Obj_UserTools_applied.lst_Fixture_tools[j].Template_2_Name = temp2Path;

                                        temp2.Save(temp2Path);

                                    }

                                    for (int j = 0; j < Obj_UserTools_applied.lst_CROI_tool.Count; j++)
                                    {
                                        Rectangle rectangle = Obj_UserTools_applied.lst_CROI_tool[j].Rect_roi.ToRectangle();

                                        Bitmap temp = bmp.Clone(rectangle, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                                        Obj_UserTools_applied.lst_CROI_tool[j].TemplateName = Obj_UserTools_applied.lst_CROI_tool[j].TemplateName.Replace(previousId, pose.Id);

                                        string tempPath = Obj_UserTools_applied.lst_CROI_tool[j].TemplateName;

                                        temp.Save(tempPath);
                                    }

                                    //for (int j = 0; j < Obj_UserTools_applied.lst_QR_tools.Count; j++)
                                    //{
                                    //    Rectangle rectangle = Obj_UserTools_applied.lst_QR_tools[j].Rect_roi.ToRectangle();

                                    //    Bitmap temp = bmp.Clone(rectangle, System.Drawing.Imaging.PixelFormat.Format24bppRgb);


                                    //}
                                    //for (int k = 0; k < Obj_UserTools_applied.lst_GrayPresence_tools.Count; k++)
                                    //{
                                    //    Rectangle rectangle = Obj_UserTools_applied.lst_GrayPresence_tools[k].Rect_roi.ToRectangle();

                                    //    Bitmap temp = bmp.Clone(rectangle, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                                    //    Obj_UserTools_applied.lst_GrayPresence_tools[k].Name = Obj_UserTools_applied.lst_GrayPresence_tools[k].TemplateName.Replace(previousId, pose.Id);

                                    //    string tempPath = Obj_UserTools_applied.lst_GrayPresence_tools[k].TemplateName;

                                    //    temp.Save(tempPath);
                                    //}
                                }


                                string newfilePath = $@"{AppData.ModelPath}\{cameraPose.Id}\" + "MarkInsTools.json";

                                applied_tools.Save_to_file(newfilePath, Obj_UserTools_applied);
                                ReplaceInJsonFile(newfilePath, previousId, cameraPose.Id);

                                PoseControl poseControl = new PoseControl(AppData.Camera.Poses.Last(), AppData.Camera.Poses.Count);
                                flowLayoutPanelPose.Controls.Add(poseControl);

                                await Task.Delay(1000);

                                bmp.Dispose();

                            }
                            // move the actuator according to new pose

                            // capture image and save pose values to folder

                            // copy cpp config files to new folder with new id

                            // once done prompt the user for work done



                        }
                        else
                        {
                            MessageBox.Show("Invalid offset value or pose list empty.", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }));
                   
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
            });
            
           
        }
    }
}
