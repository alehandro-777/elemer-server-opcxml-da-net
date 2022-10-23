using System;
using System.Collections.Generic;
using System.Text;
using ElemerDriver;
using PollingProccessSupport;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using PollingProccessSupport.Interfaces;
using PollingProccessSupport.Events;



namespace ElemerReadingConsole
{
    class Program 
    {
        static void Main(string[] args)
        {
            Tracer tracer = new Tracer();

            DomainEvents.Register<RawDataReadedDomainEventArgs>(tracer.Handle);
            DomainEvents.Register<TraceMsgAddedEventArgs>(tracer.Handle);

            IOServerConfig cfg = ElemerConfig.ReadIOServerConfig();
            IOServer server = new IOServer(cfg);

            server.Start();
            Console.WriteLine("Server started Ok Press Enter to stop");
            Console.ReadLine();
            server.Stop();
            tracer.ToString();
        }

    }
}
