using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CustomSetup
{
    public partial class Form4Progress : Form
    {
        public Form4Progress()
        {
            InitializeComponent();
        }

        private void Form4Progress_Load(object sender, EventArgs e)
        {
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            progressBar1.Value = 0;
            backgroundWorker1.RunWorkerAsync();
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //install app
            InstallManager.InstallApplication(backgroundWorker1);

            Hide();
            if (InstallManager.finish == null)
                InstallManager.finish = new Form5Finish();
            InstallManager.finish.Show();
        }
    }
}
