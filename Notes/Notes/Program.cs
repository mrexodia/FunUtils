using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Notes.Properties;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;

namespace Notes
{
    static class Program
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Check if there is already another instance open
            // https://stackoverflow.com/a/59079638/1806760
            var mutex = new Mutex(true, $"Global\\{typeof(Notes).GUID}", out bool mutexCreated);
            if (!mutexCreated)
            {
                // Attempt to bring the other instance to the front
                var processName = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
                var process = Process.GetProcessesByName(processName).Where(p => p.Id != Process.GetCurrentProcess().Id).FirstOrDefault();

                if (process != null)
                {
                    var hWnd = process.MainWindowHandle;
                    if (hWnd != IntPtr.Zero)
                    {
                        ShowWindow(hWnd, ShowWindowEnum.Restore);
                        SetForegroundWindow(hWnd);
                        FlashWindow(hWnd, true);
                        return;
                    }
                }

                MessageBox.Show("Another instance is already running!");
                return;
            }

            // Verify settings
            var defaultTable = Settings.Default.DefaultTable;
            switch (defaultTable)
            {
                case "Personal":
                case "Work":
                    break;
                default:
                    MessageBox.Show($"Invalid DefaultTable \"{defaultTable}\". Fix your configuration!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                    break;
            }
            if (Settings.Default.APIKey == "NOTSET" || Settings.Default.BaseID == "NOTSET")
            {
                MessageBox.Show("APIKey and/or BaseID not set up correctly. Fix your configuration!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.Start("https://airtable.com/api");
                Environment.Exit(1);
            }

            Application.Run(new Notes());
        }
    }
}
