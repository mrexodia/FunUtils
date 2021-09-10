using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace VsCompiler
{
    class Program
    {
        static string Cmd(string fileName, string arguments)
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
            });
            return process.StandardOutput.ReadToEnd().Trim();
        }

        static void Main(string[] args)
        {
            var vswhere = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vswhere.exe");
            var vsDirectory = Cmd(vswhere, "-version \"16.0,17.0)\" -property installationPath");
            var vcvarsall = Path.Combine(vsDirectory, @"VC\Auxiliary\Build\vcvarsall.bat");
            var environment = Cmd($"cmd.exe", "/C call \"{vcvarsall}\" && echo ==VSENV== && set");
            Console.WriteLine(vsDirectory);
        }
    }
}
