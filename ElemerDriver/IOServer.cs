using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PollingProccessSupport;
using PollingProccessSupport.Interfaces;

namespace ElemerDriver
{
    public class IOServer
    {
        private IOServerConfig _ioServerCfg;
        private List<IOChannell> _ioChanList;
        private Dictionary<string, DbItem> _valueMap;

        static IOServer()
        { }

        public IOServer(IOServerConfig ioServerCfg)
        {
            _ioServerCfg = ioServerCfg;
            _ioChanList = new List<IOChannell>();
            _valueMap = new Dictionary<string, DbItem>();
            InitServer();
        }

        private void InitServer()
        { 
            //создать каналы            
            foreach (IOChanellConfig chanCfg in _ioServerCfg.ChannellsCfg)
            {
                IOChannell chan = new IOChannell(chanCfg);
                _ioChanList.Add(chan);
                
                //добавить точки в словарь для быстрого доступа по ключу к локальному хранилищу
                foreach (var query in chanCfg.Queries)
                {
                    foreach (var item in query.DbItems)
                    {
                        try
                        {
                        //TODO !!! Обработать ошибку если ключ объекта не уникален !!!!!
                        _valueMap.Add(item.Id, item);
                        }
                        catch (Exception)
                        {
                        }

                    }
                }
            }
        }
        public void Start()
        {
            foreach (var chan in _ioChanList)
            {
                chan.Start();
            }
        }
        public void Stop()
        {
            foreach (var chan in _ioChanList)
            {
                chan.Stop();
            }
        }
        public DbItem ReadData(string id)
        { 
            return _valueMap[id];
        }
    }
}
