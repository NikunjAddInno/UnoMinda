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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace tryThresholds.IP_tools
{
    public partial class uc_GrayPresence : UserControl
    {
        void set_fixtureData(tool_GrayPresence t)
        {
            Console.WriteLine($"tool loading fixture :T::{t.FixtureType}  Mode::{t.Fixture_mode} REf ID::{t.FixtureToolReference}");
            lblFixtureType.Text = ((p_FixtureType)t.FixtureType).ToString();
            if (t.FixtureType < 2)
            {
                lblFixtureMode.Visible = false;
                lblRefTool.Visible = false;
                lblD_DRefTool.Visible = false;
                lblD_Fmode.Visible = false;
            }
            else
            {
                lblFixtureMode.Visible = true;
                lblRefTool.Visible = true;
                lblD_DRefTool.Visible = true;
                lblD_Fmode.Visible = true;
                lblFixtureMode.Text = ((p_FixtureMode)t.Fixture_mode).ToString();
                lblRefTool.Text = t.FixtureToolReference.ToString();
            }
        }
        public uc_GrayPresence()
        {
            InitializeComponent();
            toolData = new tool_GrayPresence("GrayPresence");
            if (userColours.list_userColours.Count > 0)
            {
                foreach (colourRange_hsv c in userColours.list_userColours)
                {
                    cmbcolours.Items.Add(c.name + "_" + c.remark);
                }

            }
            if (cmbcolours.SelectedIndex == -1)
            {
                if (cmbcolours.Items.Count > 0)
                {
                    cmbcolours.SelectedIndex = 0;
                }
            }
            Set_toolData_to_UI(toolData);
        }
        tool_GrayPresence toolData; //= new tool_ROI("temp");

        void setModeTools(int modeTempl)
        {
            cmbcolours.Visible = false;
            trkThreshold.Visible = false;
            lblthreshold.Visible = false;
            chkThtype.Visible = false;

            if (modeTempl == 2)
            {
                cmbcolours.Visible = true;
            }
            else if (modeTempl == 1)
            {
                trkThreshold.Visible = true;
                lblthreshold.Visible = true;
                chkThtype.Visible = true;
            }

        }
        private void nudMatchPercent_ValueChanged(object sender, EventArgs e)
        {
           toolData.MatchPercent=(float)nudMatchPercent.Value;
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

        private void trkThreshold_Scroll(object sender, EventArgs e)
        {
            toolData.Threshold = trkThreshold.Value;
            lblthreshold.Text = trkThreshold.Value.ToString();
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
            }
        }
        public tool_GrayPresence Set_toolData_to_UI(tool_GrayPresence boundaryGap_tool)
        {
            toolData = boundaryGap_tool;
            nudMatchPercent.Value = (decimal)toolData.MatchPercent;
            txtName.Text = toolData.Name;

            trkThreshold.Value = toolData.Threshold;
            lblthreshold.Text = trkThreshold.Value.ToString();

            if (toolData.ThresholdType == 1)
            { chkThtype.Checked = true; }
            else
            { chkThtype.Checked = false; }

            if (cmbcolours.Items.Count > 0)
            {
                int index = userColours.getIndexFromId((int)toolData.ColourId);
                if (index != -1 && index < cmbcolours.Items.Count)
                {
                    cmbcolours.SelectedIndex = index;
                }

                Console.WriteLine($"index of colout id {index} id {toolData.Id}");
            }
            else
            {
                Console.WriteLine("items not present in cmb at init");
            }
    
            if (toolData.Mode == 2)
            {
               rdbColour.Checked = true;
            }
            else
            { rdbThresh.Checked = true; }
            
            Set_RoiRegion(toolData.Obj_toolShape, "");
            set_fixtureData(toolData);
            return toolData;
        }
        public tool_GrayPresence get_toolData_from_UI()
        {
            // toolData.Name = newName;
            return toolData;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            toolData.Name = txtName.Text;
            txtName.Text = toolData.Name;
            if (cmbcolours.SelectedIndex >= 0)
            {
                toolData.ColourId = userColours.list_userColours[cmbcolours.SelectedIndex].id;
            }
  
            Set_RoiRegion(toolData.Obj_toolShape, "");
        }

        private void rdbColour_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbColour.Checked)
            {
                toolData.Mode = 2;
                setModeTools(2);
            }
        }

        private void rdbThresh_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbThresh.Checked)
            {
                toolData.Mode = 1;
                setModeTools(1);
            }
        }
    }
}
