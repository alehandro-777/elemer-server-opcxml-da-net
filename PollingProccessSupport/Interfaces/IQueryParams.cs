using System;

namespace PollingProccessSupport.Interfaces
{
    public interface IQueryParams
    {
        System.Collections.Generic.List<DbItem> DbItems { get; }
        byte[] RawQuery { get; }
        int Timeout { get; }
    }
}
