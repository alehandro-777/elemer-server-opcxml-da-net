using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PollingProccessSupport
{
    public class DbItem
    {
        public DbItem(string id)
        {
            Id = id;
            LastUpdate = DateTime.Now;
        }
        public string Id { get; set; }
        public Single Value { get; set; }
        public DateTime LastUpdate { get; set; }
        public int CurrentQuality { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Id:" + Id);
            sb.Append(" Value:" + Value);
            sb.Append(" LastUpdate:" + LastUpdate.ToShortTimeString());
            sb.Append(" CurrentQuality:" + CurrentQuality.ToString());
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
