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
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Insert your Netowor IP in CIDR notation and press Enter");
                var input = Console.ReadLine();
                Console.WriteLine("");

                IEnumerable<string> Result = new List<string>();
                try
                {
                    Result = Generator.Generate(input);

                    foreach (var ipAddress in Result)
                    {
                        Console.WriteLine($"   {ipAddress}");
                    }

                    Console.WriteLine("");
                    Console.WriteLine($"   {Result.Count()} valid IPs on CIDR");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"   {ex.Message}");
                }

                Console.WriteLine("");
                
            }

        }
    }
}
