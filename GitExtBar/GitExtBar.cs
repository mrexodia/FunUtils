using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using GitExtBar.Properties;

namespace GitExtBar
{
    public partial class GitExtBar : Form
    {
        public class GitExtAction
        {
            public string Name;
            public string Command;
            public Bitmap Icon;
            public char Hotkey;

            public GitExtAction(string name, string command, Bitmap icon)
            {
                var hotkeyIdx = name.IndexOf('&');
                if (hotkeyIdx == -1)
                    throw new ArgumentException();
                Hotkey = name[hotkeyIdx + 1];
                Name = name.Substring(0, hotkeyIdx) + "(" + Hotkey + ")" + name.Substring(hotkeyIdx + 2);
                Command = command;
                Icon = icon;
            }

            public GitExtAction(string name, string command, Icon icon)
                : this(name, command, icon.ToBitmap()) { }

            public void Run()
            {
                var splitIdx = Command.IndexOf(':');
                var fileName = Command.Substring(0, splitIdx);
                var arguments = Command.Substring(splitIdx + 1);
                var currentDirectory = Directory.GetCurrentDirectory();
                arguments = arguments.Replace("$currentdir$", currentDirectory);
                Process.Start(new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    WorkingDirectory = currentDirectory,
                    UseShellExecute = true,
                });
            }
        }

        GitExtAction[] _actions;

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

        public GitExtBar()
        {
            InitializeComponent();
            KeyPreview = true;
            Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

            if (Git("rev-parse --is-inside-work-tree") != "true")
            {
                _actions = new GitExtAction[]
                {
                    new GitExtAction("&Init", "GitExtensions.exe:init \"$currentdir$\"", Resources.IconRepoCreate),
                    new GitExtAction("&Clone", "GitExtensions.exe:clone \"$currentdir$\"", Resources.IconCloneRepoGit),
                };
            }
            else
            {
                _actions = new GitExtAction[]
                {
                    new GitExtAction("&Commit", "GitExtensions.exe:commit", Resources.IconCommit),
                    new GitExtAction("&Browse", "GitExtensions.exe:browse", Resources.IconBrowseFileExplorer),
                    new GitExtAction("Pu&ll", "GitExtensions.exe:pull", Resources.IconPull),
                    new GitExtAction("&Push", "GitExtensions.exe:push", Resources.IconPush),
                    new GitExtAction("&Fetch", "cmd.exe:/c \"git fetch & pause\"", Resources.IconPullFetch),
                    new GitExtAction("&Status", "cmd.exe:/c \"git status & pause\"", Resources.IconAbout),
                    new GitExtAction("&Remotes", "GitExtensions.exe:remotes", Resources.IconSettings),
                };
            }

            listViewActions.ItemActivate += (s, e) =>
            {
                _actions[listViewActions.SelectedItems[0].ImageIndex].Run();
                Close();
            };

            listViewActions.LargeImageList = new ImageList();
            for (var i = 0; i < _actions.Length; i++)
            {
                var action = _actions[i];

                listViewActions.LargeImageList.Images.Add(action.Icon);

                listViewActions.Items.Add(new ListViewItem(action.Name)
                {
                    ImageIndex = i,
                });
            }
            listViewActions.Items[0].Selected = true;
            listViewActions.Focus();
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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            else
            {
                var ch = char.ToLower(ToChar(e.KeyCode));
                var action = _actions.Where(a => char.ToLower(a.Hotkey) == ch).FirstOrDefault();
                if (action != null)
                {
                    action.Run();
                    Close();
                }
            }
            base.OnKeyDown(e);
        }
    }
}
