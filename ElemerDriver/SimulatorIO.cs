using System;
using System.Collections.Generic;
using System.Text;

namespace ElemerDriver
{
    public class SimulatorIO
    {
        public static Byte[] Get2402Response21()
        {
            return new Byte[] { 
                0xFF,  //0
                0xFF,  //1
                0xFF,  //2
                0xFF,  //3
                0x21,  //4
                0x30,  //5
                0x30,  //6
                0x30,  //7
                0x30,  //8
                0x30,  //9
                0x30,  //10
                0x30,  //11
                0x34,  //12
                0x31,  //13
                0x30,  //14
                0x30,  //15
                0x30,  //16
                0x30,  //17
                0x30,  //18
                0x30,  //19
                0x30,  //20
                0x30,  //21
                0x30,  //22
                0x30,  //23
                0x30,  //24
                0x30,  //25
                0x3B,  //26
                0x30,  //27
                0x30,  //28
                0x36,  //29
                0x35,  //30
                0x2E,  //31
                0x32,  //32
                0x39,  //33
                0x3B,  //34
                0x30,  //35
                0x30,  //36
                0x35,  //37
                0x35,  //38
                0x2E,  //39
                0x33,  //40
                0x38,  //41
                0x3B,  //42
                0x30,  //43
                0x30,  //44
                0x35,  //45
                0x32,  //46
                0x2E,  //47
                0x32,  //48
                0x36,  //49
                0x3B,  //50
                0x30,  //51
                0x30,  //52
                0x35,  //53
                0x38,  //54
                0x2E,  //55
                0x36,  //56
                0x36,  //57
                0x3B,  //58
                0x30,  //59
                0x30,  //60
                0x35,  //61
                0x31,  //62
                0x2E,  //63
                0x37,  //64
                0x31,  //65
                0x3B,  //66
                0x30,  //67
                0x30,  //68
                0x35,  //69
                0x35,  //70
                0x2E,  //71
                0x39,  //72
                0x38,  //73
                0x3B,  //74
                0x30,  //75
                0x30,  //76
                0x35,  //77
                0x35,  //78
                0x2E,  //79
                0x39,  //80
                0x35,  //81
                0x3B,  //82
                0x30,  //83
                0x30,  //84
                0x35,  //85
                0x33,  //86
                0x2E,  //87
                0x39,  //88
                0x31,  //89
                0x3B,  //90
                0x30,  //91
                0x30,  //92
                0x36,  //93
                0x35,  //94
                0x2E,  //95
                0x31,  //96
                0x34,  //97
                0x3B,  //98
                0x30,  //99
                0x30,  //100
                0x36,  //101
                0x32,  //102
                0x2E,  //103
                0x35,  //104
                0x32,  //105
                0x3B,  //106
                0x30,  //107
                0x30,  //108
                0x35,  //109
                0x38,  //110
                0x2E,  //111
                0x37,  //112
                0x34,  //113
                0x3B,  //114
                0x30,  //115
                0x30,  //116
                0x36,  //117
                0x31,  //118
                0x2E,  //119
                0x37,  //120
                0x36,  //121
                0x3B,  //122
                0x38,  //123
                0x64,  //124
                0x0D   //125
            };
        }
        public static Byte[] Build2402Req_21()
        {
            return new Byte[] {0x3E, 0x32, 0x31, 0x3B, 0x39, 0x45, 0x0D };
        }
        public static Byte[] Build2402Req_22()
        {
            return new Byte[] { 0x3E, 0x32, 0x32, 0x3B, 0x39, 0x46, 0x0D };
        }
        public static Byte[] Build2402Req_24()
        {
            return new Byte[] { 0x3E , 0x32 , 0x34 , 0x3B , 0x41 , 0x31, 0x0D };
        }
        public static Byte[] Strange_6b_Response()
        {
            return new Byte[] { 0x0A, 0x00, 0x00, 0x00, 0x00, 0x00 };
        }

        public static Byte[] Build_69_Req_1_1()
        {
            return new Byte[] { 0xFF, 0x3A, 0x31, 0x3B, 0x31, 0x3B, 0x30, 0x3B, 0x37, 0x36, 0x32, 0x37, 0x0D };
        }
        public static Byte[] Build_69_Req_1_2()
        {
            return new Byte[] { 0xFF, 0x3A, 0x31, 0x3B, 0x31, 0x3B, 0x31, 0x3B, 0x33, 0x36, 0x32, 0x39, 0x38, 0x0D };
        }
        public static Byte[] Build_69_Req_1_3()
        {
            return new Byte[] { 0xFF, 0x3A, 0x31, 0x3B, 0x31, 0x3B, 0x32, 0x3B, 0x33, 0x32, 0x32, 0x30, 0x32, 0x0D };
        }
        public static Byte[] Build_69_Req_1_4()
        {
            return new Byte[] { 0xFF, 0x3A, 0x31, 0x3B, 0x31, 0x3B, 0x33, 0x3B, 0x36, 0x30, 0x38, 0x37, 0x35, 0x0D };
        }
        public static Byte[] Build_69_Req_1_5()
        {
            return new Byte[] { 0xFF, 0x3A, 0x31, 0x3B, 0x31, 0x3B, 0x34, 0x3B, 0x35, 0x36, 0x37, 0x37, 0x37, 0x0D };
        }
        public static Byte[] Build_69_Req_1_6()
        {
            return new Byte[] { 0xFF, 0x3A, 0x31, 0x3B, 0x31, 0x3B, 0x35, 0x3B, 0x31, 0x39, 0x39, 0x31, 0x32, 0x0D };
        }

        public static Byte[] Build_69_Resp_1_1()
        {
            return new Byte[] { 
                0x0A,  //0
                0x0,  //1
                0x0,  //2
                0x0,  //3
                0x0,  //4
                0x0,  //5

                0x21,  //6
                0x31,  //7
                0x3B,  //8
                0x36,  //9
                0x35,  //10
                0x39,  //11
                0x2E,  //12
                0x39,  //13
                0x37,  //14
                0x32,  //15
                0x34,  //16
                0x3B,  //17
                0x32,  //18
                0x36,  //19
                0x38,  //20
                0x37,  //21
                0x35,  //22
                0x0D  //23
            };
        }
        public static Byte[] Build_69_Resp_1_2()
        {
            return new Byte[] { 
                0x21,  //0
                0x31,  //1
                0x3B,  //2
                0x36,  //3
                0x32,  //4
                0x33,  //5
                0x2E,  //6
                0x35,  //7
                0x37,  //8
                0x35,  //9
                0x31,  //10
                0x3B,  //11
                0x37,  //12
                0x38,  //13
                0x35,  //14
                0x38,  //15
                0x0D   //16
            };
        }
        public static Byte[] Build_69_Resp_1_3()
        {
            return new Byte[] { 
                0x21,  //0
                0x31,  //1
                0x3B,  //2
                0x31,  //3
                0x37,  //4
                0x31,  //5
                0x2E,  //6
                0x36,  //7
                0x34,  //8
                0x34,  //9
                0x35,  //10
                0x3B,  //11
                0x33,  //12
                0x38,  //13
                0x32,  //14
                0x34,  //15
                0x30,  //16
                0x0D  //17
            };
        }
        public static Byte[] Build_69_Resp_1_4()
        {
            return new Byte[] { 
                0x21,  //0
                0x31,  //1
                0x3B,  //2
                0x33,  //3
                0x37,  //4
                0x31,  //5
                0x2E,  //6
                0x39,  //7
                0x37,  //8
                0x32,  //9
                0x38,  //10
                0x3B,  //11
                0x38,  //12
                0x36,  //13
                0x34,  //14
                0x39,  //15
                0x0D   //16
            };
        }
        public static Byte[] Build_69_Resp_1_5()
        {
            return new Byte[] { 
                0x21,  //0
                0x31,  //1
                0x3B,  //2
                0x34,  //3
                0x38,  //4
                0x39,  //5
                0x2E,  //6
                0x33,  //7
                0x33,  //8
                0x35,  //9
                0x35,  //10
                0x3B,  //11
                0x36,  //12
                0x31,  //13
                0x37,  //14
                0x30,  //15
                0x36,  //16
                0x0D   //17
            };
        }
        public static Byte[] Build_69_Resp_1_6()
        {
            return new Byte[] { 
                0x21,  //0
                0x31,  //1
                0x3B,  //2
                0x34,  //3
                0x33,  //4
                0x33,  //5
                0x2E,  //6
                0x35,  //7
                0x37,  //8
                0x33,  //9
                0x38,  //10
                0x3B,  //11
                0x35,  //12
                0x38,  //13
                0x32,  //14
                0x35,  //15
                0x32,  //16
                0x0D   //17
            };
        }
        public static Byte[] Build_69_ErrorResp_11_0()
        {
            //22 bytes
            return new Byte[] { 
                0x21,//0
                0x31,//
                0x31,//
                0x3B,//
                0x2D,//
                0x31,//
                0x30,//
                0x30,//
                0x30,//
                0x30,//
                0x2E,//
                0x30,//
                0x30,//
                0x30,//
                0x30,//
                0x3B,//
                0x35,//
                0x32,//
                0x37,//
                0x39,//
                0x30,//
                0x0D //21
            };
        }
    }
}
