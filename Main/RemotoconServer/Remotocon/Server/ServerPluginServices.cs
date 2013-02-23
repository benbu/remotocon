using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using RemotoconServerPlugin;

namespace Remotocon.Server
{
    public class ServerPluginServices
    {
        private static readonly string applicationDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string _pluginDirectory;
        public static string PluginDirectory
        {
            get
            {
                if (_pluginDirectory == null)
                {
                    _pluginDirectory = Path.Combine(applicationDir, "Plugins");
                    if (!Directory.Exists(_pluginDirectory))
                        Directory.CreateDirectory(_pluginDirectory);
                }
                return _pluginDirectory;
            }
        }
        private List<ActivePlugin> _activePlugins = new List<ActivePlugin>();

        /// <summary>
        /// A Collection of all Plugins Found and Loaded by the FindPlugins() Method
        /// </summary>
        public List<ActivePlugin> ActivePlugins
        {
            get { return _activePlugins; }
            set { _activePlugins = value; }
        }


		/// <summary>
		/// Constructor of the Class
		/// </summary>
        public ServerPluginServices()
		{
		}
		
		/// <summary>
		/// Searches the Application's Startup Directory for Plugins
		/// </summary>
		public void FindPlugins(IXmlRpcServer server)
		{
			FindPlugins(PluginDirectory, server);
		}
		/// <summary>
		/// Searches the passed Path for Plugins
		/// </summary>
		/// <param name="Path">Directory to search for Plugins in</param>
		public void FindPlugins(string Path, IXmlRpcServer server)
		{
			//First empty the collection, we're reloading them all
            ClosePlugins();
		
			//Go through all the files in the plugin directory
			foreach (string fileOn in Directory.GetFiles(Path))
			{
				FileInfo file = new FileInfo(fileOn);
				
				//Preliminary check, must be .dll
				if (file.Extension.Equals(".dll"))
				{	
					//Add the 'plugin'
					this.AddPlugin(fileOn, server);
				}
			}
		}
		
		/// <summary>
		/// Unloads and Closes all AvailablePlugins
		/// </summary>
		public void ClosePlugins()
		{
			foreach (ActivePlugin pluginOn in _activePlugins)
			{
				//Close all plugin instances
				//We call the plugins Dispose sub first incase it has to do 
				//Its own cleanup stuff
				pluginOn.Instance.Dispose(); 
				
				//After we give the plugin a chance to tidy up, get rid of it
				pluginOn.Instance = null;
			}
			
			//Finally, clear our collection of available plugins
			_activePlugins.Clear();
		}
		
		public void AddPlugin(string FileName, IXmlRpcServer server)
		{
			//Create a new assembly from the plugin file we're adding..

            Assembly pluginAssembly = null;

            try
            {
                pluginAssembly = Assembly.LoadFrom(FileName);
            }
            catch (System.IO.FileLoadException fd)
            {
                Console.WriteLine(fd);
            }

            Type[] types = null;

            try
            {
                types = pluginAssembly.GetTypes();
            }
            catch (ReflectionTypeLoadException rtle)
            {
                Console.WriteLine("Error adding plugin, make sure the reference to Remotocon in your plugin project has the CopyLocal property set to false.");
            }

			
			//Next we'll loop through all the Types found in the assembly
			foreach (Type pluginType in pluginAssembly.GetTypes())
			{
				if (pluginType.IsPublic) //Only look at public types
				{
					if (!pluginType.IsAbstract)  //Only look at non-abstract types
					{
						//Gets a type object of the interface we need the plugins to match
						Type typeInterface = pluginType.GetInterface("IServerPlugin", true);
						
						//Make sure the interface we want to use actually exists
						if (typeInterface != null)
						{
							//Create a new available plugin since the type implements the IPlugin interface
							ActivePlugin newPlugin = new ActivePlugin();
							
							//Set the filename where we found it
							newPlugin.AssemblyPath = FileName;
							
							//Create a new instance and store the instance in the collection for later use
							//We could change this later on to not load an instance.. we have 2 options
							//1- Make one instance, and use it whenever we need it.. it's always there
							//2- Don't make an instance, and instead make an instance whenever we use it, then close it
							//For now we'll just make an instance of all the plugins
							newPlugin.Instance = (IServerPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));
							
							//Set the Plugin's host to this class which inherited IPluginHost
							newPlugin.Instance.Server = server;

							//Call the initialization sub of the plugin
                            try
                            {
                                newPlugin.Instance.Initialize();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }

							
							//Add the new plugin to our collection here
							this._activePlugins.Add(newPlugin);
							
							//cleanup a bit
							newPlugin = null;
						}	
						
						typeInterface = null; //Mr. Clean			
					}				
				}			
			}
			
			pluginAssembly = null; //more cleanup
		}
    }
}
