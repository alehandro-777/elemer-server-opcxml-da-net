using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PollingProccessSupport.Interfaces;
using PollingProccessSupport.Examples;

namespace PollingProccessSupport.Events
{
    public class CustomDomainEventFired : IDomainEvent
    {
        public DomainEntity DomainEntity { get; set; }
    }
}
