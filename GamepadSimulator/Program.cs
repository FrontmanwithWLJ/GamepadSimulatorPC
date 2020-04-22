using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GamepadSimulator
{

  

    static class Program
    {
        //调用之后还是无法看到控制台消息，这里有一说一Winform调试时真的不方便，打印log都不行
        //还是打断点看list来慢慢调
        /*[DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();*/

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //AllocConsole();
            //Console.WriteLine("DEBUG");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Press.Init();
            Application.Run(new MainForm());
            //FreeConsole();
        }
    }
}
