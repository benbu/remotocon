using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RemotoconServerPlugin;
using Remotocon.Server;

namespace Remotocon
{
    public partial class MainForm : Form, IPluginList
    {
        TextWriter _writer = null;
        TcpServer _server = null;
        ServerPluginServices pluginServices = null;
        ServerState serverState = ServerState.Stopped;
        Configuration config;

        public delegate bool SetStringTextCallback(string message);
        public delegate bool SetCharTextCallback(char message);
        public delegate void RefreshCallback();

        public MainForm(bool startMinimized)
        {
            InitializeComponent();
            if (startMinimized)
                WindowState = FormWindowState.Minimized;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Instantiate the writer
            _writer = new TextBoxStreamWriter(SetCharTextSafely, SetStringTextSafely);
            // Redirect the out Console stream
            Console.SetOut(_writer);

            Console.WriteLine("Now redirecting output to the text box");

            Configuration.Initialize(this);
            config = Configuration.Instance;

            InitializeServer();
            Resize += new EventHandler(MainForm_Resize);
            notifyIcon1.DoubleClick += new EventHandler(notifyIcon1_DoubleClick);
            _server.Start();

            MainForm_Resize(this, new EventArgs());
        }

        void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Restore();
        }

        void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }

        public bool SetStringTextSafely(string message)
        {
            if (txtConsole.InvokeRequired)
            {
                SetStringTextCallback d = new SetStringTextCallback(SetStringTextSafely);
                this.Invoke(d, message);
            }
            else
                txtConsole.AppendText(message);

            return true;
        }

        public bool SetCharTextSafely(char message)
        {
            if (txtConsole.InvokeRequired)
            {
                SetCharTextCallback d = new SetCharTextCallback(SetCharTextSafely);
                this.Invoke(d, message);
            }
            else
                txtConsole.AppendText(message.ToString());

            return true;
        }

        private void InitializeServer()
        {
            _server = new TcpServer(config.UserName, config.Password, config.Port);
            _server.OutStream = _writer;
            pluginServices = new ServerPluginServices();
            pluginServices.FindPlugins(_server);
            _server.RegisterHandler("Server", new ServerHandler(_server, pluginServices, this));

            RefreshPluginListSafely();
        }

        public void RefreshPluginListSafely()
        {
            if (lvPlugins.InvokeRequired)
            {
                RefreshCallback d = new RefreshCallback(RefreshPluginListSafely);
                this.Invoke(d);
            }
            else
            {
                lvPlugins.Items.Clear();

                foreach (ActivePlugin ap in pluginServices.ActivePlugins)
                {
                    ListViewItem lvi = new ListViewItem(ap.Instance.Name);
                    lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, ap.Instance.Version));
                    lvPlugins.Items.Add(lvi);
                }
            }
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration.Instance.SpawnSettingsWindow(this);
        }

        public void settingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((SettingsForm)sender).SettingsChanged && serverState != ServerState.Stopped)
            {
                DialogResult result = MessageBox.Show(
                    "Server must be restarted for changes to take effect. Would you like to restart the server now?",
                    "Server restart required.",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1
                    );

                if (result == DialogResult.Yes)
                    RestartServer();
            }
        }

        private void RestartServer()
        {
            _server.Stop();
            InitializeServer();
            _server.Start();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Restore();
        }

        private void Restore()
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration.Instance.SpawnSettingsWindow(this);
        }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
