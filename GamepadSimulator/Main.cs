using System;
using System.Text;
using System.Windows.Forms;
using InTheHand.Net.Sockets;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace GamepadSimulator
{
    //PC作为服务端，运行时即开启监听
    public partial class MainForm : Form
    {
        //保持和手机一致
        Guid guid = new Guid("00001101-0000-1000-8000-00805F9B34FB");
        private BluetoothListener bluetoothListener = null;
        private BluetoothClient conn = null;
        private bool ClientConnected = false;
        //用于托盘显示
        private readonly NotifyIcon notify = new NotifyIcon();
        private bool running = false;
        //监听线程
        private Thread listenThread = null;

        public MainForm()
        {
            InitializeComponent();
            //new ReceiveData();
            Init();
        }

        private void Init()
        {
            bluetoothListener = new BluetoothListener(guid);
            bluetoothListener.Start();
            RunLog.Text = "等待客户端发送连接请求";
            //开启监听线程
            listenThread = new Thread(ServerThread);
            listenThread.Start();
        }

        private void ServerThread()
        {
            //此服务同一时间只接受一个客户端的请求 1对1
            NetworkStream stream;
            byte[] received = new byte[200];
            while (!ClientConnected)
            {
                conn = bluetoothListener.AcceptBluetoothClient();//等待客户端连接
                stream = conn.GetStream();
                Msg("客户端上线");

                //首次连接等待客户端发送连接指令
                stream.Read(received,0,received.Length);
                stream.Flush();
                String tmp = Encoding.UTF8.GetString(received);
                String str = "GamePadAndroid";
                Msg(tmp);
                if (tmp.Substring(0,14) != str) {
                    if (stream != null) stream.Close();
                    if (conn != null) conn.Close();
                    continue;
                }
                else
                {
                    //收到正确指令，发送确认报文
                    byte[] sent = Encoding.UTF8.GetBytes("GamePadPC");
                    stream.Write(sent, 0, sent.Length);
                    //stream.Flush();
                    Msg("connected");
                    ClientConnected = true;
                    while (true)
                    {
                        try
                        {
                            stream.Read(received, 0, received.Length);
                            String rec = Encoding.UTF8.GetString(received);
                            //Thread t = new Thread(new ParameterizedThreadStart(Press.run));
                            //t.Start(rec);
                            Press.run(rec);
                            Msg(rec);
                        }
                        catch (IOException)
                        {
                            Msg("客户端断开连接");
                            ClientConnected = false;
                        }
                    }
                }
            }
        }

        private void RunSimulator_Click(object sender, EventArgs e)
        { 
            if (!running)
            {
                running = true;
                RunSimulator.Text = "禁用";
            }
            else
            {
                running = false;
                RunSimulator.Text = "启用";
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e) 
        {
            //按下最小化按钮
            if (WindowState == FormWindowState.Minimized)
            {
                //隐藏任务栏图标
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(0, "提示", "进入托盘模式运行", ToolTipIcon.None);
            }
        }

        private void Msg(String msg)
        {
            Task task = new Task(() => {
                MethodInvoker mi = new MethodInvoker(() =>
                {
                    RunLog.Text = msg;
                });
                this.BeginInvoke(mi);
            });
            task.Start();
        }

        //双击托盘图标还原窗口
        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left&&WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示 
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点 
                this.Activate();
                //任务栏区显示图标 
                this.ShowInTaskbar = true;
                //托盘区图标隐藏 
                notify.Visible = false;
            }
        }

        /*
        * 对Encoding.UTF8.GetString()，进行处理，
        * 这个方法返回的字符串空位全部都是\0,判等有问题，可能还占内存
        */
        private String GetString(byte[] bytes)
        {
            String str = "";
            String tmp = Encoding.UTF8.GetString(bytes);
            for (int i = 0; i < tmp.Length; i++)
            {
                String t = tmp.Substring(i, 1);
                if (t == "\0")
                    break;
                else
                {
                    str += t;
                }
            }
            return str+"\0";
        }
    }
}
