using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemotoconServerPlugin;

namespace Remotocon.Server
{
    public class ActivePlugin
    {
        //This is the actual ActivePlugin object.. 
        //Holds an instance of the plugin to access
        //ALso holds assembly path... not really necessary
        private IServerPlugin myInstance = null;
        private string myAssemblyPath = "";

        public IServerPlugin Instance
        {
            get { return myInstance; }
            set { myInstance = value; }
        }
        public string AssemblyPath
        {
            get { return myAssemblyPath; }
            set { myAssemblyPath = value; }
        }
    }
}
