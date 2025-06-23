using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement_AI.classes
{
    class modelDataTools
    {   public String modelName;
        public String dateCreated;
        public  List<DrawShapesOnpicBox> list_appliedTools = new List<DrawShapesOnpicBox>();
        public  List<MeasurementTools> list_toolCpp = new List<MeasurementTools>();
   
    }
}
