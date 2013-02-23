using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomSetup
{
    public partial class Form3_1Configure : Form
    {
        public Form3_1Configure()
        {
            InitializeComponent();
        }

        private void btnConfigNext_Click(object sender, EventArgs e)
        {
            //Write new config file
            InstallManager.UserName = txtUserName.Text;
            InstallManager.Password = txtPassWord.Text;
            InstallManager.Port = (ushort)numPort.Value;
            InstallManager.Dedicated = ckRemotoconDedicated.Checked;

            Hide();
            if (InstallManager.progress == null)
                InstallManager.progress = new Form4Progress();
            InstallManager.progress.Show();
        }

        private void btnConfigCancel_Click(object sender, EventArgs e)
        {
            InstallManager.RollbackCancel(sender, e);
        }

        private void btnConfigBack_Click(object sender, EventArgs e)
        {
            Hide();
            InstallManager.location.Show();
        }
    }
}
