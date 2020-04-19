using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;


namespace GamepadSimulator
{
    class Press
    {
        public struct KeyInfo
        {
            public String key;
            public bool pressed;
        }
        private static readonly Dictionary<string, byte> key = new Dictionary<string, byte>();
        public static void Init()
        {
            key.Add("backspace", 8);
            key.Add("shift", 16);
            key.Add("tab", 9);
            key.Add("enter", 13);
            key.Add("ctrl", 17);
            key.Add("alt", 18);
            key.Add("caps", 20);
            key.Add("esc", 27);
            key.Add("space", 32);
            key.Add("left", 37);
            key.Add("up", 38);
            key.Add("right", 39);
            key.Add("down", 40);
            key.Add("0", 96);
            key.Add("1", 97);
            key.Add("2", 98);
            key.Add("3", 99);
            key.Add("4", 100);
            key.Add("5", 101);
            key.Add("6", 102);
            key.Add("7", 103);
            key.Add("8", 104);
            key.Add("9", 105);
            key.Add("*", 106);
            key.Add("+", 107);
            //key.Add("enter", 108);
            key.Add("-", 109);
            key.Add(".", 110);
            key.Add("/", 111);
            key.Add("f1", 112);
            key.Add("f2", 113);
            key.Add("f3", 114);
            key.Add("f4", 115);
            key.Add("f5", 116);
            key.Add("f6", 117);
            key.Add("f7", 118);
            key.Add("f8", 119);
            key.Add("f9", 120);
            key.Add("f10", 121);
            key.Add("f11", 122);
            key.Add("f12", 123);
            key.Add("~", 126);
            key.Add("a", 65);
            key.Add("b", 66);
            key.Add("c", 67);
            key.Add("d", 68);
            key.Add("e", 69);
            key.Add("f", 70);
            key.Add("g", 71);
            key.Add("h", 72);
            key.Add("i", 73);
            key.Add("j", 74);
            key.Add("k", 75);
            key.Add("l", 76);
            key.Add("m", 77);
            key.Add("n", 78);
            key.Add("o", 79);
            key.Add("p", 80);
            key.Add("q", 81);
            key.Add("r", 82);
            key.Add("s", 83);
            key.Add("t", 84);
            key.Add("u", 85);
            key.Add("v", 86);
            key.Add("w", 87);
            key.Add("x", 88);
            key.Add("y", 89);
            key.Add("z", 90);
        }

        //用来存储每个键操作线程
        private static readonly Dictionary<String, int> keyEvent = new Dictionary<string, int>();



        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
                int bVk,       //虚拟键值
                byte bScan,     // 一般为0
                int dwFlags,    //这里是整数类型  0 为按下，2为释放
                int dwExtraInfo //这里是整数类型 一般情况下设成为 0
            );

        //获取目前持有焦点的窗口句柄
        [DllImport("user32.dll")]
        public static extern long GetFocus();

        //向指定窗口发送消息
        [DllImport("user32.dll")]
        public static extern long SendMessage(long hwnd, long wMsg, long wPraram, object lParam);


        /**
         * args key:true 按下此键 false 释放
         */
        public static void Run(object args)
        {
            String str = args.ToString();
            KeyInfo keyInfo = GetKey(str);
            if (keyInfo.key == "\0") return;//解析出错
            if (!keyInfo.pressed)//如果是释放按键的指令则直接执行
            {
                keyEvent[keyInfo.key]--;//减1,通知线程结束
                //info.pressed = false;//改变按键状态停止线程
                keybd_event(key[keyInfo.key], 0, 2, 0);//发送释放指令
                //keyEvent.Remove(keyInfo.key);//由线程自主决定移除keyevent
            }
            else//按下则写入字典，创建线程
            {
                //为每个线程设立不同的flag   
                int flag = 1;
                if (keyEvent.ContainsKey(keyInfo.key))
                {
                    flag = 2 + keyEvent[keyInfo.key];//这里加2是为了后面号判断现在这个按下事件由谁来终止
                }
                else
                {
                    keyEvent.Add(keyInfo.key, flag);
                }
                Thread t = new Thread(new ParameterizedThreadStart(PressKey));
                t.Start(keyInfo.key);
            }
        }

        private static KeyInfo GetKey(String str)
        {
            KeyInfo key;
            int index = str.IndexOf(':');
            if (index == -1) {//解析出错
                key.key = "\0";
                key.pressed = true;
                return key; 
            }
            key.key = str.Substring(0, index);
            key.pressed = str.Substring(index + 1, 1) == "t";
            return key;
        }

        private static void PressKey(object o)
        {
            //强制转换之后会不会导致引用地址改变
            String k = o.ToString();
            //保存自己的flag
            int flag = keyEvent[k];
            keybd_event(key[k], 0, 0, 0);//先按下
            Thread.Sleep(300);
            while (keyEvent[k] == flag)//进入长按状态
            {
                keybd_event(key[k], 0, 0, 0);//长按状态  
                Thread.Sleep(20);
            }
            //keybd_event(key[k], 0, 2, 0);
            if (flag == flag - 1)//如果flag只相差一的话，说明这个事件是这个线程在处理，并且已经收到了释放的命令
            {
                keyEvent.Remove(k);
            }

        }
    }
}