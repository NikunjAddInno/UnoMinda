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
    public partial class uc_CROI : UserControl
    {
        void set_fixtureData(tool_CROI t)
        {
            Console.WriteLine($"tool loading fixture :T::{t.FixtureType}  Mode::{t.Fixture_mode} REf ID::{t.FixtureToolReference}");
           lblFixtureType .Text = ((p_FixtureType)t.FixtureType).ToString();
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
        void setModeTools(int modeTempl)
        { 
            cmbcolours.Visible = false;
            trackBar1.Visible = false;
            lblSelectedColour.Visible = false;
            checkBox1.Visible = false;

            if (modeTempl == 1)
            {
                cmbcolours.Visible = true;
            }
            else if (modeTempl == 2)
            { 
                trackBar1.Visible= true;
                lblSelectedColour.Visible= true;
                checkBox1.Visible = true;
            }
            
        }
        public uc_CROI()
        {
            InitializeComponent();
            toolData = new tool_CROI("ComplexROI");
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
        tool_CROI toolData; //= new tool_ROI("temp");
        private void rdbColour_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbColour.Checked)
            {
                toolData.Mode = 1;
                setModeTools(1);
            }
        }

        private void uc_CROI_Load(object sender, EventArgs e)
        {
            //if (userColours.list_userColours.Count > 0)
            //{
            //    foreach (colourRange_hsv c in userColours.list_userColours)
            //    {
            //        cmbcolours.Items.Add(c.name + "_" + c.remark);
            //    }

            //}
            //if (cmbcolours.SelectedIndex == -1)
            //{
            //    if (cmbcolours.Items.Count > 0)
            //    {
            //        cmbcolours.SelectedIndex = 0;
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

        private void nudHshiftTol_ValueChanged(object sender, EventArgs e)
        {
            toolData.ShiftTolerance.X=(float)nudHshiftTol.Value;
        }

        private void nudVshiftTol_ValueChanged(object sender, EventArgs e)
        {
            toolData.ShiftTolerance.Y=(float)nudVshiftTol.Value;
        }
        public void Set_RoiRegion(DrawPbMmarkIns toolShape, String templateName)
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
                toolData.Rect_roi = new cvRect(roi_pix);
                Rectangle searchRgn_pix = imageFns_bmp.getRectFromPoints(toolShape.list_points[2].ToPoint(), toolShape.list_points[3].ToPoint());
                toolData.SearchRegion = new cvRect(searchRgn_pix);
                lblRegion.Text = $"X={roi_pix.X}, Y={roi_pix.Y}, W={roi_pix.Width}, H={roi_pix.Height}";
                lblRegion.Text = lblRegion.Text+ $"  X={searchRgn_pix.X}, Y={searchRgn_pix.Y}, W={searchRgn_pix.Width}, H={searchRgn_pix.Height}";

                toolData.TemplateName = templateName;
                lblTemplateName.Text = toolData.TemplateName;
                Console.WriteLine("points complete");
            }
        }
        public tool_CROI Set_toolData_to_UI(tool_CROI roi_toolData)
        {
            toolData = roi_toolData;
            nudRotationTolerance.Value = (decimal)toolData.RotationLimit;
            nudMatchThresh.Value = (decimal)toolData.MathcScoreThresh;
            txtName.Text = toolData.Name;
            lblTemplateName.Text = toolData.TemplateName;
            nudHshiftTol.Value = (decimal)toolData.ShiftTolerance.X;
            nudVshiftTol.Value = (decimal)toolData.ShiftTolerance.Y;
            nudHshiftTol_neg.Value = (decimal)toolData.ShiftTolerance_neg.X;
            nudVshiftTol_neg.Value = (decimal)toolData.ShiftTolerance_neg.Y;
            trackBar1.Value = toolData.Threshold;
            lblSelectedColour.Text = trackBar1.Value.ToString();
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
            if (toolData.ThresholdType == 1)
            { checkBox1.Checked = true; }
            else
            { checkBox1.Checked = false; }

            if (toolData.Mode == 1)
            {
                rdbColour.Checked = true;
            }
            else if (toolData.Mode == 2)
            { rdbThresh.Checked = true; }
            else if (toolData.Mode == 3)
            { rdbGray.Checked = true; }
            else
            { rdbGray.Checked = true; }
            Set_RoiRegion(toolData.Obj_toolShape, toolData.TemplateName);


            set_fixtureData(toolData);

            return toolData;
        }

        public tool_CROI get_toolData_from_UI()
        {
            // toolData.Name = newName;
            return toolData;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            toolData.Name = txtName.Text;
            txtName.Text = toolData.Name;
            toolData.ThresholdType=checkBox1.Checked ? 1 : 0;
            if (cmbcolours.SelectedIndex >= 0)
            {
                toolData.ColourId = userColours.list_userColours[cmbcolours.SelectedIndex].id;
            }
            Set_RoiRegion(toolData.Obj_toolShape, toolData.TemplateName);
        }

        private void cmbcolours_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbcolours.SelectedIndex != -1 && cmbcolours.SelectedIndex < userColours.list_userColours.Count())
            //{
            //    toolData.ColourId = userColours.list_userColours[cmbcolours.SelectedIndex].id;
            //    Console.WriteLine($"colorID :: {toolData.ColourId}");
            //}
            //else
            //{
            //    Console.WriteLine($"Cmb box condition not met sel index ={cmbcolours.SelectedIndex} ,, list cnt ={userColours.list_userColours.Count()}");
            //}
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            toolData.Threshold = trackBar1.Value;
            lblSelectedColour.Text=trackBar1.Value.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                toolData.ThresholdType = 0;
            }
            else
            { toolData.ThresholdType = 1; };
        }

        private void rdbThresh_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbThresh.Checked)
            {
                toolData.Mode = 2;
                setModeTools(2);
            }
        }

        private void rdbGray_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbGray.Checked)
            {
                toolData.Mode = 3;
                setModeTools(3);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void nudHshiftTol_neg_ValueChanged(object sender, EventArgs e)
        {
            toolData.ShiftTolerance_neg.X = (float)nudHshiftTol_neg.Value;
        }

        private void nudVshiftTol_neg_ValueChanged(object sender, EventArgs e)
        {
            toolData.ShiftTolerance_neg.Y = (float)nudVshiftTol_neg.Value;
        }

        public int updateLocationReference(p_FixtureType type, p_FixtureMode mode, int referenceID)
        {
           
            toolData.FixtureType = (int)type;
            toolData.Fixture_mode = (int)mode;
            toolData.FixtureToolReference = referenceID; 
            return toolData.FixtureToolReference;
        }

        private void btnFixtureEdit_Click(object sender, EventArgs e)
        {

        }
    }
}
