using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DirBrowser
{
    public partial class DirBrowser : Form
    {
        Dictionary<char, Button> _hotkeys = new Dictionary<char, Button>();

        public DirBrowser()
        {
            InitializeComponent();
            KeyPreview = true;
            Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);

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
            switch(e.KeyCode)
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

        private void buttonCmd_Click(object sender, EventArgs e)
        {
            var dir = Directory.GetCurrentDirectory();
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/K \"title {Path.GetFileNameWithoutExtension(dir)}\"",
                WorkingDirectory = dir,
            });
            Close();
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = @"c:\totalcmd\TOTALCMD64.EXE",
                Arguments = $"/P=L \"/L={Directory.GetCurrentDirectory()}\"",
            });
            Close();
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = @"c:\totalcmd\TOTALCMD64.EXE",
                Arguments = $"/P=R \"/R={Directory.GetCurrentDirectory()}\"",
            });
            Close();
        }
    }
}
