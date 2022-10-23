using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PollingProccessSupport;

namespace ElemerDriver
{
    public class IOServerConfig
    {
        public IOServerConfig(List<IOChanellConfig> channellsCfg)
	    {
            ChannellsCfg = channellsCfg;
	    }
        public List<IOChanellConfig> ChannellsCfg { get; set; }
    }
}
