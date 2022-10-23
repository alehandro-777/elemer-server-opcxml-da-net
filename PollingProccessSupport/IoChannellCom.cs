using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using PollingProccessSupport.Interfaces;
using System.Diagnostics;
using PollingProccessSupport.Events;

namespace PollingProccessSupport
{
    public class IoChannellCom : IDisposable, IIoChannell
    {
        //Доступ к переменной _serialPort - многопотоковый должен быть
        private SerialPort  _serialPort = null;
        SerialPortParams _serialPortParams = null;

        private System.Threading.WaitCallback _readCallBack;

        public IoChannellCom(SerialPortParams serialPortParams)
        {
            _serialPortParams = serialPortParams;
        }
        //
        private void OpenConnecton()
        {
            if (_serialPort == null)
            {
                InitCom();
            }
            //is opened
            if (_serialPort.IsOpen)
            {
                return;
            }

            //InitCom();
            _serialPort.Open();
        }
        //
        private void InitCom()
        {
            _serialPort = new SerialPort(_serialPortParams.Name);
            _serialPort.BaudRate = _serialPortParams.BaudRate;
            _serialPort.DataBits = _serialPortParams.DataBits;
            _serialPort.Parity = _serialPortParams.Parity;
            _serialPort.Handshake = _serialPortParams.Handshake;
            _serialPort.RtsEnable = _serialPortParams.RtsEnable;
            _serialPort.StopBits = _serialPortParams.StopBits;
            _serialPort.DataReceived += ReadCallback;
        }
        //
        private void ReadCallback(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] readBuffer = new byte[1024];
            int byteCount =0;
            try
            {
                byteCount = _serialPort.Read(readBuffer, 0, readBuffer.Length);
            }
            catch (Exception ex)
            {
                RawIoResult result = new RawIoResult() { IsOk = false, OccuredException = ex };
                //Close();
                Console.WriteLine("COM Error !!! ReadCallback " + ex.Message);
                return;
            }
            
            byte[] realBytes = new byte[byteCount];
            
            Array.Copy( readBuffer, 0, realBytes, 0, byteCount);

            DomainEvents.Raise(new RawDataReadedDomainEventArgs() { sender = this, Data = realBytes, Message = "<- COM read <-" });

            RawIoResult okresult = new RawIoResult() { Buffer = realBytes, BytesCount = byteCount, IsOk = true };
            OnReadComplete(okresult);
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
                //Close();
                OpenConnecton();
                _serialPort.DiscardInBuffer();
                _serialPort.DiscardOutBuffer();
                _serialPort.Write(writeBuffer, 0, writeBuffer.Length);                
            }

            catch (Exception ex)
            {
                RawIoResult result = new RawIoResult() { Buffer = writeBuffer, BytesCount = writeBuffer.Length, IsOk = false, OccuredException = ex };
                Close();
                Console.WriteLine("COM Error !!! BeginWrite " + ex.Message);
                Console.WriteLine("COM Closed ");
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
            if (_serialPort != null)
            {
                try
                {
                    if (_serialPort.IsOpen) _serialPort.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("COM Error !!! Close " + ex.Message);
                }
            }          
            //_serialPort = null;
        }
        public void ReadNextChunk()
        {
            throw new NotImplementedException();
        }
    }
}
