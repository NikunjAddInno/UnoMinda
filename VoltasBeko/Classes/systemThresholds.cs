using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace tryThresholds
{

    class cls_KVPdata 
    {
        private string name; // name of key
        public float maxVal=100; //upper range of value
        public float minVal=0;// lower range of value
        float value_k=1;// value
        int scope=0;//threshold accessible in c#=0 or cpp=1

        public string Name
        {
            get => name;
            set
            {
                if (value != null)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (char c in value)
                    {
                        if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_')
                        {
                            sb.Append(c);
                        }
                    }
                    name = sb.ToString();
                }
                else
                    name = value;
            }
        }
        public cls_KVPdata(string a_name, float a_val, int a_scope = 0, float a_minVal = 0, float a_maxVal = 255)
        {
            Name = a_name;
            Scope = a_scope;
            minVal = a_minVal;
            maxVal = a_maxVal;
            Value = a_val;
        }
        public float Value
        {
            get => value_k;
            set
            {
                if (value <= maxVal && value >= minVal)
                    value_k = value;
                else
                    value_k = minVal;
            }
        }
        public int Scope
        {
            get => scope;
            set
            {
                if (value >= 0 && value <= 1)
                    scope = value;
                else
                    scope = 0;
            }
        }

        public float MaxVal { get => maxVal; }
        public float MinVal { get => minVal; }
      //  public string Name { get => name; set => name = value; }
    }
    class systemThresholds
    {
        private string instanceName = "";
        public List<cls_KVPdata> list_kvp = new List<cls_KVPdata>();

        public string InstanceName { get => instanceName; set => instanceName = value; }

        public systemThresholds(string instanceName = null) //initialize the list with all defined kvps
        {
            list_kvp = new List<cls_KVPdata>();
            this.instanceName = instanceName;
        }
                
        public void loadDefaultKVP() //one time use to create json files.. Edit this method according to application & create new json
        {
            list_kvp = new List<cls_KVPdata>();
            list_kvp.Add(new cls_KVPdata("cam1_exposure", 5000, 0, 100, 100000));
            list_kvp.Add(new cls_KVPdata("thresh_imageIn", 50,1));
            list_kvp.Add(new cls_KVPdata("thresh_imageRot", 70,1,0,255));
            list_kvp.Add(new cls_KVPdata("numCameras",3, 0, 1,5));
            list_kvp.Add(new cls_KVPdata("th_sobel", 40,1));
            list_kvp.Add(new cls_KVPdata("edgeThickness", 100,1));
            Console.WriteLine("list initialized");

        }
        public float getValue(string key)
        {
            foreach (var s in this.list_kvp)
            {
                if (s.Name.Equals(key))
                    return s.Value;
            }
            return -1;
        }
        public int getRange(string key, out int lb , out int ub)
        {
            foreach (var s in this.list_kvp)
            {
                if (s.Name.Equals(key))
                {
                    lb = (int)s.MinVal;
                    ub = (int)s.MaxVal;
                    return 1;
                }
            }
            lb = -1;
            ub = -1;
            return -1;
        }


        public float setValue(string key, float valueIn)
        {
            foreach (var s in this.list_kvp)
            {
                if (s.Name.Equals(key))
                {
                    s.Value = valueIn;
                    return 1;
                }
            }
            return -1;
        }

        public systemThresholds DeepCopy()
        {
            systemThresholds cls_dc = new systemThresholds(this.instanceName);

            cls_dc.list_kvp.Clear();
            foreach (var data in this.list_kvp)
            {
                cls_dc.list_kvp.Add(new cls_KVPdata(data.Name,data.Value,data.Scope,data.MinVal,data.MaxVal));
            }
            return cls_dc;
        }

    }

    class updateMethods
    {
        public static  int readJsonFromFile(out systemThresholds obj ,string fileName)
        {
            try
            {
                obj =JsonConvert.DeserializeObject<systemThresholds>(File.ReadAllText(fileName));
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in reading json from disk" + ex.Message);
                obj = null;
                return -1;
            }
        }
        public static int saveToJson(DataGridView dg,ref systemThresholds thBase, systemThresholds thTemp, string fileName)
        {
            for (int i = 0; i < dg.Rows.Count - 1; i++)
            {
                if ((dg.Rows[i].Cells[1].Style.BackColor == Color.Red) || (dg.Rows[i].Cells[0].Style.BackColor == Color.Red))
                {
                    MessageBox.Show("Data not saved." + Environment.NewLine + "Please correct all values before saving");
                    
                    return -1;
                }
            }
            thBase = thTemp.DeepCopy();
            string Result = JsonConvert.SerializeObject(thBase);
            File.WriteAllText(fileName, Result);
            MessageBox.Show("Settings saved Successfully");
            return 1;
        }

        public static void onDGridUpdate(DataGridView dg, int row, int col, systemThresholds thTemp)
        {
            if (row < 0 || col < 0)
                return;
            //Console.WriteLine("row:" + e.RowIndex.ToString() + "  colIdx:" + e.ColumnIndex.ToString() + " val:" + dGridKVP.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            try
            {
                foreach (char c in dg.Rows[row].Cells[col].Value.ToString())
                {
                    if ((c < '0' || c > '9') && (c != '.'))
                    {
                        dg.Rows[row].Cells[col].Style.BackColor = Color.Red;
                        return; //invalid character
                    }
                }

                float value = float.Parse(dg.Rows[row].Cells[1].Value.ToString());
                string key = dg.Rows[row].Cells[0].Value.ToString();
                if (thTemp.setValue(key, value) == -1)
                {
                    dg.Rows[row].Cells[0].Style.BackColor = Color.Red;
                    return; //no key found
                }
                float updatedVal = thTemp.getValue(key);
                Console.WriteLine("updatedVal : " + updatedVal.ToString());
                dg.Rows[row].Cells[1].Value = updatedVal.ToString();
                if (thTemp.getValue(key) != value)
                {
                    dg.Rows[row].Cells[1].Style.BackColor = Color.Red; //value out of range
                    int lb, ub;
                    thTemp.getRange(key, out lb, out ub);
                    MessageBox.Show("Value of parameter " + key + " is out of allowed range (" + lb.ToString() + "-" + ub.ToString() + ")");
                }
                else
                    dg.Rows[row].Cells[1].Style.BackColor = Color.White;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void refresh_D_Grid(DataGridView dg, systemThresholds thBase)
        {
            dg.Rows.Clear();
            foreach (var item in thBase.list_kvp)
            {
                dg.Rows.Add(item.Name, item.Value);
            }
        }
    }
}
