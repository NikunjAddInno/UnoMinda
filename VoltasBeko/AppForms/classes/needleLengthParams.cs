using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CathaterTipInspection.classes
{
    class needleLengthParams
    {
        private float currLength = 2.1f;
        public float setlength = 2.4f;
        public float errorLimU = 0.1f;
        public float errorLimL = 0.1f;
        public float offset = 0;

        public float CurrLength
        {
            get => currLength;

            set => currLength = value;
        }

        public needleLengthParams()
        {
            currLength = 0;
            setlength = 0;
            errorLimU = 0;
            errorLimL = 0;
            offset = 0;
        }
    }
}
