using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var client = new HttpClient();
                string url = "";

                while (url == string.Empty)
                {
                    Console.WriteLine("Ingrese la URL:");
                    url = Console.ReadLine();
                }

                //TomL file URL
                var endpoint = new Uri(url);

                //HttpRequest to URL.
                HttpResponseMessage result = client.GetAsync(endpoint).Result;

                //Convert the content to string.
                string toml = result.Content.ReadAsStringAsync().Result;

                //Parse the string to a TomlTable.
                Nett.TomlTable TT = Nett.Toml.ReadString(toml);

                //Convert the table into a dictionary<string, object>
                Dictionary<string, object> asd = TT.ToDictionary();

                //Serialize the dictionary.
                string TomLInJson = System.Text.Json.JsonSerializer.Serialize(asd);

                //Ask if dump the data.
                Console.WriteLine("Dump JSON to file? (Y = yes / N = no)");
                ConsoleKeyInfo dump = Console.ReadKey();

                //If it's a Y or y we dump it.
                if(dump.KeyChar == 89 || dump.KeyChar == 121)
                {
                    string path = Directory.GetCurrentDirectory() + "/OUTPUT/";
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    File.WriteAllText(path + "dump.txt", TomLInJson);
                }

                //Output -> JSON
                Console.WriteLine(TomLInJson);

                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
