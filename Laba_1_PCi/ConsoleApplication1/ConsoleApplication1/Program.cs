using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace ConsoleApplication1
{
    class Program
    {
        private const string deviceID = "DeviceID";
        private const string vendorID = "VenderID";
        private const string pci = "PCI";

        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------------------------------------------");
            foreach (var obj in GetPciDev().ToArray())
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine("-----------------------------------------------------------");
        }

        public static IEnumerable<string> GetPciDev()
        {
            return new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity").Get()
                .Cast<ManagementBaseObject>()
                .Select(obj => obj[deviceID])
                .Cast<string>()
                .Where(obj => obj.Contains(pci))
                .Select(str => $"{deviceID}: {str.Substring(17, 4)} {vendorID}: 0x{str.Substring(8, 4)}");
        }
    }
}
