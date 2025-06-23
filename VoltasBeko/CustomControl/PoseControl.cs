using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using tryThresholds;
using VoltasBeko.Classes;
using tryThresholds.IP_tools;
using Measurement_AI.classes;

namespace VoltasBeko.CustomControl
{
    public partial class PoseControl : UserControl
    {
        public Pose pose = new Pose();
        public Pose poseCopy = new Pose();
        private bool editMode = false;
        private bool _selected = false;

        public bool Selected { get { return _selected; }
            set 
            {
                _selected = value;
                this.Invalidate();
            }
        }

        public PoseControl(Pose _pose, int index)
        {
            InitializeComponent();

            if (index % 2 != 0)
            {
                this.BackColor = Color.AntiqueWhite;
            }
            buttonIcon.FitImage($"{AppData.ProjectDirectory}/Icons/caution.png");

            pose = _pose;
            pose.Id = _pose.Id;
            poseCopy = new Pose(pose);
            numericUpDownExposure.Value = pose.exposure;
            
            textBoxX.Text = pose.location.X.ToString();
            textBoxY.Text = pose.location.Y.ToString();
            textBoxZ.Text = pose.location.Z.ToString();
            buttonDelete.Visible = true;
            buttonEdit.Visible = true;
            foreach (Control control in this.Controls)
            {
                if (control.Name == "buttonEdit" || control.Name == "buttonDelete" || control.Name.Contains("label") || control.Name.Contains("panel"))
                {
                    continue;
                }
                control.Enabled = false;

            }
            textBoxX.Enabled = false;
            textBoxY.Enabled = false;
            textBoxZ.Enabled = false;
            buttonIcon.Enabled = true;
            buttonIcon.Visible = true;

            AppData.Camera.Poses.ListChanged += Poses_ListChanged;
            pose.PoseSelectionChanged += Pose_PoseSelectionChanged;
            Poses_ListChanged(null, null);
        }

        private void Pose_PoseSelectionChanged(object sender, bool selected)
        {

            this.Invalidate();
        }

       
        public int poseNumber = 1;
        private void Poses_ListChanged(object sender, ListChangedEventArgs e)
        {
            for (int i = 0; i < AppData.Camera.Poses.Count; i++)
            {
                if (pose.GetHashCode() == AppData.Camera.Poses[i].GetHashCode())
                {
                    poseNumber = i + 1;
                    pose.number = poseNumber; 
                    labelPoseNumber.Text = $"Pose Number: {poseNumber}";
                    break;
                }
            }
        }

        public PoseControl()
        {
            try
            {
                InitializeComponent();
                buttonIcon.FitImage($"{AppData.ProjectDirectory}/Icons/caution.png");

                GetLoaction();
                RefreshCameraValues();
                pose.exposure = (int)AppData.Camera.Exposure;

                buttonConfig.Visible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
           
        }


       

        public void GetLoaction()
        {
            pose.location = new PointT(PlcControl.ReadLocation());
        }

        public void RefreshCameraValues()
        {
            try
            {
                //pose.exposure = (int)AppData.Camera.Exposure;

                numericUpDownExposure.Value = pose.exposure;
                textBoxX.Text = pose.location.X.ToString();
                textBoxY.Text = pose.location.Y.ToString();
                textBoxZ.Text = pose.location.Z.ToString();

            }
            catch (Exception ex)
            {
                ConsoleExtension.WriteWithColor($"{ex.Message}\n{ex.StackTrace}", ConsoleColor.Yellow);
            }
            
        }

        private async void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                bool configFileExist = File.Exists($@"{AppData.ModelPath}\{pose.Id}\MarkInsTools.json");
                PoseControl_Click(sender, e);
                editMode = !editMode;
                if (editMode)
                {
                    buttonEdit.Text = "Save and stop edit";
                    foreach (Control control in this.Controls)
                    {
                        control.Enabled = true;
                    }
                    textBoxX.Enabled = true;
                    textBoxY.Enabled = true;
                    textBoxZ.Enabled = true;

                }
                else
                {
                    buttonEdit.Text = "Edit Pose";

                    foreach (Control control in this.Controls)
                    {
                        if (control.Name == "buttonIcon" || control.Name == "buttonEdit" || control.Name == "buttonDelete" || control.Name.Contains("label") || control.Name.Contains("panel"))
                        {
                            continue;
                        }
                        control.Enabled = false;

                    }
                    AppData.Camera.BitmapRecievedEvent += Camera_BitmapRecievedEvent;
                    UpdateCameraValues();
                    await Task.Delay(800);
                    AppData.Camera.BitmapRecievedEvent -= Camera_BitmapRecievedEvent;

                    Console.WriteLine(AppData.ModelPath + pose.number);

                    ConsoleExtension.WriteWithColor($"{poseImage.Size}");
                    if (checkBoxUpdateImage.Checked)
                    {
                        if (configFileExist == false)
                        {
                            poseImage.DeepClone().Save($@"{AppData.ModelPath}\{pose.Id}.bmp");
                            pose.image = poseImage.DeepClone();
                            checkBoxUpdateImage.Checked = false;
                        }
                        else if (configFileExist && GlobalItems.NewImageUpdated)
                        {
                            poseImage.DeepClone().Save($@"{AppData.ModelPath}\{pose.Id}.bmp");
                            pose.image = poseImage.DeepClone();
                            checkBoxUpdateImage.Checked = false;
                            GlobalItems.NewImageUpdated = false;
                        }
                        else
                        {
                            MessageBox.Show("Can not update image. Configuration not verified on new image.");
                        }
                        GlobalItems.NewImageUpdated = false;

                    }

                    textBoxX.Enabled = false;
                    textBoxY.Enabled = false;
                    textBoxZ.Enabled = false;
                    if (configFileExist)
                    {
                        buttonIcon.FitImage($"{AppData.ProjectDirectory}/Icons/check.png");

                    }
                    else
                    {
                        buttonIcon.FitImage($"{AppData.ProjectDirectory}/Icons/caution.png");

                    }
                }
            }
            catch (Exception ex)
            {

                ConsoleExtension.WriteWithColor(ex, ConsoleColor.Red);
            }
            
        }

        private void Camera_BitmapRecievedEvent(Bitmap image, int posenumber)
        {
            Console.WriteLine("Image updated in Pose control");
            poseImage = image.DeepClone();

        }

        static Bitmap poseImage;

        private void Camera_ImageRecieved(object sender, Bitmap e)
        {
            poseImage = e.DeepClone();
        }

        
        private void SetLocation()
        {
            try
            {
                if (textBoxX.Digit() > 600 || textBoxY.Digit() > 1100)
                {
                    MessageBox.Show("Alert!", "Limit exceeded. Range X: 249 Y: 349. Z: 90", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxX.Text = pose.location.X.ToString();
                    textBoxY.Text = pose.location.Y.ToString();
                    textBoxZ.Text = pose.location.Z.ToString();
                    return;
                }

                pose.location.X = textBoxX.Digit();
                pose.location.Y = textBoxY.Digit();
                pose.location.Z = textBoxZ.Digit();
                Console.WriteLine($"Location set {pose.location}");
                PlcControl.MoveLocation(pose.location);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
          
        }

        public void MoveCamera()
        {
            SetLocation();
           
        }


        public void UpdateUI()
        {
            numericUpDownExposure.Value = pose.exposure;
            textBoxX.Text = pose.location.X.ToString();
            textBoxY.Text = pose.location.Y.ToString();
            textBoxZ.Text = pose.location.Z.ToString();
        }



        public void UpdateCameraValues()
        {
            pose.exposure = (int)numericUpDownExposure.Value;
            if (pose.location.X != textBoxX.Digit() || pose.location.Y != textBoxY.Digit() || pose.location.Z != textBoxZ.Digit()) 
            {
                SetLocation();
            }
            pose.location.X = textBoxX.Digit();
            pose.location.Y = textBoxY.Digit();
            pose.location.Z = textBoxZ.Digit();
            AppData.Camera.SetExposure(pose.exposure);

            RefreshCameraValues();
        }

        public static void ObjShallowCopy(Object dst, object src)
        {
            var srcT = src.GetType();
            var dstT = dst.GetType();
            foreach (var f in srcT.GetFields())
            {
                var dstF = dstT.GetField(f.Name);
                if (dstF == null || dstF.IsLiteral)
                    continue;
                dstF.SetValue(dst, f.GetValue(src));
            }

            foreach (var f in srcT.GetProperties())
            {
                var dstF = dstT.GetProperty(f.Name);
                if (dstF == null)
                    continue;

                dstF.SetValue(dst, f.GetValue(src, null), null);
            }
        }

        //public void CopyPose(Pose poseSrc, Pose poseDst)
        //{
        //    poseDst.number = poseSrc.number;
        //    poseDst.image = poseSrc.image;
        //    poseDst.focus = cop
        //}

        private void PoseControl_Leave(object sender, EventArgs e)
        {
            if (editMode)
            {
                DialogResult dialogResult = MessageBox.Show("Edits not saved do you want to save changes ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    UpdateCameraValues();
                    buttonEdit.PerformClick();

                }
                else
                {
                    //pose = poseCopy;
                    ObjShallowCopy(pose, poseCopy);
                    UpdateUI();
                    UpdateCameraValues();
                    RefreshCameraValues();
                    buttonEdit.PerformClick();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            PoseControl_Click(sender, e);
            
            for (int i = 0; i < AppData.Camera.Poses.Count; i++)
            {

                if (pose.GetHashCode() == AppData.Camera.Poses[i].GetHashCode())
                {
                    //MessageBox.Show($"Obj same at {i} value 1: {pose.exposure}\n HashCode {pose.GetHashCode()}" +
                    //    $" value 2: {AppData.camera.Poses[i].exposure} \n HashCode {AppData.camera.Poses[i].GetHashCode()}" +
                    //    $"");
                    
                    
                    DialogResult dialogResult = MessageBox.Show("Delete item from list ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        AppData.Camera.Poses[i].Selected = false;
                        pose.Selected = false;
                        if (AppData.Camera.Poses.Count > 0)
                        {
                            AppData.Camera.Poses[i - 1].Selected = true;
                        }
                        DeleteConfigFolder(AppData.ModelPath);
                        AppData.Camera.Poses.RemoveAt(i);
                        this.Dispose();
                    }
                   
                }
            }
        }

        private void DeleteConfigFolder(string modelPath)
        {
            try
            {
                pose.image.Dispose();
                if (File.Exists($@"{modelPath}\{pose.Id}.bmp"))
                {
                    File.Delete($@"{modelPath}\{pose.Id}.bmp");
                }
                if (Directory.Exists($@"{modelPath}\{pose.Id}"))
                {
                    Directory.Delete($@"{modelPath}\{pose.Id}", true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private void PoseControl_Load(object sender, EventArgs e)
        {
            numericUpDownExposure.KeyDown += Control_KeyDown;
            textBoxX.KeyDown += Control_KeyDown;
            textBoxY.KeyDown += Control_KeyDown;
            textBoxZ.KeyDown += Control_KeyDown;
            if (File.Exists($@"{AppData.ModelPath}\{pose.Id}\MarkInsTools.json"))
            {
                buttonIcon.FitImage($"{AppData.ProjectDirectory}/Icons/check.png");

            }
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            UpdateCameraValues();
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateCameraValues();
                e.SuppressKeyPress = true;
            }
        }

        private void PoseControl_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < AppData.Camera.Poses.Count; i++)
            {
                if (pose.GetHashCode() == AppData.Camera.Poses[i].GetHashCode())
                {
                    AppData.Camera.Poses[i].Selected = true;
                    pose.Selected = true;
                    UpdateCameraValues();
                }
                else
                {
                    AppData.Camera.Poses[i].Selected = false;
                }

            }

        }

        private void PoseControl_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (AppData.Camera.Poses.Count > 0)
                {
                    if (AppData.Camera.Poses[poseNumber - 1].Selected)
                    {
                        Color color = Color.Red;
                        int widht = 5;
                        ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, labelPosition.Height - 7, Width, Height - (labelPosition.Height - 7)),
                                  color, widht, ButtonBorderStyle.Solid,
                                  color, widht, ButtonBorderStyle.Solid,
                                  color, widht, ButtonBorderStyle.Solid,
                                  color, widht, ButtonBorderStyle.Solid);
                    }
                    else
                    {
                        ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, this.BackColor, ButtonBorderStyle.None);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            

        }

        private async void buttonConfig_Click(object sender, EventArgs e)
        {
            // mode = 0 => register new
            // mode = 1 => edit
            // mode = 2 => updateImage
            int mode = Convert.ToInt32(Directory.Exists($@"{AppData.ModelPath}\{pose.Id}") && !checkBoxRegisterNew.Checked);
            if (checkBoxUpdateImage.Checked)
            {
                mode = 2;
                AppData.Camera.BitmapRecievedEvent += Camera_BitmapRecievedEvent;
                UpdateCameraValues();
                await Task.Delay(800);
                AppData.Camera.BitmapRecievedEvent -= Camera_BitmapRecievedEvent;
                Mark_Inspection mark_Inspection = new Mark_Inspection(mode, $@"{AppData.ModelPath}\{pose.Id}", Convert.ToInt64(pose.Id), poseImage);
                mark_Inspection.Show();
            }
            else
            {
                Mark_Inspection mark_Inspection = new Mark_Inspection(mode, $@"{AppData.ModelPath}\{pose.Id}", Convert.ToInt64(pose.Id), pose.image);
                mark_Inspection.Show();
            }
           
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            MoveCamera();
        }

        private void checkBoxRegisterNew_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
