using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using System.IO;
using RemotoconServerPlugin;

namespace Remotocon.Server
{
    public class ServerHandler
    {
        private ServerPluginServices PluginServices;
        private IXmlRpcServer Server;
        private IPluginList PluginList;

        private static Dictionary<String, bool> builtInPlugins = new Dictionary<string, bool>()
        {
            {"FileManager", true}
        };

        public ServerHandler(IXmlRpcServer server, ServerPluginServices pluginServices, IPluginList pluginList)
        {
            PluginServices = pluginServices;
            Server = server;
            PluginList = pluginList;
        }

        public bool HasConnection()
        {
            return true;
        }

        public List<Dictionary<string, string>> GetServerPlugins()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach (ActivePlugin ap in PluginServices.ActivePlugins)
            {
                Dictionary<string, string> pluginDict = new Dictionary<string, string>();

                pluginDict.Add("Name", ap.Instance.Name);
                pluginDict.Add("Version", ap.Instance.Version);
                pluginDict.Add("AndroidPackageName", ap.Instance.AndroidPackageName);

                list.Add(pluginDict);
            }
            return list;
        }

        public bool DownloadInstallPlugin(String url, String dllName)
        {
            WebClient client = new WebClient();
            String pluginFilePath = Path.Combine(ServerPluginServices.PluginDirectory, dllName);
            client.DownloadFile(url, pluginFilePath);
            PluginServices.AddPlugin(pluginFilePath, Server);
            PluginList.RefreshPluginListSafely();
            return true;
        }
    }
}
