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
    public partial class ElemerOpcXmlDA : ServiceBase
    {
        internal static ServiceHost host = new ServiceHost(typeof(OpcXmlDaWcfService.OpcXmlDaService));

        public ElemerOpcXmlDA()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            host.Open();
        }

        protected override void OnStop()
        {
            if (host != null)
            {
                host.Close();
                host = null;
            }
        }
    }
}
