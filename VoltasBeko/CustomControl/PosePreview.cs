using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoltasBeko.AppForms;

namespace VoltasBeko.CustomControl
{
    public partial class PosePreview : UserControl
    {


        Bitmap bitmap = null;
        tryThresholds.IP_tools.resultFrontCam resultFrontCam = null;
        int poseNum = 0;

        public PosePreview(Bitmap bmp, tryThresholds.IP_tools.resultFrontCam result, int poseNumber)
        {
            InitializeComponent();
            
            Load += PosePreview_Load;
            bitmap = bmp;
            resultFrontCam = result;

            if (result.finalResult)
            {
                labelResult.Text = "OK";
                labelResult.BackColor = Color.LimeGreen;
            }
            else
            {
                labelResult.Text = "NG";
                labelResult.BackColor = Color.Red;
            }

            if (result.list_defectDetails.Count > 0)
            {
                for (int i = 0; i < result.list_defectDetails.Count; i++)
                {
                    if (result.list_defectDetails[i].Result == false)
                    {
                        listBoxDefects.Items.Add(result.list_defectDetails[i].DefectName);

                    }
                }
            }

            labelPoseNumber.Text += $" {poseNumber}";
            poseNum = poseNumber;
        }

        private void PosePreview_Load(object sender, EventArgs e)
        {
            if (bitmap != null)
            {
                pictureBox.SetImage(bitmap);

            }
        }

        public PosePreview()
        {
            InitializeComponent();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (this.ParentForm.Name == "PoseEnlargePreview")
            {
                return;
            }
            AppForms.PoseEnlargePreview poseEnlargePreview = new AppForms.PoseEnlargePreview(new PosePreview(this.bitmap, resultFrontCam, poseNum));
            poseEnlargePreview.ShowDialog();
        }

        private void listBoxDefects_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxDefects_Click(object sender, EventArgs e)
        {
            if (this.ParentForm.Name == "PoseEnlargePreview")
            {
                return;
            }
            AppForms.PoseEnlargePreview poseEnlargePreview = new AppForms.PoseEnlargePreview(new PosePreview(this.bitmap, resultFrontCam, poseNum));
            poseEnlargePreview.ShowDialog();
        }
    }
}
