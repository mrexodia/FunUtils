using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Ghidra
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("ghidraRun.bat"))
            {
                MessageBox.Show("Put this executable next to ghidraRun.bat", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c title Ghidra & ghidraRun.bat",
                //CreateNoWindow = true,
                UseShellExecute = false,
                //WindowStyle = ProcessWindowStyle.Minimized,
            });
        }
    }
}
