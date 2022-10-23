using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PollingProccessSupport.Interfaces;

namespace PollingProccessSupport
{
    //конкретный драйвер реализуется так
    public class IoDriver : IIoDriver<IoQueryParams, IoQueryResult>
    {
        public byte[] CreatePacket(IoQueryParams queryParams)
        {
            throw  new NotImplementedException();
        }
        public IoQueryResult ProccessResponse(IoQueryParams queryParams, byte[] responseBuffer)
        {
            throw new NotImplementedException();
        }

    }
}
