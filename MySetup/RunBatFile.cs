using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySetup
{
    public class RunBatFile
    {
        public string Path { get; set; }
        public string Run { get; set; }

        public void RunBat()
        {
            if(Run.Equals("1"))
            {
                System.Diagnostics.Process.Start(Path + "GeneratorStart.cmd");
            }
        }
    }
}
