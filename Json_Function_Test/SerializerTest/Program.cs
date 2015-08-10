using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SerializerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Vitals v = new Vitals(new bool[] { true, true, true, true }, "732pm");
            v.bp = new int[] { 120, 40 };
            v.pulse = 100;
            v.steth = new int[] { 1, 2, 3, 4 };
            v.temp = 98.6;

            Jlib.serializeVitalsToJson(@"C:\Users\Trevor\Desktop\json", v);

            Vitals vit = Jlib.deserializeJsonToVitals((@"C:\Users\Trevor\Desktop\json"));

            Console.WriteLine(vit.pulse.ToString() + "\n" + vit.temp.ToString() + "\n" + vit.bp[0].ToString() + "/" + vit.bp[1].ToString());

            Console.ReadKey();
        }
    }
}
