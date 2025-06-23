using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Measurement_AI.classes
{
    class inspectionData
    {
        int totalInspected;
        int okCnt;
        int ngCnt;

        public  int TotalInspected { get => totalInspected; set => totalInspected = value; }
        public int OkCnt { get => okCnt; set => okCnt = value; }
        public int NgCnt { get => ngCnt; set => ngCnt = value; }

        public void resetCounters()
        {
            totalInspected = 0;
            okCnt = 0;
            ngCnt = 0;
          
        }

        public  float okPercentage()
        { float val = 0;
            
                if (TotalInspected > 0 && okCnt > 0)
                    val = (float)100.0 * ((float)okCnt / (float)TotalInspected);
                else
                    val = 0;

            return val;
        }

        public  float ngPercentage()
        {
            float val = 0;

            if (TotalInspected > 0 && ngCnt > 0)
                val = (float)100.0 * ((float)ngCnt / (float)TotalInspected);
            else
                val = 0;

            return val;
        }
        public void updateDash(bool inspectionResult, Label total, Label ok, Label ng, Label okPc, Label lblresult,bool resetAll)
        {
            if (resetAll)
            {
                resetCounters();
                lblresult.Text = "N/A";   
                lblresult.BackColor = Color.Gray;
            }
            else
            {
               // totalInspected++;
                if (inspectionResult)
                {
                    lblresult.Text = "OK";
                    lblresult.BackColor = Color.ForestGreen;
                    //    okCnt++;

                }
                else
                {
                    lblresult.Text = "NG";
                    lblresult.BackColor = Color.OrangeRed;
                      //  ngCnt++;
                }
            }
            

            total.Text = TotalInspected.ToString();
            ok.Text = OkCnt.ToString();
            ng.Text = NgCnt.ToString();
            okPc.Text = okPercentage().ToString("0.00");


        }


    }
}
