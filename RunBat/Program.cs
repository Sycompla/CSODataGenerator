using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunBat
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = System.Reflection.Assembly.GetEntryAssembly().Location
                                                                            .Replace("RunBat.exe", "");
            string arguments = " " + path + "GeneratorStart.bat";
            ProcessStartInfo procStartInfo2 = new ProcessStartInfo("cmd.exe", arguments);

            ExecuteCommand(arguments);
        }

        public static int ExecuteCommand(string commnd)
        {
            var pp = new ProcessStartInfo("cmd.exe", "/K" + commnd)
            {
                UseShellExecute = false,
                WorkingDirectory = "D:\\",
            };
            var process = Process.Start(pp);

            return 0;
        }
    }
}
