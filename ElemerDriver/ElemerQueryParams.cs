using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using PollingProccessSupport.Interfaces;
using PollingProccessSupport;

namespace ElemerDriver
{
    //_temp = BitConverter.GetBytes(DVst_alwrk);
    //Array.Copy(_temp, 0, _result, 25, 4);
    //KKorr = BitConverter.ToSingle(binBlock, 45);
    //DNumWrCor = BitConverter.ToUInt16(binBlock, 59);
    public class ElemerQueryParams : IQueryParams
    {
        public ElemerQueryParams(int deviceId, int chanellId, string elemerType, List<DbItem> dbItems, int timeout,  Byte[] rawQuery=null)
        {
            DeviceId = deviceId;
            ChanellId = chanellId;
            ElemerType = (ElemerType)Enum.Parse(typeof(ElemerType), elemerType, true);
            DbItems = dbItems;
            RawQuery = rawQuery;
            Timeout = timeout;
        }
        public int DeviceId { get; private set; }
        public int ChanellId { get; private set; }
        public ElemerType ElemerType { get; private set; }
        public List<DbItem> DbItems { get; private set; }
        public Byte[] RawQuery { get; private set; }
        public int Timeout { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PLC#:" + DeviceId.ToString());
            sb.Append(" Chanell#:" + ChanellId.ToString());
            sb.AppendLine(ColumnsWriteArray());
            foreach (var item in DbItems)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }
        private string ColumnsWriteArray()
        {
            //печать на экран по колонкам из 16 байтных слов
            int strSize = 16;
            int j = 0;

            if (RawQuery == null)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0} : length = {1}", DateTime.Now.TimeOfDay, RawQuery.Length);

            for (int i = 0; i < RawQuery.Length; i++)
            {
                sb.AppendFormat("{0:X2} ", RawQuery[i]);
                j++;
                j %= strSize;
                if (j == 0)
                {
                    sb.AppendLine();
                }
            }
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
