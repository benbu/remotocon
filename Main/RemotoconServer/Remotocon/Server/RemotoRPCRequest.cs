using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Remotocon.Server
{
    class RemotoRPCRequest
    {
        String ObjectName;
        String MethodName;
        List<Object> Params;
    }
}
