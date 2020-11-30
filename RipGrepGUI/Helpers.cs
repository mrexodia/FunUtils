using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using RipGrepGUI.Properties;
using System.Reflection;

namespace RipGrepGUI
{
    static class Helpers
    {
        /// <summary>
        /// Waits asynchronously for the process to exit.
        /// </summary>
        /// <param name="process">The process to wait for cancellation.</param>
        /// <param name="cancellationToken">A cancellation token. If invoked, the task will return
        /// immediately as cancelled.</param>
        /// <returns>A Task representing waiting for the process to end.</returns>
        public static Task WaitForExitAsync(
            this Process process,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            process.EnableRaisingEvents = true;

            var taskCompletionSource = new TaskCompletionSource<object>();

            EventHandler handler = null;
            handler = (sender, args) =>
            {
                process.Exited -= handler;
                taskCompletionSource.TrySetResult(null);
            };
            process.Exited += handler;

            if (cancellationToken != default(CancellationToken))
            {
                cancellationToken.Register(
                    () =>
                    {
                        process.Exited -= handler;
                        taskCompletionSource.TrySetCanceled();
                    });
            }

            return taskCompletionSource.Task;
        }

        public static string GetFieldName<T>(object obj, object value)
        {
            foreach (FieldInfo field in typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                if (field.GetValue(obj) == value)
                    return field.Name;
            return null;
        }

        private static void Fail(string message)
        {
            MessageBox.Show(message, "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }

        private static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                MessageBox.Show(message);
                Environment.Exit(1);
            }
        }

        private static SettingsProperty FindSetting(string keyword)
        {
            Console.WriteLine(keyword);
            SettingsProperty foundProperty = null;
            foreach (SettingsProperty property in Settings.Default.Properties)
            {
                if (property.Name.Contains(keyword) || keyword.Contains(property.Name))
                {
                    if (foundProperty == null)
                        foundProperty = property;
                    else
                        Fail($"Duplicate setting for '{keyword}'");
                }
            }
            Assert(foundProperty != null, $"No setting found for '{keyword}'");
            return foundProperty;
        }

        private static void ParameterImpl(CheckBox checkBox, string parameter, bool inverted, bool saved)
        {
            var fieldName = GetFieldName<RipGrepGUI>(checkBox.Parent, checkBox);
            var prefix = "checkBox";
            Assert(fieldName.StartsWith(prefix), $"Parameter field name '{fieldName}' is not prefixed with '{prefix}'");
            var settingName = fieldName.Substring(prefix.Length);
            var setting = FindSetting(settingName);

            Assert(RipGrepGUI.Parameters.Count(tpl => tpl.Item1 == parameter) == 0, $"Already specified parameter \"{parameter}\"");
            RipGrepGUI.Parameters.Add((parameter, true));

            checkBox.CheckedChanged += (s, e) =>
            {
                var enabled = inverted ? !checkBox.Checked : checkBox.Checked;
                MessageBox.Show($"{(enabled ? "enabled" : "disabled")} {settingName}");
            };
            Console.WriteLine(setting);
        }

        public static void Parameter(this CheckBox checkBox, string parameter, bool saved = true)
        {
            ParameterImpl(checkBox, parameter, false, saved);
        }

        public static void ParameterInverted(this CheckBox checkBox, string parameter, bool saved = true)
        {
            ParameterImpl(checkBox, parameter, true, saved);

        }
    }
}
