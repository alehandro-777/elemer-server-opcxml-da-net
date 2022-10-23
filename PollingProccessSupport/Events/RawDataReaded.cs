using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PollingProccessSupport.Interfaces;
using PollingProccessSupport.Examples;

namespace PollingProccessSupport.Events
{
    public class RawDataReadedDomainEventArgs : IDomainEvent
    {
        public Object  sender { get; set; }
        public Byte[] Data { get; set; }
        public String Message { get; set; }
    }
}
