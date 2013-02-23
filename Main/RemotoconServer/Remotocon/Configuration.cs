using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Remotocon
{
    class Configuration
    {
        private static Configuration _instance;

        private string _configFilePath;
        private bool _loaded = false;
        private bool _changed = false;

        private string _password = "";
        private string _userName = "";
        private ushort _port = 4646;
        private bool _dedicated = false;
        private bool _startup = true;

        #region //properties//
        public static Configuration Instance
        {
            get
            {
                return _instance;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    _changed = true;
                }
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    _changed = true;
                }
            }
        }

        public ushort Port
        {
            get
            {
                return _port;
            }
            set
            {
                if (_port != value)
                {
                    _port = value;
                    _changed = true;
                }
            }
        }

        public bool Dedicated
        {
            get
            {
                return _dedicated;
            }
            set
            {
                if (_dedicated != value)
                {
                    _dedicated = value;
                    _changed = true;
                }
            }
        }

        public bool Startup
        {
            get
            {
                return _startup;
            }
            set
            {
                if (_startup != value)
                {
                    _startup = value;
                    _changed = true;
                }
            }
        }

        public string ConfigFilePath
        {
            get
            {
                if (_configFilePath == null)
                    _configFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "config.dat");
                return _configFilePath;
            }
        }

        #endregion //properties//

        protected Configuration()
        { }

        public static void Initialize(MainForm owner)
        {
            _instance = new Configuration();
            if (!_instance.LoadConfigFile())
                _instance.SpawnSettingsWindow(owner);
        }

        /// <returns>true if config file was successfully loaded.</returns>
        private bool LoadConfigFile()
        {
            if (!_loaded)
            {
                _loaded = true;

                if (!File.Exists(ConfigFilePath))
                {
                    MessageBox.Show("Please set a username and password", "Remotocon", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SaveConfigFile();
                    return false;
                }

                BinaryReader br = null;
                try
                {
                    br = new BinaryReader(File.OpenRead(ConfigFilePath));
                    _password = br.ReadString();
                    _userName = br.ReadString();
                    _port = br.ReadUInt16();
                    _dedicated = br.ReadBoolean();
                    _startup = br.ReadBoolean();
                    br.Close();
                }
                catch (Exception e)
                {
                    if (br != null)
                        br.Close();
                    MessageBox.Show("Settings file is corrupted.\nPlease edit settings and re-enter their values.");
                    SaveConfigFile();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if new settings were saved, false if none of the settings have been changed.</returns>
        public bool SaveConfigFile()
        {
            if (_changed)
            {
                BinaryWriter bw = null;
                try
                {
                    bw = new BinaryWriter(File.Open(ConfigFilePath, FileMode.Create));
                    bw.Write(_password);
                    bw.Write(_userName);
                    bw.Write(_port);
                    bw.Write(_dedicated);
                    bw.Write(_startup);

                    bw.Flush();
                    bw.Close();
                    _changed = false;
                }
                catch (Exception e)
                {
                    if (bw != null)
                        bw.Close();
                    MessageBox.Show(e.Message, "Error while trying to save settings.");
                }
                return true;
            }
            return false;
        }

        public void SpawnSettingsWindow(MainForm owner)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog(owner);
            settingsForm.FormClosed += new FormClosedEventHandler(owner.settingsForm_FormClosed);
        }
    }
}
