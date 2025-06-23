using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Measurement_AI.classes
{
    enum shapeType { Arc, Width, Circle, Thread,Angle,Locate, Match, Inner_Hex, Outer_Hex }
    class DrawShapesOnpicBox
    {
        public static Bitmap copyImage24Bit(Bitmap imageIn)
        {
            return imageIn.Clone(new Rectangle(0, 0, imageIn.Width, imageIn.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        }

        // fit a circle in 3 given points
        //static void findCircle(int x1, int y1,
        //                int x2, int y2,
        //                int x3, int y3,out double  radii , out Point center)
        //{
        //    int x12 = x1 - x2;
        //    int x13 = x1 - x3;

        //    int y12 = y1 - y2;
        //    int y13 = y1 - y3;

        //    int y31 = y3 - y1;
        //    int y21 = y2 - y1;

        //    int x31 = x3 - x1;
        //    int x21 = x2 - x1;

        //    // x1^2 - x3^2
        //    int sx13 = (int)(Math.Pow(x1, 2) -
        //                    Math.Pow(x3, 2));

        //    // y1^2 - y3^2
        //    int sy13 = (int)(Math.Pow(y1, 2) -
        //                    Math.Pow(y3, 2));

        //    int sx21 = (int)(Math.Pow(x2, 2) -
        //                    Math.Pow(x1, 2));

        //    int sy21 = (int)(Math.Pow(y2, 2) -
        //                    Math.Pow(y1, 2));

        //    int f = ((sx13) * (x12)
        //            + (sy13) * (x12)
        //            + (sx21) * (x13)
        //            + (sy21) * (x13))
        //            / (2 * ((y31) * (x12) - (y21) * (x13)));
        //    int g = ((sx13) * (y12)
        //            + (sy13) * (y12)
        //            + (sx21) * (y13)
        //            + (sy21) * (y13))
        //            / (2 * ((x31) * (y12) - (x21) * (y13)));

        //    int c = -(int)Math.Pow(x1, 2) - (int)Math.Pow(y1, 2) -
        //                                2 * g * x1 - 2 * f * y1;

        //    // eqn of circle be x^2 + y^2 + 2*g*x + 2*f*y + c = 0
        //    // where centre is (h = -g, k = -f) and radius r
        //    // as r^2 = h^2 + k^2 - c
        //    int h = -g;
        //    int k = -f;
        //    int sqr_of_r = h * h + k * k - c;

        //    // r is the radius
        //    radii= Math.Round(Math.Sqrt(sqr_of_r), 5);
        //    center = new Point(h, k);
            
        //   // Console.WriteLine("Centre = (" + h + "," + k + ")");
        // //   Console.WriteLine("Radius = " + radii);
        //}

        static void findCircle(int x1, int y1,
                int x2, int y2,
                int x3, int y3, out double radii, out Point center)
        {
            int x12 = x1 - x2;
            int x13 = x1 - x3;

            int y12 = y1 - y2;
            int y13 = y1 - y3;

            int y31 = y3 - y1;
            int y21 = y2 - y1;

            int x31 = x3 - x1;
            int x21 = x2 - x1;

            // x1^2 - x3^2
            float sx13 = (float)(Math.Pow(x1, 2) -
                            Math.Pow(x3, 2));

            // y1^2 - y3^2
            float sy13 = (float)(Math.Pow(y1, 2) -
                            Math.Pow(y3, 2));

            float sx21 = (float)(Math.Pow(x2, 2) -
                            Math.Pow(x1, 2));

            float sy21 = (float)(Math.Pow(y2, 2) -
                            Math.Pow(y1, 2));

            float f = ((sx13) * (x12)
                    + (sy13) * (x12)
                    + (sx21) * (x13)
                    + (sy21) * (x13))
                    / (2 * ((y31) * (x12) - (y21) * (x13)));
            float g = ((sx13) * (y12)
                    + (sy13) * (y12)
                    + (sx21) * (y13)
                    + (sy21) * (y13))
                    / (2 * ((x31) * (y12) - (x21) * (y13)));

            float c = -(float)Math.Pow(x1, 2) - (float)Math.Pow(y1, 2) -
                                        2 * g * x1 - 2 * f * y1;

            // eqn of circle be x^2 + y^2 + 2*g*x + 2*f*y + c = 0
            // where centre is (h = -g, k = -f) and radius r
            // as r^2 = h^2 + k^2 - c
            float h = -g;
            float k = -f;
            float sqr_of_r = h * h + k * k - c;

            // r is the radius
            radii = Math.Round(Math.Sqrt(sqr_of_r), 5);
            center = new Point((int)h,(int) k);

            // Console.WriteLine("Centre = (" + h + "," + k + ")");
            //   Console.WriteLine("Radius = " + radii);
        }
        public float distanceBWpoint(PointF p1, PointF p2)
        {
            return (float)(Math.Sqrt((Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2))));
        }

        // get a rect from center and raddi to draw circle on picturebox
        public static Rectangle getRectForCircle(Point center, int dia)
        {
            return new Rectangle(center.X - dia / 2, center.Y - dia / 2, dia, dia);
        }

        // modify coordinates of points according to scale factor .. image to picbox
        Point modCoordPt(PointF pIn, float m)
        {
            return new Point((int)(pIn.X / m), (int)(pIn.Y / m));
        }
        // modify coordinates of rectangle according to scale factor .. image to picbox
        Rectangle modCoordRect(Rectangle rIn, float m)
        {
            return new Rectangle((int)(rIn.X / m), (int)(rIn.Y / m), (int)(rIn.Width / m), (int)(rIn.Height / m));
        }

        // draw a cross at given point
        private void PaintCross(Graphics g, Point loc)

        {

            //Half length of the line.

            const int HALF_LEN = 5;

            //Draw horizontal line.

            Point p1 = new Point(loc.X - HALF_LEN, loc.Y);

            Point p2 = new Point(loc.X + HALF_LEN, loc.Y);

            g.DrawLine(Pens.Red, p1, p2);

            //Draw the vertical line.

            p1 = new Point(loc.X, loc.Y - HALF_LEN);

            p2 = new Point(loc.X, loc.Y + HALF_LEN);

            g.DrawLine(Pens.Red, p1, p2);

        }
        //class vars
        public int idx;
        public string name;
        public shapeType shape;
        public List<Point> list_points = new List<Point>();
        private int updateAnchorIdx=-1;
        public int pointCount = 0;
        private Point pt_moveAnchor = new Point(-1,-1);
        public bool pointsAvailable = false; // if all points have been selected
        public bool dataSaved = false;
        //----

        internal shapeType Shape
        {
            get => shape;

            set
            {
                shape = value;
                switch (value)
                {
                    case shapeType.Circle:
                        {
                            pointCount =2;
                            break;
                        }
                    case shapeType.Arc:
                        {
                            pointCount = 3;
                            break;
                        }
                    case shapeType.Width:
                        {
                            pointCount = 3;
                            break;
                        }
                    case shapeType.Thread:
                        {
                            pointCount = 2;
                            break;
                        }
                    case shapeType.Angle:
                        {
                            pointCount = 4;
                            break;
                        }
                    case shapeType.Locate:
                        {
                            pointCount = 2;
                            break;
                        }
                    case shapeType.Match:
                        {
                            pointCount = 2;
                            break;
                        }
                    case shapeType.Inner_Hex:
                        {
                            pointCount = 2;
                            break;
                        }
                    case shapeType.Outer_Hex:
                        {
                            pointCount = 2;
                            break;
                        }

                }
            }
        }
        // find nearest available anchor and return its index
        // -2 for entre tool shift
        // index for single point
        private int mark_anchor_forUpdate(Point p,float zoomF) 
        {
            if (distanceBWpoint(p, pt_moveAnchor) < 10)
                return -2;


            for (int i = 0; i < list_points.Count(); i++)
            {
                if (distanceBWpoint(list_points[i], p) < 10)
                {
                    list_points[i] = p;
                    return i;
                }
            }
            return -1;
         
        }

        // add point or modify existing nearest anchor  //called on mouse down
        public int add_or_move_anchor(Point p, Graphics g, float zoomF)
        {
            updateAnchorIdx = -1;
            int resp = -1;
           // Console.WriteLine("list size {0}  , Point count ::{1} ", list_points.Count, pointCount);
            if (list_points.Count < pointCount)
            {
                list_points.Add(p);
              //  Console.WriteLine("Added one point to list , new list size is {0}", list_points.Count());

                resp= 0;
            }
            else 
            {
                updateAnchorIdx = mark_anchor_forUpdate(p,zoomF);
                resp = updateAnchorIdx;
            }
            pointsAvailable = (list_points.Count() == pointCount);// set all points selected flag
            foreach (PointF pt in list_points) //draw anchor points
            {
                Rectangle r = getRectForCircle(modCoordPt(pt, zoomF), (int)(30 / zoomF));
                g.DrawEllipse(Pens.Red, getRectForCircle(modCoordPt(pt, zoomF), (int)(10)));
                // Rectangle r = modCoordRect(new Rectangle((int)pt.X - 3, (int)pt.Y - 3, 6, 6), zoomF);
                g.DrawRectangle(Pens.DodgerBlue,r );
            }
            return resp;
        }

        //make changes in coordinates of selsected point .. selected by mousedown fn --add_or_move_anchor
        public int updateAnchorPosition(Point p)  // on mouseup event update point selected by user
        {
            if (updateAnchorIdx >= 0 && updateAnchorIdx <= list_points.Count())
            {
                list_points[updateAnchorIdx] = p;
                return 1;
            }
            else if(updateAnchorIdx==-2)
            {
                int diffx = pt_moveAnchor.X - p.X;
                int diffy = pt_moveAnchor.Y - p.Y;
                pt_moveAnchor = p;
                for (int  q=0; q<list_points.Count();q++)
                {
                    list_points[q] = new Point(list_points[q].X - diffx, list_points[q].Y - diffy);
                }
            }
                return -1;
        }

        // draws the shape when all points are available ... else draws points
        public int drawShape(Graphics g, float zoomF)
        {
            foreach (PointF p in list_points)
            {
                //Rectangle r = modCoordRect(new Rectangle((int)p.X - 3, (int)p.Y - 3,(int) (6*2*zoomF),(int) (6*2*zoomF)), zoomF);
                Rectangle r= getRectForCircle( modCoordPt( p,zoomF),(int)(30/zoomF)); //anchor points
                g.DrawEllipse(Pens.Red, getRectForCircle(modCoordPt(p, zoomF), (int)(10)));
                g.DrawRectangle(Pens.DodgerBlue , r);
              //  Console.WriteLine("drawing point" + p.X.ToString() + "   Y:" + p.Y.ToString());
            }
            if (list_points.Count == pointCount)
            {  
                switch (Shape)
                {
                    case shapeType.Match:
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            Rectangle r = new Rectangle(x, y, w, h);
                            pt_moveAnchor = new Point(r.X + (int)w / 2, r.Y + (int)h / 2);
                            //----
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.Orange, r);
                            break;
                        }
                    case shapeType.Locate:
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            Rectangle r = new Rectangle(x, y, w, h);
                            pt_moveAnchor = new Point(r.X + (int)w / 2, r.Y + (int)h / 2);
                            //----
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.Orange, r);
                            break;
                        }
                    case shapeType.Circle:
                        {
                            //Point center = list_points[0];
                            //int radii_1 = 2*(int)distanceBWpoint(center, list_points[1]);
                            //int radii_2 =2*(int) distanceBWpoint(center, list_points[2]);
                            //Rectangle r1 = new Rectangle(new Point(center.X-radii_1/2, center.Y-radii_1/2),new Size((int) radii_1,(int)radii_1));
                            //Rectangle r2 = new Rectangle(new Point(center.X - radii_2 / 2, center.Y - radii_2 / 2), new Size((int)radii_2, (int)radii_2));

                            //pt_moveAnchor = list_points[0];

                            //r1 = modCoordRect(r1, zoomF);
                            //r2 = modCoordRect(r2, zoomF);
                            //g.DrawEllipse(Pens.Red, r1);
                            //g.DrawEllipse(Pens.Purple, r2);
                            //break;
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            Rectangle r = new Rectangle(x, y, w, h);
                            pt_moveAnchor = new Point(r.X + (int)w / 2, r.Y + (int)h / 2);
                            //----
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.LimeGreen, r);
                            break;
                        }
                    case shapeType.Arc:
                        {
                           double radii = 10;//= (int)Math.Sqrt(dx * dx + dy * dy);
                            Point center = new Point(10,10);
                            findCircle(list_points[0].X, list_points[0].Y, list_points[1].X, list_points[1].Y, list_points[2].X, list_points[2].Y, out radii, out center);
                            Rectangle r1 = new Rectangle(new Point(center.X - (int)radii , center.Y - (int)radii ), new Size((int)radii*2, (int)radii*2));
                      
                            pt_moveAnchor = new Point(center.X, center.Y);

                            r1 = modCoordRect(r1, zoomF);
                            g.DrawEllipse(Pens.Red, r1);
                            // g.DrawArc(Pens.Green, r1, 0, 300);
                            break;
                        }
                    case shapeType.Width:
                        {
                            //g.DrawRectangle(Pens.Orange, new Rectangle(300, 300, 200, 200));
                            g.DrawLine(Pens.Blue, modCoordPt(list_points[0], zoomF), modCoordPt(list_points[1], zoomF));
                            Point ptStart = new Point((list_points[0].X + list_points[1].X) / 2, (list_points[0].Y + list_points[1].Y) / 2);
                            
                            Pen p = new Pen(Color.Blue, 1);
                            AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
                            p.CustomEndCap = bigArrow;
                            g.DrawLine(p, modCoordPt(ptStart, zoomF), modCoordPt(list_points[2], zoomF));

                            break;
                        }
                    case shapeType.Thread:
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                          
                            Rectangle r = new Rectangle(x, y, w, h);
                            Point ptStart = new Point(0, 0);
                            Point ptEnd = new Point(0, 0);
                            if ((list_points[0].Y - list_points[1].Y) < 0) //horizontal direction arrow
                            {
                                ptStart = new Point(list_points[0].X, (list_points[0].Y + list_points[1].Y) / 2);
                                ptEnd = new Point(list_points[1].X, (list_points[0].Y + list_points[1].Y) / 2);
                            }
                            else  //vertical direction
                            {
                                ptStart = new Point((list_points[0].X + list_points[1].X) / 2, list_points[0].Y );
                                ptEnd = new Point((list_points[0].X + list_points[1].X) / 2, list_points[1].Y);

                            }

                            //line with arrow
                    
                            pt_moveAnchor = new Point(r.X + (int)w / 2, r.Y + (int)h / 2);

                            Pen p = new Pen(Color.Blue, 1);
                            AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
                            p.CustomEndCap = bigArrow;
                            g.DrawLine( p, modCoordPt( ptStart,zoomF), modCoordPt(ptEnd,zoomF));
                            //----
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.Orange, r);

                 
                            break;
                        }
                    case shapeType.Angle:
                        {
                            //g.DrawRectangle(Pens.Orange, new Rectangle(300, 300, 200, 200));
                            g.DrawLine(Pens.Blue, modCoordPt(list_points[0], zoomF), modCoordPt(list_points[1], zoomF));
                            g.DrawLine(Pens.Blue, modCoordPt(list_points[2], zoomF), modCoordPt(list_points[3], zoomF));
                            Point ptStart = new Point((list_points[0].X + list_points[1].X) / 2, (list_points[0].Y + list_points[1].Y) / 2);
                            Point ptEnd = new Point((list_points[2].X + list_points[3].X) / 2, (list_points[2].Y + list_points[3].Y) / 2);
                            Pen p = new Pen(Color.Blue, 1);
                            AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
                            p.CustomEndCap = bigArrow;
                            g.DrawLine(p, modCoordPt(ptStart, zoomF), modCoordPt(ptEnd, zoomF));

                            break;
                        }
                    case shapeType.Inner_Hex:
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            Rectangle r = new Rectangle(x, y, w, h);
                            pt_moveAnchor = new Point(r.X + (int)w / 2, r.Y + (int)h / 2);
                            //----
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.Orange, r);
                            break;
                        }
                    case shapeType.Outer_Hex:
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            Rectangle r = new Rectangle(x, y, w, h);
                            pt_moveAnchor = new Point(r.X + (int)w / 2, r.Y + (int)h / 2);
                            //----
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.Orange, r);
                            break;
                        }
                    default:
                        break;

                }
                PaintCross(g, modCoordPt(pt_moveAnchor,zoomF));
            }
          //  else 
          //  {
        
           // }
            return 1;
        
        
        }


    }
}
