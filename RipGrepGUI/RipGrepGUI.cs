using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RipGrepGUI
{
    public partial class RipGrepGUI : Form
    {
        public RipGrepGUI()
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            buttonSearch.Enabled = false;
            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "rg.exe",
                    Arguments = $"--pretty -i \"{textBoxSearch.Text}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                }
            };
            //MessageBox.Show(p.StartInfo.FileName + " " + p.StartInfo.Arguments);
            p.OutputDataReceived += Process_OutputDataReceived;
            p.Start();
            p.BeginOutputReadLine();
            await p.WaitForExitAsync();
            buttonSearch.Enabled = true;
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            string data = e.Data;
            richTextBoxResults.Invoke((MethodInvoker)delegate
            {
                if (!string.IsNullOrEmpty(data))
                    richTextBoxResults.AppendText(data);
                richTextBoxResults.AppendText("\r\n");
            });
        }
    }
}
