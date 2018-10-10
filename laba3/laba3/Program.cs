
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
                    Console.WriteLine("BatteryStatus: {0}", BatteryStatus(Convert.ToString(mo.Properties["BatteryStatus"].Value)));
                    Console.WriteLine("Availability: {0}", Availability(Convert.ToString(mo.Properties["Availability"].Value)));
                    Console.WriteLine("Charge: {0}", SystemInformation.PowerStatus.BatteryLifePercent);
                    Console.WriteLine("Power: " + GetTypes());
                    Thread.Sleep(400);
                }
            }
        }

        private static String BatteryStatus(String batteryStatus) {
            switch (batteryStatus) {
                case "1": return "Other";
                case "2": return "Unknown";
                case "3": return "Fully Charged";
                case "4": return "Low";
                case "5": return "Critical";
                case "6": return "Charging";
                case "7": return "Charging and High";
                case "8": return "Charging and Low";
                case "9": return "Charging and Critical";
                case "10": return "Undefined";
                case "11": return "Partially Charged";
                default:
                    return "Unknown";
            }
        }

        private static String Availability(String availability)
        {
            switch (availability)
            {
                case "1": return "Other";
                case "2": return "Unknown";
                case "3": return "Running/Full Power";
                case "4": return "Warning";
                case "5": return "In Test";
                case "6": return "Not Applicable";
                case "7": return "Power Off";
                case "8": return "Off Line";
                case "9": return "Off Duty";
                case "10": return "Degraded";
                case "11": return "Not Installed";
                case "12": return "Install Error";
                case "13": return "Power Save - Unknown";
                case "14": return "Power Save - Low Power Mode";
                case "15": return "Power Save - Standby";
                case "16": return "Power Cycle";
                case "17": return "Power Save - Warning";
                case "18": return "Paused";
                case "19": return "Not Ready";
                case "20": return "Not Configured";
                case "21": return "Quiesced";
                default:
                    return "Unknown";
            }
        }

        private static string GetTypes()
        {
            dynamic cmd = new Cmd();
            string stringValue = cmd.powercfg("-l");
            var regex = new Regex(@"[(](?<power>\w*).*[*]");

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
