using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoltasBeko.CustomControl;

namespace VoltasBeko.AppForms
{
    public partial class ReportDetailsForm : Form
    {

        string modelCode = "";
        bool markVisible = false;
        public ReportDetailsForm(string modelCode, bool markVisible)
        {
            InitializeComponent();
            this.modelCode = modelCode;
            this.markVisible = markVisible;
        }

        public void LoadModelData()
        {
            flowLayoutPanel1.Invoke(new Action(() =>
            {
                flowLayoutPanel1.Controls.Clear();
                List<LogReportData> logReports = Database.GetPartDataByModelCode(modelCode);
                //MessageBox.Show(logReports.Count.ToString());

                panel1.Invoke(new Action(() =>
                {
                    labelCode.Text = modelCode;
                    if (logReports.Count > 0)
                    {
                        labelModelName.Text = logReports[0].ModelName;
                    }
                }));
                foreach (LogReportData logReport in logReports)
                {
                    if (logReport.Result == false)
                    {
                        ReworkDataControl reworkDataControl = new ReworkDataControl(logReport, markVisible);
                        reworkDataControl.buttonAck.Visible = false;
                        flowLayoutPanel1.Controls.Add(reworkDataControl);
                    }

                }
               
            }));
        }
        public bool DefectsMarkedOk { get 
            {
                bool result = false;
                foreach (Control item in flowLayoutPanel1.Controls)
                {
                    if (item is ReworkDataControl)
                    {
                        ReworkDataControl reworkDataControl = (ReworkDataControl)item;
                        result = reworkDataControl.DefectsMarkedOk;
                        if (result == false)
                        {
                            break;
                        }
                    }
                }
                return result;
            }
        }
        //private Bitmap ByteArrayToBitmap(byte[] byteArray)
        //{
        //    if (byteArray == null || byteArray == new byte[0] || byteArray.Length <= 0)
        //    {
        //        Bitmap bitmap = new Bitmap(200, 200);
        //        return bitmap;
        //    }
        //    using (MemoryStream stream = new MemoryStream(byteArray))
        //    {
        //        return new Bitmap(stream);
        //    }
        //}
        //private List<LogReportData> GetPartDataByModelCode(string modelCode)
        //{
        //    List<LogReportData> dataList = new List<LogReportData>();

        //    using (NpgsqlConnection conn = Database.GetConnection())
        //    {
                
        //        conn.Open();

        //        using (NpgsqlCommand cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = conn;

        //            cmd.CommandText = $@"
        //            SELECT _date, _time, modelname, modelcode, defects, result, defectimage, posenumber
        //            FROM logreport
        //            WHERE modelcode = @modelCode";

        //            cmd.Parameters.AddWithValue("@modelCode", modelCode);

        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    var data = new LogReportData
        //                    {
        //                        Date = reader.GetDateTime(0),
        //                        Time = Convert.ToDateTime(reader.GetFieldValue<TimeSpan>(1).ToString()),
        //                        ModelName = reader.GetString(2),
        //                        ModelCode = reader.GetString(3),
        //                        Defects = reader.GetString(4),
        //                        Result = reader.GetBoolean(5),
        //                        DefectImage = ByteArrayToBitmap(reader.GetFieldValue<byte[]>(6)),
        //                        PoseNumber = reader.GetInt32(7)
        //                    };
        //                    dataList.Add(data);
        //                }
        //            }
        //        }
        //        conn.Close();
        //    }

        //    return dataList;
        //}
        private void ReportDetailsForm_Load(object sender, EventArgs e)
        {
            LoadModelData();
            AppData.DefectsMarkedOk = false;

        }

        private void ReportDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppData.DefectsMarkedOk = DefectsMarkedOk;

            if (flowLayoutPanel1.Controls.Count == 0)
            {
                AppData.DefectsMarkedOk = true;

            }
            if (AppData.DefectsMarkedOk == false)
            {
                DialogResult dialog = MessageBox.Show("All defects are not marked. Do you want to close?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
