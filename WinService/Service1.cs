using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;

using OpcXmlDaWcfService;


namespace HostLibXMLDAService
{
    public partial class Service1 : ServiceBase
    {
        internal static ServiceHost host = null;

        public Service1()
        {
            InitializeComponent();
            // Turn off autologging
            this.AutoLog = false;
            if (!System.Diagnostics.EventLog.SourceExists("ElemerOpcXmlDA"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "ElemerOpcXmlDA", "ElemerOpcXmlDALog");
            }
            // create an event source, specifying the name of a log that
            // does not currently exist to create a new, custom log
            // configure the event log instance to use this source name
            eventLog1.Source = "ElemerOpcXmlDA";
            eventLog1.Log = "ElemerOpcXmlDALog";
        }

        protected override void OnStart(string[] args)
        {
            if (host != null)
            {
                host.Close();
            }

                host = new ServiceHost(typeof(OpcXmlDaWcfService.OpcXmlDaService));  
                host.Open();    
   
            try
            {


            }
            catch (Exception ex)
            {
                //eventLog1.WriteEntry(ex.Message);
            }

        }

        protected override void OnStop()
        {
            if (host != null)
            {
                host.Close();
                host = null;
            }
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}
