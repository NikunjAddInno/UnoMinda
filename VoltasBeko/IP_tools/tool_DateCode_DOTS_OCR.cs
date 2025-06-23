using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace tryThresholds.IP_tools
{
   // public enum monthEncode { Jan=1,Feb=2,March=3,April=4,May=5,June=6,July=7,Aug=8,Sept=9,Oct=10,Nov=11,Dec=12 };
    public enum monthEncode { Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec };
    public enum yearDecode { A,B,C,D};

    public enum date_matchMode {setValue,Auto,None};


    public class tool_DateCode_DOTS_OCR:tool
    {
        //inout parameters
        float minDotDia_mm;
        float maxDotDia_mm;
        cvRect rect_roi;
        cvRect rect_OCR;
        
        

        //outputs
        int dotCntLeft;
        int dotCntRight;
        String midStr;
        public tool_DateCode_DOTS_OCR()
        { }
        public tool_DateCode_DOTS_OCR(String Name) : base(Name, Mark_ins_shape.DateCode)
        {
            this.MinDotDia_mm = 0.1f;
            this.MaxDotDia_mm = 3.0f;
            this.Rect_roi = new cvRect(new Rectangle(0, 0, 10, 10));
            this.Rect_OCR =new cvRect( new Rectangle(0, 0, 10, 10));
            this.dotCntLeft = 0;
            this.DotCntRight = 0;
            this.MidStr = "";
        }


      //  public Rectangle Rect_roi { get => rect_roi.ToRectangle(); set => rect_roi =new cvRect( value); }
        public cvRect Rect_roi { get => rect_roi; set => rect_roi = value; }
        public float MinDotDia_mm { get => minDotDia_mm; set => minDotDia_mm = value; }
        public float MaxDotDia_mm { get => maxDotDia_mm; set => maxDotDia_mm = value; }
      //  public Rectangle Rect_OCR { get => rect_OCR.ToRectangle(); set => rect_OCR = new cvRect(value); }
        public cvRect Rect_OCR { get => rect_OCR; set => rect_OCR=value; }
        public int DotCntLeft { get => dotCntLeft; set => dotCntLeft = value; }
        public int DotCntRight { get => dotCntRight; set => dotCntRight = value; }
        public string MidStr { get => midStr; set => midStr = value; }

        public tool_DateCode_DOTS_OCR(tool_DateCode_DOTS_OCR other):base(other)
        {
            this.minDotDia_mm = other.minDotDia_mm;
            this.maxDotDia_mm = other.maxDotDia_mm;
            this.rect_roi = new cvRect(other.rect_roi); // Assuming cvRect has a copy constructor
            this.rect_OCR = new cvRect(other.rect_OCR); // Assuming cvRect has a copy constructor
            this.dotCntLeft = other.dotCntLeft;
            this.dotCntRight = other.dotCntRight;
            this.midStr = String.Copy(other.midStr);
        }

        public int result_year()
        {
            if (MidStr.Length < 2)
            { return 0; }
            else
            {
                MidStr = MidStr.ToUpper();
                var charArr = MidStr.ToCharArray();
                Char C1 = charArr[0];// MidStr.Substring(0, 1);
                Char C2 = charArr[1];// MidStr.Substring(1, 1);
                int year = 10 * (((int)C1) - 65) + (((int)(C2)) - 65);
                return year;
            }
        
        }
        public int result_month_int() //0 means jan --11 dec
        {
            return (12 - (DotCntLeft + DotCntRight));
        }

        public String result_minth_Str()
        { 
        return ((monthEncode)(12 - (DotCntLeft + DotCntRight))).ToString();
        }
       
    }
}
