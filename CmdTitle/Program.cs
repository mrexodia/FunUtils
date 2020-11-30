using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CmdTitle
{
    class Program
    {
        public static string Git(string command)
        {
            var p = Process.Start(new ProcessStartInfo
            {
                FileName = "git",
                Arguments = command,
                WorkingDirectory = Directory.GetCurrentDirectory(),
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
            });
            p.WaitForExit();
            return p.ExitCode == 0 ? p.StandardOutput.ReadToEnd().Trim() : "";
        }

        static void Main(string[] args)
        {
            var gitRoot = Git("rev-parse --show-toplevel").Replace('/', '\\');
            if (string.IsNullOrEmpty(gitRoot))
                gitRoot = Directory.GetCurrentDirectory();
            var title = Path.GetFileName(gitRoot);
            Process.Start("cmd.exe", $"/k \"title {title}\"");
        }
    }
}
