using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemotoconServerPlugin
{
    public interface IServerPlugin
    {
        IXmlRpcServer Server { get; set; }

        string Name { get; }
        string Version { get; }

        /// <summary>
        /// The packagename parameter is android application's fully qualified package name, 
        /// as declared in the package attribute of the manifest element in the application's manifest file. 
        /// For example:
        ///
        ///     com.example.android.jetboy
        /// </summary>
        string AndroidPackageName { get; }

        /// <summary>
        /// Server.RegisterHandler() should be called in this method.
        /// </summary>
        void Initialize();
        void Dispose();
    }
}
