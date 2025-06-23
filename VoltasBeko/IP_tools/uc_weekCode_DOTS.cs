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
    public partial class uc_weekCode_DOTS : UserControl
    {
        public uc_weekCode_DOTS()
        {
            InitializeComponent();
            toolData = new tool_WeekCode_DOTS("temp");
            Set_toolData_to_UI(toolData);
        }
        tool_WeekCode_DOTS toolData;

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
              
                toolData.Rect_roi =new cvRect( roi_pix);

                lblRegion.Text = $"X={roi_pix.X}, Y={roi_pix.Y}, W={roi_pix.Width}, H={roi_pix.Height} ";


            }
        }
        public tool_WeekCode_DOTS Set_toolData_to_UI(tool_WeekCode_DOTS WeekCode_Data)
        {
            toolData = WeekCode_Data;
            txtName.Text = toolData.Name;

            nudDotDiaMin.Value = (decimal)toolData.MinDotDia_mm;
            nudDotDiaMax.Value = (decimal)toolData.MaxDotDia_mm;


            Set_RoiRegion(toolData.Obj_toolShape, "");

            return toolData;
        }
        public tool_WeekCode_DOTS get_toolData_from_UI()
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

        private void btnTestTool_Click(object sender, EventArgs e)
        {
            lblReadResult.Text ="Week No:" +toolData.result_week_int().ToString() ;
        }
    }
}
