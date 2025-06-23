using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Measurement_AI.classes
{
    public enum toolType { Arc, Width, Circle, Thread,Angle , Locate, Match, Inner_Hex, Outer_Hex }

    //class defines parameters and their limits in a tool
    class measureParam
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
        public int  updateMesureParam(String name, float stdVal, float toleranceLower, float toleranceHigher)
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

    //class defining different types of tools
    class MeasurementTools
    {
        public String toolName = "Default";

        public toolType type = toolType.Width;
        public int pointCount;
        public int paramCount;
        public List<PointF> inputPoints = new List<PointF>() ;

        public List<measureParam> listParameters = new List<measureParam>();

        public List<PointF> detectionPoints = new List<PointF>();//actual positions detected by image processing
        public List<PointF> resultPoints = new List<PointF>(); //result location for tool to tool reference if any
        public bool tool_result;
        public bool getToolResult()
        {
            
            foreach (measureParam p in listParameters)
            {
                if(p.getParamResult(p.resultVal))
                {
                    tool_result = false;
                    return false;
                }
            }
            tool_result = true;
            return tool_result;
        }

        public MeasurementTools(String name, toolType toolT)
        {
            toolName = name;
            type = toolT;
            switch (toolT)
            {
                case toolType.Circle:
                    {
                        pointCount = 2;
                       // listParameters.Clear();
                        listParameters.Add(new measureParam("Diameter"));
                        //listParameters.Add(new measureParam("Circularity"));
                        //listParameters.Add(new measureParam("Deviation"));
                        paramCount = listParameters.Count();
                        break;
                    }
                case toolType.Arc:
                    {
                        pointCount = 3;
                        listParameters.Add(new measureParam("Diameter"));
                        listParameters.Add(new measureParam("Radius"));//june22
                        listParameters.Add(new measureParam("C Dist. from Base"));//june22
                        paramCount = listParameters.Count();
                        break;
                    }
                case toolType.Width:
                    {
                        pointCount = 3;
                        listParameters.Add(new measureParam("Value"));
                        listParameters.Add(new measureParam("L Dist. from Ball"));//june22
                        listParameters.Add(new measureParam("L Dist. from Base"));//june22
                        listParameters.Add(new measureParam("P Dist. from Base"));//june22
                        paramCount = listParameters.Count();
                        break;
                    }
                case toolType.Thread:
                    {
                        pointCount = 2;
                        listParameters.Add(new measureParam("Thread Count"));
                        listParameters.Add(new measureParam("Pitch"));
                        listParameters.Add(new measureParam("Depth"));
                        paramCount = listParameters.Count();
                        break;
                    }
                case toolType.Angle:
                    {
                        pointCount = 4;
                        listParameters.Add(new measureParam("Value"));
                        paramCount = listParameters.Count();
                        break;
                    }
                case toolType.Locate:
                    {
                        pointCount = 2;
                        //listParameters.Add(new measureParam("Value"));
                        paramCount = listParameters.Count();
                        break;
                    }

                case toolType.Match:
                    {
                        pointCount = 2;
                        //listParameters.Add(new measureParam("Value"));
                        listParameters.Add(new measureParam("Score"));
                        paramCount = listParameters.Count();
                        break;
                    }
                case toolType.Inner_Hex:
                    {
                        pointCount = 2;
                        //listParameters.Add(new measureParam("Value"));
                        listParameters.Add(new measureParam("Size"));
                        listParameters.Add(new measureParam("Depth"));
                        paramCount = listParameters.Count();
                        break;
                    }
                case toolType.Outer_Hex:
                    {
                        pointCount = 2;
                        //listParameters.Add(new measureParam("Value"));
                        listParameters.Add(new measureParam("Size"));
                        paramCount = listParameters.Count();
                        break;
                    }
            }
            Console.WriteLine("Selected tool :" + toolT.ToString());
            Console.WriteLine("tool instance created : total parameters  " + paramCount.ToString());
        }

        public int updateInputPoints(List<Point> listInP)
        {
            if (listInP.Count <= pointCount)
            {
                inputPoints.Clear();
                // for json test only
                detectionPoints.Clear();
                resultPoints.Clear();

                //
                foreach (Point p in listInP)
                {
                    inputPoints.Add(p);
                    Console.WriteLine("Point added " + inputPoints.Last().ToString());
                    // for json test only
                    detectionPoints.Add(p);
                    resultPoints.Add(p);

                //
                }

            }
            return inputPoints.Count;
        }



    }

    class cpp_toolUpdateFns 
    {
        //create input panel for tool based on tool 
        public static int  generateLayout(ref Panel p,MeasurementTools m)
        {
            p.Controls.Clear();
            Console.WriteLine("tool paramcount :" + m.paramCount.ToString());

            String[] colNames = { "Parameter","Std", "Tol -", "Tol +" };
            int colWidth = 90;
            int rowGap = 30;
            int offsetX = 30;
            int offsetY = 10;

            for (int h = 0; h < colNames.Length; h++) //add headers to control columns
            {
                Label tmpLbl = new Label();
                tmpLbl.AutoSize = false;
                tmpLbl.Width = colWidth;
                tmpLbl.Text = colNames[h];
                tmpLbl.Location = new Point(offsetX + h * colWidth, offsetY);
                p.Controls.Add(tmpLbl);
            }

            offsetY = offsetY + 30;
            
            for (int i = 0; i < m.paramCount; i++)
            {

                CheckBox chk = new CheckBox();
                chk.Width = 16;
                chk.Location = new Point(8, offsetY + i * rowGap-5); //parameter name
                chk.Tag = m.listParameters[i].parName+"_en"; //parameter name
                p.Controls.Add(chk);
                Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.Width =colWidth;
                lbl.Text = m.listParameters[i].parName;
                lbl.Location = new Point(offsetX, offsetY +i *rowGap); //parameter name
                p.Controls.Add(lbl);

                String[] paramControlSet_names = { "_std", "_toln", "_tolp" };
                for (int c = 0 ; c < paramControlSet_names.Length;c++)
                    {
                        
                        NumericUpDown n = new NumericUpDown();
                        n.Width = 70;
                        n.Location = new Point(offsetX + (c+1) * colWidth, offsetY + i * rowGap);
                        n.Tag = m.listParameters[i].parName + paramControlSet_names[c];// create unique tag for reference
                        n.DecimalPlaces = 3;
                        n.Increment = (decimal)0.005;
                        n.Minimum = (decimal)0.0;
                      
                    if (c==0)
                    {
                        n.Maximum = 110;
                    }
                    else
                    {
                        n.Maximum = 30;
                    }

                        p.Controls.Add(n);
                    }
                



              //  p.Controls.Add(n);
        
                Console.WriteLine("added nud " + i.ToString());
            
            }
            return m.paramCount;
        }

        //internal function to get control from tag
        public static Control getControl_from_tag(ref Panel p,String tag)
        {
            for (int k = 0; k < p.Controls.Count; k++)
            {
                //Console.WriteLine("control tag :" +(string) p.Controls[k].Tag);
                if (tag.Equals((string)p.Controls[k].Tag))
                {
                    return p.Controls[k];
                }
            }
            return null;
        }

        //set value of a control using its tag
        //overalod for float values
        public static Control setControlVal_from_tag(ref Panel p, String tag,float value)
        {
            for (int k = 0; k < p.Controls.Count; k++)
            {
                //Console.WriteLine("control tag :" +(string) p.Controls[k].Tag);
                if (tag.Equals((string)p.Controls[k].Tag))
                {
                    ((NumericUpDown)p.Controls[k]).Value =(decimal) value;
                }
            }
            return null;
        }
        //overalod for float bool
        public static Control setControlVal_from_tag(ref Panel p, String tag, bool value)
        {
            for (int k = 0; k < p.Controls.Count; k++)
            {
                //Console.WriteLine("control tag :" +(string) p.Controls[k].Tag);
                if (tag.Equals((string)p.Controls[k].Tag))
                {
                    ((CheckBox )p.Controls[k]).Checked = value;
                }
            }
            return null;
        }

        // save parameters set by user in UI panel to a DS
        public static int updateParamValues_UI_to_list(ref Panel p,ref MeasurementTools m)
        {
            String[] paramControlSet_names = { "_std", "_toln", "_tolp" };
            Console.WriteLine("tool name:" + m.toolName);
            Console.WriteLine("tool params list count :" + m.listParameters.Count .ToString());
            for (int i = 0; i < m.paramCount; i++)
            {
                m.listParameters[i].stdValue = (float)((NumericUpDown)getControl_from_tag(ref p, m.listParameters[i].parName + "_std")).Value;
                m.listParameters[i].toleranceL = (float)((NumericUpDown)getControl_from_tag(ref p, m.listParameters[i].parName + "_toln")).Value;
                m.listParameters[i].toleranceH = (float)((NumericUpDown)getControl_from_tag(ref p, m.listParameters[i].parName + "_tolp")).Value;
                m.listParameters[i].enabled = ((CheckBox)getControl_from_tag(ref p, m.listParameters[i].parName + "_en")).Checked;

                Console.WriteLine("stdVAL :" + m.listParameters[i].stdValue.ToString());
                Console.WriteLine("tol - :" + m.listParameters[i].toleranceL.ToString());
                Console.WriteLine("tol + :" + m.listParameters[i].toleranceH.ToString());
                Console.WriteLine("enabled :" + m.listParameters[i].enabled.ToString());
            }
            return 1;
        }

        // show DS data on UI panel
        public static int updateParamValues_list_to_UI(ref Panel p, ref MeasurementTools m)
        {
            String[] paramControlSet_names = { "_std", "_toln", "_tolp" };
            Console.WriteLine("tool name:" + m.toolName);
            Console.WriteLine("tool params list count :" + m.listParameters.Count.ToString());
            for (int i = 0; i < m.paramCount; i++)
            {
                setControlVal_from_tag(ref p, m.listParameters[i].parName + "_std", m.listParameters[i].stdValue);
                setControlVal_from_tag(ref p, m.listParameters[i].parName + "_toln", m.listParameters[i].toleranceL);
                setControlVal_from_tag(ref p, m.listParameters[i].parName + "_tolp", m.listParameters[i].toleranceH);
                setControlVal_from_tag(ref p, m.listParameters[i].parName + "_en", m.listParameters[i].enabled);

                Console.WriteLine("stdVAL :" + m.listParameters[i].stdValue.ToString());
                Console.WriteLine("tol - :" + m.listParameters[i].toleranceL.ToString());
                Console.WriteLine("tol + :" + m.listParameters[i].toleranceH.ToString());
                Console.WriteLine("enabled :" + m.listParameters[i].enabled.ToString());
            }
            return 1;
        }
    }



}
