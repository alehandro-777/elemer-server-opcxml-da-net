using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PollingProccessSupport.Interfaces;
using PollingProccessSupport.Examples;

namespace PollingProccessSupport.Events
{
    public class TraceMsgAddedEventArgs : IDomainEvent
    {
        public object sender { get; set; }
        public string Message { get; set; }
    }
}
