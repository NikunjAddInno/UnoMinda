using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryThresholds
{
    class globalVars
    {
        public static Mark_Inspection markInsRegForm;

        public static int cam1_exposure = 100;
        public static int numCameras = 1;

        public static systemThresholds baseThresholds = new systemThresholds();
        public static algorithmLib.Class1 algo = new algorithmLib.Class1();

        public static int updateSystemThresholds(systemThresholds baseThresholds)
        {
        //    int idx = 0;
        //foreach (var k in baseThresholds.list_kvp)  //for cpp part
        //    { if (k.Scope == 1)
        //        {
        //            algo.UpdateThresholds(idx, k.Value);
        //            idx += 1;
        //        }
        //    }
        //    // for cs set values by using class methods
        //    cam1_exposure =(int) baseThresholds.getValue("cam1_exposure");
        //    numCameras = (int)baseThresholds.getValue("numCameras");

            return 1;
        }
        


    }
}
