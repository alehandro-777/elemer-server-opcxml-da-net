using System;


namespace PollingProccessSupport.Interfaces
{
    public interface IQueryResult
    {
        IQueryParams IoQueryParams { get; set; }
        byte[] RawQuery { get; set; }
        bool TimeOver { get; set; }
        bool Partial { get; set; }
        bool UnknownResponse { get; set; }
    }
}
