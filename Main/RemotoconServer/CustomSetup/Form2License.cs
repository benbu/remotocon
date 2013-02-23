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
    public partial class Form2License : Form
    {
        public Form2License()
        {
            InitializeComponent();
        }

        private void btnLicenseCancel_Click(object sender, EventArgs e)
        {
            InstallManager.RollbackCancel(sender, e);
        }

        private void btnLicenseNext_Click(object sender, EventArgs e)
        {
            Hide();
            if (InstallManager.location == null)
                InstallManager.location = new Form3InstallLocation();
            InstallManager.location.Show();
        }

        private void btnLicenseBack_Click(object sender, EventArgs e)
        {
            Hide();
            InstallManager.welcome.Show();
        }
    }
}
