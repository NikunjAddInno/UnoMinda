using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tryThresholds.IP_tools;

namespace tryThresholds
{
    public partial class frmCoordReference : Form
    {
        public frmCoordReference(tool t_in, List<String> listReftools_in, string refToolName)
        {
            InitializeComponent();
            arr_rdbType = new RadioButton[] { rdbFixed, rdbFixtureTool, rdb_otherTool, rdbFixture_n_othertool };
            arr_rdbMode = new RadioButton[] { rdbShiftnRot, rdbShiftOnly, rdbRotOnly };
            for (int k = 0; k < arr_rdbType.Length; k++)
            {
                arr_rdbType[k].CheckedChanged += type_CheckedChanged;
            }
            refToolNameTemp = refToolName;
            
            t = t_in;
            listReftools = listReftools_in;

            initUI();
        }

        public tool t;
        public int formResp=0;
        List<String> listReftools = new List<String>();
        public string refToolNameTemp="";
        RadioButton[] arr_rdbType;// = new RadioButton[] { };
        RadioButton[] arr_rdbMode;// = new RadioButton[] { };
        public int initUI()
        {
            arr_rdbType[t.FixtureType].Checked = true;
            listBoxAppliedTools.Items.Clear();
            for (int k = 0; k < listReftools.Count; k++)
            {
                listBoxAppliedTools.Items.Add(listReftools[k]);
            }
            if (t.FixtureType > 1)
            {
                pnlMode.Visible = true;
                pnlReftool.Visible = true;
                arr_rdbMode[t.Fixture_mode].Checked = true;
                lblCurrentRefTool.Text = refToolNameTemp;
               
            }
            else
            {
                pnlMode.Visible = false;
                pnlReftool.Visible = false;
                lblCurrentRefTool.Text = "";
            }
            return 1;
        }

        private int updateFixtureConfig()
        {
            int fixtTypeTemp = 0;
            int fixmodeTemp = 0;
            int reftooIDtemp = 0;
            for (int k = 0; k < arr_rdbType.Length; k++)
            {
                if (arr_rdbType[k].Checked)
                {
                    fixtTypeTemp = k;
                    break;
                }
            }
            if (fixtTypeTemp > 1)
            {
                for (int k = 0; k < arr_rdbMode.Length; k++)
                {
                    if (arr_rdbMode[k].Checked)
                    {
                        fixmodeTemp = k;
                        break;
                    }
                }
                if (listBoxAppliedTools.SelectedIndex != -1)
                {
                    refToolNameTemp = listBoxAppliedTools.SelectedItem.ToString();
                    reftooIDtemp=tool_helper.getToolID_Fromstring(listBoxAppliedTools.SelectedItem.ToString());

                }
                else
                {
                    MessageBox.Show("No reference tool selected. Either select reference tool or change fixture type.");
                    return 0;
                }

            }
            else
            {
                refToolNameTemp = "";
            }

            t.FixtureType = fixtTypeTemp;
            t.Fixture_mode = fixmodeTemp;
            t.FixtureToolReference = reftooIDtemp;
            formResp = 1;
            return 1;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateFixtureConfig();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdbShiftOnly_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void type_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = (RadioButton)sender;
            //rd.Tag.ToString()
            if (int.Parse(rd.Tag.ToString()) > 1)
            {
                pnlMode.Visible = true;
                pnlReftool.Visible = true;

                listBoxAppliedTools.Items.Clear();
      
                    pnlMode.Visible = true;
                    pnlReftool.Visible = true;
                    arr_rdbMode[t.Fixture_mode].Checked = true;
                    lblCurrentRefTool.Text = refToolNameTemp;
                    for (int k = 0; k < listReftools.Count; k++)
                    {
                        listBoxAppliedTools.Items.Add(listReftools[k]);
                    }
                
            }
            else
            {
                pnlMode.Visible = false;
                pnlReftool.Visible = false;
            }
        }
    }
}
