using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace AppLauncher
{
    public partial class MainForm : Form
    {
        Launchable[] launchables = new Launchable[0];

        string filter = "";
        Launchable[] filtered;

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        public MainForm()
        {
            InitializeComponent();

            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("Another instance is already running.", Text);
                Environment.Exit(1);
                return;
            }

            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            /* Example launchables.json
[
  {
    "PriorityBias": 0,
    "Name": "Visual Studio 2010",
    "Path": "c:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Microsoft Visual Studio 2010\\Microsoft Visual Studio 2010.lnk"
  },
  {
    "PriorityBias": 0,
    "Name": "Visual Studio 2013",
    "Path": "c:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Visual Studio 2013\\Visual Studio 2013.lnk"
  },
  {
    "PriorityBias": 0,
    "Name": "Visual Studio 2015",
    "Path": "c:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Visual Studio 2015.lnk"
  },
  {
    "PriorityBias": 0,
    "Name": "Visual Studio 2017",
    "Path": "c:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Visual Studio 2017.lnk"
  }
]
            */

            try
            {
                if (System.IO.File.Exists("launchables.json"))
                {
                    var json = System.IO.File.ReadAllText("launchables.json");
                    launchables = Newtonsoft.Json.JsonConvert.DeserializeObject<Launchable[]>(json);
                }
            }
            catch
            {
                MessageBox.Show("Failed to load launchables.", Text);
                Environment.Exit(1);
                return;
            }

            const int MOD_WIN = 8;
            const int VK_OEM_2 = 0xBF;
            if (!RegisterHotKey(Handle, 0, MOD_WIN, VK_OEM_2)) //Win+/
            {
                MessageBox.Show("Failed to register hotkey.", Text);
                Environment.Exit(1);
                return;
            }

            listBoxLaunchables.DataSource = launchables;
            listBoxLaunchables.KeyDown += ListBox1_KeyUp;
            listBoxLaunchables.Focus();
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            if (m.Msg == WM_HOTKEY)
            {
                if (Visible)
                    Hide();
                else
                {
                    Show();
                    UpdateFilter("");
                    listBoxLaunchables.Focus();
                }
            }
            base.WndProc(ref m);
        }

        public static char ToChar(Keys key)
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

            return c;
        }

        private void UpdateFilter(string newFilter)
        {
            var newFiltered = launchables.Where(l => l.MatchesFilter(newFilter)).OrderByDescending(l => l.Order(newFilter)).ToArray();
            if (newFiltered.Length != 0)
            {
                filter = newFilter;
                filtered = newFiltered;
                labelFilter.Text = $"Filter: \"{filter}\"";
                listBoxLaunchables.DataSource = newFiltered;
                listBoxLaunchables.SelectedIndex = 0;
            }
        }

        private void ListBox1_KeyUp(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            switch (e.KeyCode)
            {
                case Keys.F4:
                    if (e.Modifiers == Keys.Alt)
                        Close();
                    break;

                case Keys.Escape:
                    Hide();
                    break;

                case Keys.Enter:
                    var selected = listBoxLaunchables.SelectedItem as Launchable;
                    if (selected != null)
                    {
                        Hide();
                        UpdateFilter("");
                        selected.Launch();
                    }
                    break;

                case Keys.Back:
                    if (e.Control)
                        UpdateFilter("");
                    else if (filter.Length > 0)
                        UpdateFilter(filter.Substring(0, filter.Length - 1));
                    break;

                case Keys.Up:
                case Keys.Down:
                    e.SuppressKeyPress = false;
                    break;

                default:
                    var ch = ToChar(e.KeyCode);
                    if (ch != '\0')
                        UpdateFilter(filter + ch);
                    break;
            }
        }
    }
}
