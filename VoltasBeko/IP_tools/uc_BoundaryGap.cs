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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace tryThresholds.IP_tools
{
    public partial class uc_BoundaryGap : UserControl
    {
        public uc_BoundaryGap()
        {
            InitializeComponent();
            toolData = new tool_BoundaryGap("BoundaryGap");
            Set_toolData_to_UI(toolData);
        }
        tool_BoundaryGap toolData; //= new tool_ROI("temp");

        private void nudGapLeft_ValueChanged(object sender, EventArgs e)
        {
            toolData.GapLeft=(float)nudGapLeft.Value;
        }

        private void nudGapRight_ValueChanged(object sender, EventArgs e)
        {
            toolData.GapRight=(float)nudGapRight.Value; 
        }

        private void nudGapTop_ValueChanged(object sender, EventArgs e)
        {
            toolData.GapTop=(float)nudGapTop.Value; 
        }

        private void nudGapBottom_ValueChanged(object sender, EventArgs e)
        {
            toolData.GapBottom=(float)nudGapBottom.Value;   
        }
        public void Set_RoiRegion(DrawPbMmarkIns toolShape, String templateName)
        {
            if (!toolShape.pointsAvailable)
            {
                lblRegion.Text = "Not Selected";
                return;
            }
            else
            {
                toolData.Obj_toolShape = toolShape;
                Rectangle roi_pix = imageFns_bmp.getRectFromPoints(toolShape.list_points[0].ToPoint(), toolShape.list_points[1].ToPoint());
                toolData.Rect_roi = new cvRect(roi_pix);

                Rectangle roi_pix_outer = imageFns_bmp.getRectFromPoints(toolShape.list_points[2].ToPoint(), toolShape.list_points[3].ToPoint());
                toolData.Rect_roi_outer = new cvRect(roi_pix_outer);

                lblRegion.Text = $"X={roi_pix.X}, Y={roi_pix.Y}, W={roi_pix.Width}, H={roi_pix.Height}";
            }
        }
        public tool_BoundaryGap Set_toolData_to_UI(tool_BoundaryGap boundaryGap_tool)
        {
            toolData = boundaryGap_tool;
            nudGapLeft.Value = (decimal)toolData.GapLeft;
            nudGapRight.Value = (decimal)toolData.GapRight;
            nudGapTop.Value = (decimal)toolData.GapTop;
            nudGapBottom.Value = (decimal)toolData.GapBottom;
            txtName.Text = toolData.Name;
            

            trkThreshold.Value = toolData.Threshold;
            lblthreshold.Text=trkThreshold.Value.ToString();

            trkOuterThreshold.Value = toolData.Threshold_outer;
            nudFillPercent.Value = toolData.FillPercent_outer;
            lblThreshOuter.Text = trkOuterThreshold.Value.ToString();
            chkEnabledOuter.Checked = toolData.Enabled_outer;
            chkEnableMeasure.Checked = toolData.Enabled_measure;

            if (toolData.ThresholdType == 1)
            { chkThtype.Checked = true; }
            else
            { chkThtype.Checked = false; }

            Set_RoiRegion(toolData.Obj_toolShape,"");

            return toolData;
        }
        private void chkThtype_CheckedChanged(object sender, EventArgs e)
        {
            if (chkThtype.Checked)
            {
                toolData.ThresholdType = 1;
            }
            else
            {
                toolData.ThresholdType = 0;
            }
        }


        public tool_BoundaryGap get_toolData_from_UI()
        {
            // toolData.Name = newName;
            return toolData;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            toolData.Name = txtName.Text;
            txtName.Text = toolData.Name;
            toolData.Enabled_measure = chkEnableMeasure.Checked;
            toolData.Enabled_outer = chkEnabledOuter.Checked;
            Set_RoiRegion(toolData.Obj_toolShape, "");
        }

        private void trkThreshold_Scroll(object sender, EventArgs e)
        {
            toolData.Threshold= trkThreshold.Value;
            lblthreshold.Text=trkThreshold.Value.ToString();
        }

        private void trkOuterThreshold_Scroll(object sender, EventArgs e)
        {
            toolData.Threshold_outer = trkOuterThreshold.Value;
            lblThreshOuter.Text = trkOuterThreshold.Value.ToString();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            toolData.FillPercent_outer = (int)nudFillPercent.Value;
        }

        private void chkEnableMeasure_CheckedChanged(object sender, EventArgs e)
        {
            toolData.Enabled_measure = chkEnableMeasure.Checked;
        }

        private void chkEnabledOuter_CheckedChanged(object sender, EventArgs e)
        {
            toolData.Enabled_outer = chkEnabledOuter.Checked;
        }
    }
}
