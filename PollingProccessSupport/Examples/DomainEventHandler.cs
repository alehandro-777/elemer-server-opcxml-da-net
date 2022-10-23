using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PollingProccessSupport.Interfaces;
using PollingProccessSupport.Events;

namespace PollingProccessSupport.Examples
{
    public class DomainEventHandler : Handles<CustomDomainEventFired>
    {
        public void Handle(CustomDomainEventFired args)
        {
            // send email to args.Customer
            Console.WriteLine(" DomainEventHandler 1 " + args.DomainEntity.Name);
        }
    }
}
