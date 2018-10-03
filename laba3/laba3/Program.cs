
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace laba3
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Management.ObjectQuery query = new ObjectQuery("Select * FROM Win32_Battery");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject mo in collection)
            {

                Console.WriteLine("Caption: {0}", Convert.ToString(mo.Properties["Caption"].Value));
                Console.WriteLine("BatteryStatus: {0}", Convert.ToString(mo.Properties["BatteryStatus"].Value));
                Console.WriteLine("Availability: {0}", Convert.ToUInt16(mo.Properties["Availability"].Value));  //
            }
        }
    }
}
