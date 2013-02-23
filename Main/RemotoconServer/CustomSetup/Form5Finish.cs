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
    public partial class Form5Finish : Form
    {
        public Form5Finish()
        {
            InitializeComponent();
        }

        private void btnFinishClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
