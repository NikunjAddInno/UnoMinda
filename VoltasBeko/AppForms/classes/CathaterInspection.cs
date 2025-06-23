using Measurement_AI.classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace CathaterTipInspection.classes
{
    class ins_region 
    {
        private Rectangle roi = new Rectangle(0, 0, 10, 10);
        private List<Point> rect_pts = new List<Point> { new Point(10, 10), new Point(80, 50) };
        private bool enabled = false;
        private int idx = 0;
        private string name = "";
        public Dictionary<string, float> Dict_thresholds = new Dictionary<string, float>();

        public Rectangle Roi { get => roi; set => roi = value; }
        public List<Point> Rect_pts { get => rect_pts; set => rect_pts = value; }
        public bool Enabled { get => enabled; set => enabled = value; }
        public int Idx { get => idx; set => idx = value; }
        public string Name { get => name; set => name = value; }
        // public Dictionary<string, float> Dict_thresholds { get => dict_thresholds; set => dict_thresholds = value; }
        public Rectangle getRectangleFromPts()
        {
            return  new Rectangle(
                  Math.Min(rect_pts[0].X, rect_pts[1].X),
                  Math.Min(rect_pts[0].Y, rect_pts[1].Y),
                  Math.Abs(rect_pts[1].X - rect_pts[0].X),
                  Math.Abs(rect_pts[1].Y - rect_pts[0].Y));
           
        }
        public ins_region(int idxIn, String nameIn,bool enabledIn)
        {
            Idx = idxIn;
            Roi = new Rectangle(0, 0, 10, 10);
            Rect_pts = new List<Point> { new Point(10, 10), new Point(80, 50) };
            Name = nameIn;
            Enabled = enabledIn;
        }
  


        public void updateRegionPoints(Point p1, Point p2)
        {
            Rect_pts[0] = p1;
            Rect_pts[1] = p2;
        }

        public List<Point> getRegionPoints()
        {
            return Rect_pts;
        }
    }
    class CathaterInspection
    {
        public List<measureParam> List_inspections = new List<measureParam>();
        private int inspectionParamCnt = 0;
        public List<ins_region> List_ins_roi = new List<ins_region>();
        private int cameraIdx = -1;
        private float camexposure = 5000;

       // public List<measureParam> List_inspections { get => list_inspections; set => list_inspections = value; }
        public int InspectionParamCnt { get => inspectionParamCnt; set => inspectionParamCnt = value; }
       // internal List<ins_region> List_ins_roi { get => list_ins_roi; set => list_ins_roi = value; }
        public int CameraIdx { get => cameraIdx; set => cameraIdx = value; }
        public float Camexposure { get => camexposure; set => camexposure = value; }

        public void  DefCathaterInspection()
        {
            // List_inspections.Clear();

            List_inspections.Add(new measureParam("A1"));
            List_inspections.Add(new measureParam("A2"));
            List_inspections.Add(new measureParam("A3"));
            List_inspections.Add(new measureParam("Tip Deviation"));
            List_inspections.Add(new measureParam("Tip Dia"));
            List_inspections.Add(new measureParam("Catheter Length"));
            List_inspections.Add(new measureParam("Burr Tolerance"));
            InspectionParamCnt = List_inspections.Count;

            ins_region temp_rgn = new ins_region(0, "Base", true);
            temp_rgn.Dict_thresholds.Add("Edge Threshold", 10);
            List_ins_roi.Add(temp_rgn);
            temp_rgn = new ins_region(1, "Mid", true);
            temp_rgn.Dict_thresholds.Add("Edge Threshold", 5);
            List_ins_roi.Add(temp_rgn);
            temp_rgn = new ins_region(1, "Tip", true);
            temp_rgn.Dict_thresholds.Add("Edge Threshold", 4);
            List_ins_roi.Add(temp_rgn);

            Camexposure = 5000;
            CameraIdx = -1;
        }

        public void printContent()
        {
            for (int k = 0; k < this.List_ins_roi.Count; k++)
            {
                Console.WriteLine("Point for region: " + k.ToString() + "  " + this.List_ins_roi[k].Rect_pts[0].X.ToString());
                Console.WriteLine("Point for region: " + k.ToString() + "  " + this.List_ins_roi[k].Rect_pts[0].Y.ToString());

                Console.WriteLine("Point for region: " + k.ToString() + "  " + this.List_ins_roi[k].Rect_pts[1].X.ToString());
                Console.WriteLine("Point for region: " + k.ToString() + "  " + this.List_ins_roi[k].Rect_pts[1].Y.ToString());
            }
        }
    }
    class toolUpdateFns
    {
        public static void Save_to_file(String modelFolderPath, int camIdx, CathaterInspection c, bool asDefault)
        {
            string jsonpath = "";
            if (asDefault)
            {
                jsonpath = "DefalutSetting.json";
            }
            else
            {
                jsonpath = modelFolderPath + "Cam" + camIdx.ToString() + "camSettings.json";

            }
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(c, Formatting.Indented);
            System.IO.File.WriteAllText(jsonpath, json);
            Console.WriteLine("Insert json file");
        }
        public static CathaterInspection loadModelFrom_file(String fileName)
        {

            if (File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                CathaterInspection tmpCls = JsonConvert.DeserializeObject<CathaterInspection>(jsonString);
                Console.WriteLine("file found {0}", jsonString);
                Console.WriteLine("tmpCls mesurements ls sz::{0}   Roi list size::{1}", tmpCls.List_inspections.Count, tmpCls.List_ins_roi.Count);
                Console.WriteLine("unable to print list sizes");
                for (int k = 0; k < tmpCls.List_ins_roi.Count; k++)
                {
                    Console.WriteLine("Point for region: " + k.ToString() + "  " + tmpCls.List_ins_roi[k].Rect_pts[0].X.ToString());
                    Console.WriteLine("Point for region: " + k.ToString() + "  " + tmpCls.List_ins_roi[k].Rect_pts[0].Y.ToString());

                    Console.WriteLine("Point for region: " + k.ToString() + "  " + tmpCls.List_ins_roi[k].Rect_pts[1].X.ToString());
                    Console.WriteLine("Point for region: " + k.ToString() + "  " + tmpCls.List_ins_roi[k].Rect_pts[1].Y.ToString());
                }
                return tmpCls;
            }
            else
            {
                Console.WriteLine("file not found {0}", fileName);
                CathaterInspection tmpCls = new CathaterInspection();
                tmpCls.DefCathaterInspection();
                return tmpCls;
            }
          
           
        }

        public static CathaterInspection LoadSettings(String modelFolderPath, int camIdx, bool defaultSett)
        {
            string path = modelFolderPath + "Cam" + camIdx.ToString() + "camSettings.json";
         
            if (defaultSett)
            { CathaterInspection tmpCls = loadModelFrom_file("DefalutSetting.json");
                return tmpCls;
            }
            else
            {

                CathaterInspection tmpCls = loadModelFrom_file(path);
                return tmpCls;
            }
         
        }
        public static int generateLayout_thresholds(ref Panel p, ins_region m)
        {
            p.Controls.Clear();
            Console.WriteLine("tool paramcount genLayout th :" + m.Dict_thresholds.Values.Count);

            String[] colNames = { "Parameter", "Value" };
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

            for (int i = 0; i < m.Dict_thresholds.Values.Count; i++)
            {

                CheckBox chk = new CheckBox();
                chk.Width = 16;
                chk.Location = new Point(8, offsetY + i * rowGap - 5); //parameter name
                chk.Checked = true;
                chk.Visible = false;
                chk.Tag = m.Dict_thresholds.Keys.ElementAt(i)+ "_en"; //parameter name
                p.Controls.Add(chk);
                Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.Width = colWidth;
                lbl.Text = m.Dict_thresholds.Keys.ElementAt(i);
                lbl.Location = new Point(offsetX, offsetY + i * rowGap); //parameter name
                p.Controls.Add(lbl);

                String[] paramControlSet_names = { "_std"};
                for (int c = 0; c < paramControlSet_names.Length; c++)
                {

                    NumericUpDown n = new NumericUpDown();
                    n.Width = 70;
                    n.Location = new Point(offsetX + (c + 1) * colWidth, offsetY + i * rowGap);
                    n.Tag = m.Dict_thresholds.Keys.ElementAt(i) + paramControlSet_names[c];// create unique tag for reference
                    n.DecimalPlaces = 0;
                    n.Increment = (decimal)1.0;
                    n.Minimum = (decimal)1.0;

                    if (c == 0)
                    {
                        n.Maximum = 50;
                    }
                    else
                    {
                        n.Maximum = 50;
                    }

                    p.Controls.Add(n);
                }




                //  p.Controls.Add(n);

                Console.WriteLine("added nud " + i.ToString());

            }
            return m.Dict_thresholds.Values.Count;
        }
        public static int updateParamValues_UI_to_Dict(ref Panel p, ref ins_region m)
        {
            String[] paramControlSet_names = { "_std", "_toln", "_tolp" };
            //  Console.WriteLine("tool name:" + m.toolName);
            Console.WriteLine("tool params list count :" + m.Dict_thresholds.Values.Count.ToString());
            for (int i = 0; i < m.Dict_thresholds.Values.Count; i++)
            {
                m.Dict_thresholds[m.Dict_thresholds.Keys.ElementAt(i)]= (float)((NumericUpDown)getControl_from_tag(ref p, m.Dict_thresholds.Keys.ElementAt(i) + "_std")).Value;

                m.Enabled = ((CheckBox)getControl_from_tag(ref p, m.Dict_thresholds.Keys.ElementAt(i) + "_en")).Checked;

                //Console.WriteLine("stdVAL :" + m.list_inspections[i].stdValue.ToString());
                //Console.WriteLine("tol - :" + m.list_inspections[i].toleranceL.ToString());
                //Console.WriteLine("tol + :" + m.list_inspections[i].toleranceH.ToString());
                //Console.WriteLine("enabled :" + m.list_inspections[i].enabled.ToString());
            }
            return 1;
        }

        // show DS data on UI panel
        public static int updateParamValues_Dict_to_UI(ref Panel p, ref ins_region m)
        {
            String[] paramControlSet_names = { "_std", "_toln", "_tolp" };
            //Console.WriteLine("tool name:" + m.toolName);
            Console.WriteLine("tool params list count :" + m.Dict_thresholds.Values.Count.ToString());
            for (int i = 0; i < m.Dict_thresholds.Values.Count; i++)
            {
                setControlVal_from_tag(ref p, m.Dict_thresholds.Keys.ElementAt(i) + "_std", m.Dict_thresholds[m.Dict_thresholds.Keys.ElementAt(i)]);
           //     setControlVal_from_tag(ref p, m.list_inspections[i].parName + "_toln", m.list_inspections[i].toleranceL);
             //   setControlVal_from_tag(ref p, m.list_inspections[i].parName + "_tolp", m.list_inspections[i].toleranceH);
                setControlVal_from_tag(ref p, m.Dict_thresholds.Keys.ElementAt(i) + "_en",m.Enabled);

                //Console.WriteLine("stdVAL :" + m.list_inspections[i].stdValue.ToString());
                //Console.WriteLine("tol - :" + m.list_inspections[i].toleranceL.ToString());
                //Console.WriteLine("tol + :" + m.list_inspections[i].toleranceH.ToString());
                //Console.WriteLine("enabled :" + m.list_inspections[i].enabled.ToString());
            }
            return 1;
        }
        public static int generateLayout(ref Panel p, CathaterInspection m)
        {
            p.Controls.Clear();
            Console.WriteLine("tool paramcount gen Layout params :" + m.InspectionParamCnt.ToString());

            String[] colNames = { "Parameter", "Std", "Tol -", "Tol +" };
            int colWidth = 140;// 90;
            int rowGap = 30;
            int offsetX = 30;// 30;
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

            for (int i = 0; i < m.InspectionParamCnt; i++)
            {

                CheckBox chk = new CheckBox();
                chk.Width = 16;
                chk.Location = new Point(8, offsetY + i * rowGap - 5); //parameter name
                chk.Checked = true;
                chk.Tag = m.List_inspections[i].parName + "_en"; //parameter name
                p.Controls.Add(chk);
                Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.Width = colWidth;
                lbl.Text = m.List_inspections[i].parName;
                lbl.Location = new Point(offsetX, offsetY + i * rowGap); //parameter name
                p.Controls.Add(lbl);

                String[] paramControlSet_names = { "_std", "_toln", "_tolp" };
                for (int c = 0; c < paramControlSet_names.Length; c++)
                {

                    NumericUpDown n = new NumericUpDown();
                    n.Width = 70;
                    n.Location = new Point(offsetX + (c + 1) * colWidth, offsetY + i * rowGap);
                    n.Tag = m.List_inspections[i].parName + paramControlSet_names[c];// create unique tag for reference
                    n.DecimalPlaces = 2;
                    n.Increment = (decimal)1.0;
                    n.Minimum = (decimal)0.0;

                    if (c == 0)
                    {
                        n.Maximum = 500;
                    }
                    else
                    {
                        n.Maximum = 500;
                    }

                    p.Controls.Add(n);
                }




                //  p.Controls.Add(n);

                Console.WriteLine("added nud " + i.ToString());

            }
            return m.InspectionParamCnt;
        }

        public static Control getControl_from_tag(ref Panel p, String tag)
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
        public static Control setControlVal_from_tag(ref Panel p, String tag, float value)
        {
            for (int k = 0; k < p.Controls.Count; k++)
            {
                //Console.WriteLine("control tag :" +(string) p.Controls[k].Tag);
                if (tag.Equals((string)p.Controls[k].Tag))
                {
                    ((NumericUpDown)p.Controls[k]).Value = (decimal)value;
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
                    ((CheckBox)p.Controls[k]).Checked = value;
                }
            }
            return null;
        }

        // save parameters set by user in UI panel to a DS
        public static int updateParamValues_UI_to_list(ref Panel p, ref CathaterInspection m)
        {
            String[] paramControlSet_names = { "_std", "_toln", "_tolp" };
          //  Console.WriteLine("tool name:" + m.toolName);
            Console.WriteLine("tool params list count :" + m.List_inspections.Count.ToString());
            for (int i = 0; i < m.InspectionParamCnt; i++)
            {
                m.List_inspections[i].stdValue = (float)((NumericUpDown)getControl_from_tag(ref p, m.List_inspections[i].parName + "_std")).Value;
                m.List_inspections[i].toleranceL = (float)((NumericUpDown)getControl_from_tag(ref p, m.List_inspections[i].parName + "_toln")).Value;
                m.List_inspections[i].toleranceH = (float)((NumericUpDown)getControl_from_tag(ref p, m.List_inspections[i].parName + "_tolp")).Value;
                m.List_inspections[i].enabled = ((CheckBox)getControl_from_tag(ref p, m.List_inspections[i].parName + "_en")).Checked;

                Console.WriteLine("stdVAL :" + m.List_inspections[i].stdValue.ToString());
                Console.WriteLine("tol - :" + m.List_inspections[i].toleranceL.ToString());
                Console.WriteLine("tol + :" + m.List_inspections[i].toleranceH.ToString());
                Console.WriteLine("enabled :" + m.List_inspections[i].enabled.ToString());
            }
            return 1;
        }

        // show DS data on UI panel
        public static int updateParamValues_list_to_UI(ref Panel p, ref CathaterInspection m)
        {
            String[] paramControlSet_names = { "_std", "_toln", "_tolp" };
            //Console.WriteLine("tool name:" + m.toolName);
            Console.WriteLine("tool params list count :" + m.List_inspections.Count.ToString());
            for (int i = 0; i < m.InspectionParamCnt; i++)
            {
                setControlVal_from_tag(ref p, m.List_inspections[i].parName + "_std", m.List_inspections[i].stdValue);
                setControlVal_from_tag(ref p, m.List_inspections[i].parName + "_toln", m.List_inspections[i].toleranceL);
                setControlVal_from_tag(ref p, m.List_inspections[i].parName + "_tolp", m.List_inspections[i].toleranceH);
                setControlVal_from_tag(ref p, m.List_inspections[i].parName + "_en", m.List_inspections[i].enabled);

                Console.WriteLine("stdVAL :" + m.List_inspections[i].stdValue.ToString());
                Console.WriteLine("tol - :" + m.List_inspections[i].toleranceL.ToString());
                Console.WriteLine("tol + :" + m.List_inspections[i].toleranceH.ToString());
                Console.WriteLine("enabled :" + m.List_inspections[i].enabled.ToString());
            }
            return 1;
        }
    }
}
