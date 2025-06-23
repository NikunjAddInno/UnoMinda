using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoltasBeko.AppForms;

namespace VoltasBeko.CustomControl
{
    public partial class ReworkDataControl : UserControl
    {
        public ReworkDataControl()
        {
            InitializeComponent();
        }

        public bool Result { set 
            {
                if (value)
                {
                    labelResult.Text = "OK";
                    labelResult.BackColor = Color.LimeGreen;
                }
                else
                {
                    labelResult.Text = "NG";
                    labelResult.BackColor = Color.Red;
                }
            }
        }

        LogReportData LogReportData = new LogReportData();

        public ReworkDataControl(LogReportData logReportData, bool markVisible = false)
        {
            InitializeComponent();
            flowLayoutPanel1.Controls.Clear();

            LogReportData = logReportData;
            labelPoseNumber.Text += LogReportData.PoseNumber;
            Result = logReportData.Result;

           
                tryThresholds.IP_tools.resultFrontCam result = JsonConvert.DeserializeObject<tryThresholds.IP_tools.resultFrontCam>(logReportData.Defects);

                if (result.list_defectDetails.Count > 0)
                {
                    for (int i = 0; i < result.list_defectDetails.Count; i++)
                    {
                        if (result.list_defectDetails[i].Result == false)
                        {
                            //listBoxDefects.Items.Add(result.list_defectDetails[i].DefectName);
                            UserControlToolAck userControlToolAck = new UserControlToolAck(result.list_defectDetails[i].DefectName, markVisible);
                            userControlToolAck.Width = flowLayoutPanel1.Width - 4;
                            userControlToolAck.Dock = DockStyle.Top;
                            flowLayoutPanel1.Controls.Add(userControlToolAck);
                        }
                    }
                }
            
            //else
            //{
            //    labelPoseNumber.Text = $"Back Camera Number {LogReportData.PoseNumber}";

            //    string[] defects = GetDistinctWords(logReportData.Defects);
            //    for (int i = 0; i < defects.Count(); i++)
            //    {
            //        listBoxDefects.Items.Add(defects[i]);

            //    }
            //}

            
        }

        public static string[] GetDistinctWords(string input)
        {
            // Split the input string by comma and whitespace
            string[] words = input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return words;
            
        }

        private void ReworkDataControl_Load(object sender, EventArgs e)
        {
            pictureBox1.SetImage(LogReportData.DefectImage);
        }

        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.ParentForm.Name == "PoseEnlargePreview")
            {
                return;
            }
            ReworkDataControl reworkControl = new ReworkDataControl(LogReportData);
            reworkControl.buttonAck.Visible = false;
            PoseEnlargePreview poseEnlargePreview = new PoseEnlargePreview(reworkControl);
            poseEnlargePreview.ShowDialog();
        }

        public bool DefectsMarkedOk { get 
            {
                bool result = true;

                foreach (Control item in flowLayoutPanel1.Controls)
                {
                    if (item is UserControlToolAck)
                    {
                        UserControlToolAck userControlToolAck = (UserControlToolAck)item;
                        result = userControlToolAck.okMarked;
                        if (result == false)
                        {
                            break;
                        }
                    }
                }
                return result;
            } 
        }



        private bool _reworkDone = false;
        public bool ReworkDone { get { return _reworkDone; } }

        public event Action ReworkDoneChanged;   

        private void buttonAck_Click(object sender, EventArgs e)
        {
            if (_reworkDone)
            {
                buttonAck.BackColor = Color.Orange;
                buttonAck.Text = "Ack Pending";
            }
            else
            {
                buttonAck.BackColor = Color.LimeGreen;
                buttonAck.Text = "Done";

            }
            _reworkDone = !_reworkDone;
            ReworkDoneChanged?.Invoke();
        }
    }
}
