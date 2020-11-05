using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Notes
{
    [Designer(typeof(ParentControlDesigner))]
    public class TreeList : RichTextBox
    {
    }
}
