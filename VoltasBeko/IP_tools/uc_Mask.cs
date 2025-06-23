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
    public partial class uc_Mask : UserControl
    {
        public uc_Mask()
        {
            InitializeComponent();
            toolData = new tool_Mask ("temp");
            Set_toolData_to_UI(toolData);
        }
        tool_Mask toolData;
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

                lblRegion.Text = $"X={roi_pix.X}, Y={roi_pix.Y}, W={roi_pix.Width}, H={roi_pix.Height} ";


            }
        }
        public tool_Mask Set_toolData_to_UI(tool_Mask Mask_Data)
        {
            toolData = Mask_Data;
            txtName.Text = toolData.Name;
            Set_RoiRegion(toolData.Obj_toolShape, "");

            return toolData;
        }
        public tool_Mask get_toolData_from_UI()
        {
            //toolData.Name = newName;
            return toolData;
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            toolData.Name = txtName.Text;
            txtName.Text = toolData.Name;

            Set_RoiRegion(toolData.Obj_toolShape, "");
        }
    }
}
