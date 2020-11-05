using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Notes.Properties;

using AirtableApiClient;

namespace Notes
{
    public partial class Notes : Form
    {
        public Notes()
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            treeList.KeyDown += TreeList_KeyDown;
            if (Settings.Default.DefaultTable == "Work")
            {
                radioButtonWork.Checked = true;
            }
        }

        private void TreeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                // Escape can be used to close the application
                e.SuppressKeyPress = true;
                e.Handled = true;
                Close();
            }
            else if (e.Modifiers == Keys.Control && (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return))
            {
                // Ctrl + Enter can be used to submit
                buttonSubmit_Click(buttonSubmit, e);
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (treeList.TextLength > 0)
            {
                switch (MessageBox.Show(this, "Do you want to submit your notes?", "Unsaved notes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        buttonSubmit_Click(buttonSubmit, e);
                        break;
                    case DialogResult.No:
                        treeList.DumpHistory();
                        break;
                    default:
                        e.Cancel = true;
                        break;
                }
            }
            base.OnClosing(e);
        }

        private class Note
        {
            public Note(string name) => Name = name;

            public string Name = "";
            public List<(int, string)> Notes = new List<(int, string)>();
            public string FinalNotes;
        }

        private async void buttonSubmit_Click(object sender, EventArgs e)
        {
            treeList.DumpHistory();
            var notes = new List<Note>();
            var lines = treeList.Lines;
            for (var i = 0; i < treeList.Lines.Length; i++)
            {
                var line = lines[i];
                if (line.Length == 0)
                {
                    // Skip empty lines (probably a result of some corruption anyway)
                    continue;
                }
                var indent = treeList.GetLineIndent(i);
                if (indent == 0 || notes.Count == 0)
                {
                    notes.Add(new Note(line));
                }
                else
                {
                    var note = notes[notes.Count - 1];
                    note.Notes.Add((indent - 1, line));
                }
            }

            var sb = new StringBuilder();
            foreach (var note in notes)
            {
                if (sb.Length > 0)
                {
                    sb.AppendLine();
                }
                sb.AppendLine("Name: " + note.Name);
                if (note.Notes.Count > 0)
                {
                    sb.AppendLine("Notes:");
                    var sbFinal = new StringBuilder();
                    // List indentation is 4 spaces
                    // See: https://support.airtable.com/hc/en-us/articles/360044741993
                    foreach (var (indent, text) in note.Notes)
                    {
                        sbFinal.AppendLine($"{new string(' ', indent * 4)}- {text}");
                    }
                    if (note.Notes.Count == 1)
                    {
                        note.FinalNotes = note.Notes.First().Item2;
                    }
                    else
                    {
                        note.FinalNotes = sbFinal.ToString();
                    }
                    sb.Append(note.FinalNotes);
                }
            }
            var table = radioButtonWork.Checked ? "Work" : "Personal";
            if (MessageBox.Show(this, sb.ToString(), $"Submit to {table} Airtable?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var airtableBase = new AirtableBase(Settings.Default.APIKey, Settings.Default.BaseID))
                {
                    var fields = new Fields[notes.Count];
                    for (var i = 0; i < notes.Count; i++)
                    {
                        fields[i] = new Fields();
                        fields[i].AddField("Name", notes[i].Name);
                        if (!string.IsNullOrEmpty(notes[i].FinalNotes))
                        {
                            fields[i].AddField("Notes", notes[i].FinalNotes);
                        }
                    }
                    var response = await airtableBase.CreateMultipleRecords(table, fields);
                    if (response.Success)
                    {
                        MessageBox.Show(this, "Records added!", "Notes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        treeList.Clear();
                        Close();
                    }
                    else if (response.AirtableApiError is AirtableApiException)
                    {
                        MessageBox.Show(this, response.AirtableApiError.ErrorMessage, "Failed to create records :-(", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(this, "Unknown error", "Failed to create records :-(", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
