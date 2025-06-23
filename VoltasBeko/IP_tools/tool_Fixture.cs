using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryThresholds.IP_tools
{
    public class tool_Fixture : tool
    {
        String template_1_Name;
        String template_2_Name;
        float matchScoreThresh_T1;
        float matchScoreThresh_T2;
        cvRect rectRoi_T1;
        cvRect rectRoi_T2;
        cvRect rectSearcRegion_T1;
        cvRect rectSearcRegion_T2;
        float distanceBw_T1_T2;
        float rotattionLimit_deg;

        public tool_Fixture() 
        { }

        public tool_Fixture(string Name) : base(Name, Mark_ins_shape.Fixture)
        {
            this.Template_1_Name = "";
            this.Template_2_Name = "";
            this.MatchScoreThresh_T1 = 50;
            this.MatchScoreThresh_T2 = 50;
            this. RectRoi_T1= new cvRect(new Rectangle(0, 0, 10, 10));
            this. RectRoi_T2 = new cvRect(new Rectangle(0, 0, 10, 10));
            this. RectSearcRegion_T1 = new cvRect(new Rectangle(0, 0, 10, 10));
            this. RectSearcRegion_T2 = new cvRect(new Rectangle(0, 0, 10, 10));
            this.DistanceBw_T1_T2 = 0;
            this.RotattionLimit_deg = 10;

        }

        public string Template_1_Name { get => template_1_Name;
                                        set
                                        { template_1_Name = value.Replace(" ", string.Empty); }
                                       }
        public string Template_2_Name { get => template_2_Name;
                                        set
                                        { template_2_Name = value.Replace(" ", string.Empty); }
                                       }
        public float MatchScoreThresh_T1 { get => matchScoreThresh_T1; set => matchScoreThresh_T1 = value; }
        public float MatchScoreThresh_T2 { get => matchScoreThresh_T2; set => matchScoreThresh_T2 = value; }
        public cvRect RectRoi_T1 { get => rectRoi_T1; set => rectRoi_T1 = value; }
        public cvRect RectRoi_T2 { get => rectRoi_T2; set => rectRoi_T2 = value; }
        public cvRect RectSearcRegion_T1 { get => rectSearcRegion_T1; set => rectSearcRegion_T1 = value; }
        public cvRect RectSearcRegion_T2 { get => rectSearcRegion_T2; set => rectSearcRegion_T2 = value; }
        public float DistanceBw_T1_T2 { get => distanceBw_T1_T2; set => distanceBw_T1_T2 = value; }
        public float RotattionLimit_deg { get => rotattionLimit_deg; set => rotattionLimit_deg = value; }

        public tool_Fixture(tool_Fixture other) : base(other)
        {
            this.Template_1_Name = String.Copy(other.Template_1_Name); 
            this.Template_2_Name = String.Copy(other.Template_2_Name);
            this.MatchScoreThresh_T1 = other.MatchScoreThresh_T1;
            this.MatchScoreThresh_T2 = other.MatchScoreThresh_T2;
            this.RectRoi_T1 = new cvRect(other.RectRoi_T1);
            this.RectRoi_T2 = new cvRect(other.RectRoi_T2);
            this.RectSearcRegion_T1 = new cvRect(other.RectSearcRegion_T1);
            this.RectSearcRegion_T2 = new cvRect(other.RectSearcRegion_T2);
            this.DistanceBw_T1_T2 = other.DistanceBw_T1_T2;
            this.RotattionLimit_deg = other.RotattionLimit_deg;
        }

    }
}
