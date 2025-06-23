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
    public partial class uc_QR : UserControl
    {
        public uc_QR()
        {
            InitializeComponent();
            toolData = new tool_QR_code("temp");
            Set_toolData_to_UI(toolData);
        }

        tool_QR_code toolData;//= new tool_QR_code("temp");
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtRead.Text != "")
            {
                txtExpected.Text = txtRead.Text;
                toolData.ExpectedString = txtExpected.Text;
            }
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
                lblRegion.Text = $"X={roi_pix.X}, Y={roi_pix.Y}, W={roi_pix.Width}, H={roi_pix.Height}";

               // toolData.TemplateName = templateName;
                //lblTemplateName.Text = toolData.TemplateName;
            }
        }


        public tool_QR_code Set_toolData_to_UI(tool_QR_code QR_toolData)
        {
            toolData = QR_toolData;
            txtExpected.Text = toolData.ExpectedString;
            txtName.Text = toolData.Name;
            Set_RoiRegion(toolData.Obj_toolShape, "");

            return toolData;
        }

        public tool_QR_code get_toolData_from_UI()
        {
            //toolData.Name = newName;
            return toolData;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            toolData.Name = txtName.Text;
            txtName.Text = toolData.Name;
            toolData.ExpectedString = txtExpected.Text;
            Set_RoiRegion(toolData.Obj_toolShape, "");


        }

        private void uc_QR_Load(object sender, EventArgs e)
        {

        }
    }
}
