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
using System.IO;

namespace PollingProccessSupport
{
    public class IoChannellTcp : IDisposable, IIoChannell
    {
        //Доступ к переменной _serialPort - многопотоковый должен быть
        private Socket _socket = null;
        private IPEndPoint _endPoint = null;
        private SerialPortParams _portParams = null;

        private System.Threading.WaitCallback _readCallBack;

        public IoChannellTcp(SerialPortParams serialPortParams)
        {
            _portParams = serialPortParams;
            _endPoint = new IPEndPoint(IPAddress.Parse(_portParams.Ip), _portParams.Socket);
            
        }
        public void ReadNextChunk() 
        {
            Console.WriteLine("ReadCallback ReadNextChunk ReadNextChunk");
            ReadCallback(null);
        }

        //
        private void ReadCallback(object ar)
        {
            int timeout = 1000; //read timeout ms
            int size = 256;     //receive buffer size

            int startTickCount = Environment.TickCount;
            byte[] readBuffer = null;
            
            //main reading loop
            while ((Environment.TickCount < startTickCount + timeout) && (readBuffer == null))
            {
                try
                {
                    int rdBytesCount = 0;
                    byte[] chunkBuffer = new byte[size];
                    //tries to receive size bytes into the buffer to the offset position
                    //received += socket.Receive(buffer, offset + received, size - received, SocketFlags.None);
                    rdBytesCount += _socket.Receive(chunkBuffer);
                    if (rdBytesCount > 0)
                    {
                        readBuffer = new byte[rdBytesCount];
                        Array.Copy(chunkBuffer, 0, readBuffer, 0, rdBytesCount);                        
                    }
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.WouldBlock ||
                        ex.SocketErrorCode == SocketError.IOPending ||
                        ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        // socket buffer is probably empty, wait and try again
                        Thread.Sleep(30);
                    }
                    else
                    {
                        //The underlying Socket is closed An error occurred when accessing the socket.
                        Console.WriteLine("ReadCallback " + ex.Message);
                        RawIoResult result = new RawIoResult() { IsOk = false, OccuredException = ex };
                        OnReadComplete(result);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    //The underlying Socket is closed An error occurred when accessing the socket.
                    Console.WriteLine("ReadCallback " + ex.Message);
                    RawIoResult result = new RawIoResult() { IsOk = false, OccuredException = ex };
                    OnReadComplete(result);
                    return;
                }

            }//end while

            //is read Timeout 

            if (readBuffer == null)
            {
                Console.WriteLine("ReadCallback null readBuffer - timeout");
                RawIoResult result = new RawIoResult() { IsOk = false };
                OnReadComplete(result); 
                return;
            }

            //диагностика
            DomainEvents.Raise(new RawDataReadedDomainEventArgs() { sender = this, Data = readBuffer, Message = "<- COM read <-" });          
            
            //обработать результат
            RawIoResult okresult = new RawIoResult() { Buffer = readBuffer, BytesCount = readBuffer.Length, IsOk = true };
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
                OpenConnecton();
                // Disable the Nagle Algorithm for this tcp socket.
                _socket.NoDelay = true;
                _socket.Send(writeBuffer);
                //запускаем чтение
                Thread.Sleep(200);//    время на реакцию прибора
                ThreadPool.QueueUserWorkItem(ReadCallback);
            }
            catch (Exception ex)
            {
                Console.WriteLine("BeginWrite "+ex.Message);
                RawIoResult result = new RawIoResult() { Buffer = writeBuffer, BytesCount = writeBuffer.Length, IsOk = false, OccuredException = ex };
                Close();
                OnReadComplete(result);
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

        private void OpenConnecton()
        {
            if (_socket == null)
            {

                IPAddress ipAddress = IPAddress.Parse(_portParams.Ip);
                _socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                
                _socket.SendTimeout = 1000;
                _socket.ReceiveTimeout = 1000;

                _socket.Connect(_endPoint);

            }
        }

        //
        public void Close()
        {
            if (_socket != null)
            {
                try
                {
                    _socket.Shutdown(SocketShutdown.Both); 
                    _socket.Close();
                    _socket = null;
                    Thread.Sleep(500);//    время на освобождение ресурсов ???
                }
                catch (Exception)
                {
                    _socket = null;
                }
            }           
        }

    }
}
