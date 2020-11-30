
namespace RipGrepGUI
{
    partial class RipGrepGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.richTextBoxResults = new System.Windows.Forms.RichTextBox();
            this.checkBoxContext = new System.Windows.Forms.CheckBox();
            this.checkBoxBinary = new System.Windows.Forms.CheckBox();
            this.checkBoxRegularExpressions = new System.Windows.Forms.CheckBox();
            this.checkBoxCaseSensitive = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(62, 12);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(635, 20);
            this.textBoxSearch.TabIndex = 0;
            // 
            // labelSearch
            // 
            this.labelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(12, 15);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(44, 13);
            this.labelSearch.TabIndex = 1;
            this.labelSearch.Text = "Search:";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Location = new System.Drawing.Point(1059, 10);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 2;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // richTextBoxResults
            // 
            this.richTextBoxResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.richTextBoxResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxResults.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxResults.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.richTextBoxResults.Location = new System.Drawing.Point(15, 38);
            this.richTextBoxResults.Name = "richTextBoxResults";
            this.richTextBoxResults.Size = new System.Drawing.Size(1119, 400);
            this.richTextBoxResults.TabIndex = 3;
            this.richTextBoxResults.Text = "";
            // 
            // checkBoxContext
            // 
            this.checkBoxContext.AutoSize = true;
            this.checkBoxContext.Location = new System.Drawing.Point(803, 14);
            this.checkBoxContext.Name = "checkBoxContext";
            this.checkBoxContext.Size = new System.Drawing.Size(62, 17);
            this.checkBoxContext.TabIndex = 4;
            this.checkBoxContext.Text = "&Context";
            this.checkBoxContext.UseVisualStyleBackColor = true;
            // 
            // checkBoxBinary
            // 
            this.checkBoxBinary.AutoSize = true;
            this.checkBoxBinary.Location = new System.Drawing.Point(871, 14);
            this.checkBoxBinary.Name = "checkBoxBinary";
            this.checkBoxBinary.Size = new System.Drawing.Size(55, 17);
            this.checkBoxBinary.TabIndex = 5;
            this.checkBoxBinary.Text = "Bin&ary";
            this.checkBoxBinary.UseVisualStyleBackColor = true;
            // 
            // checkBoxRegularExpressions
            // 
            this.checkBoxRegularExpressions.AutoSize = true;
            this.checkBoxRegularExpressions.Location = new System.Drawing.Point(932, 14);
            this.checkBoxRegularExpressions.Name = "checkBoxRegularExpressions";
            this.checkBoxRegularExpressions.Size = new System.Drawing.Size(121, 17);
            this.checkBoxRegularExpressions.TabIndex = 6;
            this.checkBoxRegularExpressions.Text = "R&egular expressions";
            this.checkBoxRegularExpressions.UseVisualStyleBackColor = true;
            // 
            // checkBoxCaseInsensitive
            // 
            this.checkBoxCaseSensitive.AutoSize = true;
            this.checkBoxCaseSensitive.Location = new System.Drawing.Point(703, 14);
            this.checkBoxCaseSensitive.Name = "checkBoxCaseInsensitive";
            this.checkBoxCaseSensitive.Size = new System.Drawing.Size(94, 17);
            this.checkBoxCaseSensitive.TabIndex = 7;
            this.checkBoxCaseSensitive.Text = "Case sens&itive";
            this.checkBoxCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // RipGrepGUI
            // 
            this.AcceptButton = this.buttonSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 450);
            this.Controls.Add(this.checkBoxCaseSensitive);
            this.Controls.Add(this.checkBoxRegularExpressions);
            this.Controls.Add(this.checkBoxBinary);
            this.Controls.Add(this.checkBoxContext);
            this.Controls.Add(this.richTextBoxResults);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Name = "RipGrepGUI";
            this.Text = "RipGrepGUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.RichTextBox richTextBoxResults;
        private System.Windows.Forms.CheckBox checkBoxContext;
        private System.Windows.Forms.CheckBox checkBoxBinary;
        private System.Windows.Forms.CheckBox checkBoxRegularExpressions;
        private System.Windows.Forms.CheckBox checkBoxCaseSensitive;
    }
}

