using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SysShellHandler
{
    public partial class SysShellHandler : Form
    {
        private Dictionary<char, Button> _hotkeys = new Dictionary<char, Button>();
        private string _driverFile;

        public SysShellHandler(string[] args)
        {
            if (args.Length > 0)
                _driverFile = args[0];
            else
                _driverFile = chooseDriver();
            InitializeComponent();
            KeyPreview = true;
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            foreach (var c in Controls)
            {
                if (c is Button b)
                {
                    var ampIdx = b.Text.IndexOf('&');
                    if (ampIdx != -1)
                        _hotkeys.Add(char.ToLower(b.Text[ampIdx + 1]), b);
                }
            }

        }

        private static char ToChar(Keys key)
        {
            char c = '\0';
            if ((key >= Keys.A) && (key <= Keys.Z))
            {
                c = (char)((int)'a' + (int)(key - Keys.A));
            }

            else if ((key >= Keys.D0) && (key <= Keys.D9))
            {
                c = (char)((int)'0' + (int)(key - Keys.D0));
            }
            else if (key == Keys.Space)
            {
                c = ' ';
            }

            return char.ToLower(c);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                default:
                    var ch = char.ToLower(ToChar(e.KeyCode));
                    if (_hotkeys.ContainsKey(ch))
                        _hotkeys[ch].PerformClick();
                    else
                        base.OnKeyDown(e);
                    break;
            }
        }

        private string chooseDriver()
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Drivers (*.sys)|*.sys|All files (*.*)|*.*";
                if(ofd.ShowDialog() == DialogResult.OK)
                    return ofd.FileName;
            }
            Environment.Exit(1);
            return "";
        }

        private void buttonCffExplorer_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.CffExplorer, _driverFile);
            Environment.Exit(0);
        }

        private void buttonMakeUsermode_Click(object sender, EventArgs e)
        {
            var makeUsermode = Properties.Settings.Default.MakeUsermode;
            var usermods = Path.Combine(Path.GetDirectoryName(makeUsermode), "usermode_modules");
            foreach (var m in Directory.EnumerateFiles(usermods))
                File.Copy(m, Path.Combine(Path.GetDirectoryName(_driverFile), Path.GetFileName(m)), true);
            Process.Start(makeUsermode, _driverFile);
            Environment.Exit(0);
        }

        private void buttonTestsign_Click(object sender, EventArgs e)
        {
            var signtool = Properties.Settings.Default.Signtool;
            Process.Start(signtool, $"sign \"{_driverFile}\"");
            Environment.Exit(0);
        }
    }
}
