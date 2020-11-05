namespace Notes
{
    partial class Notes
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
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.radioButtonWork = new System.Windows.Forms.RadioButton();
            this.radioButtonPersonal = new System.Windows.Forms.RadioButton();
            this.treeList = new TreeList.TreeList();
            this.SuspendLayout();
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSubmit.Location = new System.Drawing.Point(422, 290);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonSubmit.TabIndex = 1;
            this.buttonSubmit.Text = "&Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // radioButtonWork
            // 
            this.radioButtonWork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonWork.AutoSize = true;
            this.radioButtonWork.Location = new System.Drawing.Point(365, 293);
            this.radioButtonWork.Name = "radioButtonWork";
            this.radioButtonWork.Size = new System.Drawing.Size(51, 17);
            this.radioButtonWork.TabIndex = 2;
            this.radioButtonWork.Text = "&Work";
            this.radioButtonWork.UseVisualStyleBackColor = true;
            // 
            // radioButtonPersonal
            // 
            this.radioButtonPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonPersonal.AutoSize = true;
            this.radioButtonPersonal.Checked = true;
            this.radioButtonPersonal.Location = new System.Drawing.Point(293, 293);
            this.radioButtonPersonal.Name = "radioButtonPersonal";
            this.radioButtonPersonal.Size = new System.Drawing.Size(66, 17);
            this.radioButtonPersonal.TabIndex = 3;
            this.radioButtonPersonal.TabStop = true;
            this.radioButtonPersonal.Text = "&Personal";
            this.radioButtonPersonal.UseVisualStyleBackColor = true;
            // 
            // treeList
            // 
            this.treeList.AcceptsTab = true;
            this.treeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeList.BackColor = System.Drawing.Color.White;
            this.treeList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList.Location = new System.Drawing.Point(12, 12);
            this.treeList.Name = "treeList";
            this.treeList.Size = new System.Drawing.Size(485, 272);
            this.treeList.TabIndex = 0;
            // 
            // Notes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(509, 325);
            this.Controls.Add(this.radioButtonPersonal);
            this.Controls.Add(this.radioButtonWork);
            this.Controls.Add(this.treeList);
            this.Controls.Add(this.buttonSubmit);
            this.Name = "Notes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonSubmit;
        private TreeList.TreeList treeList;
        private System.Windows.Forms.RadioButton radioButtonWork;
        private System.Windows.Forms.RadioButton radioButtonPersonal;
    }
}

