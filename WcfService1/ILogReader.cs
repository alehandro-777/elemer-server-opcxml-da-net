using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace OpcXmlDaWcfService
{
    public interface ILogReader
    {
        string GetLogMessage(int index);
        int GetLogMessageSize();
    }
}