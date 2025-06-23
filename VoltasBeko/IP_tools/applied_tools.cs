using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tryThresholds.IP_tools
{
    public class applied_tools
    {
        public String markConfigname;//1
        public long markConfigId;//2
        public int subPCno;//7
        public cvRect fullROI;
        public float roi_rotation;
        public int roi_flipMode;//added later 0 mirror, 1 y , 2 both 3 none
        public String roi_fileName;
        public String glass_templateFileName;//6
        public String rotatedROIFileName;
        public cvRect glassLocation;//3
        public float logoShiftTolX;//4
        public float logoShiftTolY;//5
        public List<tool_ROI> lst_roiTools;
        public List<tool_QR_code> lst_QR_tools;
        public List<tool_Group_printCheck> lst_Group_printCheck_tools;
        public List<tool_DateCode_DOTS_OCR> lst_Datecode_tools;
        public List<tool_WeekCode_DOTS> lst_WeekCode_tools;
        public List<tool_Mask> lst_Mask_tools;
        public List<tool_Fixture> lst_Fixture_tools;
        public List<tool_CROI> lst_CROI_tool;
        public List<tool_BoundaryGap> lst_BoundaryGap_tools;
        public List<tool_GrayPresence> lst_GrayPresence_tools;

        int appliedToolCnt;
    

        //constructor
        public applied_tools()
        {
            this.markConfigname = "";
            this.markConfigId = 0;
            this.subPCno = 1;

            this.fullROI = new cvRect(0, 0, 10, 10);// new  Rectangle(0, 0, 10, 10);
            this.roi_rotation = 0;
            this.roi_flipMode = 0;//default x flip

            this.roi_fileName = "";
            this.lst_roiTools = new List<tool_ROI>();
            this.lst_QR_tools = new List<tool_QR_code>();
            this.lst_Group_printCheck_tools = new List<tool_Group_printCheck>();
            this.lst_Datecode_tools = new List<tool_DateCode_DOTS_OCR>();
            this.lst_WeekCode_tools = new List<tool_WeekCode_DOTS>();
            this.lst_Mask_tools = new List<tool_Mask>();
            this.lst_Fixture_tools = new List<tool_Fixture>();
            this.lst_CROI_tool = new List<tool_CROI>();
            this.lst_BoundaryGap_tools = new List<tool_BoundaryGap>();
            this.lst_GrayPresence_tools = new List<tool_GrayPresence>();

            this.glassLocation = new cvRect(-1, -1, 10, 10);
            this.logoShiftTolX = 0;
            this.logoShiftTolY = 0;
            this.glass_templateFileName = "";
            this.rotatedROIFileName = "";
            
            appliedToolCnt = 0;

        }

        //total applied tools property
        public int AppliedToolCnt
        {
            get
            {
                appliedToolCnt = lst_roiTools.Count + lst_QR_tools.Count + lst_Group_printCheck_tools.Count+ lst_Datecode_tools.Count+ lst_WeekCode_tools.Count+ lst_Mask_tools.Count+lst_Fixture_tools.Count+ lst_CROI_tool.Count+lst_BoundaryGap_tools.Count+lst_GrayPresence_tools.Count;
                return appliedToolCnt;// (lst_roiTools.Count+lst_QR_tools.Count+lst_Group_printCheck_tools.Count); 
            }
        }
        //save and retrieve json files
        public static void Save_to_file(String filename,applied_tools obj_userTools)
        {
   
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(obj_userTools, Formatting.Indented);
            System.IO.File.WriteAllText(filename, json);
            Console.WriteLine("Insert json file");
        }

        public static int deleteUnusedTemplates(String currentmodelPath)
        {
            try
            {
                string[] bmpFiles = Directory.GetFiles(currentmodelPath, "*.bmp");
                string jsonText = File.ReadAllText(currentmodelPath + "/MarkInsTools.json");
                // Parse the JSON text
                JObject jsonObject = JObject.Parse(jsonText);
                // Check if the text exists in the JSON
                // Display the file names
                foreach (string file in bmpFiles)
                {
                    //Console.WriteLine(Path.GetFileName(file));
                    String filename = Path.GetFileName(file);
                    bool textExists = jsonObject.ToString().Contains(filename);

                    if (textExists)
                    {
                        Console.WriteLine($"{filename} +++++++exists in the JSON file.");
                    }
                    else
                    {
                        File.Delete(file);
                        Console.WriteLine($"{filename} -----------------does not exist in the JSON file.");
                    }
                }

               // return 1;
            }
            catch (Exception exx)
            {
                //MessageBox.Show("Exception in deleteTemplate fn " + exx.Message);
                Console.WriteLine("Exception in deleteTemplate fn " + exx.Message);
                return 0;
            }
            try
            {
                string[] bmpFiles = Directory.GetFiles(currentmodelPath+"/c2", "*.bmp");
                string jsonText = File.ReadAllText(currentmodelPath + "/c2/MarkInsTools.json");
                // Parse the JSON text
                JObject jsonObject = JObject.Parse(jsonText);
                // Check if the text exists in the JSON
                // Display the file names
                foreach (string file in bmpFiles)
                {
                    //Console.WriteLine(Path.GetFileName(file));
                    String filename = Path.GetFileName(file);
                    bool textExists = jsonObject.ToString().Contains(filename);

                    if (textExists)
                    {
                        Console.WriteLine($"{filename} +++++++exists in the JSON file.");
                    }
                    else
                    {
                        File.Delete(file);
                        Console.WriteLine($"{filename} -----------------does not exist in the JSON file.");
                    }
                }

                
            }
            catch (Exception exx)
            {
                //MessageBox.Show("Exception in deleteTemplate fn " + exx.Message);
                Console.WriteLine("Exception in deleteTemplate fn " + exx.Message);
                return 0;
            }
            return 1;
        }
        public static applied_tools Load_from_file(String fileName)
        {

            if (File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                applied_tools tmpCls = JsonConvert.DeserializeObject<applied_tools>(jsonString);
               // Console.WriteLine("file found {0}", jsonString);

             
                    Console.WriteLine("ROI tool Cnt: " + tmpCls.lst_roiTools.Count.ToString());
                    Console.WriteLine("QR tool Cnt: " + tmpCls.lst_QR_tools.Count.ToString());
                    Console.WriteLine("Group tool Cnt: " + tmpCls.lst_Group_printCheck_tools.Count.ToString());
                    Console.WriteLine("Group tool Cnt: " + tmpCls.lst_Datecode_tools.Count.ToString());
                    Console.WriteLine("Group tool Cnt: " + tmpCls.lst_WeekCode_tools.Count.ToString());
                    Console.WriteLine("Group tool Cnt: " + tmpCls.lst_Mask_tools.Count.ToString());
                    Console.WriteLine("Group fixture Cnt: " + tmpCls.lst_Fixture_tools.Count.ToString());
                    Console.WriteLine("Group CROI Cnt: " + tmpCls.lst_CROI_tool.Count.ToString());
                    Console.WriteLine("Group BoundaryGap Cnt: " + tmpCls.lst_BoundaryGap_tools.Count.ToString());
                    Console.WriteLine("Group GrayPresence Cnt: " + tmpCls.lst_GrayPresence_tools.Count.ToString());
                    
                return tmpCls;
            }
            else
            {
                Console.WriteLine("file not found {0}", fileName);
                applied_tools tmpCls = new applied_tools();
                
                return tmpCls;
            }


        }

        //returns max id --used to assign new unique id to tool
        public int get_maxId()
        {
            int maxId = 0;

            foreach (var t in lst_roiTools) { if (t.Id > maxId) { maxId = t.Id; } }
            foreach (var t in lst_QR_tools) { if (t.Id > maxId) { maxId = t.Id; } }
            foreach (var t in lst_Group_printCheck_tools) { if (t.Id > maxId) { maxId = t.Id; } }
            foreach (var t in lst_Datecode_tools) { if (t.Id > maxId) { maxId = t.Id; } }
            foreach (var t in lst_WeekCode_tools) { if (t.Id > maxId) { maxId = t.Id; } }
            foreach (var t in lst_Mask_tools) { if (t.Id > maxId) { maxId = t.Id; } }
            foreach (var t in lst_Fixture_tools) { if (t.Id > maxId) { maxId = t.Id; } }
            foreach (var t in lst_CROI_tool) { if (t.Id > maxId) { maxId = t.Id; } }
            foreach (var t in lst_BoundaryGap_tools) { if (t.Id > maxId) { maxId = t.Id; } }
            foreach (var t in lst_GrayPresence_tools) { if (t.Id > maxId) { maxId = t.Id; } }

            return maxId;
        }
        
        //return list of tool ID
        public List< int> get_toolID_list()
        {
            List<int> list_toolID = new List<int>();

            foreach (var t in lst_roiTools) { list_toolID.Add(t.Id); }
            foreach (var t in lst_QR_tools) { list_toolID.Add(t.Id); }
            foreach (var t in lst_Group_printCheck_tools) { list_toolID.Add(t.Id); }
            foreach (var t in lst_Datecode_tools) { list_toolID.Add(t.Id); }
            foreach (var t in lst_WeekCode_tools) { list_toolID.Add(t.Id); }
            foreach (var t in lst_Mask_tools) { list_toolID.Add(t.Id); }
            foreach (var t in lst_Fixture_tools) { list_toolID.Add(t.Id); }
            foreach (var t in lst_CROI_tool) { list_toolID.Add(t.Id); }
            foreach (var t in lst_BoundaryGap_tools) { list_toolID.Add(t.Id); }
            foreach (var t in lst_GrayPresence_tools) { list_toolID.Add(t.Id); }

            return list_toolID;
        }

        //add tool overload for each tool type
        #region add/create_tool_overload_functions
        public int addTool(tool_ROI tIn)
        {
            tool_ROI t = new tool_ROI(tIn);

            t.Id = get_maxId() + 1;
            lst_roiTools.Add(t);
            Console.WriteLine($"item added to roiList with ID{t.Id}");
            for (int k = 0; k < lst_roiTools.Count; k++)
            {
                Console.WriteLine($"id of element {k} is {lst_roiTools[k].Id} and toolName::{lst_roiTools[k].Name}");
            }
            return 1;
        }
        static int cnt = 0;
        public int addTool(tool_QR_code tIn)
        {
            tool_QR_code t = new tool_QR_code(tIn);
            t.Id = get_maxId() + 1;
            lst_QR_tools.Add(t);

            return 1;
        }
        public int addTool(tool_Group_printCheck tIn)
        {
            tool_Group_printCheck t = new tool_Group_printCheck(tIn);
            t.Id = get_maxId() + 1;
            lst_Group_printCheck_tools.Add(t);
            return 1;
        }
        public int addTool(tool_DateCode_DOTS_OCR tIn)
        {
            tool_DateCode_DOTS_OCR t = new tool_DateCode_DOTS_OCR(tIn);
            t.Id = get_maxId() + 1;
            lst_Datecode_tools.Add(t);
            return 1;
        }
        public int addTool(tool_WeekCode_DOTS tIn)
        {
            tool_WeekCode_DOTS t = new tool_WeekCode_DOTS(tIn);
            t.Id = get_maxId() + 1;
            lst_WeekCode_tools.Add(t);
            return 1;
        }
        public int addTool(tool_Mask tIn)
        {
            tool_Mask t = new tool_Mask(tIn);
            t.Id = get_maxId() + 1;
            lst_Mask_tools.Add(t);
            return 1;
        }
        public int addTool(tool_Fixture tIn)
        {
            tool_Fixture t = new tool_Fixture(tIn);
            t.Id = get_maxId() + 1;
            lst_Fixture_tools.Add(t);
            return 1;
        }
        public int addTool(tool_CROI tIn)
        {
            tool_CROI t = new tool_CROI(tIn);
            t.Id = get_maxId() + 1;
            lst_CROI_tool.Add(t);
            return 1;
        }
        public int addTool(tool_BoundaryGap tIn)
        {
            tool_BoundaryGap t = new tool_BoundaryGap(tIn);
            t.Id = get_maxId() + 1;
            lst_BoundaryGap_tools.Add(t);
            return 1;
        }
        public int addTool(tool_GrayPresence tIn)
        {
            tool_GrayPresence t = new tool_GrayPresence(tIn);
            t.Id = get_maxId() + 1;
            lst_GrayPresence_tools.Add(t);
            return 1;
        }
        #endregion

        //update tool overloads
        #region updateOperation_overload_functions
        public int update_tool(tool_ROI t)
        {
            try
            {
                int index = lst_roiTools.FindIndex(x => x.Id == t.Id);
                if (index != -1)
                {
                    lst_roiTools[index] =new tool_ROI( t);
                }
                return 1;
            }
            catch (Exception exx)
            {
                return -1;
            }

        }
        public int update_tool(tool_QR_code t)
        {
            try
            {
                int index = lst_QR_tools.FindIndex(x => x.Id == t.Id);
                if (index != -1)
                {
                    lst_QR_tools[index] =new tool_QR_code( t);
                }
                return 1;
            }
            catch (Exception exx)
            {
                return -1;
            }

        }
        public int update_tool(tool_Group_printCheck t)
        {
            try
            {
                int index = lst_Group_printCheck_tools.FindIndex(x => x.Id == t.Id);
                if (index != -1)
                {
                    lst_Group_printCheck_tools[index] =new tool_Group_printCheck( t);
                }
                return 1;
            }
            catch (Exception exx)
            {
                return -1;
            }

        }
        public int update_tool(tool_DateCode_DOTS_OCR t)
        {
            try
            {
                int index = lst_Datecode_tools.FindIndex(x => x.Id == t.Id);
                if (index != -1)
                {
                    lst_Datecode_tools[index] = new tool_DateCode_DOTS_OCR(t);
                }
                return 1;
            }
            catch (Exception exx)
            {
                return -1;
            }
        }
            public int update_tool(tool_WeekCode_DOTS t)
            {
                try
                {
                    int index = lst_WeekCode_tools.FindIndex(x => x.Id == t.Id);
                    if (index != -1)
                    {
                        lst_WeekCode_tools[index] = new tool_WeekCode_DOTS(t);
                    }
                    return 1;
                }
                catch (Exception exx)
                {
                    return -1;
                }

            }
            public int update_tool(tool_Mask t)
            {
                try
                {
                    int index = lst_Mask_tools.FindIndex(x => x.Id == t.Id);
                    if (index != -1)
                    {
                        lst_Mask_tools[index] = new tool_Mask(t);
                    }
                    return 1;
                }
                catch (Exception exx)
                {
                    return -1;
                }

            }
            public int update_tool(tool_Fixture t)
            {
                try
                {
                    int index = lst_Fixture_tools.FindIndex(x => x.Id == t.Id);
                    if (index != -1)
                    {
                        lst_Fixture_tools[index] = new tool_Fixture(t);
                    }
                    return 1;
                }
                catch (Exception exx)
                {
                    return -1;
                }

            }
        public int update_tool(tool_CROI t)
        {
            try
            {
                int index = lst_CROI_tool.FindIndex(x => x.Id == t.Id);
                if (index != -1)
                {
                    lst_CROI_tool[index] = new tool_CROI(t);
                }
                return 1;
            }
            catch (Exception exx)
            {
                return -1;
            }

        }
        public int update_tool(tool_BoundaryGap t)
        {
            try
            {
                int index = lst_BoundaryGap_tools.FindIndex(x => x.Id == t.Id);
                if (index != -1)
                {
                    lst_BoundaryGap_tools[index] = new tool_BoundaryGap(t);
                }
                return 1;
            }
            catch (Exception exx)
            {
                return -1;
            }

        }
        public int update_tool(tool_GrayPresence t)
        {
            try
            {
                int index = lst_GrayPresence_tools.FindIndex(x => x.Id == t.Id);
                if (index != -1)
                {
                    lst_GrayPresence_tools[index] = new tool_GrayPresence(t);
                }
                return 1;
            }
            catch (Exception exx)
            {
                return -1;
            }

        }
        #endregion

        //delete tool  single function
        public int deleteTool(int id)
        {

            lst_roiTools.RemoveAll(x => x.Id == id);
            lst_QR_tools.RemoveAll(x => x.Id == id);
            lst_Group_printCheck_tools.RemoveAll(x => x.Id == id);
            lst_Datecode_tools.RemoveAll(x => x.Id == id);
            lst_WeekCode_tools.RemoveAll(x => x.Id == id);
            lst_Mask_tools.RemoveAll(x => x.Id == id);
            lst_Fixture_tools.RemoveAll(x => x.Id == id);
            lst_CROI_tool.RemoveAll(x => x.Id == id);
            lst_BoundaryGap_tools.RemoveAll(x => x.Id == id);
            lst_GrayPresence_tools.RemoveAll(x => x.Id == id);
            return 1;
        }

        //get_tooldata overloads
        #region get_tool_overload_functions
        public int get_toolData(int id, ref tool_QR_code t)
        {
            int index = -1;
            index = lst_QR_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { t =new tool_QR_code( lst_QR_tools[index]); }
            else
            {
                Console.WriteLine("matching ID not found");
            }
            return index;
        }

        public int get_toolData(int id, ref tool_ROI t)
        {
            int index = -1;
            index = lst_roiTools.FindIndex(x => x.Id == id);
           // Console.WriteLine($"Index of search resil with id::{id} result::{index}");
            if (index != -1)
            {
                String json = Newtonsoft.Json.JsonConvert.SerializeObject(lst_roiTools[index], Formatting.Indented);
                //Console.WriteLine($"get_toolData::json string ROI::{ json}");
                t = new tool_ROI(lst_roiTools[index]);
            }
            else
            {
                Console.WriteLine("matching ID not found");
            }
            return index;
        }

        public int get_toolData(int id, ref tool_Group_printCheck t)
        {
            int index = -1;
            index = lst_Group_printCheck_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { t = new tool_Group_printCheck( lst_Group_printCheck_tools[index]); }
            else
            {
                Console.WriteLine("matching ID not found");
            }
            return index;
        }
        public int get_toolData(int id, ref tool_DateCode_DOTS_OCR t)
        {
            int index = -1;
            index = lst_Datecode_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { t = new tool_DateCode_DOTS_OCR(lst_Datecode_tools[index]); }
            else
            {
                Console.WriteLine("matching ID not found");
            }
            return index;
        }
        public int get_toolData(int id, ref tool_WeekCode_DOTS t)
        {
            int index = -1;
            index = lst_WeekCode_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { t = new tool_WeekCode_DOTS(lst_WeekCode_tools[index]); }
            else
            {
                Console.WriteLine("matching ID not found");
            }
            return index;
        }
        public int get_toolData(int id, ref tool_Mask t)
        {
            int index = -1;
            index = lst_Mask_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { t = new tool_Mask(lst_Mask_tools[index]); }
            else
            {
                Console.WriteLine("matching ID not found");
            }
            return index;
        }
        public int get_toolData(int id, ref tool_Fixture t)
        {
            int index = -1;
            index = lst_Fixture_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { t = new tool_Fixture(lst_Fixture_tools[index]); }
            else
            {
                Console.WriteLine("matching ID not found");
            }
            return index;
        }
        public int get_toolData(int id, ref tool_CROI t)
        {
            int index = -1;
            index = lst_CROI_tool.FindIndex(x => x.Id == id);
            if (index != -1)
            { t = new tool_CROI(lst_CROI_tool[index]); }
            else
            {
                Console.WriteLine("matching ID not found");
            }
            return index;
        }        
        public int get_toolData(int id, ref tool_BoundaryGap t)
        {
            int index = -1;
            index = lst_BoundaryGap_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { t = new tool_BoundaryGap(lst_BoundaryGap_tools[index]); }
            else
            {
                Console.WriteLine("matching ID not found");
            }
            return index;
        }
        public int get_toolData(int id, ref tool_GrayPresence t)
        {
            int index = -1;
            index = lst_GrayPresence_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { t = new tool_GrayPresence(lst_GrayPresence_tools[index]); }
            else
            {
                Console.WriteLine("matching ID not found");
            }
            return index;
        }
        #endregion


        //return tool type from id
        public int get_toolShapeType(int id, ref Mark_ins_shape shape,ref String name)
        {
            int index = -1;
            index = lst_Group_printCheck_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { shape = Mark_ins_shape.Group;
              name = lst_Group_printCheck_tools[index].Name;
            }
            index = lst_roiTools.FindIndex(x => x.Id == id);
            if (index != -1)
            { shape = Mark_ins_shape.ROI;
              name = lst_roiTools[index].Name;
            }
            index = lst_QR_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { shape = Mark_ins_shape.QR;
              name = lst_QR_tools[index].Name;
            }
            index = lst_Datecode_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { shape = Mark_ins_shape.DateCode;
              name = lst_Datecode_tools[index].Name;
            }
            index = lst_WeekCode_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { shape = Mark_ins_shape.WeekCode;
              name = lst_WeekCode_tools[index].Name;
            }
            index = lst_Mask_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { shape = Mark_ins_shape.Mask;
                name = lst_Mask_tools[index].Name;
            }
            index = lst_Fixture_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { shape = Mark_ins_shape.Fixture;
                name = lst_Fixture_tools[index].Name;
            }
            index = lst_CROI_tool.FindIndex(x => x.Id == id);
            if (index != -1)
            { shape = Mark_ins_shape.CROI;
                name = lst_CROI_tool[index].Name;
            }
            index = lst_BoundaryGap_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { shape = Mark_ins_shape.Boundary_Gap;
                name = lst_BoundaryGap_tools[index].Name;
            }
            index = lst_GrayPresence_tools.FindIndex(x => x.Id == id);
            if (index != -1)
            { shape = Mark_ins_shape.Gray_Presence;
                name = lst_GrayPresence_tools[index].Name;
            }

            return index;

        }

    }
}
