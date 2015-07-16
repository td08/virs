using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCode
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = Int32.Parse(Console.ReadLine());
            Console.WriteLine(length);

            byte[] b = new byte[4];
            b = BitConverter.GetBytes(length);
            foreach (byte bite in b)
            {
                Console.WriteLine(Convert.ToString(bite, 2));
            }

            int convert = BitConverter.ToInt32(b, 0);
            Console.WriteLine();
            Console.WriteLine("Bytes: " + convert);
            Console.WriteLine(b.Length);
            Console.ReadKey();
        }
    }
}
