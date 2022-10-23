using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpcXmlDaWcfService
{
    public static class LogConsole
    {
        static List<string> messageList = new List<string>();
        public static string GetLogMessage(int index)
        {
            string mess = null;
            try
            {
                mess = messageList[index];
            }
            catch
            {
                mess = "error LogConsole index";
            }
            return mess;
        }
        public static int GetLogMessageSize()
        {
            return messageList.Count;
        }
        public static void WriteLine(string logmessage)
        {
            if (messageList.Count > 1000) { messageList.Clear(); }
            messageList.Add( DateTime.Now + " : " + logmessage);
        }
    }
}