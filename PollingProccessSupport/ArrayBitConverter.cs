using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PollingProccessSupport
{
    //_temp = BitConverter.GetBytes(DVst_alwrk);
    //Array.Copy(_temp, 0, _result, 25, 4);
    //KKorr = BitConverter.ToSingle(binBlock, 45);
    //DNumWrCor = BitConverter.ToUInt16(binBlock, 59);

    public static class ArrayBitConverter
    {
        public static Byte[] GetBytes(UInt16[] ints16)
        {
            if (ints16 == null) return null;
            int size = ints16.Length*2;
            Byte[] result = new byte[size];
            int offset = 0;
            foreach (UInt16 int16 in ints16)
            {
                Byte[] source = BitConverter.GetBytes(int16);
                Array.Copy(source, 0, result, offset, 2);
                offset = offset + 2;
            }
            return result;
        }
        public static UInt16[] ToUInt16Array(Byte[]  bytes)
        {
            if (bytes == null) return null;
            int size = bytes.Length / 2;
            UInt16[] result = new UInt16[size];
            int offset = 0;
            for(int i=0;i<size;i++)
            {
                result[0] = BitConverter.ToUInt16(bytes, offset);
                offset = offset + 2;
            }
            return result;
        }
        //--------------------------------------------
        public static Byte[] GetBytes(Single[] floats)
        {
            if (floats == null) return null;
            int size = floats.Length * 4;
            Byte[] result = new byte[size];
            int offset = 0;
            foreach (Single fl in floats)
            {
                Byte[] source = BitConverter.GetBytes(fl);
                Array.Copy(source, 0, result, offset, 4);
                offset = offset + 4;
            }
            return result;
        }
        public static Single[] ToSingleArray(Byte[] bytes)
        {
            if (bytes == null) return null;
            int size = bytes.Length / 4;
            Single[] result = new Single[size];
            int offset = 0;
            for (int i = 0; i < size; i++)
            {
                result[0] = BitConverter.ToSingle(bytes, offset);
                offset = offset + 4;
            }
            return result;
        }
        //--------------------------------------------

    }
}
