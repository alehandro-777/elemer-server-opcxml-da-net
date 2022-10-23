using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PollingProccessSupport.Interfaces
{
    public interface IIoQuery
    {
        void BeginExecute(System.Threading.WaitCallback queryComplete);
    }

}
