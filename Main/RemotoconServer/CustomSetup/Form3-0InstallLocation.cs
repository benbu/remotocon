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
    public partial class Form3InstallLocation : Form
    {
        public Form3InstallLocation()
        {
            InitializeComponent();
        }

        private void Form3InstallLocation_Load(object sender, EventArgs e)
        {
            txtInstallLocation.Text = InstallManager.InstallDirectory;
        }

        private void txtInstallLocation_TextChanged(object sender, EventArgs e)
        {
            InstallManager.InstallDirectory = txtInstallLocation.Text;
            lblInstallLocation.Text = Path.Combine(InstallManager.InstallDirectory, "remotocon");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog(this);
            txtInstallLocation.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnLocationNext_Click(object sender, EventArgs e)
        {
            InstallManager.Startup = ckbStartup.Checked;

            Hide();
            if (InstallManager.config == null)
                InstallManager.config = new Form3_1Configure();
            InstallManager.config.Show();
        }

        private void btnLocationBack_Click(object sender, EventArgs e)
        {
            Hide();
            InstallManager.license.Show();
        }

        private void btnLocationCancel_Click(object sender, EventArgs e)
        {
            InstallManager.RollbackCancel(sender, e);
        }
    }
}
