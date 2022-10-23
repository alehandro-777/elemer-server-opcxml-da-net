using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PollingProccessSupport.Events;

namespace PollingProccessSupport.Examples
{
    public class DomainEntity
    {
        public string Name { get; set; }
        public void DoSomething()
        {
            DomainEvents.Raise(new CustomDomainEventFired() { DomainEntity = this });
        }
    }
}
