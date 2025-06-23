using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltasBeko.Classes
{
    public class DetectionResult
    {
        public bool Success { get; set; }
        public string Image { get; set; }
        public List<string> CropDetectionsLabels { get; set; }
        public List<string> DetectionLabels { get; set; }
    }
}
