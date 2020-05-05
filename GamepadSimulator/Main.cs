using System;
using System.Text;
using System.Windows.Forms;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
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
        public static bool ClientConnected = false;
        //用于托盘显示
        private readonly NotifyIcon notify = new NotifyIcon();
        public static bool running = false;
        //监听线程
        private Thread listenThread = null;
        //标记是否结束程序
        private readonly bool quit = false;
        //输入流
        private NetworkStream stream;


        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            var radio = BluetoothRadio.PrimaryRadio;
            if(radio == null)
            {
                MessageBox.Show("蓝牙不可用\n请开启蓝牙后重新启动程序", "提示");
                System.Environment.Exit(0);
            }
            Run();
            bluetoothListener = new BluetoothListener(guid);
            bluetoothListener.Start();
            RunLog.Text = "等待客户端发送连接请求";
            //开启监听线程
            listenThread = new Thread(new ThreadStart(ServerThread));
            listenThread.Start();
        }

        private void ServerThread()
        {
            //此服务同一时间只接受一个客户端的请求 1对1
            byte[] received;
            while (!ClientConnected && !quit)
            {
                received = new byte[200];
                try
                {
                    conn = bluetoothListener.AcceptBluetoothClient();//等待客户端连接
                }
                catch (Exception) { break; }
                stream = conn.GetStream();
                Msg("客户端上线");
                DeviceInfo("未知");
                //首次连接等待客户端发送连接指令
                stream.Read(received, 0, received.Length);
                String tmp = Encoding.UTF8.GetString(received);
                String str = "GamePadAndroid";
                tmp = tmp.Substring(0, 14);
                //Msg(tmp);
                if (tmp != str)
                {
                    Msg("未收到客户端对接指令或已断开连接");
                    if (stream != null) stream.Close();
                    if (conn != null) conn.Close();
                    continue;
                }
                else
                {
                    DeviceInfo(tmp);
                    //收到正确指令，发送确认报文
                    byte[] sent = Encoding.UTF8.GetBytes("GamePadPC");
                    stream.Write(sent, 0, sent.Length);
                    //stream.Flush();
                    Msg("已连接");
                    ClientConnected = true;
                    while (!quit)
                    {
                        try
                        {
                            //这一段基本没用，断开连接了也判断不出来
                            /**if (!conn.Connected || !stream.CanRead || !stream.CanWrite)
                                throw new IOException("client disconnect");*/
                            received = new byte[200];
                            stream.Read(received, 0, received.Length);
                            String rec = Encoding.UTF8.GetString(received);
                            String t = rec.Substring(0, 1);
                            //客户端断开连接后会导致在缓冲区一直读空数据"\0"
                            if (t == "\0") throw new IOException("client disconnect");
                            rec.TrimEnd('\0');
                            //todo 这里执行按键操作，后期用队列实现
                            if (t != "_" && running)
                            {
                                //Thread thread = new Thread(new ParameterizedThreadStart(Press.Run));
                                //thread.Start(rec);
                                Press.Run(rec);
                            }
                            Msg(rec);
                        }
                        catch (IOException)
                        {
                            ClientConnected = false;
                            Msg("客户端断开连接");
                            DeviceInfo("无连接");
                            new Task(() =>
                            {
                                MethodInvoker methodInvoker = new MethodInvoker(() =>
                                {
                                    if (WindowState == FormWindowState.Minimized)
                                    {
                                        //弹出窗口
                                        Run();
                                        WindowState = FormWindowState.Normal;
                                        this.ShowInTaskbar = true;
                                    }
                                });
                                this.BeginInvoke(methodInvoker);
                            }).Start();

                            stream.Close();
                            conn.Close();
                            break;
                        }
                    }
                }
            }
        }

        private void RunSimulator_Click(object sender, EventArgs e)
        {
            Run();
        }
        
        private void Run()
        {
            if (!running)
            {
                running = true;
                RunSimulator.Text = "禁用";
                if (!ClientConnected)
                {
                    RunLog.Text = "已启用但无设备连接";
                }
                else
                {
                    RunLog.Text = "已启用";
                }
                //屏蔽启用按钮
                启用ToolStripMenuItem.Enabled = false;
                禁用ToolStripMenuItem.Enabled = true;
                //notifyIcon1.ShowBalloonTip(0, "提示", "手柄模拟器已启用", ToolTipIcon.None);
            }
            else
            {
                running = false;
                RunSimulator.Text = "启用";
                RunLog.Text = "已禁用";
                //屏蔽禁用按钮
                启用ToolStripMenuItem.Enabled = true;
                禁用ToolStripMenuItem.Enabled = false;
                //notifyIcon1.ShowBalloonTip(0, "提示", "手柄模拟器已禁用", ToolTipIcon.None);
            }
        }

        protected override void WndProc(ref Message m)
        { 
            int WM_SYSCOMMAND = 0x112;
            int SC_CLOSE = 0xF060;
            if (m.Msg == WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt32() == SC_CLOSE) //是否点击关闭
                {
                    //这里写操作代码
                    //this.Visible = false; //隐藏窗体
                    WindowState = FormWindowState.Minimized;
                    this.ShowInTaskbar = false;
                    //notifyIcon1.Visible = true;
                    return;
                }
               
            }
            //调用父类的方法
            base.WndProc(ref m);
        }

        private void DeviceInfo(String info)
        {
            Task task = new Task(() =>
            {
                MethodInvoker methodInvoker = new MethodInvoker(() =>
                {
                    SelectedDevice.Text = info;
                });
                this.BeginInvoke(methodInvoker);
            });
            task.Start();
        }

        private void Msg(String msg)
        {
            Task task = new Task(() =>
            {
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
            if (e.Button == MouseButtons.Left && WindowState == FormWindowState.Minimized)
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

        private void 启用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void 禁用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stream.Close();
            conn.Close();
            System.Environment.Exit(0);
        }
    }
}
