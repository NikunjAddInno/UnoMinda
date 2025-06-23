using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using static System.Windows.Forms.AxHost;

namespace VoltasBeko.AppForms
{
    public partial class ReportPage : Form
    {
        public ReportPage()
        {
            InitializeComponent();
            //LoadFolderNamesToComboBox($"{AppData.ProjectDirectory}/Models", comboBoxModel);

            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            startTimePicker.Format = DateTimePickerFormat.Time;
            startTimePicker.ShowUpDown = true;
            endTimePicker.Format = DateTimePickerFormat.Time;
            endTimePicker.ShowUpDown = true;
        }


        public static void LoadFolderNamesToComboBox(string folderPath, ComboBox comboBox)
        {
            try
            {
                // Clear the ComboBox to start with a clean slate
                comboBox.Items.Clear();

                // Check if the folder path exists
                if (Directory.Exists(folderPath))
                {
                    // Get all subdirectories (folder names) in the specified path
                    string[] folderNames = Directory.GetDirectories(folderPath);

                    // Add each folder name to the ComboBox
                    foreach (string folderName in folderNames)
                    {
                        // Get the folder name from the full path
                        string folder = Path.GetFileName(folderName);

                        comboBox.Items.Add(folder);
                    }
                    comboBox.SelectedIndex = 0;
                }
                else
                {
                    // Handle the case where the folder path does not exist
                    MessageBox.Show("Folder does not exist.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the process
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void LoadData()
        {
            using (NpgsqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                string modelQuery = "";
                //if (checkBoxModelFilter.Checked)
                //{
                //    modelQuery = $" where modelname = '{comboBoxModel.SelectedItem}'";
                //}
                string startTime = startTimePicker.Value.ToString("HH:mm:ss");
                string endTime = endTimePicker.Value.ToString("HH:mm:ss");
                string alterTime = checkBoxTimeFilter.Checked ? $" and _time between '{startTime}' and '{endTime}'" : "";
                string alterDate = $@"_date between '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' and '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}'";
                string filter = "";

                if (checkBoxModelCode.Checked)
                {
                    filter += $" and modelcode = '{textBoxModelCode.Text}'";
                    alterDate = $@"_date between '2000-01-01' and '{DateTime.Now.ToString("yyyy-MM-dd")}'";

                }
                if (checkBoxModelName.Checked)
                {
                    filter += $" and modelname = '{textBoxModelName.Text}'";
                    alterDate = $@"_date between '2000-01-01' and '{DateTime.Now.ToString("yyyy-MM-dd")}'";
                }


                string selectQuery = $@"SELECT _date as ""Date"", _time as ""Time"", modelname as ""Model Name"", modelcode as ""Model Code"", inspection_count as ""Inspection Count"", case when result = TRUE then 'Ok' else 'Ng' end as ""Result"" FROM public.final_logreport
                                       WHERE {alterDate} {filter} {alterTime}
                                        {modelQuery}";

                using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(selectQuery, connection))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                labelTotalParts.Text = dataGridView1.Rows.Count.ToString();
            }


        }

        private void LoadReworkData(string code)
        {
            using (NpgsqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                string modelQuery = "";
                modelQuery = $" where modelcode = '{code}'";

                string selectQuery = $@"SELECT _date as ""Date"", _time as ""Time"", modelname as ""Model Name"", modelcode as ""Model Code"" FROM public.reworkstation {modelQuery}";

                using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(selectQuery, connection))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView2.DataSource = dataTable;
                }
                //labelTotalPlatesCount.Text = dataGridView1.Rows.Count.ToString();
            }
        }






        //private void LoadFrontCamData(string code)
        //{
        //    using (NpgsqlConnection connection = Database.GetConnection())
        //    {
        //        connection.Open();
        //        string modelQuery = "";
        //        modelQuery = $" where modelcode = '{code}'";

        //        string selectQuery = $@"SELECT _date as ""Date"", _time as ""Time"", modelname as ""Model Name"",
        //                            modelcode as ""Model Code"", posenumber as ""Pose Number"", inspection_count as ""Inspection Count"",
        //                            case when result = TRUE then 'Ok' else 'Ng' end as ""Result"" FROM public.logreport {modelQuery} order by inspection_count asc";

        //        using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(selectQuery, connection))
        //        {
        //            DataTable dataTable = new DataTable();
        //            dataAdapter.Fill(dataTable);
        //            dataGridView4.DataSource = dataTable;
        //        }
        //        //labelTotalPlatesCount.Text = dataGridView1.Rows.Count.ToString();
        //    }
        //}

        private void LoadFrontCamData(string code)
        {
            using (NpgsqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                string modelQuery = "";
                modelQuery = $" where modelcode = '{code}'";

                string selectQuery = $@"
            SELECT 
                _date as ""Date"", 
                _time as ""Time"", 
                modelname as ""Model Name"",
                modelcode as ""Model Code"", 
                posenumber as ""Pose Number"", 
                CASE 
                    WHEN result = TRUE THEN 'Ok' 
                    ELSE 'Ng' 
                END as ""Result"",
                defects::json->'list_defectDetails' as ""Defect Details""
            FROM 
                public.logreport 
            {modelQuery}";

                using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(selectQuery, connection))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView4.DataSource = dataTable;
                }
                //labelTotalPlatesCount.Text = dataGridView1.Rows.Count.ToString();
            }
        }


        private void LoadBackCamData(string code)
        {
            using (NpgsqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                string modelQuery = "";
                modelQuery = $" where modelcode = '{code}'";

                string selectQuery = $@"SELECT _date as ""Date"", _time as ""Time"", modelname as ""Model Name"",
                                    modelcode as ""Model Code"", defects as ""Defects"", camnumber as ""Cam Number"",
                                    case when result = TRUE then 'Ok' else 'Ng' end as ""Result"", inspection_count as ""Inspection Count"" FROM public.logreport_backcamera {modelQuery} order by inspection_count asc";

                using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(selectQuery, connection))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView3.DataSource = dataTable;
                }
                //labelTotalPlatesCount.Text = dataGridView1.Rows.Count.ToString();
            }
        }


        private void buttonShowData_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        

        private void buttonSaveReport_Click(object sender, EventArgs e)
        {
            //ExportToExcel(dataGridView1);
            CsvExporter.ExportToCsv(dataGridView1);
            //Database.InsertRandomData(30);
        }


        public void ExportToExcel(DataGridView dataGridView)
        {
            try
            {
                // Open SaveFileDialog
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx|All Files|*.*";
                saveFileDialog.Title = "Save Excel File";
                saveFileDialog.ShowDialog();

                // Check if the user clicked OK
                if (saveFileDialog.FileName != "")
                {
                    // Creating Excel Application
                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = false;

                    // Creating new Workbook
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Add(Type.Missing);
                    Excel.Worksheet excelWorksheet = excelWorkbook.ActiveSheet;

                    // Setting Column Widths
                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        excelWorksheet.Columns[i + 1].ColumnWidth = dataGridView.Columns[i].Width / 7; // dividing by 7 to convert from pixels to Excel width
                        excelWorksheet.Cells[1, i + 1] = dataGridView.Columns[i].HeaderText; // Adding column headers
                                                                                             //switch (i)
                                                                                             //{
                                                                                             //    case 2:
                                                                                             //    case 3:
                                                                                             //    case 6:
                                                                                             //        excelWorksheet.Columns[i + 1].ColumnWidth = (dataGridView.Columns[i].Width / 7) + 40; // dividing by 7 to convert from pixels to Excel width
                                                                                             //        break;
                                                                                             //    default:
                                                                                             //        break;
                                                                                             //}
                    }



                    // Adding DataGridView data to Excel
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView.Columns.Count; j++)
                        {
                            excelWorksheet.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value.ToString();
                            excelWorksheet.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        }
                    }

                    // Saving the Excel file
                    excelWorkbook.SaveAs(saveFileDialog.FileName);

                    // Closing the Workbook and Excel Application
                    excelWorkbook.Close();
                    excelApp.Quit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string modelcode = Convert.ToString(selectedRow.Cells["Model Code"].Value);
                try
                {
                    //LoadReworkData(modelcode);
                    //LoadBackCamData(modelcode);
                    LoadFrontCamData(modelcode);
                }
                catch (Exception ex)
                {
                    ConsoleExtension.WriteWithColor(ex.Message, ConsoleColor.Red);
                }
            }
        }

        private void ReportPage_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CsvExporter.ExportToCsv(dataGridView2);

        }

        private void buttonShowImages_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string modelcode = Convert.ToString(selectedRow.Cells["Model Code"].Value);
               // int inspectionCount = Convert.ToInt32(selectedRow.Cells["Inspection Count"].Value);
                ReportDetailsForm reportDetailsForm = new ReportDetailsForm(modelcode, false);
                reportDetailsForm.ShowDialog();
            }
        }

        private void buttonbuttonSaveReport2_Click(object sender, EventArgs e)
        {
            ExportToExcel(dataGridView2);

        }

        private void buttonbuttonSaveReportBackCam_Click(object sender, EventArgs e)
        {
            ExportToExcel(dataGridView3);

        }

        private void buttonSaveReportFrontCam_Click(object sender, EventArgs e)
        {
            //ExportToExcel(dataGridView4);
            CsvExporter.ExportToCsv(dataGridView4);

        }
    }
}
