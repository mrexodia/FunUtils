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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(149, 107);
            this.Controls.Add(this.buttonTestsign);
            this.Controls.Add(this.buttonMakeUsermode);
            this.Controls.Add(this.buttonCffExplorer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Driver Opener";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCffExplorer;
        private System.Windows.Forms.Button buttonMakeUsermode;
        private System.Windows.Forms.Button buttonTestsign;
    }
}

