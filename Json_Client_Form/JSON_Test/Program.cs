using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


namespace JSON_Test
{
    class Program
    {
        //private static String jtest = "{\"name\":\"Trevor\",\"email\":[\"masnun@gmail.com\",\"masnun@leevio.com\"],\"websites\":{\"home page\":\"http://masnun.com\",\"blog\":\"http://masnun.me\"}}";
        //private static String j = @"{'name':'masnun','email':['masnun@gmail.com','masnun@leevio.com'],'websites':{'home page':'http://masnun.com','blog':'http://masnun.me'}}";
        private static String path = @"C:\Users\Trevor\Documents\School Work\Ecen 403\Visual Studio\JSON_Test\jstring.json";

        static void Main(string[] args)
        {
            String name = null;
            List<String> emails = new List<string>();
            List<String> websites = new List<string>();
            String home = null;
            String blog = null;

            StreamReader filereader = new StreamReader(path);
            String json = filereader.ReadToEnd();
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            JObject j = JObject.Parse(json);

            Console.WriteLine(j.GetValue("email").Count());
            Console.Read();

                

            while (reader.Read())
            {
                if ((String)reader.Value == "name")
                {
                    reader.Read();
                    name = (String)reader.Value;
                }

                if ((String)reader.Value == "email")
                {
                    reader.Read();
                    while (reader.TokenType.ToString() != "EndArray")
                    {
                        if (reader.Value != null)
                        {
                            emails.Add((String)reader.Value);
                        }
                        reader.Read();
                    }
                }

                if ((String)reader.Value == "home page")
                {
                    reader.Read();
                    home = (String)reader.Value;
                }

                if ((String)reader.Value == "blog")
                {
                    reader.Read();
                    blog = (String)reader.Value;
                }

            }

            Console.WriteLine("Name: " + name);
            Console.Write("Emails: ");
            foreach (Object obj in emails)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine("Homepage: " + home + "\nBlog: " + blog);
            Console.WriteLine("\nDone");
            Console.Read();

        }
    }
}
