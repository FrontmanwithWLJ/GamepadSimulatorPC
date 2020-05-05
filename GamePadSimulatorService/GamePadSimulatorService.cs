using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace GamePadSimulatorService
{
    public partial class GamePadSimulatorService : ServiceBase
    {
        //保持和手机一致
        Guid guid = new Guid("00001101-0000-1000-8000-00805F9B34FB");
        //蓝牙监听
        private BluetoothListener bluetoothListener = null;
        //连接对象
        private BluetoothClient conn = null;
        public static bool ClientConnected = false;
        public static bool running = false;
        //监听线程
        private Thread listenThread = null;
        //标记是否结束程序
        private readonly bool quit = false;
        //输入流
        private NetworkStream stream = null;
        public GamePadSimulatorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {
            if (conn != null)
            {
                conn.Close();
                conn = null;
            }
            if (stream != null)
            {
                stream.Close();
                stream = null;
            }
        }
    }
}
