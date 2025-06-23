using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoltasBeko.classes;

namespace tryThresholds.IP_tools
{
    public partial class uc_ROI : UserControl
    {
        public uc_ROI()
        {
            InitializeComponent();
            toolData = new tool_ROI("temp");
            Set_toolData_to_UI(toolData);

        }
        tool_ROI toolData; //= new tool_ROI("temp");
        private void uc_ROI_Load(object sender, EventArgs e)
        {
            //if (userColours.list_userColours.Count > 0)
            //{
            //    foreach (colourRange_hsv c in userColours.list_userColours)
            //    {
            //        cmbcolours.Items.Add(c.name+"_"+c.remark);
            //    }
            
            //}
        }

        private void nudRotationTolerance_ValueChanged(object sender, EventArgs e)
        {
            toolData.RotationLimit = (float)nudRotationTolerance.Value;
        }

        private void nudMatchThresh_ValueChanged(object sender, EventArgs e)
        {
            toolData.MathcScoreThresh = (float)nudMatchThresh.Value;
        }

        //public void Set_RoiRegion(Rectangle roi_pix,String templateName)
        //{
        //    toolData.Rect_roi = roi_pix;
        //    lblRegion.Text = $"X={roi_pix.X}, Y={roi_pix.Y}, W={roi_pix.Width}, H={roi_pix.Height}";

        //    toolData.TemplateName = templateName;
        //    lblTemplateName.Text = toolData.TemplateName;
        //}       
        public void Set_RoiRegion(DrawPbMmarkIns toolShape,String templateName)
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
                toolData.Rect_roi =new cvRect( roi_pix);
                lblRegion.Text = $"X={roi_pix.X}, Y={roi_pix.Y}, W={roi_pix.Width}, H={roi_pix.Height}";

                toolData.TemplateName = templateName;
                lblTemplateName.Text = toolData.TemplateName;
                Console.WriteLine("points complete");
            }
        }

        public tool_ROI Set_toolData_to_UI(tool_ROI roi_toolData)
        {
            toolData = roi_toolData;
            nudRotationTolerance.Value = (decimal)toolData.RotationLimit;
            nudMatchThresh.Value = (decimal)toolData.MathcScoreThresh;
            txtName.Text = toolData.Name;
            Set_RoiRegion(toolData.Obj_toolShape, toolData.TemplateName);
        
            return toolData;
        }

        public tool_ROI get_toolData_from_UI()
        {
           // toolData.Name = newName;
            return toolData;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            toolData.Name = txtName.Text;
            txtName.Text = toolData.Name;
            Set_RoiRegion(toolData.Obj_toolShape, toolData.TemplateName);

        }

        private void cmbcolours_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbcolours.SelectedIndex != -1 && cmbcolours.SelectedIndex<userColours.list_userColours.Count())
            //{
            //    lblSelectedColour.Text = userColours.list_userColours[cmbcolours.SelectedIndex].name;
            //}
        }
    }
}
