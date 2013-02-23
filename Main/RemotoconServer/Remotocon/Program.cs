using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Remotocon;

namespace Remotocon
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            bool startup = false;
            if (args.Length > 0)
                startup = args[0].Equals("-startup", StringComparison.CurrentCultureIgnoreCase);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(startup));
        }
    }
}
