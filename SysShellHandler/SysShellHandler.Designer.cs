namespace SysShellHandler
{
    partial class SysShellHandler
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
            this.buttonCffExplorer = new System.Windows.Forms.Button();
            this.buttonMakeUsermode = new System.Windows.Forms.Button();
            this.buttonTestsign = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonServiceRegistration = new System.Windows.Forms.Button();
            this.buttonServiceControl = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCffExplorer
            // 
            this.buttonCffExplorer.Location = new System.Drawing.Point(12, 12);
            this.buttonCffExplorer.Name = "buttonCffExplorer";
            this.buttonCffExplorer.Size = new System.Drawing.Size(125, 23);
            this.buttonCffExplorer.TabIndex = 0;
            this.buttonCffExplorer.Text = "&CFF Explorer";
            this.buttonCffExplorer.UseVisualStyleBackColor = true;
            this.buttonCffExplorer.Click += new System.EventHandler(this.buttonCffExplorer_Click);
            // 
            // buttonMakeUsermode
            // 
            this.buttonMakeUsermode.Location = new System.Drawing.Point(12, 41);
            this.buttonMakeUsermode.Name = "buttonMakeUsermode";
            this.buttonMakeUsermode.Size = new System.Drawing.Size(125, 23);
            this.buttonMakeUsermode.TabIndex = 1;
            this.buttonMakeUsermode.Text = "Make&Usermode";
            this.buttonMakeUsermode.UseVisualStyleBackColor = true;
            this.buttonMakeUsermode.Click += new System.EventHandler(this.buttonMakeUsermode_Click);
            // 
            // buttonTestsign
            // 
            this.buttonTestsign.Location = new System.Drawing.Point(12, 70);
            this.buttonTestsign.Name = "buttonTestsign";
            this.buttonTestsign.Size = new System.Drawing.Size(125, 23);
            this.buttonTestsign.TabIndex = 2;
            this.buttonTestsign.Text = "&Testsign";
            this.buttonTestsign.UseVisualStyleBackColor = true;
            this.buttonTestsign.Click += new System.EventHandler(this.buttonTestsign_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonServiceControl);
            this.groupBox1.Controls.Add(this.buttonServiceRegistration);
            this.groupBox1.Location = new System.Drawing.Point(143, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(88, 81);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service";
            // 
            // buttonServiceRegistration
            // 
            this.buttonServiceRegistration.Location = new System.Drawing.Point(6, 19);
            this.buttonServiceRegistration.Name = "buttonServiceRegistration";
            this.buttonServiceRegistration.Size = new System.Drawing.Size(75, 23);
            this.buttonServiceRegistration.TabIndex = 0;
            this.buttonServiceRegistration.Text = "&Register";
            this.buttonServiceRegistration.UseVisualStyleBackColor = true;
            this.buttonServiceRegistration.Click += new System.EventHandler(this.buttonServiceRegistration_Click);
            // 
            // buttonServiceControl
            // 
            this.buttonServiceControl.Location = new System.Drawing.Point(6, 48);
            this.buttonServiceControl.Name = "buttonServiceControl";
            this.buttonServiceControl.Size = new System.Drawing.Size(75, 23);
            this.buttonServiceControl.TabIndex = 1;
            this.buttonServiceControl.Text = "&Start";
            this.buttonServiceControl.UseVisualStyleBackColor = true;
            this.buttonServiceControl.Click += new System.EventHandler(this.buttonServiceControl_Click);
            // 
            // SysShellHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 102);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonTestsign);
            this.Controls.Add(this.buttonMakeUsermode);
            this.Controls.Add(this.buttonCffExplorer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SysShellHandler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Driver Opener";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCffExplorer;
        private System.Windows.Forms.Button buttonMakeUsermode;
        private System.Windows.Forms.Button buttonTestsign;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonServiceControl;
        private System.Windows.Forms.Button buttonServiceRegistration;
    }
}

