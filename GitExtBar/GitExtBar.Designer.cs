namespace GitExtBar
{
    partial class GitExtBar
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
            this.listViewActions = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelAction = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewActions
            // 
            this.listViewActions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewActions.Location = new System.Drawing.Point(-1, 9);
            this.listViewActions.Name = "listViewActions";
            this.listViewActions.Size = new System.Drawing.Size(415, 42);
            this.listViewActions.TabIndex = 1;
            this.listViewActions.UseCompatibleStateImageBehavior = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.listViewActions);
            this.panel1.Location = new System.Drawing.Point(12, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(415, 61);
            this.panel1.TabIndex = 2;
            // 
            // labelAction
            // 
            this.labelAction.AutoSize = true;
            this.labelAction.Location = new System.Drawing.Point(12, 9);
            this.labelAction.Name = "labelAction";
            this.labelAction.Size = new System.Drawing.Size(93, 13);
            this.labelAction.TabIndex = 0;
            this.labelAction.Text = "Choose an action:";
            // 
            // GitExtBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 108);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelAction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GitExtBar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Git Extensions";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listViewActions;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelAction;
    }
}

