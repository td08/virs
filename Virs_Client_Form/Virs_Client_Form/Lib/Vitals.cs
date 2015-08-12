using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virs_Client_Form
{
    //class that contains a set of vital information fields to be serialized into a JSON formatted data structure
    [Serializable]
    class Vitals
    {
        public bool[] fileChecks;   // bool array that holds values indicating whether a field will be populated with recorded data
        [NonSerialized] public int[] steth;
        public int pulse;
        public int[] bp;
        public double temp;
        public string logTime, firstName, lastName, age, weight;

        public Vitals(bool[] checks, string time)
        {
            this.fileChecks = checks;
            this.logTime = time;
        }
    }    
}
