using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace GamepadSimulator
{
    class Press
    {
        public struct Key
        {
            public String key;
            public bool pressed;
        }
        private static Dictionary<string, byte> key = new Dictionary<string, byte>();
        public static void init()
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
        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
                int bVk,       //虚拟键值
                byte bScan,     // 一般为0
                int dwFlags,    //这里是整数类型  0 为按下，2为释放
                int dwExtraInfo //这里是整数类型 一般情况下设成为 0
            );

        /**
         * args key:true 按下此键 false 释放
         */
        public static void run(object args)
        {
            String str = args.ToString();
            Key keys = getKey(str);
            keybd_event(key[keys.key], 0, keys.pressed?0:2, 0);
        }
       
        private static Key getKey(String str)
        {
            Key key;
            int index = str.IndexOf(':');
             key.key = str.Substring(0, index);
            key.pressed = str.Substring(index+1, 1) == "t";
            return key;
        }
    }
}
