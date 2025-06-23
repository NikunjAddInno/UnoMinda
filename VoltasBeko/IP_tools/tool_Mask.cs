using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace tryThresholds.IP_tools
{
    public class tool_Mask : tool
    {
        cvRect rect_roi;
        public tool_Mask()
        {}

        public tool_Mask(String Name) : base(Name, Mark_ins_shape.Mask)
        {
            this.Rect_roi = new cvRect(new Rectangle(0, 0, 10, 10));
        }


        public tool_Mask(tool_Mask other) : base(other)
        {
            this.rect_roi = new cvRect(other.rect_roi);//
        }


        //public Rectangle Rect_roi { get => rect_roi.ToRectangle(); set => rect_roi = new cvRect(value); }
        public cvRect Rect_roi { get => rect_roi; set => rect_roi =value; }

        //deep copy constr missing
    }
}
