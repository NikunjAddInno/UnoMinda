using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryThresholds.IP_tools
{
    public class tool_GrayPresence : tool
    {

        float matchPercent;
 
        int threshold;
        int thresholdType;//binary or binary inv

        int mode; //colour/gray/binary
        float colourId;
        cvRect rect_roi;



        public tool_GrayPresence()
        { }
        public tool_GrayPresence(String Name) : base(Name, Mark_ins_shape.WeekCode)
        {
            this.MatchPercent = 80.0f;
            this.Rect_roi = new cvRect(new Rectangle(0, 0, 10, 10));
            this.threshold = 120;
            this.thresholdType = 0;
            this.colourId = 0;
            this.mode = 1;

        }

        public tool_GrayPresence(tool_GrayPresence other) : base(other)
        {
            this.matchPercent = other.matchPercent;
            this.rect_roi = new cvRect(other.rect_roi);
            this.threshold = other.threshold;
            this.thresholdType = other.thresholdType;
            this.colourId = other.colourId;
            this.mode = other.mode;

        }


        // public Rectangle Rect_roi { get => rect_roi.ToRectangle(); set => rect_roi = new cvRect(value); }
        public cvRect Rect_roi { get => rect_roi; set => rect_roi = value; }



        public int Threshold { get => threshold; set => threshold = value; }
        public int ThresholdType { get => thresholdType; set => thresholdType = value; }
        public float MatchPercent { get => matchPercent; set => matchPercent = value; }
        public float ColourId { get => colourId; set => colourId = value; }
        public int Mode { get => mode; set => mode = value; }
    }
}
