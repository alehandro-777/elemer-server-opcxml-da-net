using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using PollingProccessSupport;

namespace ElemerDriver
{
    public class ElemerConfig
    {
        public static IOServerConfig ReadIOServerConfig()
        {
            XDocument xdoc;
            List<IOChanellConfig> ioChannellList = new List<IOChanellConfig>();

            string currDir = AppDomain.CurrentDomain.BaseDirectory;

            //чтение конфигурации 
            using (XmlReader xr = XmlReader.Create(Path.Combine(currDir, "IOServerConfig.xml")))
            {
                xdoc = XDocument.Load(xr);
                foreach (XElement IOChanellConfigElement in xdoc.Element("IOServerConfig").Elements("IOChanellConfig"))
                {
                    IOChanellConfig ioChanellConfig = ReadIOChanellConfig(IOChanellConfigElement);
                    ioChannellList.Add(ioChanellConfig);
                }
            }
            return new IOServerConfig(ioChannellList);
        }
        private static IOChanellConfig ReadIOChanellConfig(XElement ioChanellConfigElement)
        {
            XElement serialPortParamsElement = ioChanellConfigElement.Element("SerialPortParams");
            SerialPortParams serialPortParams = ReadSerialPortParams(serialPortParamsElement);
            List<ElemerQueryParams> queries = ReadElemerQueries(ioChanellConfigElement.Element("Queries"));

            return new IOChanellConfig(serialPortParams, queries);
        }
        private static SerialPortParams ReadSerialPortParams(XElement serialPortParamsElement)
        {
            XAttribute name = serialPortParamsElement.Attribute("Name");
            XAttribute BaudRateElement = serialPortParamsElement.Attribute("BaudRate");
            XAttribute ParityElement = serialPortParamsElement.Attribute("Parity");
            XAttribute StopBitsElement = serialPortParamsElement.Attribute("StopBits");
            XAttribute DataBitsElement = serialPortParamsElement.Attribute("DataBits");
            XAttribute HandshakeElement = serialPortParamsElement.Attribute("Handshake");
            XAttribute RtsEnableElement = serialPortParamsElement.Attribute("RtsEnable");

            XAttribute Ip = serialPortParamsElement.Attribute("Ip");
            XAttribute Socket = serialPortParamsElement.Attribute("Socket");

            if (Ip != null)
            { 
                return new SerialPortParams(Ip.Value, int.Parse(Socket.Value));
            }

            SerialPortParams serialPortParams = new SerialPortParams(name.Value,
                                                                    int.Parse(BaudRateElement.Value), 
                                                                    ParityElement.Value, 
                                                                    StopBitsElement.Value, 
                                                                    int.Parse(DataBitsElement.Value));

            return serialPortParams;
        }
        private static List<ElemerQueryParams> ReadElemerQueries(XElement ioChanellConfigElement)
        {
            List<ElemerQueryParams> _result = new List<ElemerQueryParams>();
            foreach (XElement elemerQueryParamsElement in ioChanellConfigElement.Elements("ElemerQueryParams"))
            {
                ElemerQueryParams elemerQueryParams = ReadElemerQueryParams(elemerQueryParamsElement);
                _result.Add(elemerQueryParams);
            }
            return _result;
        }
        private static ElemerQueryParams ReadElemerQueryParams(XElement elemerQueryParamsElement)
        {
            XAttribute DeviceIdElement = elemerQueryParamsElement.Attribute("DeviceId");
            XAttribute ChanellIdElement = elemerQueryParamsElement.Attribute("ChanellId");
            XAttribute ElemerTypeElement = elemerQueryParamsElement.Attribute("ElemerType");
            XAttribute TimeoutElement = elemerQueryParamsElement.Attribute("Timeout");

            XElement DbItemsElement = elemerQueryParamsElement.Element("DbItems");
            List<DbItem> dbItems = ReadDbItems(DbItemsElement);

            XElement RawQueryElement = elemerQueryParamsElement.Element("RawQuery");
            Byte[] rawQuery = ReadRawQuery(RawQueryElement);

            ElemerQueryParams elemerQueryParams = new ElemerQueryParams(int.Parse(DeviceIdElement.Value), int.Parse(ChanellIdElement.Value), ElemerTypeElement.Value,
                                                  dbItems, int.Parse(TimeoutElement.Value), rawQuery);
            return elemerQueryParams;
        }
        //
        private static List<DbItem> ReadDbItems(XElement DbItemsElement)
        {
            List<DbItem> _result = new List<DbItem>();
            foreach (XElement dbItemElement in DbItemsElement.Elements("DbItem"))
            {
                XAttribute Id = dbItemElement.Attribute("Id");
                DbItem item = new DbItem(Id.Value);
                _result.Add(item);
            }
            return _result;
        }
        //
        private static Byte[] ReadRawQuery(XElement RawQueryElement)
        {
            if (RawQueryElement == null) return null;
            string[] svalues = RawQueryElement.Attribute("Bytes").Value.Split(',');
            Byte[] values = new Byte[svalues.Length];
            for (int i = 0; i < svalues.Length; i++)
            {
                values[i] = Byte.Parse(svalues[i], System.Globalization.NumberStyles.HexNumber);
            }
            return values;
        }
    }
}
