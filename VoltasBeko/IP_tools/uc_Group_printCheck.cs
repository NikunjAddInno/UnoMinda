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
    public partial class uc_Group_printCheck : UserControl
    {
        public uc_Group_printCheck()
        {
            InitializeComponent();
            toolData = new tool_Group_printCheck("temp");
            Set_toolData_to_UI(toolData);

        }
        tool_Group_printCheck toolData;//= new tool_Group_printCheck("temp");
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
                lblRegion.Text = $"X={roi_pix.X}, Y={roi_pix.Y}, W={roi_pix.Width}, H={roi_pix.Height}";

                toolData.TemplateName = templateName;
                lblTemplateName.Text = toolData.TemplateName;
            }
        }
        public tool_Group_printCheck Set_toolData_to_UI(tool_Group_printCheck group_toolData)
        {
            toolData = group_toolData;
            txtName.Text = toolData.Name;

            nudHshiftTol.Value = (decimal)toolData.shiftXTol;
            nudVshiftTol.Value = (decimal)toolData.shiftYTol;
            nudWidthTol.Value = (decimal)toolData.WidthTolerance;
            nudHeightTol.Value = (decimal)toolData.HeighTolerance;
            nudAreaTol.Value = (decimal)toolData.AreaTolerance;

            nudDarkLim.Value = (decimal)toolData.burnThreshold;
            nudLightLim.Value = (decimal)toolData.Threshold;
            nudThicknessLim.Value = (decimal)toolData.BoundaryThickness;
            Set_RoiRegion(toolData.Obj_toolShape, toolData.TemplateName);

            return toolData;
        }

        public tool_Group_printCheck get_toolData_from_UI()
        {
            //toolData.Name = newName;
            toolData.shiftXTol =(float) nudHshiftTol.Value;
            toolData.shiftYTol = (float)nudVshiftTol.Value;
            toolData.WidthTolerance= (float) nudWidthTol.Value;
            toolData.HeighTolerance= (float) nudHeightTol.Value ;
            toolData.AreaTolerance=(float)nudAreaTol.Value;
            toolData.burnThreshold= (float)nudDarkLim.Value;
            toolData.Threshold=(float) nudLightLim.Value;
            toolData.BoundaryThickness=(float) nudThicknessLim.Value;

            return toolData;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            toolData.Name = txtName.Text;
            txtName.Text = toolData.Name;
          

            toolData.shiftXTol = (float)nudHshiftTol.Value;
            toolData.shiftYTol = (float)nudVshiftTol.Value;
            toolData.WidthTolerance = (float)nudWidthTol.Value;
            toolData.HeighTolerance = (float)nudHeightTol.Value;
            toolData.AreaTolerance = (float)nudAreaTol.Value;
            toolData.burnThreshold = (float)nudDarkLim.Value;
            toolData.Threshold = (float)nudLightLim.Value;
            toolData.BoundaryThickness = (float)nudThicknessLim.Value;

            Set_RoiRegion(toolData.Obj_toolShape, toolData.TemplateName);

        }

        private void uc_Group_printCheck_Load(object sender, EventArgs e)
        {

        }
    }
}
