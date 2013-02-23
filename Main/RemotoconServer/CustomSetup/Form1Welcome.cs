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
    public partial class Form1Welcome : Form
    {
        public Form1Welcome()
        {
            InitializeComponent();

            InstallManager.welcome = this;
        }

        private void btnWelcomeNext_Click(object sender, EventArgs e)
        {
            Hide();
            if (InstallManager.license == null)
                InstallManager.license = new Form2License();
            InstallManager.license.Show();
        }

        private void btnWelcomeCancel_Click(object sender, EventArgs e)
        {
            InstallManager.RollbackCancel(sender, e);
        }
    }
}
