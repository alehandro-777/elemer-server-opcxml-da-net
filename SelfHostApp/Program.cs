using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;
using OpcXmlDaWcfService;
using System.Xml;

using PollingProccessSupport.Interfaces;
using PollingProccessSupport.Events;
using PollingProccessSupport;

namespace SelfHostApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Tracer tracer = new Tracer();

            DomainEvents.Register<RawDataReadedDomainEventArgs>(tracer.Handle);
            DomainEvents.Register<TraceMsgAddedEventArgs>(tracer.Handle);

            string sUrl = ConfigurationManager.AppSettings["service_url"];

            Uri baseAddress = new Uri(sUrl);

            using (ServiceHost host = new ServiceHost(typeof(OpcXmlDaService), baseAddress))
            {

                BasicHttpBinding binding = new BasicHttpBinding();

                //Add a service endpoint
                host.AddServiceEndpoint(typeof(OpcXmlDaWcfService.IOpcXmlDaService), binding, "");
                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);

                ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                var behavior = host.Description.Behaviors.Find<ServiceBehaviorAttribute>();
                behavior.InstanceContextMode = InstanceContextMode.PerCall;

                // if not found - add behavior with setting turned on 
                if (debug == null)
                {
                    host.Description.Behaviors.Add(
                         new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
                }
                else
                {
                    // make sure setting is turned ON
                    if (!debug.IncludeExceptionDetailInFaults)
                    {
                        debug.IncludeExceptionDetailInFaults = true;
                    }
                }

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();
                Console.WriteLine("ELEMER (2402 and 69)-> OPC XML DA 1.0 Gate Service");
                Console.WriteLine("The service is ready at {0}", baseAddress);
                Console.WriteLine("The service is started at {0}", DateTime.Now);
                Console.WriteLine("Press <Enter> to stop SelfHost Application");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }
        }
    }
}
