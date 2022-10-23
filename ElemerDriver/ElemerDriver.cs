using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PollingProccessSupport.Interfaces;
using PollingProccessSupport;
using System.Threading;

namespace ElemerDriver
{
    //конкретный драйвер Elemer
    public class ElemerDriver1 : IIoDriver<ElemerQueryParams, ElemerQueryResult>
    {
        public byte[] CreatePacket(ElemerQueryParams queryParams)
        {
            //2402 запросы по 7 байт не понятно как формировать - идут прямо из конфига
            if (queryParams.ElemerType == ElemerType.IPTM2402)
            { 
                return queryParams.RawQuery;
            }

            if (queryParams.ElemerType == ElemerType.PMT69Ex)
            {
                Byte[] request;
                
                Byte[] head = new Byte[] {0xFF,0x3A};
                Byte[] end = new Byte[] { 0x0D };
                
                char[] id = queryParams.DeviceId.ToString().ToCharArray();
                Byte[] body = new Byte[5 + id.Length];
                id.Select(c => (Byte)c).ToArray().CopyTo(body, 0);

                body[id.Length] = 0x3B;
                body[id.Length + 1] = 0x31;
                body[id.Length + 2] = 0x3B;
                body[id.Length + 3] = (Byte)(0x30 + (Byte)queryParams.ChanellId);
                body[id.Length + 4] = 0x3B;

                char[] ccrc = Crc.CRC16(body, body.Length).ToString().ToCharArray();
                request = ConcatIPT69Request( head,  body,  ccrc,  end);
                return request;
            }
            if (queryParams.ElemerType == ElemerType.IRT1730)
            {
                Byte[] request;

                Byte[] head = new Byte[] { 0xFF, 0x3A };
                Byte[] end = new Byte[] { 0x0D };

                char[] id = queryParams.DeviceId.ToString().ToCharArray();
                Byte[] body = new Byte[5 + id.Length];
                id.Select(c => (Byte)c).ToArray().CopyTo(body, 0);

                body[id.Length] = 0x3B;
                body[id.Length + 1] = 0x31;
                body[id.Length + 2] = 0x3B;
                body[id.Length + 3] = (Byte)(0x30 + (Byte)queryParams.ChanellId);
                body[id.Length + 4] = 0x3B;

                char[] ccrc = Crc.CRC16(body, body.Length).ToString().ToCharArray();
                request = ConcatIPT69Request(head, body, ccrc, end);
                return request;
            }

            return null;
        }


        //попытка парсинга 
        public ElemerQueryResult ProccessResponse(ElemerQueryParams queryParams, byte[] responseBuffer)
        {

            if (responseBuffer == null || responseBuffer.Length < 13) return null;

            //заголовок FF FF FF 21
            //ответ =132 байт = 126 + 0A 00 00 00 00 00 -6 байт
            if (queryParams.ElemerType == ElemerType.IPTM2402)
            {                
                int start =0;
                return Proccess2402Response( queryParams, responseBuffer, 12, start);
            }
            //ответ одного канала 17-18 байт 0x21 ...0x0D
            if (queryParams.ElemerType == ElemerType.PMT69Ex)
            {
                int start = 0;
                return Proccess69Response(queryParams, responseBuffer, start);
            }
            if (queryParams.ElemerType == ElemerType.IRT1730)
            {
                int start = 0;
                return Proccess1730Response(queryParams, responseBuffer, start);
            }
            return null;
        }
        //
        private Byte[] ConcatIPT69Request(Byte[] head, Byte[] body, char[] ccrc, Byte[] end)
        {
            int len = head.Length + body.Length + ccrc.Length + end.Length;
            Byte[] result = new Byte[len];
            
            head.CopyTo(result, 0);
            body.CopyTo(result, head.Length);
            ccrc.Select(c=>(Byte)c).ToArray().CopyTo(result, head.Length + body.Length);
            end.CopyTo(result, head.Length + body.Length + ccrc.Length);

            return result;
        }
        //
        //обрыв	62 30 2D 31 31 31 2E 30 30 30 нет датчика	выводится ChErr b
	    //39 34 2D 39 39 39 2E 30 30 30	выводится ChErr 9

        private ElemerQueryResult Proccess2402Response(ElemerQueryParams queryParams, byte[] responseBuffer, int numOfchann, int start=0)
        {
            //не полный пакет - нужно продолжить чтение
            if (responseBuffer.Length < 97) return new ElemerQueryResult() { Partial = true};     //заголовок FF FF FF 21
            //packet is > 132
            //if (responseBuffer.Length > 132) return new ElemerQueryResult() { UnknownResponse = true };
            //не верный заголовок
            if (responseBuffer[0] != 0xFF) return new ElemerQueryResult() { UnknownResponse = true };     //заголовок FF FF FF 21
            //не верный терминатор
            int endIdx = responseBuffer.Length - 6;
            if (responseBuffer[endIdx] != 0x0A) return new ElemerQueryResult() { UnknownResponse = true };     //ends with 0A 00 00 00 00 00 -6 байт


            // validation Ok


            int[] posOf3b = new int[numOfchann+1];
            int coun3b = 0;
            ElemerQueryResult result = new ElemerQueryResult();
            result.IoQueryParams = queryParams;
            result.RawQuery = responseBuffer;

            //12 channells 
            Byte[][] valuesArr = new Byte[numOfchann][];


            //разделение на 12 каналов в отдельные массивы
            //находим позиции символа ";"
            for (int i = start; i < responseBuffer.Length-start-6; i++)
            {

                if (responseBuffer[i] == 0x3B)
                {
                    posOf3b[coun3b] = i;
                    coun3b++;
                }
            }
            //копирование на 12 каналов в отдельные массивы
            for (int i = 0; i < coun3b-1; i++)
            {
                int dataLen = posOf3b[i + 1] - posOf3b[i] - 1;
                valuesArr[i] = new Byte[dataLen];
                Array.Copy(responseBuffer, posOf3b[i] + 1, valuesArr[i], 0, dataLen);
            }
            //проверка канала на ошибку
            for (int i = 0; i < numOfchann; i++)
            {

            }

            //преобразование из ASCII и обновление данных
            for (int i = 0; i < queryParams.DbItems.Count; i++)
            {

                //2 первый байта в канале - код ошибки 0х30 - Ок иначе ошибка канала
                Byte error1 = valuesArr[i][0];
                Byte error2 = valuesArr[i][1];

                if (error1 != 0x30)
                {
                    queryParams.DbItems[i].LastUpdate = DateTime.Now;
                    queryParams.DbItems[i].CurrentQuality = error1;

                    continue;
                }
                //2 первых байта под код ошибки !!!
                queryParams.DbItems[i].Value = AsciiToSingle(valuesArr[i], 2, valuesArr[i].Length - 2); 
                queryParams.DbItems[i].LastUpdate = DateTime.Now; 
                queryParams.DbItems[i].CurrentQuality = 192;

            }

            return result;
        }
        
        //Обрыв к	2D 31 30 30 30 30 2E 30 30 30 30	значение  -10000
        private ElemerQueryResult Proccess69Response(ElemerQueryParams queryParams, byte[] responseBuffer, int start = 0)
        {
            int endIdx = responseBuffer.Length - 1;
            //такие короткие пакеты не бьются 
            if (responseBuffer.Length - start < 13) return new ElemerQueryResult() { UnknownResponse = true };
            //проверка заколовка
            if (responseBuffer[0] != 0x21) return new ElemerQueryResult() { UnknownResponse = true };         
            //проверка терминатора
            if (responseBuffer[endIdx] != 0x0D) return new ElemerQueryResult() { UnknownResponse = true };    


            //считаем что предварительно валидация Ок
            int[] posOf3b = new int[2];
            int coun3b = 0;
            ElemerQueryResult result = new ElemerQueryResult();
            result.IoQueryParams = queryParams;
            result.RawQuery = responseBuffer;

            //1 channell 
            Byte[] valueArr;

            //разделение на 1 каналов в отдельные массивы
            //находим позиции символа ";"
            for (int i = start; i < responseBuffer.Length - start; i++)
            {
                if (responseBuffer[i] == 0x3B)
                {
                    posOf3b[coun3b] = i;
                    coun3b++;
                }
            }
            //копирование данных 1 канала в отдельный массив
                int dataLen = posOf3b[1] - posOf3b[0] - 1;
                valueArr = new Byte[dataLen];
                Array.Copy(responseBuffer, posOf3b[0] + 1, valueArr, 0, dataLen);

            //преобразование из ASCII
                var floatVal = AsciiToSingle(valueArr, 0, valueArr.Length); // was - 2 ??? why 

                if (float.IsNaN(floatVal))
                {
                    queryParams.DbItems[0].LastUpdate = DateTime.Now;
                    queryParams.DbItems[0].CurrentQuality = 16;

                    return result;
                }

                //ERROR CODE
                if (floatVal < -1000.000f)
                {
                    queryParams.DbItems[0].Value = floatVal;
                    queryParams.DbItems[0].LastUpdate = DateTime.Now;
                    queryParams.DbItems[0].CurrentQuality = 16;

                    return result;
                }

                queryParams.DbItems[0].Value = floatVal;
                queryParams.DbItems[0].LastUpdate = DateTime.Now;
                queryParams.DbItems[0].CurrentQuality = 192;

            return result;
        }
        private ElemerQueryResult Proccess1730Response(ElemerQueryParams queryParams, byte[] responseBuffer, int start = 0)
        {
            int endIdx = responseBuffer.Length - 1;
            //такие короткие пакеты не бьются 
            if (responseBuffer.Length - start < 13) return new ElemerQueryResult() { UnknownResponse = true };
            //проверка заколовка
            if (responseBuffer[0] != 0xFF) return new ElemerQueryResult() { UnknownResponse = true };
            //проверка терминатора
            if (responseBuffer[endIdx] != 0x0D) return new ElemerQueryResult() { UnknownResponse = true };


            //считаем что предварительно валидация Ок
            int[] posOf3b = new int[2];
            int coun3b = 0;
            ElemerQueryResult result = new ElemerQueryResult();
            result.IoQueryParams = queryParams;
            result.RawQuery = responseBuffer;

            //1 channell 
            Byte[] valueArr;

            //разделение на 1 каналов в отдельные массивы
            //находим позиции символа ";"
            for (int i = start; i < responseBuffer.Length - start; i++)
            {
                if (responseBuffer[i] == 0x3B)
                {
                    posOf3b[coun3b] = i;
                    coun3b++;
                }
            }
            //копирование данных 1 канала в отдельный массив
            int dataLen = posOf3b[1] - posOf3b[0] - 1;
            valueArr = new Byte[dataLen];
            Array.Copy(responseBuffer, posOf3b[0] + 1, valueArr, 0, dataLen);

            //преобразование из ASCII
            var floatVal = AsciiToSingle(valueArr, 0, valueArr.Length );// -2 was 18-06-2019

            if (float.IsNaN(floatVal))
            {
                queryParams.DbItems[0].LastUpdate = DateTime.Now;
                queryParams.DbItems[0].CurrentQuality = 16;

                return result;
            }

            //ERROR CODE
            if (floatVal < -1000.000f)
            {
                queryParams.DbItems[0].Value = floatVal;
                queryParams.DbItems[0].LastUpdate = DateTime.Now;
                queryParams.DbItems[0].CurrentQuality = 16;

                return result;
            }

            queryParams.DbItems[0].Value = floatVal;
            queryParams.DbItems[0].LastUpdate = DateTime.Now;
            queryParams.DbItems[0].CurrentQuality = 192;

            return result;
        }
        private Single AsciiToSingle(Byte[] source, int start, int count)
        {
            string s = ASCIIEncoding.ASCII.GetString(source, start, count);
            string svalue = s.Replace('.', Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator));

            return Single.Parse(svalue);
        }
    }
}
