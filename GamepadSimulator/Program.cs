using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GamepadSimulator
{

  

    static class Program
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();

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
