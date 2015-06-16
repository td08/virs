using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Json_Function_Test
{
    class Program
    {
        private static String path = @"C:\Users\Trevor\Documents\School Work\Ecen 403\Visual Studio\JSON_Test\jstring.json";

        static void Main(string[] args)
        {
            StreamReader filereader = new StreamReader(path);
            String json = filereader.ReadToEnd();
            //JsonTextReader reader = new JsonTextReader(new StringReader(json));
            JObject j = JObject.Parse(json);
            JArray ja = (JArray)j.GetValue("ekg");
            String sys = (String)j["bp"]["systolic"];
            
            int ct = ja.Count;
            float[] fl = new float[ct];
            for (int i = 0; i < ct; i++)
            {
                fl[i] = (float)ja[i];
            }
            Console.WriteLine(sys);

            Console.Read();
        }
    }
}
