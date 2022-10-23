using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using PollingProccessSupport.Interfaces;
using System.Diagnostics;
using PollingProccessSupport.Events;

namespace PollingProccessSupport
{
    //реализует процесс обмена запрос-ответ с помощью драйвера
    //очередь ответов, состояние запроса

    public class IoQuery<TQ,TR> : IIoQuery
    {
        private readonly IIoChannell _chanell = null;
        private System.Threading.WaitCallback _queryComplete;
        private readonly TQ _queryParams = default(TQ);
        private readonly IIoDriver<TQ, TR> _driver = null;
        //private Timer _queryTimer;
        private int _queryTimeOut;
        private Byte[] _responseBuffer; //здесь накапливаются пакеты в случае фрагментирования
        Object _lock = new Object();

        //должен быть таймаут на запрос !!! (учесть возможно коннект на 1 запросе)
        //параметры запроса
        //может быть состояние если запрос и несколько ответов

        public IoQuery(IIoChannell chanell, IIoDriver<TQ,TR> driver, TQ qParams, int timeout)
        {
            _chanell = chanell;
            _queryParams = qParams;
            _driver = driver;
            //_queryTimer = new Timer(QueryTimerCallBack);
            _queryTimeOut = timeout;
        }


        //таймаут запроса - вернуть ошибку
        private void QueryTimerCallBack(object o)
        {
            //возможен параллельный вызов также из канала !
            lock (_lock)
            {
                OnQueryComplete(null);
            } 
        }
        private void StartQueryTimer()
        {
            //одноразовый запуск через таймаут запроса
            //_queryTimer.Change(_queryTimeOut, Timeout.Infinite);
        }
        private void StopQueryTimer()
        {
            //стоп таймаут запроса
            //_queryTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        //пришел ответ из канала, вызывается только из вторичного потока !!!
        //возможно параллельное выполнение потоками завершения чтения из канала
        private void ioChan_ReadComplete(object o)
        {

            RawIoResult ioResult = (RawIoResult) o;

            IQueryResult qResult = null;


            //если ошибка чтения или таймаут - вернуться
            if (!ioResult.IsOk) 
            {
                //timeout  or io error
                OnQueryComplete(null);
                return; 
            }

            //Обработка ответа из канала
            try
            {
                //ответ добавляется к в буфер обработки (возможно пришла только часть ответа)
                AddPacketToBuff(ioResult.Buffer);
                //попытка парсинга 
                qResult = _driver.ProccessResponse(_queryParams, _responseBuffer) as IQueryResult;
                //DomainEvents.Raise(new RawDataReadedDomainEventArgs() { sender = this, Data = _responseBuffer });
                  
            }
            catch (Exception ex)
            {
                DomainEvents.Raise(new TraceMsgAddedEventArgs() { sender = this, Message = "IoQuery - Elemer Driver exeption " + ex.Message });
                DomainEvents.Raise(new RawDataReadedDomainEventArgs() { sender = this, Data = _responseBuffer });
            }
                //неудачный парсинг
            if (qResult == null)
            {
                OnQueryComplete(null);
                return; 
            }

            //неудачный парсинг размер меньше нормы - пришел не полный пакет - запустить еще чтение
            if (qResult.Partial) 
            {
                _chanell.ReadNextChunk();
                return; 
            }
            //неудачный парсинг - не прошел предварительную валидацию
            if (qResult.UnknownResponse)
            {
                OnQueryComplete(null); 
                return;
            }
            
            //если сюда попали - запрос обработан нормально, можно переходить к следующему иначе сработает таймаут запроса
            OnQueryComplete(qResult);  
        }

        //
        private void OnQueryComplete(IQueryResult e)
        {
            StopQueryTimer();

            //Сделать выдачу ошибки по таймауту
            if (e == null)
            {
                IQueryParams param = _queryParams as IQueryParams;

                foreach (var item in param.DbItems)
                {
                    item.CurrentQuality = -1;
                    item.LastUpdate = DateTime.Now;
                }
            }
          
            //_responseBuffer = null; //буфер больше не нужен

            if (_queryComplete != null)
            {
                _queryComplete(e);
            }
        }
        //       
        private void AddPacketToBuff(Byte[] packet)
        {
            try
            {
                if (packet == null) return;
                if (_responseBuffer == null)
                {
                    _responseBuffer = packet;
                    return;
                }
                //
                var tempArr = new Byte[_responseBuffer.Length + packet.Length];
                _responseBuffer.CopyTo(tempArr, 0);
                packet.CopyTo(tempArr, _responseBuffer.Length);

                _responseBuffer = tempArr;
            }
            catch (Exception)
            {
            }
        }

        //IIoQuery - интерфейс !!!
        public void BeginExecute(System.Threading.WaitCallback queryComplete)
        {
            _queryComplete = queryComplete;
            _responseBuffer = null; //буфер больше не нужен

            //запрос специфичный для драйвера и типа
            try
            {
                byte[] sendBuffer = _driver.CreatePacket(_queryParams);
                _chanell.BeginWrite(sendBuffer, ioChan_ReadComplete);
            }
            catch (Exception ex)
            {
                DomainEvents.Raise(new TraceMsgAddedEventArgs() { sender = this, Message = "IoQuery - Begin execute error " +ex.Message});

                //нет смыла ждать - завершаем запрос с ошибкой

                OnQueryComplete(null);
                return;
            }
            StartQueryTimer();
        }
        public override string ToString()
        {
            return _queryParams.ToString();
        }
    }
}
