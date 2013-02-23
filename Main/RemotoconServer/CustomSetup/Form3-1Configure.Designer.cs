namespace CustomSetup
{
    partial class Form3_1Configure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3_1Configure));
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.lblPort = new System.Windows.Forms.Label();
            this.btnConfigNext = new System.Windows.Forms.Button();
            this.btnConfigCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckRemotoconDedicated = new System.Windows.Forms.CheckBox();
            this.lblAuth = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblPortInfo = new System.Windows.Forms.Label();
            this.btnConfigBack = new System.Windows.Forms.Button();
            this.picConfig = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(106, 105);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(135, 20);
            this.txtUserName.TabIndex = 0;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(40, 108);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(60, 13);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "UserName:";
            // 
            // txtPassWord
            // 
            this.txtPassWord.Location = new System.Drawing.Point(106, 132);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Size = new System.Drawing.Size(135, 20);
            this.txtPassWord.TabIndex = 2;
            this.txtPassWord.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(44, 135);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Password:";
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(106, 68);
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(73, 20);
            this.numPort.TabIndex = 4;
            this.numPort.Value = new decimal(new int[] {
            4646,
            0,
            0,
            0});
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(71, 68);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 13);
            this.lblPort.TabIndex = 5;
            this.lblPort.Text = "Port:";
            // 
            // btnConfigNext
            // 
            this.btnConfigNext.Location = new System.Drawing.Point(351, 325);
            this.btnConfigNext.Name = "btnConfigNext";
            this.btnConfigNext.Size = new System.Drawing.Size(90, 23);
            this.btnConfigNext.TabIndex = 6;
            this.btnConfigNext.Text = "Next >>";
            this.btnConfigNext.UseVisualStyleBackColor = true;
            this.btnConfigNext.Click += new System.EventHandler(this.btnConfigNext_Click);
            // 
            // btnConfigCancel
            // 
            this.btnConfigCancel.Location = new System.Drawing.Point(143, 325);
            this.btnConfigCancel.Name = "btnConfigCancel";
            this.btnConfigCancel.Size = new System.Drawing.Size(100, 23);
            this.btnConfigCancel.TabIndex = 7;
            this.btnConfigCancel.Text = "Cancel";
            this.btnConfigCancel.UseVisualStyleBackColor = true;
            this.btnConfigCancel.Click += new System.EventHandler(this.btnConfigCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckRemotoconDedicated);
            this.groupBox1.Controls.Add(this.lblAuth);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.lblUserName);
            this.groupBox1.Controls.Add(this.txtPassWord);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Location = new System.Drawing.Point(143, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 201);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Authentication";
            // 
            // ckRemotoconDedicated
            // 
            this.ckRemotoconDedicated.AutoSize = true;
            this.ckRemotoconDedicated.Enabled = false;
            this.ckRemotoconDedicated.Location = new System.Drawing.Point(106, 169);
            this.ckRemotoconDedicated.Name = "ckRemotoconDedicated";
            this.ckRemotoconDedicated.Size = new System.Drawing.Size(133, 17);
            this.ckRemotoconDedicated.TabIndex = 5;
            this.ckRemotoconDedicated.Text = "Remotocon Dedicated";
            this.ckRemotoconDedicated.UseVisualStyleBackColor = true;
            // 
            // lblAuth
            // 
            this.lblAuth.AutoSize = true;
            this.lblAuth.Location = new System.Drawing.Point(17, 27);
            this.lblAuth.Name = "lblAuth";
            this.lblAuth.Size = new System.Drawing.Size(250, 65);
            this.lblAuth.TabIndex = 4;
            this.lblAuth.Text = resources.GetString("lblAuth.Text");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblPortInfo);
            this.groupBox2.Controls.Add(this.lblPort);
            this.groupBox2.Controls.Add(this.numPort);
            this.groupBox2.Location = new System.Drawing.Point(143, 219);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 100);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Port";
            // 
            // lblPortInfo
            // 
            this.lblPortInfo.AutoSize = true;
            this.lblPortInfo.Location = new System.Drawing.Point(17, 25);
            this.lblPortInfo.Name = "lblPortInfo";
            this.lblPortInfo.Size = new System.Drawing.Size(261, 26);
            this.lblPortInfo.TabIndex = 6;
            this.lblPortInfo.Text = "If you need to use a different port then change it here.\r\nOtherwise you can leave" +
                " it alone.";
            // 
            // btnConfigBack
            // 
            this.btnConfigBack.Location = new System.Drawing.Point(250, 325);
            this.btnConfigBack.Name = "btnConfigBack";
            this.btnConfigBack.Size = new System.Drawing.Size(95, 23);
            this.btnConfigBack.TabIndex = 10;
            this.btnConfigBack.Text = "<< Back";
            this.btnConfigBack.UseVisualStyleBackColor = true;
            this.btnConfigBack.Click += new System.EventHandler(this.btnConfigBack_Click);
            // 
            // picConfig
            // 
            this.picConfig.Image = ((System.Drawing.Image)(resources.GetObject("picConfig.Image")));
            this.picConfig.Location = new System.Drawing.Point(1, 1);
            this.picConfig.Name = "picConfig";
            this.picConfig.Size = new System.Drawing.Size(132, 359);
            this.picConfig.TabIndex = 11;
            this.picConfig.TabStop = false;
            // 
            // Form3_1Configure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 360);
            this.ControlBox = false;
            this.Controls.Add(this.picConfig);
            this.Controls.Add(this.btnConfigBack);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnConfigCancel);
            this.Controls.Add(this.btnConfigNext);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(469, 398);
            this.Name = "Form3_1Configure";
            this.Text = "Remotocon Installation Wizard - Configure";
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConfig)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Button btnConfigNext;
        private System.Windows.Forms.Button btnConfigCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckRemotoconDedicated;
        private System.Windows.Forms.Label lblAuth;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblPortInfo;
        private System.Windows.Forms.Button btnConfigBack;
        private System.Windows.Forms.PictureBox picConfig;
    }
}