using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.ComponentModel;
using Remotocon.Server;

namespace CustomSetup
{
    class InstallManager
    {
        private static string _installDirectory;

        public static Form1Welcome welcome;
        public static Form2License license;
        public static Form3InstallLocation location;
        public static Form4Progress progress;
        public static Form3_1Configure config;
        public static Form5Finish finish;

        public static bool Startup;

        public static string UserName = "";
        public static string Password = "";
        public static ushort Port = 4646;
        public static bool Dedicated = false;

        public static string InstallDirectory 
        {
            get
            {
                if (_installDirectory == null)
                {
                    _installDirectory = Environment.GetEnvironmentVariable("PROGRAMFILES(X86)") ??
                        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                    _installDirectory = Path.Combine(_installDirectory, "Remotocon");
                }
                return _installDirectory;
            }
            set
            {
                _installDirectory = value;
            }
        }

        internal static void RollbackCancel(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Sure?", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                RollbackInstallation();
                Application.Exit();
            }
        }

        public static void InstallApplication(BackgroundWorker bgw)
        {
            bgw.ReportProgress(5);

            if (Startup)
                SetStartupRegistry();

            bgw.ReportProgress(10);

            //string InstallLocation = @"C:\Program Files (x86)\Remotocon";
            string pluginsFolder = Path.Combine(InstallDirectory, "Plugins");

            if (!Directory.Exists(InstallDirectory))
                Directory.CreateDirectory(InstallDirectory);

            if (!Directory.Exists(pluginsFolder))
                Directory.CreateDirectory(pluginsFolder);

            string remotoconFilePath = Path.Combine(InstallDirectory, "remotocon.exe");
            string remotoconConfigPath = Path.Combine(InstallDirectory, "remotocon.exe.config");

            File.WriteAllBytes(remotoconFilePath, Files.remotocon_exe);

            bgw.ReportProgress(60);

            string EncryptedPass = GetEncryptedPass();

            bgw.ReportProgress(80);

            WriteConfigFile(EncryptedPass, UserName, Port, Dedicated);

            bgw.ReportProgress(100);
        }

        private static void RollbackInstallation()
        {
            try
            {
                if(Directory.Exists(InstallDirectory))
                    Directory.Delete(InstallDirectory, true);

                //remove any registry settings that might have been set.
                RegistryKey rkApp = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                rkApp.DeleteValue("Remotocon", false);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Removing Installation Items", MessageBoxButtons.OK);
            }
        }

        internal static void SetStartupRegistry()
        {
            RegistryKey rkApp = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rkApp.SetValue("Remotocon", Path.Combine(InstallDirectory, "remotocon.exe"));
        }

        internal static string GetEncryptedPass()
        {
            Encryption encryptor = new Encryption();
            return encryptor.EncryptToString(Password);
        }

        private static void WriteConfigFile(string encryptedPass, string username, ushort port, bool dedicated)
        {
            try
            {
                string configFilePath = Path.Combine(InstallManager.InstallDirectory, "config.dat");

                BinaryWriter bw = new BinaryWriter(File.Open(configFilePath, FileMode.Create));
                bw.Write(encryptedPass);
                bw.Write(username);
                bw.Write(port);
                bw.Write(dedicated);

                bw.Flush();
                bw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error while trying to save settings.");
            }
        }
    }
}
