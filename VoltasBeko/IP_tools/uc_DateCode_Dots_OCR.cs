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
    public partial class uc_DateCode_Dots_OCR : UserControl
    {
        public uc_DateCode_Dots_OCR()
        {
            InitializeComponent();
            toolData = new tool_DateCode_DOTS_OCR("temp");
            Set_toolData_to_UI(toolData);
        }
        tool_DateCode_DOTS_OCR toolData;

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
                Rectangle roi_OCR_pix = imageFns_bmp.getRectFromPoints(toolShape.list_points[2].ToPoint(), toolShape.list_points[3].ToPoint());
                toolData.Rect_roi = new cvRect(roi_pix);
                toolData.Rect_OCR =new cvRect( roi_OCR_pix);
                lblRegion.Text = $"X={roi_pix.X}, Y={roi_pix.Y}, W={roi_pix.Width}, H={roi_pix.Height} "+ Environment.NewLine
                   + $"X={roi_OCR_pix.X}, Y={roi_OCR_pix.Y}, W={roi_OCR_pix.Width}, H={roi_OCR_pix.Height} "  ;

            }
        }
        public tool_DateCode_DOTS_OCR Set_toolData_to_UI(tool_DateCode_DOTS_OCR DateCode_Data)
        {
            toolData = DateCode_Data;
            txtName.Text = toolData.Name;

            nudDotDiaMin.Value = (decimal)toolData.MinDotDia_mm;
            nudDotDiaMax.Value = (decimal)toolData.MaxDotDia_mm;
            
           
            Set_RoiRegion(toolData.Obj_toolShape, "");

            return toolData;
        }

        public tool_DateCode_DOTS_OCR get_toolData_from_UI()
        {
            //toolData.Name = newName;
            toolData.MinDotDia_mm = (float)nudDotDiaMin.Value;
            toolData.MaxDotDia_mm = (float)nudDotDiaMax.Value;
            return toolData;
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            toolData.Name = txtName.Text;
            txtName.Text = toolData.Name;

            toolData.MinDotDia_mm = (float)nudDotDiaMin.Value;
            toolData.MaxDotDia_mm = (float)nudDotDiaMax.Value;
            

            Set_RoiRegion(toolData.Obj_toolShape, "");

        }

        private void uc_DateCode_Dots_OCR_Load(object sender, EventArgs e)
        {

        }

        private void btnTestTool_Click(object sender, EventArgs e)
        {
            String mnthYr = toolData.result_minth_Str() + toolData.result_year().ToString();
            String detections = toolData.DotCntLeft.ToString() + "_" + toolData.MidStr + toolData.DotCntRight.ToString();
            lblReadResult.Text = detections + "-" + mnthYr;
        }
    }
}
