using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Measurement_AI.classes
{
   public  class pipeline_obj
    {
        private Bitmap image0;
 
        private bool uploaded;
        private bool processed;
        private bool displayed;
    
        //private IGrabResult grabResult;

        public pipeline_obj(Bitmap image0,  bool uploaded, bool processed,bool displayed)
        {
            this.image0 = image0;
         
            this.uploaded = uploaded;
            this.processed = processed;
            this.Displayed = displayed;
        }
        //public pipeline_obj(IGrabResult grabResult, bool uploaded, bool processed)
        //{
        //    this.grabResult = grabResult;
        //    this.uploaded = uploaded;
        //    this.processed = processed;
        //}

        public Bitmap Image0
        {
            get { return image0; }   // get method
            set { image0 = value; }  // set method
        }

        //public IGrabResult GrabResult
        //{
        //    get { return grabResult; }   // get method
        //    set { grabResult = value; }  // set method
        //}


        public bool Uploaded
        {
            get { return uploaded; }   // get method
            set { uploaded = value; }  // set method
        }
        public  bool Processed
           {
            get { return processed; }   // get method
            set { processed = value; }  // set method
        }

        public bool Displayed
        { get => displayed; set => displayed = value; }
    }
}
