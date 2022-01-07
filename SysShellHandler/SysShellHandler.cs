using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace SysShellHandler
{
    public partial class SysShellHandler : Form
    {
        private Dictionary<char, Button> _hotkeys = new Dictionary<char, Button>();
        private string _driverFile;

        enum Status
        {
            Unregistered,
            Running,
            Stopped,
        }

        string ServiceName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(_driverFile);
            }
        }

        Status ServiceStatus
        {
            get
            {
                try
                {
                    if (cmd($"sc query \"{ServiceName}\"").Contains("RUNNING"))
                    {
                        return Status.Running;
                    }
                    else
                    {
                        return Status.Stopped;
                    }
                }
                catch (Win32Exception x)
                {
                    var exitCode = x.NativeErrorCode;
                    if (exitCode == 1060)
                    {
                        // Service doesn't exist
                        return Status.Unregistered;
                    }
                    else
                    {
                        MessageBox.Show(this, $"Unknown exit code {exitCode} for sc query \"{ServiceName}\":\n\n{x.Message}", "Error");
                        return Status.Unregistered;
                    }
                }
            }
        }

        void RefreshServiceControls()
        {
            switch (ServiceStatus)
            {
                case Status.Running:
                    buttonServiceControl.Text = "&Stop";
                    buttonServiceControl.Enabled = true;
                    buttonServiceRegistration.Text = "&Unregister";
                    break;
                case Status.Stopped:
                    buttonServiceControl.Text = "&Start";
                    buttonServiceControl.Enabled = true;
                    buttonServiceRegistration.Text = "&Unregister";
                    break;
                case Status.Unregistered:
                    buttonServiceControl.Text = "&Start";
                    buttonServiceControl.Enabled = false;
                    buttonServiceRegistration.Text = "&Register";
                    break;
            }
            buttonServiceRegistration.Enabled = true;
        }

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
            RefreshServiceControls();
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
                if (ofd.ShowDialog() == DialogResult.OK)
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

        string cmd(string command, bool elevate = false)
        {
            var fileName = "";
            var arguments = "";
            var spaceIdx = command.IndexOf(' ');
            if (spaceIdx != -1)
            {
                fileName = command.Substring(0, spaceIdx);
                arguments = command.Substring(spaceIdx + 1);
            }
            else
            {
                fileName = command;
            }


            if (elevate && MessageBox.Show(this,
                $"This will run the follwing command as Administrator:\n\n{command}\n\nDo you want to continue?",
                "Confirm",
                MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                throw new OperationCanceledException();
            }

            var p = Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = elevate,
                Verb = elevate ? "runas" : "",
                RedirectStandardOutput = !elevate,
                CreateNoWindow = !elevate,
            });
            var stdout = "";
            if (p.StartInfo.RedirectStandardOutput)
            {
                stdout = p.StandardOutput.ReadToEnd();
            }
            p.WaitForExit();
            if (p.ExitCode != 0)
                throw new Win32Exception(p.ExitCode, stdout);
            return stdout;
        }

        private void buttonServiceRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                var servicePath = Path.Combine(Environment.SystemDirectory, "drivers", Path.GetFileName(_driverFile));
                if (ServiceStatus == Status.Unregistered)
                {
                    if (File.Exists(servicePath))
                    {
                        MessageBox.Show(this, $"{servicePath} already exists!", "Error");
                        return;
                    }
                    else
                    {
                        cmd($"cmd /c mklink \"{servicePath}\" \"{_driverFile}\" && sc create \"{ServiceName}\" binPath= \"{servicePath}\" type= kernel", true);
                    }
                }
                else
                {
                    cmd($"cmd /c del \"{servicePath}\" && sc delete \"{ServiceName}\"", true);
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Win32Exception x)
            {
                MessageBox.Show(this, x.Message, $"Exit code {x.NativeErrorCode}");
            }
            RefreshServiceControls();
        }

        private void buttonServiceControl_Click(object sender, EventArgs e)
        {
            try
            {
                if (ServiceStatus == Status.Running)
                {
                    cmd($"cmd /k sc stop \"{ServiceName}\"", true);
                }
                else
                {
                    cmd($"cmd /k sc start \"{ServiceName}\"", true);
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Win32Exception)
            {
            }
            RefreshServiceControls();
        }
    }
}
