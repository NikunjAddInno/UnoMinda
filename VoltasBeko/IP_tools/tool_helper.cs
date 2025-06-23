using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tryThresholds;
using VoltasBeko.classes;
namespace tryThresholds.IP_tools
{
    public class tool_helper
    {
        public static int update_tool_shape(ref Control C, DrawPbMmarkIns obj_tool_shape,PictureBox pb,String modelPath)
        {
            String fileName = "";
            Bitmap b;
            Bitmap imgCopy;
            switch (obj_tool_shape.shape)
            { 
                case Mark_ins_shape.ROI:
                    var t1 = (uc_ROI)C;
                    //var td1= t1.get_toolData_from_UI("tool1");
                    //t1.Set_toolData_to_UI(td1);
                    fileName = modelPath+ "ROI_t_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".bmp";
                    b  = imageFns_bmp.getCroppedImage(pb, obj_tool_shape.list_points[0].ToPoint(), obj_tool_shape.list_points[1].ToPoint());
                    //imgCopy =imageFns_bmp.get24BitDeepCopy(b);
                    b.Save(fileName);
                    t1.Set_RoiRegion(obj_tool_shape, fileName);//use jsonString to get set data
                    break;
                case Mark_ins_shape.Group:
                    var t2 = (uc_Group_printCheck)C;
                    // var td2= t2.get_toolData_from_UI("tool1");
                    //t2.Set_toolData_to_UI(td2);
                    fileName = modelPath+"Group_PrintIns_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".bmp";
                    b = imageFns_bmp.getCroppedImage(pb, obj_tool_shape.list_points[0].ToPoint(), obj_tool_shape.list_points[1].ToPoint());
                    //imgCopy = imageFns_bmp.get24BitDeepCopy(b);
                    b.Save(fileName);
                    t2.Set_RoiRegion(obj_tool_shape, fileName);//use jsonString to get set data
                    break;
                case Mark_ins_shape.QR:
                    var t3 = (uc_QR)C;
                   // var td3= t3.get_toolData_from_UI("tool1");
                    //t3.Set_toolData_to_UI(td3);
                    t3.Set_RoiRegion(obj_tool_shape, "");//use jsonString to get set data
                    break;
                case Mark_ins_shape.DateCode:
                    var t4 = (uc_DateCode_Dots_OCR)C;
                    t4.Set_RoiRegion(obj_tool_shape, "");//use jsonString to get set data
                    break;
                case Mark_ins_shape.WeekCode:
                    var t5 = (uc_weekCode_DOTS)C;
                    t5.Set_RoiRegion(obj_tool_shape, "");//use jsonString to get set data
                    break;
                case Mark_ins_shape.Mask:
                    var t6 = (uc_Mask)C;
                    t6.Set_RoiRegion(obj_tool_shape, "");//use jsonString to get set data
                    break;
                case Mark_ins_shape.Fixture:
                    var t7 = (uc_fixture)C;
                    //var td1= t1.get_toolData_from_UI("tool1");
                    //t1.Set_toolData_to_UI(td1);
                    fileName = modelPath + "ROI_t_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss")+"_T1" + ".bmp";
                    string fileName2 = modelPath + "ROI_t_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss")+"_T2" + ".bmp";
                    b = imageFns_bmp.getCroppedImage(pb, obj_tool_shape.list_points[0].ToPoint(), obj_tool_shape.list_points[1].ToPoint());
                    Bitmap b2 = imageFns_bmp.getCroppedImage(pb, obj_tool_shape.list_points[2].ToPoint(), obj_tool_shape.list_points[3].ToPoint());
                    //imgCopy = imageFns_bmp.get24BitDeepCopy(b);
                    b.Save(fileName);
                    b2.Save(fileName2);
                    t7.Set_RoiRegion(obj_tool_shape, fileName,fileName2);//use jsonString to get set data
                    break;
                case Mark_ins_shape.CROI:
                    var t8 = (uc_CROI)C;
                    //var td1= t1.get_toolData_from_UI("tool1");
                    //t1.Set_toolData_to_UI(td1);
                    fileName = modelPath + "ROI_t_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".bmp";
                    b = imageFns_bmp.getCroppedImage(pb, obj_tool_shape.list_points[0].ToPoint(), obj_tool_shape.list_points[1].ToPoint());
                    //imgCopy =imageFns_bmp.get24BitDeepCopy(b);
                    b.Save(fileName);
                    t8.Set_RoiRegion(obj_tool_shape, fileName);//use jsonString to get set data
                    break;
                case Mark_ins_shape.Boundary_Gap:
                    var t9 = (uc_BoundaryGap)C;
                    t9.Set_RoiRegion(obj_tool_shape, fileName);//use jsonString to get set data
                    break;                
                case Mark_ins_shape.Gray_Presence:
                    var t10 = (uc_GrayPresence)C;
                    t10.Set_RoiRegion(obj_tool_shape, fileName);//use jsonString to get set data
                    break;
                default:
                    return -1;
                    
            }
            return 1;
        }
        public static int get_fixtureReferenceData(ref Control C, DrawPbMmarkIns obj_tool_shape, ref tool tTemp)
        {
            int resp = 1;

            switch (obj_tool_shape.shape)
            {
                case Mark_ins_shape.ROI:
                    var t1 = (uc_ROI)C;
                    var td1 = t1.get_toolData_from_UI();
                    //  json = Newtonsoft.Json.JsonConvert.SerializeObject(td1, Formatting.Indented);
                    // Console.WriteLine($"Adding tool  ROI::{ json}");

                    tTemp.FixtureToolReference = td1.FixtureToolReference;
                    tTemp.FixtureType = td1.FixtureType;
                    tTemp.Fixture_mode = td1.Fixture_mode;
                    tTemp.Id = td1.Id;
                    //  json = Newtonsoft.Json.JsonConvert.SerializeObject(obj_appliedTools, Formatting.Indented);
                    //   Console.WriteLine($"all tools ::{ json}");

                    break;
                case Mark_ins_shape.Group:
                    var t2 = (uc_Group_printCheck)C;
                    var td2 = t2.get_toolData_from_UI();
                    tTemp.FixtureToolReference = td2.FixtureToolReference;
                    tTemp.FixtureType = td2.FixtureType;
                    tTemp.Fixture_mode = td2.Fixture_mode;
                    tTemp.Id = td2.Id;
                    break;
                case Mark_ins_shape.QR:
                    var t3 = (uc_QR)C;
                    var td3 = t3.get_toolData_from_UI();
                    tTemp.FixtureToolReference = td3.FixtureToolReference;
                    tTemp.FixtureType = td3.FixtureType;
                    tTemp.Fixture_mode = td3.Fixture_mode;
                    tTemp.Id = td3.Id;
                    break;
                case Mark_ins_shape.DateCode:
                    var t4 = (uc_DateCode_Dots_OCR)C;
                    var td4 = t4.get_toolData_from_UI();
                    tTemp.FixtureToolReference = td4.FixtureToolReference;
                    tTemp.FixtureType = td4.FixtureType;
                    tTemp.Fixture_mode = td4.Fixture_mode;
                    tTemp.Id = td4.Id;
                    break;
                case Mark_ins_shape.WeekCode:
                    var t5 = (uc_weekCode_DOTS)C;
                    var td5 = t5.get_toolData_from_UI();
                    tTemp.FixtureToolReference = td5.FixtureToolReference;
                    tTemp.FixtureType = td5.FixtureType;
                    tTemp.Fixture_mode = td5.Fixture_mode;
                    tTemp.Id = td5.Id;
                    break;
                case Mark_ins_shape.Mask:
                    var t6 = (uc_Mask)C;
                    var td6 = t6.get_toolData_from_UI();
                    tTemp.FixtureToolReference = td6.FixtureToolReference;
                    tTemp.FixtureType = td6.FixtureType;
                    tTemp.Fixture_mode = td6.Fixture_mode;
                    tTemp.Id = td6.Id;
                    break;
                case Mark_ins_shape.Fixture:
                    var t7 = (uc_fixture)C;
                    var td7 = t7.get_toolData_from_UI();
                    tTemp.FixtureToolReference = td7.FixtureToolReference;
                    tTemp.FixtureType = td7.FixtureType;
                    tTemp.Fixture_mode = td7.Fixture_mode;
                    tTemp.Id = td7.Id;
                    break;
                case Mark_ins_shape.CROI:
                    var t8 = (uc_CROI)C;
                    var td8 = t8.get_toolData_from_UI();
                    tTemp.FixtureToolReference = td8.FixtureToolReference;
                    tTemp.FixtureType = td8.FixtureType;
                    tTemp.Fixture_mode = td8.Fixture_mode;
                    tTemp.Id = td8.Id;
                    break;
                case Mark_ins_shape.Boundary_Gap:
                    var t9 = (uc_BoundaryGap)C;
                    var td9 = t9.get_toolData_from_UI();
                    tTemp.FixtureToolReference = td9.FixtureToolReference;
                    tTemp.FixtureType = td9.FixtureType;
                    tTemp.Fixture_mode = td9.Fixture_mode;
                    tTemp.Id = td9.Id;
                    break;
                case Mark_ins_shape.Gray_Presence:
                    var t10 = (uc_GrayPresence)C;
                    var td10 = t10.get_toolData_from_UI();
                    tTemp.FixtureToolReference = td10.FixtureToolReference;
                    tTemp.FixtureType = td10.FixtureType;
                    tTemp.Fixture_mode = td10.Fixture_mode;
                    tTemp.Id = td10.Id;
                    break;
                default:
                    resp = -1;
                    break;

            }
            Console.WriteLine($"fixture data from get data fn:: type {tTemp.FixtureType}   mode={tTemp.Fixture_mode}  reftool={tTemp.FixtureToolReference}");


            return resp;
        }
        public static int add_tool(ref Control C, DrawPbMmarkIns obj_tool_shape,ref applied_tools obj_appliedTools)
        {
            String json = "";
            int resp = -1;
            switch (obj_tool_shape.shape)
            {
                case Mark_ins_shape.ROI:
                    var t1 = (uc_ROI)C;
                    var td1 = t1.get_toolData_from_UI();
                    //  json = Newtonsoft.Json.JsonConvert.SerializeObject(td1, Formatting.Indented);
                    // Console.WriteLine($"Adding tool  ROI::{ json}");
                    
                    resp = obj_appliedTools.addTool(td1);
                  //  json = Newtonsoft.Json.JsonConvert.SerializeObject(obj_appliedTools, Formatting.Indented);
                 //   Console.WriteLine($"all tools ::{ json}");

                    break;
                case Mark_ins_shape.Group:
                    var t2 = (uc_Group_printCheck)C;
                    var td2 = t2.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td2);
                    break;
                case Mark_ins_shape.QR:
                    var t3 = (uc_QR)C;
                    var td3= t3.get_toolData_from_UI();
                    resp= obj_appliedTools.addTool(td3);
                    break;
                case Mark_ins_shape.DateCode:
                    var t4 = (uc_DateCode_Dots_OCR)C;
                    var td4 = t4.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td4);
                    break;
                case Mark_ins_shape.WeekCode:
                    var t5 = (uc_weekCode_DOTS)C;
                    var td5 = t5.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td5);
                    break;
                case Mark_ins_shape.Mask:
                    var t6 = (uc_Mask)C;
                    var td6 = t6.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td6);
                    break;
                case Mark_ins_shape.Fixture:
                    var t7 = (uc_fixture)C;
                    var td7 = t7.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td7);
                    break;
                case Mark_ins_shape.CROI:
                    var t8 = (uc_CROI)C;
                    var td8 = t8.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td8);
                    break;
                case Mark_ins_shape.Boundary_Gap:
                    var t9 = (uc_BoundaryGap)C;
                    var td9 = t9.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td9);
                    break;
                case Mark_ins_shape.Gray_Presence:
                    var t10 = (uc_GrayPresence)C;
                    var td10 = t10.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td10);
                    break;
                default:
                    break;
                   

            }
            return resp;
        }
        public static int add_tool(ref Control C, DrawPbMmarkIns obj_tool_shape, ref applied_tools obj_appliedTools, tool fixtureData)
        {
            // String json = "";
            int resp = -1;
            switch (obj_tool_shape.shape)
            {
                case Mark_ins_shape.ROI:
                    var t1 = (uc_ROI)C;
                    var td1 = t1.get_toolData_from_UI();
                    //  json = Newtonsoft.Json.JsonConvert.SerializeObject(td1, Formatting.Indented);
                    // Console.WriteLine($"Adding tool  ROI::{ json}");
                    td1.FixtureType = fixtureData.FixtureType;
                    td1.Fixture_mode = fixtureData.Fixture_mode;
                    td1.FixtureToolReference = fixtureData.FixtureToolReference;
                    resp = obj_appliedTools.addTool(td1);
                    //  json = Newtonsoft.Json.JsonConvert.SerializeObject(obj_appliedTools, Formatting.Indented);
                    //   Console.WriteLine($"all tools ::{ json}");

                    break;
                case Mark_ins_shape.Group:
                    var t2 = (uc_Group_printCheck)C;
                    var td2 = t2.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td2);
                    break;
                case Mark_ins_shape.QR:
                    var t3 = (uc_QR)C;
                    var td3 = t3.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td3);
                    break;
                case Mark_ins_shape.DateCode:
                    var t4 = (uc_DateCode_Dots_OCR)C;
                    var td4 = t4.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td4);
                    break;
                case Mark_ins_shape.WeekCode:
                    var t5 = (uc_weekCode_DOTS)C;
                    var td5 = t5.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td5);
                    break;
                case Mark_ins_shape.Mask:
                    var t6 = (uc_Mask)C;
                    var td6 = t6.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td6);
                    break;
                case Mark_ins_shape.Fixture:
                    var t7 = (uc_fixture)C;
                    var td7 = t7.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td7);
                    break;
                case Mark_ins_shape.CROI:
                    var t8 = (uc_CROI)C;
                    var td8 = t8.get_toolData_from_UI();
                    td8.FixtureType = fixtureData.FixtureType;
                    td8.Fixture_mode = fixtureData.Fixture_mode;
                    td8.FixtureToolReference = fixtureData.FixtureToolReference;
                    resp = obj_appliedTools.addTool(td8);
                    break;
                case Mark_ins_shape.Boundary_Gap:
                    var t9 = (uc_BoundaryGap)C;
                    var td9 = t9.get_toolData_from_UI();
                    resp = obj_appliedTools.addTool(td9);
                    break;
                case Mark_ins_shape.Gray_Presence:
                    var t10 = (uc_GrayPresence)C;
                    var td10 = t10.get_toolData_from_UI();
                    td10.FixtureType = fixtureData.FixtureType;
                    td10.Fixture_mode = fixtureData.Fixture_mode;
                    td10.FixtureToolReference = fixtureData.FixtureToolReference;
                    resp = obj_appliedTools.addTool(td10);
                    break;
                default:
                    break;


            }
            return resp;
        }
        public static int update_tool(ref Control C, DrawPbMmarkIns obj_tool_shape,ref applied_tools obj_appliedTools)
        {

            switch (obj_tool_shape.shape)
            {
                case Mark_ins_shape.ROI:
                    var t1 = (uc_ROI)C;
                    var td1 = t1.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td1);
                   // t1.Set_toolData_to_UI(td1);
                    //t1.Set_RoiRegion(obj_tool_shape, "templROI.bmp");//use jsonString to get set data
                    break;
                case Mark_ins_shape.Group:
                    var t2 = (uc_Group_printCheck)C;
                    var td2 = t2.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td2);
                    //t2.Set_toolData_to_UI(td2);
                    //t2.Set_RoiRegion(obj_tool_shape, "templ_1.bmp");//use jsonString to get set data
                    break;
                case Mark_ins_shape.QR:
                    var t3 = (uc_QR)C;
                    var td3 = t3.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td3);
                    //t3.Set_toolData_to_UI(td3);
                    //t3.Set_RoiRegion(obj_tool_shape, "templ_1.bmp");//use jsonString to get set data
                    break;
                case Mark_ins_shape.DateCode:
                    var t4 = (uc_DateCode_Dots_OCR)C;
                    var td4 = t4.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td4);
                    break;
                case Mark_ins_shape.WeekCode:
                    var t5 = (uc_weekCode_DOTS)C;
                    var td5 = t5.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td5);
                    break;
                case Mark_ins_shape.Mask:
                    var t6 = (uc_Mask)C;
                    var td6 = t6.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td6);
                    break;
                case Mark_ins_shape.Fixture:
                    var t7 = (uc_fixture)C;
                    var td7 = t7.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td7);
                    break;
                case Mark_ins_shape.CROI:
                    var t8 = (uc_CROI)C;
                    var td8 = t8.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td8);
                    break;
                case Mark_ins_shape.Boundary_Gap:
                    var t9 = (uc_BoundaryGap)C;
                    var td9 = t9.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td9);
                    break;
                case Mark_ins_shape.Gray_Presence:
                    var t10 = (uc_GrayPresence)C;
                    var td10 = t10.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td10);
                    break;
                default:
                    return -1;
                    break;
                    

            }
            return 1;
        }
        public static int update_tool(ref Control C, DrawPbMmarkIns obj_tool_shape, ref applied_tools obj_appliedTools, tool fixtureData)
        {

            switch (obj_tool_shape.shape)
            {
                case Mark_ins_shape.ROI:
                    var t1 = (uc_ROI)C;
                    var td1 = t1.get_toolData_from_UI();
                    td1.FixtureType = fixtureData.FixtureType;
                    td1.Fixture_mode = fixtureData.Fixture_mode;
                    td1.FixtureToolReference = fixtureData.FixtureToolReference;
                    obj_appliedTools.update_tool(td1);
                    // t1.Set_toolData_to_UI(td1);
                    //t1.Set_RoiRegion(obj_tool_shape, "templROI.bmp");//use jsonString to get set data
                    break;
                case Mark_ins_shape.Group:
                    var t2 = (uc_Group_printCheck)C;
                    var td2 = t2.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td2);
                    //t2.Set_toolData_to_UI(td2);
                    //t2.Set_RoiRegion(obj_tool_shape, "templ_1.bmp");//use jsonString to get set data
                    break;
                case Mark_ins_shape.QR:
                    var t3 = (uc_QR)C;
                    var td3 = t3.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td3);
                    //t3.Set_toolData_to_UI(td3);
                    //t3.Set_RoiRegion(obj_tool_shape, "templ_1.bmp");//use jsonString to get set data
                    break;
                case Mark_ins_shape.DateCode:
                    var t4 = (uc_DateCode_Dots_OCR)C;
                    var td4 = t4.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td4);
                    break;
                case Mark_ins_shape.WeekCode:
                    var t5 = (uc_weekCode_DOTS)C;
                    var td5 = t5.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td5);
                    break;
                case Mark_ins_shape.Mask:
                    var t6 = (uc_Mask)C;
                    var td6 = t6.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td6);
                    break;
                case Mark_ins_shape.Fixture:
                    var t7 = (uc_fixture)C;
                    var td7 = t7.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td7);
                    break;
                case Mark_ins_shape.CROI:
                    var t8 = (uc_CROI)C;
                    var td8 = t8.get_toolData_from_UI();
                    td8.FixtureType = fixtureData.FixtureType;
                    td8.Fixture_mode = fixtureData.Fixture_mode;
                    td8.FixtureToolReference = fixtureData.FixtureToolReference;
                    obj_appliedTools.update_tool(td8);
                    break;
                case Mark_ins_shape.Boundary_Gap:
                    var t9 = (uc_BoundaryGap)C;
                    var td9 = t9.get_toolData_from_UI();
                    obj_appliedTools.update_tool(td9);
                    break;
                case Mark_ins_shape.Gray_Presence:
                    var t10 = (uc_GrayPresence)C;
                    var td10 = t10.get_toolData_from_UI();
                    td10.FixtureType = fixtureData.FixtureType;
                    td10.Fixture_mode = fixtureData.Fixture_mode;
                    td10.FixtureToolReference = fixtureData.FixtureToolReference;
                    obj_appliedTools.update_tool(td10);
                    break;
                default:
                    return -1;
                    break;


            }
            return 1;
        }

        public static int delete_tool(int toolID,ref applied_tools obj_appliedTools)
        {
            obj_appliedTools.deleteTool(toolID);
            return 1;
          
        }
        public static int select_tool(ref Control C,int toolID, Mark_ins_shape shape,applied_tools obj_appliedTools,ref DrawPbMmarkIns shapeRet)
        {
            String json = "";
            Console.WriteLine($"Looking for tool ID ::{toolID} shape type {shape.ToString()}");
            int resp = -1;
            
            switch (shape)
            {
                case Mark_ins_shape.ROI:
                    var t1 = (uc_ROI)C;
                    var tdTemp1 = new tool_ROI("");
                    resp=obj_appliedTools.get_toolData(toolID, ref tdTemp1);
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(tdTemp1, Formatting.Indented);
                    Console.WriteLine($"json string ROI::{ json}");
                    t1.Set_toolData_to_UI(tdTemp1);
                    shapeRet = tdTemp1.Obj_toolShape.DeepCopy();
                    break;
                case Mark_ins_shape.Group:
                    var t2 = (uc_Group_printCheck)C;
                    var tdTemp2 = new tool_Group_printCheck("");
                    resp =obj_appliedTools.get_toolData(toolID, ref tdTemp2);
                    shapeRet = tdTemp2.Obj_toolShape.DeepCopy();
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(tdTemp2, Formatting.Indented);
                    Console.WriteLine($"json string group::{ json}");
                    t2.Set_toolData_to_UI(tdTemp2);
                    break;
                case Mark_ins_shape.QR:
                    var t3 = (uc_QR)C;
                    var tdTemp3 = new tool_QR_code("");
                    resp =obj_appliedTools.get_toolData(toolID, ref tdTemp3);
                    shapeRet = tdTemp3.Obj_toolShape.DeepCopy();
                    t3.Set_toolData_to_UI(tdTemp3);
                    break;
                case Mark_ins_shape.DateCode:
                    var t4 = (uc_DateCode_Dots_OCR)C;
                    var tdTemp4 = new tool_DateCode_DOTS_OCR("");
                    resp = obj_appliedTools.get_toolData(toolID, ref tdTemp4);
                    shapeRet = tdTemp4.Obj_toolShape.DeepCopy();
                    t4.Set_toolData_to_UI(tdTemp4);
                    break;
                case Mark_ins_shape.WeekCode:
                    var t5 = (uc_weekCode_DOTS)C;
                    var tdTemp5 = new tool_WeekCode_DOTS("");
                    resp = obj_appliedTools.get_toolData(toolID, ref tdTemp5);
                    shapeRet = tdTemp5.Obj_toolShape.DeepCopy();
                    t5.Set_toolData_to_UI(tdTemp5);
                    break;
                case Mark_ins_shape.Mask:
                    var t6 = (uc_Mask)C;
                    var tdTemp6 = new tool_Mask("");
                    resp = obj_appliedTools.get_toolData(toolID, ref tdTemp6);
                    shapeRet = tdTemp6.Obj_toolShape.DeepCopy();
                    t6.Set_toolData_to_UI(tdTemp6);
                    break;
                case Mark_ins_shape.Fixture:
                    var t7 = (uc_fixture)C;
                    var tdTemp7 = new tool_Fixture("");
                    resp = obj_appliedTools.get_toolData(toolID, ref tdTemp7);
                    shapeRet = tdTemp7.Obj_toolShape.DeepCopy();
                    t7.Set_toolData_to_UI(tdTemp7);
                    break;
                case Mark_ins_shape.CROI:
                    var t8 = (uc_CROI)C;
                    var tdTemp8 = new tool_CROI("");
                    resp = obj_appliedTools.get_toolData(toolID, ref tdTemp8);
                    shapeRet = tdTemp8.Obj_toolShape.DeepCopy();
                    t8.Set_toolData_to_UI(tdTemp8);
                    break;
                case Mark_ins_shape.Boundary_Gap:
                    var t9 = (uc_BoundaryGap)C;
                    var tdTemp9 = new tool_BoundaryGap("");
                    resp = obj_appliedTools.get_toolData(toolID, ref tdTemp9);
                    shapeRet = tdTemp9.Obj_toolShape.DeepCopy();
                    t9.Set_toolData_to_UI(tdTemp9);
                    break;
                case Mark_ins_shape.Gray_Presence:
                    var t10 = (uc_GrayPresence)C;
                    var tdTemp10 = new tool_GrayPresence("");
                    resp = obj_appliedTools.get_toolData(toolID, ref tdTemp10);
                    shapeRet = tdTemp10.Obj_toolShape.DeepCopy();
                    t10.Set_toolData_to_UI(tdTemp10);
                    break;
                default:
                    break;


            }
            return resp;
        }

        public static int getToolID_Fromstring(string fullname)
        {
            int id = 0;
            int underscoreIndex = fullname.LastIndexOf('_');
            if (underscoreIndex >= 0 && underscoreIndex < fullname.Length)
            {
                int.TryParse(fullname.Substring(underscoreIndex + 1, fullname.Length - 1 - underscoreIndex), out id);

            }
            return id;
        }

    }

   
}
