using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryThresholds.IP_tools
{

    class colourRange_hsv
    {
        public string name = "Base";
        public String remark = "___";
        public int H_low = 0;
        public int S_low = 0;
        public int V_low = 0;
        public int H_high = 180;
        public int S_high = 255;
        public int V_high = 255;
        public int id = 0;

        public colourRange_hsv()
        {
            this.name = "Base";
            this.remark = "_______";
            H_low = 0;
            S_low = 0;
            V_low = 0;
            H_high = 180;
            S_high = 255;
            V_high = 255;
            id = 0;
        }

        public colourRange_hsv(string name, string remark, int h_low, int s_low, int v_low, int h_high, int s_high, int v_high)
        {
            this.name = name;
            this.remark = remark;
            H_low = h_low;
            S_low = s_low;
            V_low = v_low;
            H_high = h_high;
            S_high = s_high;
            V_high = v_high;
        }

        public void setLowRange(int h, int s, int v)
        {
            H_low = h;
            S_low = s;
            V_low = v;
        }

        public void setHighRange(int h, int s, int v)
        {
            H_high = h;
            S_high = s;
            V_high = v;
        }

        // Deep copy constructor
        public colourRange_hsv(colourRange_hsv original)
        {
            this.name = original.name;
            this.remark = original.remark;
            this.H_low = original.H_low;
            this.S_low = original.S_low;
            this.V_low = original.V_low;
            this.H_high = original.H_high;
            this.S_high = original.S_high;
            this.V_high = original.V_high;
            this.id = original.id;
        }
    }
    class userColours
    {
        public static List<colourRange_hsv> list_userColours = new List<colourRange_hsv>();
        public static int get_maxId()
        {
            int maxId = 0;

            foreach (var t in list_userColours) { if (t.id > maxId) { maxId = t.id; } }


            return maxId;
        }
        public static int deleteColour(int index)
        {
            if (index != -1 && index < list_userColours.Count())
            {
                list_userColours.RemoveAt(index);
            }
            return index;
        }
        //add user colour from list
        public static int addColour(colourRange_hsv c)
        {
            c.id = get_maxId() + 1;
            list_userColours.Add(c);
            return list_userColours.Last().id; //id of added element
        }

        public static int getIndexFromId(int id)
        {

            return list_userColours.FindIndex(x => x.id == id);
        }
        //edit user colour by passing index and new value 
        public static int editColour(int index, colourRange_hsv c)
        {
            //  int index = list_userColours.FindIndex(x => x.id == id);
            if (index != -1 && index < list_userColours.Count())
            {
                c.id = list_userColours[index].id;
                list_userColours[index] = c;
            }
            return index; //index of modified element
        }
        public static int count()
        {
            return list_userColours.Count();
        }
        public userColours()
        {

            //list_userColours.Clear();
            //colourRange_hsv blue = new colourRange_hsv("Blue","Sheet",96,69,25,112,229,255);
            //colourRange_hsv red = new colourRange_hsv("Red","Red CP",3,143,32,11,223,253);
            //colourRange_hsv black = new colourRange_hsv("Black","Camera",0,0,10,150,43,19);
            //addColour(blue);
            //addColour(red);
            //addColour(black);
        }

        //save and retrieve json files
        public static void Save_to_file(String filename, List<colourRange_hsv> obj_userTools)
        {

            String json = Newtonsoft.Json.JsonConvert.SerializeObject(obj_userTools, Formatting.Indented);
            System.IO.File.WriteAllText(filename, json);
            Console.WriteLine("Insert json file colours");
        }
        public static List<colourRange_hsv> Load_from_file(String fileName)
        {

            if (File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                List<colourRange_hsv> tmpCls = JsonConvert.DeserializeObject<List<colourRange_hsv>>(jsonString);
                // Console.WriteLine("file found {0}", jsonString);
                //  Console.WriteLine("loaded usercolours from file Cnt: " + tmpCls.list_userColours.Count.ToString());
                return tmpCls;
            }
            else
            {
                Console.WriteLine("file not found {0}", fileName);
                List<colourRange_hsv> tmpCls = new List<colourRange_hsv>();

                return tmpCls;
            }


        }
    }
}
