using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Remotocon.Server
{
    class XmlRpcException : Exception
    {
        public XmlRpcException(string message)
            : base(message)
        { }

        public XmlRpcException(string p, params object[] args)
            : this(String.Format(p, args))
        { }
    }
}
