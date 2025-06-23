using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace tryThresholds.IP_tools
{
    public class tool_CROI : tool
    {
        String templateName;
        float rotationLimit;
        float mathcScoreThresh;
        cvRect rect_roi;
        cvRect searchRegion;
        cvPoint2f shiftTolerance;//positive side tol
        cvPoint2f shiftTolerance_neg;//negative side tol
        int mode; //colour/gray/binary
        float colourId;
        int threshold;
        int thresholdType;//binary or binary inv
        public enum CROI_mode {Colour,Threshold,Gray };

        public tool_CROI()
        { }
        public tool_CROI(String Name) : base(Name, Mark_ins_shape.ROI)
        {

            this.TemplateName = "";
            this.RotationLimit = 5;
            this.MathcScoreThresh = 50;
            this.Rect_roi = new cvRect(new Rectangle(0, 0, 10, 10));
            this.SearchRegion = new cvRect(new Rectangle(0, 0, 20, 20));
            this.ShiftTolerance = new cvPoint2f(3.0f,3.0f);
            this.ShiftTolerance_neg = new cvPoint2f(3.0f,3.0f);
            this.colourId = 0;
            this.threshold = 150;
            this.mode = 1;
            this.thresholdType = 0;
        }
        //public tool_CROI(tool_CROI other) : base(other)
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
        public cvRect SearchRegion { get => searchRegion; set => searchRegion = value; }
        public cvPoint2f ShiftTolerance { get => shiftTolerance; set => shiftTolerance = value; }
        public int Mode { get => mode; set => mode = value; }
        public float ColourId { get => colourId; set => colourId = value; }
        public int Threshold { get => threshold; set => threshold = value; }
        public int ThresholdType { get => thresholdType; set => thresholdType = value; }
        public cvPoint2f ShiftTolerance_neg { get => shiftTolerance_neg; set => shiftTolerance_neg = value; }

        // Deep copy constructor
        public tool_CROI(tool_CROI other) : base(other)
        {
            this.templateName = String.Copy(other.templateName);
            this.rotationLimit = other.rotationLimit;
            this.mathcScoreThresh = other.mathcScoreThresh;
            this.rect_roi = new cvRect(other.rect_roi);  // Assuming Rectangle is a class with X, Y, Width, and Height properties
            this.searchRegion = new cvRect(other.searchRegion);  // Assuming Rectangle is a class with X, Y, Width, and Height properties
            this.shiftTolerance =new cvPoint2f( other.shiftTolerance);
            this.shiftTolerance_neg =new cvPoint2f( other.shiftTolerance_neg);
            this.mode = other.mode;
            this.colourId = other.colourId;
            this.threshold = other.threshold;
            this.thresholdType = other.thresholdType;
        }
    }
}
