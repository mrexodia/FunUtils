using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace TreeList
{
    [Designer(typeof(ParentControlDesigner))]
    public class TreeList : RichTextBox
    {
        private readonly int Indent = 30;

        public TreeList()
        {
            SelectionBullet = true;
            AcceptsTab = true;
        }

        public void DumpHistory()
        {
            var historyDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "history");
            Directory.CreateDirectory(historyDir);
            var historyFile = Path.Combine(historyDir, $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss_fff}.rtf");
            File.WriteAllText(historyFile, Rtf);
        }

        public int GetLineIndent(int line)
        {
            return GetPositionFromCharIndex(GetFirstCharIndexFromLine(line)).X / Indent;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (SelectionLength > 0)
            {
                // Do not attempt to customize the behavior when there is a selection
                base.OnKeyDown(e);
            }
            else if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
                var lineIdx = GetLineFromCharIndex(SelectionStart);
                if (e.Shift)
                {
                    if (SelectionIndent != 0)
                    {
                        // Only allow un-indenting if there is indentation
                        SelectionIndent -= Indent;
                    }
                    else
                    {
                        // Ignore un-indent
                    }
                }
                else if (lineIdx > 0 && GetLineIndent(lineIdx - 1) >= GetLineIndent(lineIdx))
                {
                    // First line cannot be indented
                    // Only allow indenting if the previous line has a bigger or equal level
                    SelectionIndent += Indent;
                }
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (SelectionStart == 0)
                {
                    // Do not delete the list itself
                    e.Handled = true;
                }
                else if (SelectionStart - GetFirstCharIndexOfCurrentLine() == 0)
                {
                    // When pressing backspace on an empty bullet: delete the bullet and move to the end of the previous line
                    SelectionBullet = false;
                    SelectionStart--;
                    SelectionBullet = true;
                    e.Handled = true;
                }
                else
                {
                    base.OnKeyDown(e);
                }
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                // TODO: detect enter on the first line and act accordingly (create new bullet)
                if (/* false && */ SelectionStart - GetFirstCharIndexOfCurrentLine() == 0)
                {
                    // When pressing enter on an empty bullet: do nothing (TODO: allow empty bullets), or reduce nesting if nested
                    if (SelectionIndent > 0)
                        SelectionIndent -= Indent;
                    e.Handled = true;
                }
                else
                {
                    base.OnKeyDown(e);
                }
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == '\t')
            {
                e.Handled = true;
            }
            else
            {
                base.OnKeyPress(e);

                // If the user manages to escape the bulleted list, start a new one
                SelectionBullet = true;
            }
        }
    }
}
