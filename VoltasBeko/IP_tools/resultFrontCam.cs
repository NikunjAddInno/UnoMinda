using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryThresholds.IP_tools
{
    public class resultFrontCam
    {
        public class DefectDetails
        {
            public string DefectName = "";
            public cvRect CvRect = new cvRect();
            public bool Result = false;

            public DefectDetails(DefectDetails details)
            {
                this.DefectName = details.DefectName;
                this.Result = details.Result;
                this.CvRect = details.CvRect;
            }

            public DefectDetails()
            {
                
            }
        }

        public int poseNum;
        public cvRect ROI = new cvRect(0, 0, 10, 10);
        public List<DefectDetails> list_defectDetails = new List<DefectDetails>();
        public bool finalResult=false;
        public bool isROIonly = false;
        public resultFrontCam()
        {
            poseNum = 0;
            ROI = new cvRect(0, 0, 10, 10);

            list_defectDetails = new List<DefectDetails>();
            finalResult = false;
            isROIonly = false;
    }
        public static void Save_to_file(String filename, resultFrontCam obj_userTools)
        {

            String json = Newtonsoft.Json.JsonConvert.SerializeObject(obj_userTools, Formatting.Indented);
            System.IO.File.WriteAllText(filename, json);
            Console.WriteLine("Insert json file");
        }
        public static resultFrontCam Load_from_file(String fileName)
        {

            if (File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                resultFrontCam tmpCls = JsonConvert.DeserializeObject<resultFrontCam>(jsonString);
                // Console.WriteLine("file found {0}", jsonString);


                return tmpCls;
            }
            else
            {
                Console.WriteLine("file not found {0}", fileName);
                resultFrontCam tmpCls = new resultFrontCam();

                return tmpCls;
            }


        }

    }
}
