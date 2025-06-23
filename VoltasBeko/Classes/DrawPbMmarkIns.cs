using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using tryThresholds.IP_tools;
using Newtonsoft.Json;

namespace VoltasBeko.classes
{
    //enum Mark_ins_shape { ROI,Group, QR, DateCode, WeekCode, Mask }
    public class DrawPbMmarkIns
    {

        public static Bitmap copyImage24Bit(Bitmap imageIn)
        {
            return imageIn.Clone(new Rectangle(0, 0, imageIn.Width, imageIn.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        }



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
            center = new Point((int)h, (int)k);

            // Console.WriteLine("Centre = (" + h + "," + k + ")");
            //   Console.WriteLine("Radius = " + radii);
        }
        public float distanceBWpoint(cvPoint p1, cvPoint p2)
        {
            return (float)(Math.Sqrt((Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2))));
        }

        // get a rect from center and raddi to draw circle on picturebox
        public static Rectangle getRectForCircle(Point center, int dia)
        {
            return new Rectangle(center.X - dia / 2, center.Y - dia / 2, dia, dia);
        }

        // modify coordinates of points according to scale factor .. image to picbox
        Point modCoordPt(cvPoint pIn, float m)
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
        public Mark_ins_shape shape;
        // public List<Point> list_points = new List<Point>();
        public List<cvPoint> list_points = new List<cvPoint>();
        private int updateAnchorIdx = -1;
        public int pointCount = 0;
        //private Point pt_moveAnchor = new Point(-1, -1);
        private cvPoint pt_moveAnchor = new cvPoint(-1, -1);
        public bool pointsAvailable = false; // if all points have been selected
        public bool dataSaved = false;
        //----
        Font drawFont = new Font("Arial", 14);
        SolidBrush drawBrush = new SolidBrush(Color.LimeGreen);
        internal Mark_ins_shape Shape
        {
            get => shape;

            set
            {
                shape = value;
                switch (value)
                {
                    case Mark_ins_shape.ROI:
                        {
                            pointCount = 2;
                            break;
                        }
                    case Mark_ins_shape.Group:
                        {
                            pointCount = 2;
                            break;
                        }
                    case Mark_ins_shape.QR:
                        {
                            pointCount = 2;
                            break;
                        }
                    case Mark_ins_shape.DateCode:
                        {
                            pointCount = 4;
                            break;
                        }
                    case Mark_ins_shape.WeekCode:
                        {
                            pointCount = 2;
                            break;
                        }
                    case Mark_ins_shape.Mask:
                        {
                            pointCount = 2;
                            break;
                        }
                    case Mark_ins_shape.Fixture:
                        {
                            pointCount = 8;
                            break;
                        }
                    case Mark_ins_shape.CROI:
                        {
                            pointCount = 4;
                            break;
                        }
                    case Mark_ins_shape.Boundary_Gap:
                        {
                            pointCount = 4;
                            break;
                        }
                    case Mark_ins_shape.Gray_Presence:
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
        private int mark_anchor_forUpdate(cvPoint p, float zoomF)
        {
            if (distanceBWpoint(p, pt_moveAnchor) < 15)
                return -2;


            for (int i = 0; i < list_points.Count(); i++)
            {
                if (distanceBWpoint(list_points[i], p) < 15)
                {
                    list_points[i] = p;
                    return i;
                }
            }
            return -1;

        }

        // add point or modify existing nearest anchor  //called on mouse down
        public int add_or_move_anchor(cvPoint p, Graphics g, float zoomF)
        {
            updateAnchorIdx = -1;
            int resp = -1;
            if (list_points.Count < pointCount)
            {
                list_points.Add(p);
                resp = 0;
            }
            else
            {
                updateAnchorIdx = mark_anchor_forUpdate(p, zoomF);
                resp = updateAnchorIdx;
            }
            pointsAvailable = (list_points.Count() == pointCount);// set all points selected flag
            foreach (cvPoint pt in list_points) //draw anchor points
            {
                Rectangle r = getRectForCircle(modCoordPt(pt, zoomF), (int)(20 / zoomF));
                g.DrawEllipse(Pens.Red, getRectForCircle(modCoordPt(pt, zoomF), (int)(20)));
                // Rectangle r = modCoordRect(new Rectangle((int)pt.X - 3, (int)pt.Y - 3, 6, 6), zoomF);
                g.DrawRectangle(Pens.DodgerBlue, r);
            }
            return resp;
        }

        //make changes in coordinates of selsected point .. selected by mousedown fn --add_or_move_anchor
        public int updateAnchorPosition(cvPoint p)  // on mouseup event update point selected by user
        {
            if (updateAnchorIdx >= 0 && updateAnchorIdx <= list_points.Count())
            {
                list_points[updateAnchorIdx] = p;
                return 1;
            }
            else if (updateAnchorIdx == -2)
            {
                int diffx = pt_moveAnchor.X - p.X;
                int diffy = pt_moveAnchor.Y - p.Y;
                pt_moveAnchor = p;
                for (int q = 0; q < list_points.Count(); q++)
                {
                    list_points[q] = new cvPoint(list_points[q].X - diffx, list_points[q].Y - diffy);
                }
            }
            return -1;
        }

        // draws the shape when all points are available ... else draws points
        public int drawShape(Graphics g, float zoomF)
        {
            foreach (cvPoint p in list_points)
            {
                //Rectangle r = modCoordRect(new Rectangle((int)p.X - 3, (int)p.Y - 3,(int) (6*2*zoomF),(int) (6*2*zoomF)), zoomF);
                Rectangle r = getRectForCircle(modCoordPt(p, zoomF), (int)(10 / zoomF)); //anchor points
                g.DrawEllipse(Pens.Red, getRectForCircle(modCoordPt(p, zoomF), (int)(10)));
                g.DrawRectangle(Pens.DodgerBlue, r);
                //  Console.WriteLine("drawing point" + p.X.ToString() + "   Y:" + p.Y.ToString());
            }

            switch (Shape)
            {// { ROI,Group, QR, DateCode, WeekCode, Mask }
                case Mark_ins_shape.ROI:
                    {
                        if (list_points.Count == pointCount)
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            Rectangle r = new Rectangle(x, y, w, h);

                            pt_moveAnchor = new cvPoint(r.X + (int)w / 2, r.Y + (int)h / 2);
                            //make a circle with  min dia of min d
                            int dia = Math.Min(w, h);

                            Rectangle circle = new Rectangle(pt_moveAnchor.X - (dia / 2), pt_moveAnchor.Y - (dia / 2), dia, dia);
                            //----
                            r = modCoordRect(r, zoomF);
                            circle = modCoordRect(circle, zoomF);
                            g.DrawRectangle(Pens.DarkOliveGreen, r);

                            g.DrawEllipse(Pens.Orange, circle);
                            g.DrawString(Shape.ToString(), drawFont, drawBrush, r);
                        }
                        break;
                    }
                case Mark_ins_shape.Group:
                    {
                        if (list_points.Count == pointCount)
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            Rectangle r = new Rectangle(x, y, w, h);
                            pt_moveAnchor = new cvPoint(r.X + (int)w / 2, r.Y + (int)h / 2);
                            //----
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.Orange, r);
                            g.DrawString(Shape.ToString(), drawFont, drawBrush, r);
                        }
                        break;
                    }
                case Mark_ins_shape.QR:
                    {
                        if (list_points.Count == pointCount)
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            Rectangle r = new Rectangle(x, y, w, h);
                            pt_moveAnchor = new cvPoint(r.X + (int)w / 2, r.Y + (int)h / 2);
                            //----
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.HotPink, r);
                            g.DrawString(Shape.ToString(), drawFont, drawBrush, r);
                        }
                        break;
                    }
                case Mark_ins_shape.DateCode:
                    {
                        Rectangle r = new Rectangle(0, 0, 10, 10);
                        if (list_points.Count >= 2)
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            r = new Rectangle(x, y, w, h);
                            pt_moveAnchor = new cvPoint(r.X + (int)w / 2, r.Y + (int)h / 2);
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.DarkMagenta, r);
                        }
                        //----
                        if (list_points.Count == pointCount)
                        {
                            int x1 = Math.Min(list_points[2].X, list_points[3].X);
                            int y1 = Math.Min(list_points[2].Y, list_points[3].Y);
                            int w1 = Math.Abs(list_points[3].X - list_points[2].X);
                            int h1 = Math.Abs(list_points[3].Y - list_points[2].Y);
                            Rectangle rRead = new Rectangle(x1, y1, w1, h1);



                            rRead = modCoordRect(rRead, zoomF);
                            g.DrawRectangle(Pens.Yellow, rRead);
                            g.DrawString(Shape.ToString(), drawFont, drawBrush, r);
                        }
                        break;
                    }
                case Mark_ins_shape.WeekCode:
                    {
                        if (list_points.Count == pointCount)
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            Rectangle r = new Rectangle(x, y, w, h);
                            pt_moveAnchor = new cvPoint(r.X + (int)w / 2, r.Y + (int)h / 2);
                            //----
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.Blue, r);
                            g.DrawString(Shape.ToString(), drawFont, drawBrush, r);
                        }
                        break;
                    }
                case Mark_ins_shape.Mask:
                    {
                        if (list_points.Count == pointCount)
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            Rectangle r = new Rectangle(x, y, w, h);
                            pt_moveAnchor = new cvPoint(r.X + (int)w / 2, r.Y + (int)h / 2);
                            //----
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.Orange, r);
                            g.DrawString(Shape.ToString(), drawFont, drawBrush, r);
                        }
                        break;
                    }
                case Mark_ins_shape.Fixture:
                    {
                        Rectangle rT1 = new Rectangle(0, 0, 10, 10);

                        if (list_points.Count >= 2 && list_points.Count < pointCount)
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            Rectangle r = new Rectangle(x, y, w, h);
                            rT1 = new Rectangle(x, y, w, h);
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.Orange, r);
                            //Console.WriteLine("Point count is 2");
                            g.DrawString(Shape.ToString() + "_t1", drawFont, drawBrush, r);
                        }
                        if (list_points.Count == 4)
                        {
                            int x = Math.Min(list_points[2].X, list_points[3].X);
                            int y = Math.Min(list_points[2].Y, list_points[3].Y);
                            int w = Math.Abs(list_points[3].X - list_points[2].X);
                            int h = Math.Abs(list_points[3].Y - list_points[2].Y);
                            Rectangle r = new Rectangle(x, y, w, h);


                            g.DrawLine(Pens.Red, modCoordPt(new cvPoint(rT1.X + rT1.Width / 2, rT1.Y + rT1.Height / 2), zoomF), modCoordPt(new cvPoint(r.X + r.Width / 2, r.Y + r.Height / 2), zoomF));
                            list_points.Add(new cvPoint(rT1.X - 20, rT1.Y - 20));
                            list_points.Add(new cvPoint(rT1.X + rT1.Width + 40, rT1.Y + rT1.Height + 40));
                            list_points.Add(new cvPoint(r.X - 20, r.Y - 20));
                            list_points.Add(new cvPoint(r.X + r.Width + 40, r.Y + r.Height + 40));

                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.Orange, r);
                            g.DrawString(Shape.ToString() + "_t2", drawFont, drawBrush, r);
                            Console.WriteLine("Point count is 4");
                            pointsAvailable = true;

                        }
                        if (list_points.Count == pointCount)
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            Rectangle r = new Rectangle(x, y, w, h);
                            rT1 = new Rectangle(x, y, w, h);


                            int x2 = Math.Min(list_points[2].X, list_points[3].X);
                            int y2 = Math.Min(list_points[2].Y, list_points[3].Y);
                            int w2 = Math.Abs(list_points[3].X - list_points[2].X);
                            int h2 = Math.Abs(list_points[3].Y - list_points[2].Y);
                            Rectangle r2 = new Rectangle(x2, y2, w2, h2);

                            cvPoint c1 = new cvPoint(rT1.X + rT1.Width / 2, rT1.Y + rT1.Height / 2);
                            cvPoint c2 = new cvPoint(r2.X + r2.Width / 2, r2.Y + r2.Height / 2);
                            g.DrawLine(Pens.Red, modCoordPt(c1, zoomF), modCoordPt(c2, zoomF));

                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.Orange, r);
                            r2 = modCoordRect(r2, zoomF);
                            g.DrawRectangle(Pens.Orange, r2);
                            pt_moveAnchor = new cvPoint((c1.X + c2.X) / 2, (c1.Y + c2.Y) / 2);
                            int x3 = Math.Min(list_points[4].X, list_points[5].X);
                            int y3 = Math.Min(list_points[4].Y, list_points[5].Y);
                            int w3 = Math.Abs(list_points[5].X - list_points[4].X);
                            int h3 = Math.Abs(list_points[5].Y - list_points[4].Y);
                            Rectangle r3 = new Rectangle(x3, y3, w3, h3);

                            int x4 = Math.Min(list_points[6].X, list_points[7].X);
                            int y4 = Math.Min(list_points[6].Y, list_points[7].Y);
                            int w4 = Math.Abs(list_points[7].X - list_points[6].X);
                            int h4 = Math.Abs(list_points[7].Y - list_points[6].Y);
                            Rectangle r4 = new Rectangle(x4, y4, w4, h4);

                            r3 = modCoordRect(r3, zoomF);
                            g.DrawRectangle(Pens.Orange, r3);
                            r4 = modCoordRect(r4, zoomF);
                            g.DrawRectangle(Pens.Orange, r4);

                            g.DrawString(Shape.ToString() + "_t1", drawFont, drawBrush, r);
                            g.DrawString(Shape.ToString() + "_t2", drawFont, drawBrush, r2);
                        }

                        break;
                    }
                case Mark_ins_shape.CROI:
                    {
                        Rectangle r = new Rectangle(0, 0, 10, 10);
                        if (list_points.Count == 2)
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            r = new Rectangle(x, y, w, h);

                            list_points.Add(new cvPoint(r.X - 20, r.Y - 20));
                            list_points.Add(new cvPoint(r.X + r.Width + 40, r.Y + r.Height + 40));
                            pointsAvailable = true;
                            pt_moveAnchor = new cvPoint(r.X + (int)w / 2, r.Y + (int)h / 2);
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.DarkMagenta, r);
                        }
                        //----
                        if (list_points.Count == pointCount)
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            r = new Rectangle(x, y, w, h);
                            pt_moveAnchor = new cvPoint(r.X + (int)w / 2, r.Y + (int)h / 2);
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.DarkMagenta, r);

                            int x1 = Math.Min(list_points[2].X, list_points[3].X);
                            int y1 = Math.Min(list_points[2].Y, list_points[3].Y);
                            int w1 = Math.Abs(list_points[3].X - list_points[2].X);
                            int h1 = Math.Abs(list_points[3].Y - list_points[2].Y);
                            Rectangle rRead = new Rectangle(x1, y1, w1, h1);



                            rRead = modCoordRect(rRead, zoomF);
                            g.DrawRectangle(Pens.Yellow, rRead);
                            g.DrawString(Shape.ToString(), drawFont, drawBrush, r);
                        }
                        break;
                    }
                case Mark_ins_shape.Boundary_Gap:
                    {
                        Rectangle r = new Rectangle(0, 0, 10, 10);
                        if (list_points.Count == 2)
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            r = new Rectangle(x, y, w, h);

                            list_points.Add(new cvPoint(r.X - 10, r.Y - 10));
                            list_points.Add(new cvPoint(r.X + r.Width + 20, r.Y + r.Height + 20));
                            pointsAvailable = true;
                            pt_moveAnchor = new cvPoint(r.X + (int)w / 2, r.Y + (int)h / 2);
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.DarkMagenta, r);
                        }
                        //----
                        if (list_points.Count == pointCount)
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            r = new Rectangle(x, y, w, h);
                            pt_moveAnchor = new cvPoint(r.X + (int)w / 2, r.Y + (int)h / 2);
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.DarkMagenta, r);

                            int x1 = Math.Min(list_points[2].X, list_points[3].X);
                            int y1 = Math.Min(list_points[2].Y, list_points[3].Y);
                            int w1 = Math.Abs(list_points[3].X - list_points[2].X);
                            int h1 = Math.Abs(list_points[3].Y - list_points[2].Y);
                            Rectangle rRead = new Rectangle(x1, y1, w1, h1);



                            rRead = modCoordRect(rRead, zoomF);
                            g.DrawRectangle(Pens.Yellow, rRead);
                            g.DrawString(Shape.ToString(), drawFont, drawBrush, r);
                        }
                        //if (list_points.Count == pointCount)
                        //{
                        //    int x = Math.Min(list_points[0].X, list_points[1].X);
                        //    int y = Math.Min(list_points[0].Y, list_points[1].Y);
                        //    int w = Math.Abs(list_points[1].X - list_points[0].X);
                        //    int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                        //    Rectangle r = new Rectangle(x, y, w, h);
                        //    pt_moveAnchor = new cvPoint(r.X + (int)w / 2, r.Y + (int)h / 2);
                        //    //----
                        //    r = modCoordRect(r, zoomF);
                        //    g.DrawRectangle(Pens.Orange, r);
                        //    g.DrawString(Shape.ToString(), drawFont, drawBrush, r);
                        //}
                        break;
                    }
                case Mark_ins_shape.Gray_Presence:
                    {
                        if (list_points.Count == pointCount)
                        {
                            int x = Math.Min(list_points[0].X, list_points[1].X);
                            int y = Math.Min(list_points[0].Y, list_points[1].Y);
                            int w = Math.Abs(list_points[1].X - list_points[0].X);
                            int h = Math.Abs(list_points[1].Y - list_points[0].Y);
                            Rectangle r = new Rectangle(x, y, w, h);
                            pt_moveAnchor = new cvPoint(r.X + (int)w / 2, r.Y + (int)h / 2);
                            //----
                            r = modCoordRect(r, zoomF);
                            g.DrawRectangle(Pens.Orange, r);
                            g.DrawString(Shape.ToString(), drawFont, drawBrush, r);
                        }
                        break;
                    }


                default:
                    break;

            }
            if (list_points.Count == pointCount)
            {
                PaintCross(g, modCoordPt(pt_moveAnchor, zoomF));
            }
            //  else 
            //  {

            // }
            return 1;


        }

        public DrawPbMmarkIns DeepCopy()
        {
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(this, Formatting.Indented);
            return JsonConvert.DeserializeObject<DrawPbMmarkIns>(json);

        }

    }
}



