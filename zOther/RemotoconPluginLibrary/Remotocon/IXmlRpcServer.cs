using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemotoconServerPlugin
{
    public interface IXmlRpcServer
    {
        void RegisterHandler(string objectName, object obj);
    }
}
