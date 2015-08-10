using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTime1
{
    class Programf
    {
        static void Main(string[] args)
        {
             DateTime localDate = DateTime.Now;
             Console.WriteLine(localDate);
             string newdate = localDate.ToString("yyyyMMddHHmm");
             Console.WriteLine(newdate);

             string formatString = "yyyyMMddHHmm";
             DateTime dt = DateTime.ParseExact(newdate, formatString, null);

             Console.WriteLine(dt);
             Console.ReadKey();
      //DateTime utcDate = DateTime.UtcNow;
      //String[] cultureNames = { "en-US", "en-GB", "fr-FR",
      //                          "de-DE", "ru-RU" } ;

      //foreach (var cultureName in cultureNames) {
      //   var culture = new CultureInfo(cultureName);
      //   Console.WriteLine("{0}:", culture.NativeName);
      //   Console.WriteLine("   Local date and time: {0}, {1:G}", localDate.ToString(culture), localDate.Kind);
      //   Console.WriteLine("   UTC date and time: {0}, {1:G}\n",
      //                     utcDate.ToString(culture), utcDate.Kind);
        }
    }
}
