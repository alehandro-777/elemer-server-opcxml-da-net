using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PollingProccessSupport.Interfaces
{
    public interface IIoDriver<TQ, TR>
    {
        byte[] CreatePacket(TQ queryParams);
        TR ProccessResponse(TQ queryParams, byte[] responseBuffer);
    }
}
