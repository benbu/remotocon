using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Remotocon.Server
{
    class DuplicateObjectMethodNameException : Exception
    {
        public DuplicateObjectMethodNameException(string message)
            : base(message)
        { }

        public DuplicateObjectMethodNameException(string p, params object[] args)
            : base(String.Format(p, args))
        { }
    }
}
