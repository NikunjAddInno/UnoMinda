using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace tryThresholds.IP_tools
{
    public class tool_ROI : tool
    {
        String templateName;
        float rotationLimit;
        float mathcScoreThresh;
        cvRect rect_roi;

        public tool_ROI()
        { }
        public tool_ROI(String Name) : base(Name, Mark_ins_shape.ROI)
        {

            this.TemplateName = "";
            this.RotationLimit = 0;
            this.MathcScoreThresh = 50;
            this.Rect_roi = new cvRect(new Rectangle(0, 0, 10, 10));
        }
        //public tool_ROI(tool_ROI other) : base(other)
        //{
        //    this.TemplateName = other.TemplateName;
        //    this.RotationLimit = other.RotationLimit;
        //    this.Rect_roi = other.Rect_roi;
        //    this.MathcScoreThresh = other.MathcScoreThresh;
        //}
        public string TemplateName
        {
            get => templateName;
            set
            {
                templateName = value.Replace(" ", string.Empty); 
            }
        }

        public float RotationLimit { get => rotationLimit; set => rotationLimit = value; }
        public float MathcScoreThresh { get => mathcScoreThresh; set => mathcScoreThresh = value; }
        //public Rectangle Rect_roi { get => rect_roi.ToRectangle(); set => rect_roi = new cvRect(value); }
        public cvRect Rect_roi { get => rect_roi; set => rect_roi = value; }

        // Deep copy constructor
        public tool_ROI(tool_ROI other) : base(other)
        {
            this.templateName = String.Copy(other.templateName);
            this.rotationLimit = other.rotationLimit;
            this.mathcScoreThresh = other.mathcScoreThresh;
            this.rect_roi = new cvRect(other.rect_roi);  // Assuming Rectangle is a class with X, Y, Width, and Height properties
        }
    }
}
