using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace tryThresholds.IP_tools
{
    public class tool_Group_printCheck : tool
    {

        //font height tol and font width tol missing

        public float BoundaryThickness { get; set; }
        public float Threshold { get; set; } //light
        public float burnThreshold { get; set; }//dark
        public float HeighTolerance { get; set; }
        public float WidthTolerance { get; set; }
        public float AreaTolerance { get; set; }
        //rect roi
         cvRect rect_roi;

        //template path
        string templateName;

        //shift
        public float shiftXTol { get; set; }
        public float shiftYTol { get; set; }

        public tool_Group_printCheck()
        { }
        public tool_Group_printCheck(string Name) : base(Name, Mark_ins_shape.Group)
        {
            this.BoundaryThickness = 0.1f;
            this.Threshold = 5;
            this.burnThreshold = 5;
            this.HeighTolerance = 0.1f;
            this.WidthTolerance = 0.1f;
            this.AreaTolerance = 0.25f; ;
            this.shiftXTol = 0.2f;
            this.shiftYTol = 0.2f;
            this.TemplateName = "";
            this.Rect_roi = new cvRect(new Rectangle(0, 0, 10, 10));
        }
       // public Rectangle Rect_roi { get => rect_roi.ToRectangle(); set => rect_roi = new cvRect(value); }
        public cvRect Rect_roi { get => rect_roi; set => rect_roi =value; }
        public string TemplateName
        {
            get => templateName;
            set
            {
                templateName = value.Replace(" ", string.Empty);
            }
        }

        public tool_Group_printCheck(tool_Group_printCheck other) : base(other)
        {
            this.BoundaryThickness = other.BoundaryThickness;
            this.Threshold = other.Threshold;
            this.burnThreshold = other.burnThreshold;
            this.HeighTolerance = other.HeighTolerance;
            this.WidthTolerance = other.WidthTolerance;
            this.AreaTolerance = other.AreaTolerance;
            this.rect_roi = new cvRect(other.rect_roi);  // Assuming Rectangle is a class with X, Y, Width, and Height properties
            this.templateName = String.Copy(other.templateName);
            this.shiftXTol = other.shiftXTol;
            this.shiftYTol = other.shiftYTol;
        }

    }
}
