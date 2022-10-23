using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PollingProccessSupport
{
    public class RawIoResult
    {
        public byte[] Buffer { get; set; }
        public int BytesCount { get; set; }
        public bool IsOk { get; set; }
        public System.Exception OccuredException { get; set; }

        public override string ToString()
        {
            return string.Format("IsOk={0} Count={1} Data:{2}", IsOk, BytesCount, ColumnsWriteArray());
        }
        private string ColumnsWriteArray()
        {
            //печать на экран по колонкам из 16 байтных слов
            int strSize = 16;
            int j = 0;

            if (Buffer == null)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0} : length = {1}", DateTime.Now.TimeOfDay, Buffer.Length);

            for (int i = 0; i < Buffer.Length; i++)
            {
                sb.AppendFormat("{0:X2} ", Buffer[i]);
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
