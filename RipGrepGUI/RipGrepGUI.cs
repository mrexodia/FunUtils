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
using RipGrepGUI.Properties;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Configuration;

namespace RipGrepGUI
{
    public partial class RipGrepGUI : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        internal static List<(string, bool)> Parameters = new List<(string, bool)>();

        public RipGrepGUI()
        {
#if DEBUG
            AllocConsole();
#endif

            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            Text += " (" + System.IO.Directory.GetCurrentDirectory() + ")";
            checkBoxCaseSensitive.ParameterInverted("-i");
            checkBoxContext.Parameter("-C5");
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            buttonSearch.Enabled = false;
            richTextBoxResults.Clear();
            var expression = textBoxSearch.Text;
            var split = expression.Split(':');
            var p = new Process
            {
                // -f PATTERN
                StartInfo = new ProcessStartInfo
                {
                    FileName = "rg.exe",
                    Arguments = $"--pretty --sort-files -i -C3 -H \"{textBoxSearch.Text}\"",
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

        private static Color FromAnsi(int ansi)
        {
            switch (ansi)
            {
                /*Bright Red: \u001b[*/
                case 31:
                    return Color.FromArgb(231, 63, 52);
                /*Bright Green: \u001b[*/
                case 32:
                    return Color.FromArgb(19, 161, 14);
                /*Bright Cyan: \u001b[*/
                case 36:
                    return Color.FromArgb(58, 150, 221);
#if false // TODO: match colors with Windows
                /*Bright Black: \u001b[*/
                case 30:
                    return Color.Black;
                /*Bright Yellow: \u001b[*/
                case 33:
                    return Color.Yellow;
                /*Bright Blue: \u001b[*/
                case 34:
                    return Color.Blue;
                /*Bright Magenta: \u001b[*/
                case 35:
                    return Color.Magenta;
                /*Bright White: \u001b[*/
                case 37:
                    return Color.White;
#endif
                default:
                    Debugger.Break();
                    break;
            }
            return Color.HotPink;
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            string data = e.Data;
            richTextBoxResults.Invoke((MethodInvoker)delegate
            {
                if (!string.IsNullOrEmpty(data))
                {
                    var sb = new StringBuilder();
                    void Flush()
                    {
                        if (sb.Length == 0)
                            return;
                        var s = sb.ToString();
                        richTextBoxResults.AppendText(sb.ToString());
                        sb.Clear();
                    }

                    bool inColor = false;
                    for (var i = 0; i < data.Length; i++)
                    {
                        var ch = data[i];
                        if (inColor)
                        {
                            if (ch == 'm')
                            {
                                inColor = false;
                                var colorText = sb.ToString();
                                var ansiSplit = colorText.Split(';');
                                var ansiColor = int.Parse(ansiSplit[0]);
                                if (ansiSplit.Length != 1)
                                    Debugger.Break();
                                if (ansiColor == 0)
                                {
                                    richTextBoxResults.SelectionColor = richTextBoxResults.ForeColor;
                                    richTextBoxResults.SelectionBackColor = richTextBoxResults.BackColor;
                                }
                                else if (ansiColor == 1)
                                {
                                }
                                else
                                {
                                    // TODO: handle background color
                                    richTextBoxResults.SelectionColor = FromAnsi(ansiColor);
                                }
                                sb.Clear();
                            }
                            else if (ch != '[')
                            {
                                sb.Append(ch);
                            }
                        }
                        else
                        {
                            if (ch == 0x1b)
                            {
                                inColor = true;
                                Flush();
                            }
                            else
                            {
                                sb.Append(ch);
                            }
                        }
                    }
                    Flush();
                }
                richTextBoxResults.AppendText("\r\n");
            });
        }
    }
}
