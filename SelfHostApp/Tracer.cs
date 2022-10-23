using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PollingProccessSupport.Interfaces;
using PollingProccessSupport.Events;

namespace SelfHostApp
{
    public class Tracer : Handles<RawDataReadedDomainEventArgs>, Handles<TraceMsgAddedEventArgs>
    {
        public void Handle(RawDataReadedDomainEventArgs args)
        {
            ConsoleWriteArray(args.Message, args.Data);
        }
        public void Handle(TraceMsgAddedEventArgs args)
        {
            Console.WriteLine(args.Message);
        }
        public void ConsoleWriteArray(string message, byte[] array)
        {
            //печать на экран по колонкам из 16 байтных слов
            int strSize = 16;
            int j = 0;

            if (array == null)
            {
                Console.WriteLine("{0} : {1} length = {2}", DateTime.Now.TimeOfDay, "Array is null");
                return;
            }

            Console.WriteLine("{0} : {1} length = {2}", DateTime.Now.TimeOfDay, message, array.Length);

            if (array == null) return;

            for (int i = 0; i < array.Length; i++)
            {
                Console.Write("{0:X2} ", array[i]);
                j++;
                j %= strSize;
                if (j == 0)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
    }
}
