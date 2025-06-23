using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryThresholds.IP_tools
{
    public class tool_WeekCode_DOTS:tool
    {

        float minDotDia_mm;
        float maxDotDia_mm;
        cvRect rect_roi;

        //outputs
        int dotCnt;
        public tool_WeekCode_DOTS()
        { }
        public tool_WeekCode_DOTS(String Name) : base(Name, Mark_ins_shape.WeekCode)
        {
            this.MinDotDia_mm = 0.1f;
            this.MaxDotDia_mm = 3.0f;
            this.Rect_roi =new cvRect( new Rectangle(0, 0, 10, 10));
            this.dotCnt = 0;
        }

        public tool_WeekCode_DOTS(tool_WeekCode_DOTS other) : base(other)
        {
            this.MinDotDia_mm = other.minDotDia_mm;
            this.MaxDotDia_mm = other.maxDotDia_mm;
            this.rect_roi = new cvRect(other.rect_roi);
            this.dotCnt = other.dotCnt;
        }


       // public Rectangle Rect_roi { get => rect_roi.ToRectangle(); set => rect_roi = new cvRect(value); }
        public cvRect Rect_roi { get => rect_roi; set => rect_roi = value; }
        public float MinDotDia_mm { get => minDotDia_mm; set => minDotDia_mm = value; }
        public float MaxDotDia_mm
        {
            get => maxDotDia_mm; set => maxDotDia_mm = value;
        }
        public int DotCnt { get => dotCnt; set => dotCnt = value; }

        public int result_week_int() //0 means 1st week
        {
            return ((5 - (DotCnt))+1);
        }


    }
}
