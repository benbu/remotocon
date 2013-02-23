namespace CustomSetup
{
    partial class Form3InstallLocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3InstallLocation));
            this.label1Info = new System.Windows.Forms.Label();
            this.txtInstallLocation = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblInstallLocation = new System.Windows.Forms.Label();
            this.btnLocationCancel = new System.Windows.Forms.Button();
            this.btnLocationBack = new System.Windows.Forms.Button();
            this.btnLocationNext = new System.Windows.Forms.Button();
            this.ckbStartup = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1Info
            // 
            this.label1Info.AutoSize = true;
            this.label1Info.Location = new System.Drawing.Point(158, 42);
            this.label1Info.Name = "label1Info";
            this.label1Info.Size = new System.Drawing.Size(254, 13);
            this.label1Info.TabIndex = 0;
            this.label1Info.Text = "Remotocon will be installed in the following directory.";
            // 
            // txtInstallLocation
            // 
            this.txtInstallLocation.Location = new System.Drawing.Point(161, 83);
            this.txtInstallLocation.Name = "txtInstallLocation";
            this.txtInstallLocation.Size = new System.Drawing.Size(198, 20);
            this.txtInstallLocation.TabIndex = 1;
            this.txtInstallLocation.TextChanged += new System.EventHandler(this.txtInstallLocation_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(366, 81);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblInstallLocation
            // 
            this.lblInstallLocation.AutoSize = true;
            this.lblInstallLocation.Location = new System.Drawing.Point(128, 110);
            this.lblInstallLocation.Name = "lblInstallLocation";
            this.lblInstallLocation.Size = new System.Drawing.Size(0, 13);
            this.lblInstallLocation.TabIndex = 3;
            // 
            // btnLocationCancel
            // 
            this.btnLocationCancel.Location = new System.Drawing.Point(139, 325);
            this.btnLocationCancel.Name = "btnLocationCancel";
            this.btnLocationCancel.Size = new System.Drawing.Size(86, 23);
            this.btnLocationCancel.TabIndex = 8;
            this.btnLocationCancel.Text = "Cancel";
            this.btnLocationCancel.UseVisualStyleBackColor = true;
            this.btnLocationCancel.Click += new System.EventHandler(this.btnLocationCancel_Click);
            // 
            // btnLocationBack
            // 
            this.btnLocationBack.Location = new System.Drawing.Point(231, 325);
            this.btnLocationBack.Name = "btnLocationBack";
            this.btnLocationBack.Size = new System.Drawing.Size(95, 23);
            this.btnLocationBack.TabIndex = 7;
            this.btnLocationBack.Text = "<< Back";
            this.btnLocationBack.UseVisualStyleBackColor = true;
            this.btnLocationBack.Click += new System.EventHandler(this.btnLocationBack_Click);
            // 
            // btnLocationNext
            // 
            this.btnLocationNext.Location = new System.Drawing.Point(332, 325);
            this.btnLocationNext.Name = "btnLocationNext";
            this.btnLocationNext.Size = new System.Drawing.Size(101, 23);
            this.btnLocationNext.TabIndex = 6;
            this.btnLocationNext.Text = "Next >>";
            this.btnLocationNext.UseVisualStyleBackColor = true;
            this.btnLocationNext.Click += new System.EventHandler(this.btnLocationNext_Click);
            // 
            // ckbStartup
            // 
            this.ckbStartup.AutoSize = true;
            this.ckbStartup.Checked = true;
            this.ckbStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbStartup.Location = new System.Drawing.Point(161, 206);
            this.ckbStartup.Name = "ckbStartup";
            this.ckbStartup.Size = new System.Drawing.Size(95, 17);
            this.ckbStartup.TabIndex = 9;
            this.ckbStartup.Text = "Run at Startup";
            this.ckbStartup.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 359);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // Form3InstallLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 360);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ckbStartup);
            this.Controls.Add(this.btnLocationCancel);
            this.Controls.Add(this.btnLocationBack);
            this.Controls.Add(this.btnLocationNext);
            this.Controls.Add(this.lblInstallLocation);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtInstallLocation);
            this.Controls.Add(this.label1Info);
            this.MaximumSize = new System.Drawing.Size(469, 398);
            this.MinimumSize = new System.Drawing.Size(469, 398);
            this.Name = "Form3InstallLocation";
            this.Text = "Remotocon Installation Wizard - Install Location";
            this.Load += new System.EventHandler(this.Form3InstallLocation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1Info;
        private System.Windows.Forms.TextBox txtInstallLocation;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblInstallLocation;
        private System.Windows.Forms.Button btnLocationCancel;
        private System.Windows.Forms.Button btnLocationBack;
        private System.Windows.Forms.Button btnLocationNext;
        private System.Windows.Forms.CheckBox ckbStartup;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}