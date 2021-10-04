using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IPGenerator.Library;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Insert your Netowor IP in CIDR notation and press Enter");
            var input = Console.ReadLine();
            Console.WriteLine("");

            IEnumerable<string> Result = new List<string>();
            try
            {
                Result = Generator.Generate(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            foreach (var item in Result)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("");
            Console.WriteLine($"{Result.Count()} valid IPs on CIDR");
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit");
            Console.Read();
            

        }
    }
}
