using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json_Server_Form
{
    //class that contains a set of vital information parsed from a JSON formatted data structure
    class Vitals
    {
        public float temp { get; set; }
        public int heart { get; set; }
        public int[] bp { get; set; }
        public double[] ekg { get; set; }
        public String logTime { get; set; }
        

        public Vitals(String jstring)
        {
            this.temp = Jlib.getTemp(jstring);
            this.heart = Jlib.getHR(jstring);
            this.bp = Jlib.getBP(jstring);
            this.ekg = Jlib.getEKG(jstring);
            this.logTime = Jlib.getLogTime(jstring);
        }
    }
}
