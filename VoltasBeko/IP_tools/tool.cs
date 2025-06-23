using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using VoltasBeko.classes;

namespace tryThresholds.IP_tools
{
    public class cvRect
    {
        int x;
        int y;
        int w;
        int h;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Width { get => w; set => w = value; }
        public int Height { get => h; set => h = value; }

        public cvRect()
        { }

        public cvRect(int x, int y, int w, int h)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Height = h;
        }

        public cvRect(Rectangle r)
        {
            this.X = r.X;
            this.Y = r.Y;
            this.Width = r.Width;
            this.Height = r.Height;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Width, this.Height);
        }

        public cvRect(cvRect r)
        {
            this.X = r.X;
            this.Y = r.Y;
            this.Width = r.Width;
            this.Height = r.Height;
        }

    }

    public class cvPoint
    {
        int x;
        int y;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public cvPoint()
        { }

        public cvPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public cvPoint(Point p)
        {
            this.X = p.X;
            this.Y = p.Y;
        }


        public Point ToPoint()
        {
            return new Point(this.X, this.Y);
        }

        public cvPoint(cvPoint p)
        {
            this.X = p.X;
            this.Y = p.Y;
        }
    }
    public class cvPoint2f
    {
        float x;
        float y;

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }

        public cvPoint2f()
        { }

        public cvPoint2f(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public cvPoint2f(PointF p)
        {
            this.X = p.X;
            this.Y = p.Y;
        }
        public Point ToPoint()
        {
            return new Point((int)this.X, (int)this.Y);
        }
        public PointF ToPointF()
        {
            return new PointF(this.X, this.Y);
        }
        public cvPoint2f(cvPoint2f p)
        {
            this.X = p.X;
            this.Y = p.Y;
        }

    }
    public class measureParam
    {
        public string parName = " ";
        public bool enabled;
        public float stdValue;
        public float toleranceL;
        public float toleranceH;

        public float resultVal;
        public bool m_result;



        public bool getParamResult(float measuredValue)
        {
            resultVal = measuredValue;
            if ((measuredValue >= (stdValue - toleranceL)) && (measuredValue <= (stdValue + toleranceH)))
            {
                m_result = true;
            }
            else
            {
                m_result = false;
            }
            return m_result;
        }

        public measureParam(String name)
        {
            parName = name;
            stdValue = 0; ;
            toleranceL = 0;
            toleranceH = 0;
            resultVal = -100;
            m_result = false;
        }
        public int updateMesureParam(String name, float stdVal, float toleranceLower, float toleranceHigher)
        {
            if (name == "")
            {
                Console.WriteLine("Measure parameter name cannot be blank");
                return -1;

            }
            else
            {
                parName = name;
                stdValue = stdVal;
                toleranceL = toleranceLower;
                toleranceH = toleranceHigher;
                resultVal = -100;
                m_result = false;
                return 1;
            }

        }
    }

    public enum Mark_ins_shape { ROI, Group, QR, DateCode, WeekCode, Mask, Fixture, CROI, Boundary_Gap, Gray_Presence }
    public enum p_FixtureType { Fixed, Fixture, Tool, Fixture_n_tool }
    public enum p_FixtureMode { Shift_n_rotation, Shift_only, Rotation_only }
    public class tool
    {
        int id;
        String name;
        int index;
        Mark_ins_shape tool_type;
        cvPoint centerLoc;
        cvPoint locationDetected;
        bool tool_result;
        DrawPbMmarkIns obj_toolShape;

        //new params
        int fixtureType; //no reference(fixed position), fixture( move wrt fixture), fixture and other tool(move wrt fixture then other tool), other tool (only other tool) 
        int fixtureToolReference;//id of other tool to be referred,
        int fixture_mode;

        public tool()
        {

        }
        public tool(String Name, Mark_ins_shape type)
        {
            this.name = Name;
            this.Tool_type = Mark_ins_shape.Mask;
            this.CenterLoc = new cvPoint(0, 0);// new Point(0, 0);
            this.LocationDetected = new cvPoint(0, 0);// new Point(0, 0);
            this.Tool_result = false;
            this.Obj_toolShape = new DrawPbMmarkIns();
            this.Obj_toolShape.shape = Mark_ins_shape.Mask;
            this.id = 0;
            this.FixtureType = (int)p_FixtureType.Fixture;
            this.Fixture_mode = (int)p_FixtureMode.Shift_n_rotation;
            this.FixtureToolReference = -1;
        }

        public string Name
        {
            get => name;
            set
            {
                name = value.Replace(" ", string.Empty); ;
            }
        }

        //public Point CenterLoc { get => centerLoc.ToPoint(); set => centerLoc = new cvPoint(value); }
        public cvPoint CenterLoc { get => centerLoc; set => centerLoc = value; }
        public int Index { get => index; set => index = value; }
        //  public Point LocationDetected { get => locationDetected.ToPoint(); set => locationDetected =new cvPoint(value); }
        public cvPoint LocationDetected { get => locationDetected; set => locationDetected = value; }
        public bool Tool_result { get => tool_result; set => tool_result = value; }
        public int Id { get => id; set => id = value; }
        public Mark_ins_shape Tool_type { get => tool_type; set => tool_type = value; }
        public DrawPbMmarkIns Obj_toolShape { get => obj_toolShape; set => obj_toolShape = value; }

        //new params
        public int FixtureType { get => fixtureType; set => fixtureType = value; }
        public int FixtureToolReference { get => fixtureToolReference; set => fixtureToolReference = value; }
        public int Fixture_mode { get => fixture_mode; set => fixture_mode = value; }

        // Deep copy method
        // Deep copy constructor
        public tool(tool other)
        {
            this.id = other.id;
            this.name = String.Copy(other.name);
            this.index = other.index;
            this.tool_type = other.tool_type;  // Assuming Mark_ins_shape is a value type
            this.centerLoc = new cvPoint(other.centerLoc);  // Assuming Point is a class with X and Y properties
            this.locationDetected = new cvPoint(other.locationDetected);  // Assuming Point is a class with X and Y properties
            this.tool_result = other.tool_result;
            this.obj_toolShape = other.obj_toolShape;  // Assuming DrawPbMmarkIns is a value type or immutable
            //new props
            this.fixture_mode = other.fixture_mode;
            this.fixtureType = other.fixtureType;
            this.fixtureToolReference = other.fixtureToolReference;
        }
    }

}
