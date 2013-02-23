namespace CustomSetup
{
    partial class Form2License
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2License));
            this.txtLicense = new System.Windows.Forms.TextBox();
            this.rdoLicenseAccept = new System.Windows.Forms.RadioButton();
            this.rdoLicenseDecline = new System.Windows.Forms.RadioButton();
            this.btnLicenseNext = new System.Windows.Forms.Button();
            this.btnLicenseBack = new System.Windows.Forms.Button();
            this.btnLicenseCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLicense
            // 
            this.txtLicense.Location = new System.Drawing.Point(12, 12);
            this.txtLicense.Multiline = true;
            this.txtLicense.Name = "txtLicense";
            this.txtLicense.ReadOnly = true;
            this.txtLicense.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLicense.Size = new System.Drawing.Size(429, 276);
            this.txtLicense.TabIndex = 0;
            this.txtLicense.Text = resources.GetString("txtLicense.Text");
            // 
            // rdoLicenseAccept
            // 
            this.rdoLicenseAccept.AutoSize = true;
            this.rdoLicenseAccept.Location = new System.Drawing.Point(351, 5);
            this.rdoLicenseAccept.Name = "rdoLicenseAccept";
            this.rdoLicenseAccept.Size = new System.Drawing.Size(59, 17);
            this.rdoLicenseAccept.TabIndex = 1;
            this.rdoLicenseAccept.TabStop = true;
            this.rdoLicenseAccept.Text = "Accept";
            this.rdoLicenseAccept.UseVisualStyleBackColor = true;
            // 
            // rdoLicenseDecline
            // 
            this.rdoLicenseDecline.AutoSize = true;
            this.rdoLicenseDecline.Location = new System.Drawing.Point(247, 5);
            this.rdoLicenseDecline.Name = "rdoLicenseDecline";
            this.rdoLicenseDecline.Size = new System.Drawing.Size(61, 17);
            this.rdoLicenseDecline.TabIndex = 2;
            this.rdoLicenseDecline.TabStop = true;
            this.rdoLicenseDecline.Text = "Decline";
            this.rdoLicenseDecline.UseVisualStyleBackColor = true;
            // 
            // btnLicenseNext
            // 
            this.btnLicenseNext.Location = new System.Drawing.Point(340, 325);
            this.btnLicenseNext.Name = "btnLicenseNext";
            this.btnLicenseNext.Size = new System.Drawing.Size(101, 23);
            this.btnLicenseNext.TabIndex = 3;
            this.btnLicenseNext.Text = "Next >>";
            this.btnLicenseNext.UseVisualStyleBackColor = true;
            this.btnLicenseNext.Click += new System.EventHandler(this.btnLicenseNext_Click);
            // 
            // btnLicenseBack
            // 
            this.btnLicenseBack.Location = new System.Drawing.Point(239, 325);
            this.btnLicenseBack.Name = "btnLicenseBack";
            this.btnLicenseBack.Size = new System.Drawing.Size(95, 23);
            this.btnLicenseBack.TabIndex = 4;
            this.btnLicenseBack.Text = "<< Back";
            this.btnLicenseBack.UseVisualStyleBackColor = true;
            this.btnLicenseBack.Click += new System.EventHandler(this.btnLicenseBack_Click);
            // 
            // btnLicenseCancel
            // 
            this.btnLicenseCancel.Location = new System.Drawing.Point(139, 325);
            this.btnLicenseCancel.Name = "btnLicenseCancel";
            this.btnLicenseCancel.Size = new System.Drawing.Size(94, 23);
            this.btnLicenseCancel.TabIndex = 5;
            this.btnLicenseCancel.Text = "Cancel";
            this.btnLicenseCancel.UseVisualStyleBackColor = true;
            this.btnLicenseCancel.Click += new System.EventHandler(this.btnLicenseCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoLicenseAccept);
            this.panel1.Controls.Add(this.rdoLicenseDecline);
            this.panel1.Location = new System.Drawing.Point(12, 294);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 25);
            this.panel1.TabIndex = 6;
            // 
            // Form2License
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 360);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnLicenseCancel);
            this.Controls.Add(this.btnLicenseBack);
            this.Controls.Add(this.btnLicenseNext);
            this.Controls.Add(this.txtLicense);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(469, 398);
            this.MinimumSize = new System.Drawing.Size(469, 398);
            this.Name = "Form2License";
            this.Text = "Remotocon Installation Wizard - License Agreement";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLicense;
        private System.Windows.Forms.RadioButton rdoLicenseAccept;
        private System.Windows.Forms.RadioButton rdoLicenseDecline;
        private System.Windows.Forms.Button btnLicenseNext;
        private System.Windows.Forms.Button btnLicenseBack;
        private System.Windows.Forms.Button btnLicenseCancel;
        private System.Windows.Forms.Panel panel1;
    }
}