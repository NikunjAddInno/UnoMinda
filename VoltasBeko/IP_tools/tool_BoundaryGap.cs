using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryThresholds.IP_tools
{
    public class tool_BoundaryGap : tool
    {

        float gapLeft;
        float gapRight;
        float gapTop;
        float gapBottom;
        int threshold;
        int thresholdType;//binary or binary inv
        cvRect rect_roi;
        bool enabled_measure;
        cvRect rect_roi_outer;
        int threshold_outer;
        int fillPercent_outer;
        bool enabled_outer;

        public tool_BoundaryGap()
        { }
        public tool_BoundaryGap(String Name) : base(Name, Mark_ins_shape.WeekCode)
        {
            this.GapLeft = 0.5f;
            this.GapRight = 0.5f;
            this.GapTop = 0.5f;
            this.GapBottom = 0.5f;
            this.Rect_roi = new cvRect(new Rectangle(0, 0, 10, 10));
            this.threshold = 120;
            this.thresholdType = 0;
            this.enabled_measure = true;
            this.rect_roi_outer= new cvRect(new Rectangle(0, 0, 12, 12));
            this.threshold_outer = 90;
            this.fillPercent_outer = 90;
            this.enabled_outer = true;
        }

        public tool_BoundaryGap(tool_BoundaryGap other) : base(other)
        {
            this.GapLeft = other.GapLeft;
            this.GapRight = other.GapRight;
            this.GapTop = other.GapTop;
            this.GapBottom = other.GapBottom;
            this.rect_roi = new cvRect(other.rect_roi);
            this.threshold=other.threshold;
            this.thresholdType = other.thresholdType;
            this.enabled_measure = other.enabled_measure;
            this.rect_roi_outer = new cvRect(other.rect_roi_outer);
            this.threshold_outer =other.threshold_outer;
            this.fillPercent_outer = other.fillPercent_outer;
            this.enabled_outer = other.enabled_outer;
        }


        // public Rectangle Rect_roi { get => rect_roi.ToRectangle(); set => rect_roi = new cvRect(value); }
        public cvRect Rect_roi { get => rect_roi; set => rect_roi = value; }
 


        public float GapLeft { get => gapLeft; set => gapLeft = value; }
        public float GapRight { get => gapRight; set => gapRight = value; }
        public float GapTop { get => gapTop; set => gapTop = value; }
        public float GapBottom { get => gapBottom; set => gapBottom = value; }
        public int Threshold { get => threshold; set => threshold = value; }
        public int ThresholdType { get => thresholdType; set => thresholdType = value; }
        public bool Enabled_measure { get => enabled_measure; set => enabled_measure = value; }
        public cvRect Rect_roi_outer { get => rect_roi_outer; set => rect_roi_outer = value; }
        public int Threshold_outer { get => threshold_outer; set => threshold_outer = value; }
        public int FillPercent_outer { get => fillPercent_outer; set => fillPercent_outer = value; }
        public bool Enabled_outer { get => enabled_outer; set => enabled_outer = value; }
    }
}
