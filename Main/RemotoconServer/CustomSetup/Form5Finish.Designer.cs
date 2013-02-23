namespace CustomSetup
{
    partial class Form5Finish
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5Finish));
            this.btnFinishClose = new System.Windows.Forms.Button();
            this.picFinished = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picFinished)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFinishClose
            // 
            this.btnFinishClose.Location = new System.Drawing.Point(366, 325);
            this.btnFinishClose.Name = "btnFinishClose";
            this.btnFinishClose.Size = new System.Drawing.Size(75, 23);
            this.btnFinishClose.TabIndex = 1;
            this.btnFinishClose.Text = "Close";
            this.btnFinishClose.UseVisualStyleBackColor = true;
            this.btnFinishClose.Click += new System.EventHandler(this.btnFinishClose_Click);
            // 
            // picFinished
            // 
            this.picFinished.Image = ((System.Drawing.Image)(resources.GetObject("picFinished.Image")));
            this.picFinished.Location = new System.Drawing.Point(1, 1);
            this.picFinished.Name = "picFinished";
            this.picFinished.Size = new System.Drawing.Size(132, 359);
            this.picFinished.TabIndex = 2;
            this.picFinished.TabStop = false;
            // 
            // Form5Finish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 360);
            this.Controls.Add(this.picFinished);
            this.Controls.Add(this.btnFinishClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(469, 398);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(469, 398);
            this.Name = "Form5Finish";
            this.Text = "Form7Finish";
            ((System.ComponentModel.ISupportInitialize)(this.picFinished)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFinishClose;
        private System.Windows.Forms.PictureBox picFinished;
    }
}