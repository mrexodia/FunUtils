using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;

namespace NewFile
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = Interaction.InputBox("Enter file name to create:", "Total Commander");
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName.Replace('/', Path.DirectorySeparatorChar));

            if (string.IsNullOrEmpty(fullPath))
            {
                return;
            }
            
            if (File.Exists(fullPath))
            {
                Interaction.MsgBox("File already exists!");
                return;
            }

            var baseDirectory = Path.GetDirectoryName(fullPath);
            Directory.CreateDirectory(baseDirectory);
            using (var file = File.Create(fullPath))
            {

            }
        }
    }
}
