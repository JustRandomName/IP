using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows.Forms;
using System.Threading;
using cmd;
using System.Text.RegularExpressions;

namespace laba3
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                System.Management.ObjectQuery query = new ObjectQuery("Select * FROM Win32_Battery");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                ManagementObjectCollection collection = searcher.Get();

                foreach (ManagementObject mo in collection)
                {
                    Console.Clear();
                    Console.WriteLine("Caption: {0}", Convert.ToString(mo.Properties["Caption"].Value));
                    Console.WriteLine("BatteryStatus: {0}", Convert.ToString(mo.Properties["BatteryStatus"].Value));
                    Console.WriteLine("Availability: {0}", Convert.ToUInt16(mo.Properties["Availability"].Value));
                    Console.WriteLine("Charge: {0}", SystemInformation.PowerStatus.BatteryLifePercent);
                    Console.WriteLine("Power: " + GetTypes());
                    Thread.Sleep(400);
                }
            }
        }

        private static string GetTypes()
        {
            dynamic cmd = new Cmd();
            string stringValue = cmd.powercfg("-l");
            var regex = new Regex(@"[(](?<power>\w*)");

            MatchCollection matches = regex.Matches(stringValue);
            StringBuilder builder = new StringBuilder();
            for (int i = 1; i < matches.Count; i++)
            {
                builder.AppendLine(matches[i].Groups["power"].Value);
            }

            return builder.ToString();
        }
    }
}
