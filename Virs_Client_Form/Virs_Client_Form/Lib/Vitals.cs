using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virs_Client_Form
{
    //class that contains a set of vital information parsed from a JSON formatted data structure
    class Vitals
    {
        public double temp { get; set; }
        public int pulse { get; set; }
        public int[] bp { get; set; }
        public int[] steth { get; set; }
        public bool[] fileChecks { get; set; }
        //public String logTime { get; set; }
        

        public Vitals(bool[] checks)
        {
            this.fileChecks = checks;
            //this.temp = Jlib.getTemp(jstring);
            //this.heart = Jlib.getHR(jstring);
            //this.bp = Jlib.getBP(jstring);
            //this.ekg = Jlib.getEKG(jstring);
            //this.logTime = Jlib.getLogTime(jstring);
        }
    }
}
