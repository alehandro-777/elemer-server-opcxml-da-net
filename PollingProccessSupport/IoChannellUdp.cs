using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Remoting.Messaging;
using System.Threading;
using PollingProccessSupport.Interfaces;
using System.Diagnostics;
using PollingProccessSupport.Events;

using System.Net;
using System.Net.Sockets;

namespace PollingProccessSupport
{
    public class IoChannellUdp : IDisposable, IIoChannell
    {
        //Доступ к переменной _serialPort - многопотоковый должен быть
        private UdpClient _udpPort = null;
        private IPEndPoint _endPoint = null;

        SerialPortParams _serialPortParams = null;

        private System.Threading.WaitCallback _readCallBack;

        public IoChannellUdp(SerialPortParams serialPortParams)
        {
            _serialPortParams = serialPortParams;
            _endPoint = new IPEndPoint(IPAddress.Parse(_serialPortParams.Ip), _serialPortParams.Socket);
            _udpPort = new UdpClient();
        }
        //
        private void ReadCallback(IAsyncResult ar)
        {
            byte[] readBuffer = null;

            try
            {
                readBuffer = _udpPort.EndReceive(ar, ref _endPoint);
            }
            catch (Exception ex)
            {
                RawIoResult result = new RawIoResult() { IsOk = false, OccuredException = ex };
                return;
            }

            DomainEvents.Raise(new RawDataReadedDomainEventArgs() { sender = this, Data = readBuffer, Message = "<- COM read <-" });

            RawIoResult okresult = new RawIoResult() { Buffer = readBuffer, BytesCount = readBuffer.Length, IsOk = true };
            OnReadComplete(okresult);
            _udpPort.BeginReceive(ReadCallback, null);
        }
        //
        public void BeginWrite(byte[] writeBuffer, System.Threading.WaitCallback readCallBack)
        {
            DomainEvents.Raise(new RawDataReadedDomainEventArgs() { sender = this, Data = writeBuffer, Message = "-> COM write ->" });

            if (writeBuffer == null) throw new ArgumentNullException("writeBuffer in null");
            if (readCallBack == null) throw new ArgumentNullException("writeComplete in null");       
            
            _readCallBack = readCallBack;          

            try
            {
                _udpPort.Connect(_endPoint);
                _udpPort.Send(writeBuffer, writeBuffer.Length, _endPoint);
                _udpPort.BeginReceive(ReadCallback, null);
            }
            catch (Exception ex)
            {
                RawIoResult result = new RawIoResult() { Buffer = writeBuffer, BytesCount = writeBuffer.Length, IsOk = false, OccuredException = ex };
                OnReadComplete(result);
                return;
            }
        }
        //
        private void OnReadComplete(RawIoResult e)
        {

            if (_readCallBack != null)
            {
                _readCallBack(e);
            }
        }

        public void Dispose()
        {
            Close();
        }

        //
        public void Close()
        {
            if (_udpPort != null)
            {
                try
                {
                    _udpPort.Close();
                }
                catch (Exception)
                {
                }
            }           
        }
        public void ReadNextChunk()
        {
            throw new NotImplementedException();
        }
    }

    public struct UdpState
    {
        public UdpClient u;
        public IPEndPoint e;
    }
}
