using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Remotocon.Server
{
    class LoginException : Exception
    {
        public LoginException(string message)
            : base(message)
        { }

        public LoginException(string p, params object[] args)
            : this(String.Format(p, args))
        { }
    }
}
