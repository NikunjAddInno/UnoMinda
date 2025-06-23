using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoltasBeko.classes;

namespace tryThresholds.IP_tools
{
    public partial class uc_fixture : UserControl
    {
        tool_Fixture toolData; //= new tool_ROI("temp");
        public uc_fixture()
        {
            InitializeComponent();
            toolData = new tool_Fixture("Fixture");
            Set_toolData_to_UI(toolData);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            toolData.Name = txtName.Text;
            txtName.Text = toolData.Name;
            Set_RoiRegion(toolData.Obj_toolShape, toolData.Template_1_Name,toolData.Template_2_Name);


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void nudRotationTolerance_ValueChanged(object sender, EventArgs e)
        {
            toolData.RotattionLimit_deg = (float)nudRotationTolerance.Value;
        }

        private void nudMatchThreshT1_ValueChanged(object sender, EventArgs e)
        {
            toolData.MatchScoreThresh_T1 = (float)nudMatchThreshT1.Value;
        }

        private void nudMatchThreshT2_ValueChanged(object sender, EventArgs e)
        {
            toolData.MatchScoreThresh_T2 = (float)nudMatchThreshT2.Value;
        }

        public void Set_RoiRegion(DrawPbMmarkIns toolShape, String templateNameT1, String templateNameT2)
        {
            if (!toolShape.pointsAvailable)
            {
                Console.WriteLine("roi points not complete");
                lblRegion.Text = "Not Selected";
                return;
            }
            else
            {
                toolData.Obj_toolShape = toolShape;
                Rectangle roi_pix = imageFns_bmp.getRectFromPoints(toolShape.list_points[0].ToPoint(), toolShape.list_points[1].ToPoint());
                toolData.RectRoi_T1 = new cvRect(roi_pix);   
                Rectangle roi_pix2 = imageFns_bmp.getRectFromPoints(toolShape.list_points[2].ToPoint(), toolShape.list_points[3].ToPoint());
                toolData.RectRoi_T2 = new cvRect(roi_pix2);

                Rectangle search_pix1 = imageFns_bmp.getRectFromPoints(toolShape.list_points[4].ToPoint(), toolShape.list_points[5].ToPoint());
                Rectangle search_pix2 = imageFns_bmp.getRectFromPoints(toolShape.list_points[6].ToPoint(), toolShape.list_points[7].ToPoint());
                toolData.RectSearcRegion_T1 = new cvRect(search_pix1);
                toolData.RectSearcRegion_T2 = new cvRect(search_pix2);

                cvPoint2f centerT1 = new cvPoint2f(roi_pix.X + roi_pix.Width / 2, roi_pix.Y + roi_pix.Height / 2);
                cvPoint2f centerT2 = new cvPoint2f(roi_pix2.X + roi_pix2.Width / 2, roi_pix2.Y + roi_pix2.Height / 2);
                
                toolData.CenterLoc= new cvPoint((int)((centerT1.X+centerT2.X)/2), (int)((centerT1.Y+centerT2.Y)/2));

                lblRegion.Text = $"X={roi_pix.X}, Y={roi_pix.Y}, W={roi_pix.Width}, H={roi_pix.Height}";
                lblRegion.Text =lblRegion.Text+ $"  X={roi_pix2.X}, Y={roi_pix2.Y}, W={roi_pix2.Width}, H={roi_pix2.Height}";

                toolData.Template_1_Name = templateNameT1;
                toolData.Template_2_Name = templateNameT2;
                lblTemplateNameT1.Text = toolData.Template_1_Name;
                lblTemplateNameT2.Text = toolData.Template_2_Name;
                Console.WriteLine("points complete");
            }
        }
        public tool_Fixture Set_toolData_to_UI(tool_Fixture fixture_toolData)
        {
            toolData = fixture_toolData;
            nudRotationTolerance.Value = (decimal)toolData.RotattionLimit_deg;
            nudMatchThreshT1.Value = (decimal)toolData.MatchScoreThresh_T1;
            nudMatchThreshT2.Value = (decimal)toolData.MatchScoreThresh_T2;
            txtName.Text = toolData.Name;
            Set_RoiRegion(toolData.Obj_toolShape, toolData.Template_1_Name,toolData.Template_2_Name);

            return toolData;
        }
        public tool_Fixture get_toolData_from_UI()
        {
            // toolData.Name = newName;
            return toolData;
        }


    }
}
