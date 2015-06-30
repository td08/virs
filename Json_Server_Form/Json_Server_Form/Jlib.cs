using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Forms;

namespace Json_Server_Form
{
    class Jlib
    {

        //returns json string from data stream read from file
        public static String getJStringFromStream(Stream s)
        {
            StreamReader sr = new StreamReader(s);
            String json = sr.ReadToEnd();
            return json;
        }

        public static float getTemp(String jstring)
        {
            float temp;
            JObject jo = JObject.Parse(jstring);
            temp = (float)jo.GetValue("temp");
            return temp;
        }

        public static int getHR(String jstring)
        {
            int hr;
            JObject jo = JObject.Parse(jstring);
            hr = (int)jo.GetValue("heart");
            return hr;
        }

        public static int[] getBP(String jstring)
        {
            int[] bp = new int[2];
            JObject jo = JObject.Parse(jstring);
            JArray ja = (JArray)jo.GetValue("bp");
            bp[0] = (int)ja[0];
            bp[1] = (int)ja[1];
            return bp;
        }        
        
        //returns data set in a double array for populating form chart data series
        public static double[] getEKG(String jstring)
        {
            JObject jo = JObject.Parse(jstring);
            JArray ja = (JArray)jo.GetValue("ekg");
            int arrayCount = ja.Count;
            double[] ekgArray = new double[arrayCount];
            for (int i = 0; i < arrayCount; i++)
            {
                ekgArray[i] = (double)ja[i];
            }
                return ekgArray;
        }

        public static String getLogTime(String jstring)
        {
            String date = null;
            JObject jo = JObject.Parse(jstring);
            date = (String)jo.GetValue("logTime");
            return date;
        }

        public static String getTime(String jstring)
        {
            String time = null;
            JObject jo = JObject.Parse(jstring);
            time = (String)jo.GetValue("time");
            return time;
        }

        public static String toJson(Vitals obj)
        {
           string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
           return json;
        }

    }
}



