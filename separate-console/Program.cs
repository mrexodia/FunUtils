using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace separate_console
{
    class Program
    {
        static int Main(string[] args)
        {
            if(args.Length < 1)
            {
                Console.Error.WriteLine("Usage: separate-console my.exe [argument]");
                return 1;
            }
            var p = Process.Start(new ProcessStartInfo
            {
                FileName = args[0],
                Arguments = args.Length > 1 ? $"\"{args[1]}\"" : "",
                UseShellExecute = true,
                CreateNoWindow = false,
            });
            p.WaitForExit();
            return p.ExitCode;
        }
    }
}
