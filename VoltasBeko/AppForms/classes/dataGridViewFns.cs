using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Measurement_AI.classes
{
    class showData
    {
        public String toolname;
        public String paramName;
        public float stdVal;
        public float tolP;
        public float tolN;
        public float measurement;
        public bool result;
        public float deviation;
        public showData()
        {
            this.toolname = "NULL";
        }
        public showData(String toolName, String parName, float std, float tolp, float toln, float measVal, bool rslt)
        {          toolname= toolName;
         this.paramName=parName;
       stdVal=std;
         tolP=tolp;
         tolN=toln;
         measurement=measVal;
        result=rslt;
            if (stdVal != 0)
            {
                deviation = ((measurement - stdVal) / stdVal) * 100;
            }
        }
        public string resultStr()
        {
            if (result)
                return "OK";
            else
                return "NG";
        }
        public float deviationPC()
        {
            if (stdVal != 0)
            {
                return ((  measurement-stdVal) / stdVal) * 100;
            }
            else
            {
                return 100;
            }
        }

        public float deviationMM()
        {
            if (stdVal != 0)
            {
                return measurement - stdVal;
            }
            else
            {
                return 100;
            }
        }
    }
    class dataGridViewFns
    {
        static String[] dg_columns = { "Tool Name", "Parameter", "Std Value", "Tol+", "Tol-", "Measurement", "Result", "Deviation" };
        public static int populateColumns(ref DataGridView dg )
        {
            dg.ColumnCount = dg_columns.Count();
            for (int k = 0; k < dg_columns.Count(); k++)
            {
                dg.Columns[k].HeaderText = dg_columns[k];
            }
            return 1;
        }

        public static int addColumnValues(ref DataGridViewRow d_row, showData d)
        {
            d_row.Cells[0].Value = d.toolname;
            d_row.Cells[1].Value = d.paramName;
            d_row.Cells[2].Value = d.stdVal.ToString("N3");
            d_row.Cells[3].Value = d.tolP.ToString("N3");
            d_row.Cells[4].Value = d.tolN.ToString("N3");
            d_row.Cells[5].Value = d.measurement.ToString("N3");
            d_row.Cells[6].Value = d.resultStr();
            d_row.Cells[7].Value = d.deviationMM().ToString("N3") ;
            if (d.result)
            {
                d_row.DefaultCellStyle.BackColor = Color.LimeGreen;
            }
            else 
            {
            d_row.DefaultCellStyle.BackColor = Color.Red; }
            return 1;
        }
        public static bool updateResultsOnDGrid(ref DataGridView dg, modelDataTools mData )
        {
            //add row

            //--add data to row
            //dg.Rows.Clear();
            bool resultAllMeasurements = true;
            for (int k = 0; k < mData.list_toolCpp.Count(); k++)
            {
              
                for(int j =0; j< mData.list_toolCpp[k].listParameters.Count();j++)
                {
                    measureParam p = mData.list_toolCpp[k].listParameters[j];
                    if (p.enabled)
                    {
                      
                        showData sd = new showData();
                        sd.toolname =mData.list_toolCpp[k].type.ToString()+"  " + mData.list_toolCpp[k].toolName;
                        sd.paramName = p.parName;
                        sd.stdVal = p.stdValue;
                        sd.tolP = p.toleranceH;
                        sd.tolN = p.toleranceL;
                        sd.measurement = p.resultVal;
                        sd.result = p.m_result;
                        //---add row
                        int rowId = dg.Rows.Add();
                        DataGridViewRow row = dg.Rows[rowId];
                        addColumnValues(ref row, sd);
                        //---
                        if (!p.m_result)
                        { resultAllMeasurements = false; }
                    }

                }
            }
            // Add the data
          //  row.Cells["Column1"].Value = "Value1";
            //row.Cells["Column2"].Value = "Value2";

            return resultAllMeasurements;
        }
     
    }
}
