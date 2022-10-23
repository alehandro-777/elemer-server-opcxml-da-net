using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PollingProccessSupport;
using PollingProccessSupport.Interfaces;
using System.Threading;
using System.Diagnostics;
using PollingProccessSupport.Events;

namespace ElemerDriver
{
    public class IOChannell
    {
        private IOChanellConfig _ioChaCfg;
        private Queue<IoQuery<ElemerQueryParams, ElemerQueryResult>> _queryQueue;
        private IoQuery<ElemerQueryParams, ElemerQueryResult> _currentQuery;
        private IIoChannell _com;

        public IOChannell(IOChanellConfig ioChaCfg)
        {
            _ioChaCfg = ioChaCfg;
            _queryQueue = new Queue<IoQuery<ElemerQueryParams, ElemerQueryResult>>();
            InitChannell();
        }

        private void InitChannell()
        { 
            //создать канал    COM or UDP or TCP ???        
            //_com = new IoChannellCom(_ioChaCfg.SerialPortParams);
            //_com = new IoChannellUdp(_ioChaCfg.SerialPortParams);
            _com = new IoChannellTcp(_ioChaCfg.SerialPortParams);

            foreach (ElemerQueryParams qParams in _ioChaCfg.Queries)
                {
                    //по идее драйвер может быть различным для разных запросов - тут упрощение
                    IIoDriver<ElemerQueryParams, ElemerQueryResult> driver = new ElemerDriver1();
                    
                    //создать запросы добавить в очередь
                    IoQuery<ElemerQueryParams, ElemerQueryResult> query = new IoQuery<ElemerQueryParams, ElemerQueryResult>(_com, driver, qParams, qParams.Timeout);
                    _queryQueue.Enqueue(query);
                }
        }

        private void processQueryResult(Object state)
        {
            //timedOut
            if (state == null)
            {
                //foreach (var item in _currentQuery.)
                //{
                    
                //}

            }
            ElemerQueryResult result = (ElemerQueryResult)state;
            //обработка результата запроса
            Thread.Sleep(100);
            //запрос в конец очереди
            _queryQueue.Enqueue(_currentQuery);
            //рестарт процесса
            Start();
        }

        public void Start()
        {
            _currentQuery = _queryQueue.Dequeue();
            string s = _currentQuery.ToString();

            _currentQuery.BeginExecute(processQueryResult);
        }

        public void Stop()
        {
            _com.Close();
        }
    }
}
