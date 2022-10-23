using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PollingProccessSupport;

namespace ElemerDriver
{
    [Serializable]
    public class IOChanellConfig
    {
        public IOChanellConfig(SerialPortParams serialPortParams, List<ElemerQueryParams> queries)
        {
            SerialPortParams = serialPortParams;
            Queries = queries;
        }
        public SerialPortParams SerialPortParams { get; set; }
        public List<ElemerQueryParams> Queries { get; set; }
    }
}
