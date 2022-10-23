using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PollingProccessSupport.Interfaces
{
    public interface IIoChannell
    {
        void Close();
        void BeginWrite(byte[] writeBuffer, System.Threading.WaitCallback ioChannellWriteComplete);
        void ReadNextChunk();
    }
}
