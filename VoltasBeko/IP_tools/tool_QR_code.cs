using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace tryThresholds.IP_tools
{
    public class tool_QR_code : tool
    {
        String expectedString;
        String resultString;
        cvRect rect_roi;
        int mode; //colour/gray/binary
        float colourId;
        public tool_QR_code()  
        { }
        public tool_QR_code(String Name) : base(Name, Mark_ins_shape.QR)
        {
            ExpectedString = "";
            ResultString = "";
            this.Rect_roi =new cvRect( new Rectangle(0, 0, 10, 10));
            int mode; //colour/gray/binary
            colourId = 0;
            mode = 0;

        }

        public string ExpectedString { get => expectedString; set => expectedString = value; }
        public string ResultString { get => resultString; set => resultString = value; }
        //public Rectangle Rect_roi { get => rect_roi.ToRectangle(); set => rect_roi = new cvRect(value); }
        public cvRect Rect_roi { get => rect_roi; set => rect_roi = value; }
        public int Mode { get => mode; set => mode = value; }
        public float ColourId { get => colourId; set => colourId = value; }

        public bool check_matched()
        {
            if (this.ExpectedString == null || this.ResultString == null)
            {
                Console.WriteLine("Expected or result string is empty");
                return false;
            }
            if (ResultString.Equals(ExpectedString, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        // Deep copy constructor
        public tool_QR_code(tool_QR_code other) : base(other)
        {
            this.expectedString = String.Copy(other.expectedString);
            this.resultString = String.Copy(other.resultString);
            this.rect_roi = new cvRect(other.rect_roi);  // Assuming Rectangle is a class with X, Y, Width, and Height properties
            this.colourId = other.colourId;
            this.mode = other.mode;
        }
    }
}
