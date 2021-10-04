using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPGenerator.Library
{
    public static class Generator
    {
        public static List<string> Generate(string inputIp)
        {
            string[] input = inputIp.Split('/');

            ValidateInput(input);

            IPAddress.TryParse(input[0], out IPAddress ip);
            int.TryParse(input[1], out int cidr);
            
            //Geting the Mask. (Using uint.maxValue to get a stream of 32 ones and 'rigth-shift' the amount of bites of the suffix)
            uint mask = ~(uint.MaxValue >> cidr);

            // Convert the IP address to bytes.
            byte[] ipInBytes = ip.GetAddressBytes();

            //Convert the Mask to bytes
            byte[] maskInBytes = BitConverter.GetBytes(mask).Reverse().ToArray();

            //Initialize the first & last in 4 array of bytes (one for each octate)
            byte[] firstIP = new byte[4];
            byte[] lastIP = new byte[4];

            // Calculate the bytes of the start and end IP addresses.
            for (int i = 0; i < ipInBytes.Length; i++)
            {
                firstIP[i] = (byte)(ipInBytes[i] & maskInBytes[i]);
                lastIP[i] = (byte)(ipInBytes[i] | ~maskInBytes[i]);
            }

            //Convert the first and last ip addresses in bites to uInt in so i can increment by 1
            uint sIP = BitConverter.ToUInt32(firstIP.Reverse().ToArray(), 0);
            uint eIP = BitConverter.ToUInt32(lastIP.Reverse().ToArray(), 0);

            // remove first and last addresses since that are broadcast & network that should be explicitly removeved
            sIP++; 
            eIP--;

            var output = new List<string>();

            while (sIP <= eIP)
            {
                output.Add(new IPAddress(RevertUintBytes(sIP)).ToString());
                sIP++;
            }

            return output;

        }

        private static uint RevertUintBytes(uint ip)
        {
            return BitConverter.ToUInt32(BitConverter.GetBytes(ip).Reverse().ToArray(), 0);
        }


        private static void ValidateInput(string[] input)
        {
            IPAddress ipInput;
            int cidr;

            if (input.Length != 2)
            {
                throw new ArgumentException("The Argument is not a valid CIDR notation");
            }

            if (int.TryParse(input[1], out cidr) == false)
            {
                throw new ArgumentException("The second part of the argument is not a valid CIDR suffix");
            }

            if (IPAddress.TryParse(input[0], out ipInput) == false)
            {
                throw new ArgumentException("The first part of the argument is not a valid IP address");
            }

            if (cidr < 9 || cidr > 30)
            {
                throw new ArgumentException("The CIDR suffix is out of range");
            }
        }

    }
}
