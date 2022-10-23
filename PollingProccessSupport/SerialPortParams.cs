using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Xml;
using System.Xml.Serialization;

namespace PollingProccessSupport
{
    public class SerialPortParams
    {
        string _serialPortName;
        int _serialPortBaudRate;
        Parity _serialPortParity;
        StopBits _serialPortStopBits;
        int _serialPortDataBits;
        Handshake _serialPortHandshake;
        bool _serialPortRtsEnable;

        string _ip;
        int _socket;

        public string Ip { get { return _ip; } }
        public int Socket { get { return _socket; } }

        public string Name { get {return _serialPortName;} }
        public int BaudRate { get { return _serialPortBaudRate; } }
        public Parity Parity { get { return _serialPortParity; } }
        public StopBits StopBits { get { return _serialPortStopBits; } }
        public int DataBits { get { return _serialPortDataBits; } }
        public Handshake Handshake { get { return _serialPortHandshake; } }
        public bool RtsEnable { get { return _serialPortRtsEnable; } }

        public SerialPortParams(string serialPortName, int serialPortBaudRate,
            string serialPortParity, string serialPortStopBits, int serialPortDataBits)
        {
            InitMainParams(serialPortName, serialPortBaudRate, serialPortParity, serialPortStopBits, serialPortDataBits);
        }

        private void InitMainParams(string serialPortName, int serialPortBaudRate, string serialPortParity, string serialPortStopBits, int serialPortDataBits)
        {
            _serialPortName = serialPortName;
            _serialPortBaudRate = serialPortBaudRate;
            _serialPortParity = (Parity)Enum.Parse(typeof(Parity), serialPortParity, true);
            _serialPortStopBits = (StopBits)Enum.Parse(typeof(StopBits), serialPortStopBits, true);
            _serialPortDataBits = serialPortDataBits;
        }

        public SerialPortParams(string serialPortName, int serialPortBaudRate,
            string serialPortParity, string serialPortStopBits, int serialPortDataBits,
            string serialPortHandshake, bool serialPortRtsEnable)
        {
            InitMainParams(serialPortName, serialPortBaudRate, serialPortParity, serialPortStopBits, serialPortDataBits);
            _serialPortHandshake = (Handshake)Enum.Parse(typeof(Handshake), serialPortHandshake, true);
            _serialPortRtsEnable = serialPortRtsEnable;
        }

        public SerialPortParams(string ip, int socket)
        {
            _ip = ip;
            _socket = socket;
        }

        // === COM1 9600,8,N,1 === FOR TEST ONLY !!!!!!!
        public SerialPortParams()
        {
            _serialPortName = "COM1";
            _serialPortBaudRate = 9600;
            _serialPortParity = Parity.None;
            _serialPortStopBits = StopBits.One;
            _serialPortDataBits = 8;
            _serialPortHandshake = Handshake.None;
            _serialPortRtsEnable = true;
        }

    }
}
