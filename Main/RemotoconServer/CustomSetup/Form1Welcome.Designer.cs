namespace CustomSetup
{
    partial class Form1Welcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1Welcome));
            this.lblWelcomeText = new System.Windows.Forms.Label();
            this.lblWelcomeTitle = new System.Windows.Forms.Label();
            this.btnWelcomeNext = new System.Windows.Forms.Button();
            this.btnWelcomeCancel = new System.Windows.Forms.Button();
            this.picWelcome = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picWelcome)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWelcomeText
            // 
            this.lblWelcomeText.AutoSize = true;
            this.lblWelcomeText.Location = new System.Drawing.Point(138, 144);
            this.lblWelcomeText.Name = "lblWelcomeText";
            this.lblWelcomeText.Size = new System.Drawing.Size(269, 39);
            this.lblWelcomeText.TabIndex = 0;
            this.lblWelcomeText.Text = "This guide will take you through the installation process.\r\n\r\nPress \'next\' to con" +
                "tinue!";
            // 
            // lblWelcomeTitle
            // 
            this.lblWelcomeTitle.AutoSize = true;
            this.lblWelcomeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcomeTitle.Location = new System.Drawing.Point(138, 85);
            this.lblWelcomeTitle.Name = "lblWelcomeTitle";
            this.lblWelcomeTitle.Size = new System.Drawing.Size(312, 17);
            this.lblWelcomeTitle.TabIndex = 1;
            this.lblWelcomeTitle.Text = "Welcome to the Remotocon Setup Wizard!";
            // 
            // btnWelcomeNext
            // 
            this.btnWelcomeNext.Location = new System.Drawing.Point(339, 325);
            this.btnWelcomeNext.Name = "btnWelcomeNext";
            this.btnWelcomeNext.Size = new System.Drawing.Size(102, 23);
            this.btnWelcomeNext.TabIndex = 2;
            this.btnWelcomeNext.Text = "Next >>";
            this.btnWelcomeNext.UseVisualStyleBackColor = true;
            this.btnWelcomeNext.Click += new System.EventHandler(this.btnWelcomeNext_Click);
            // 
            // btnWelcomeCancel
            // 
            this.btnWelcomeCancel.Location = new System.Drawing.Point(139, 325);
            this.btnWelcomeCancel.Name = "btnWelcomeCancel";
            this.btnWelcomeCancel.Size = new System.Drawing.Size(94, 23);
            this.btnWelcomeCancel.TabIndex = 3;
            this.btnWelcomeCancel.Text = "Cancel";
            this.btnWelcomeCancel.UseVisualStyleBackColor = true;
            this.btnWelcomeCancel.Click += new System.EventHandler(this.btnWelcomeCancel_Click);
            // 
            // picWelcome
            // 
            this.picWelcome.Image = ((System.Drawing.Image)(resources.GetObject("picWelcome.Image")));
            this.picWelcome.Location = new System.Drawing.Point(1, 1);
            this.picWelcome.Name = "picWelcome";
            this.picWelcome.Size = new System.Drawing.Size(132, 359);
            this.picWelcome.TabIndex = 4;
            this.picWelcome.TabStop = false;
            // 
            // Form1Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 360);
            this.ControlBox = false;
            this.Controls.Add(this.picWelcome);
            this.Controls.Add(this.btnWelcomeCancel);
            this.Controls.Add(this.btnWelcomeNext);
            this.Controls.Add(this.lblWelcomeTitle);
            this.Controls.Add(this.lblWelcomeText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(469, 398);
            this.MinimumSize = new System.Drawing.Size(469, 398);
            this.Name = "Form1Welcome";
            this.Text = "Remotocon Installation Wizard - Welcome";
            ((System.ComponentModel.ISupportInitialize)(this.picWelcome)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWelcomeText;
        private System.Windows.Forms.Label lblWelcomeTitle;
        private System.Windows.Forms.Button btnWelcomeNext;
        private System.Windows.Forms.Button btnWelcomeCancel;
        private System.Windows.Forms.PictureBox picWelcome;
    }
}

