using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Remotocon
{
    public partial class SettingsForm : Form
    {
        public bool SettingsChanged = false;
        Remotocon.Server.Encryption encryptor = new Remotocon.Server.Encryption();
        private Configuration config;

        public SettingsForm()
        {
            InitializeComponent();
            config = Configuration.Instance;
            portUpDwn.Value = config.Port;
            txtPassword.Text = encryptor.DecryptToString(config.Password);
            txtUserName.Text = config.UserName;
            ckDedicated.Checked = config.Dedicated;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (config.Port != (ushort)portUpDwn.Value ||
                config.UserName != txtUserName.Text ||
                config.Password != encryptor.EncryptToString(txtPassword.Text) ||
                config.Dedicated != ckDedicated.Checked)
            {
                DialogResult result = MessageBox.Show(
                    "Your changes have not been saved. Would you like to save now?", 
                    "Save", 
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1
                    );

                if (result == DialogResult.Yes)
                    SaveSettings();
            }

            base.OnClosing(e);
        }

        private void SaveSettings()
        {
            config.Port = (ushort)portUpDwn.Value;
            config.UserName = txtUserName.Text;
            config.Password = encryptor.EncryptToString(txtPassword.Text);
            config.Dedicated = ckDedicated.Checked;
            config.Startup = ckStartup.Checked;

            RegistryKey rkApp = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (config.Startup)
                rkApp.SetValue("Remotocon", Application.ExecutablePath + "-startup");
            else
                rkApp.DeleteValue("Remotocon", false);

            SettingsChanged = config.SaveConfigFile();
        }

        private void ckDedicated_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisablePort();
        }

        private void EnableDisablePort()
        {
            portUpDwn.Enabled = !ckDedicated.Checked;
        }
    }
}
