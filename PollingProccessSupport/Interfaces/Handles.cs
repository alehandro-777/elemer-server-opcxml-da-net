using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PollingProccessSupport.Interfaces
{
    public interface Handles<T> where T : IDomainEvent
    {
        void Handle(T args);
    }
}
