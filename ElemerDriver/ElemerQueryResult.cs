using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PollingProccessSupport.Interfaces;

namespace ElemerDriver
{
    //_temp = BitConverter.GetBytes(DVst_alwrk);
    //Array.Copy(_temp, 0, _result, 25, 4);
    //KKorr = BitConverter.ToSingle(binBlock, 45);
    //DNumWrCor = BitConverter.ToUInt16(binBlock, 59);

    public class ElemerQueryResult : IQueryResult
    {
        public ElemerQueryResult()
        {

        }
        //Interface IQueryResult
        public IQueryParams IoQueryParams { get; set; }
        public Byte[] RawQuery { get; set; }
        public bool TimeOver { get; set; }
        public bool Partial { get; set; }
        public bool UnknownResponse { get; set; }

        public int DeviceId { get; set; }
        public int ChanellId { get; set; }
        public ElemerType ElemerType { get; set; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DeviceId" + DeviceId.ToString());
            sb.AppendLine("ChanellId" + ChanellId.ToString());

            return sb.ToString();
        }
    }
}
