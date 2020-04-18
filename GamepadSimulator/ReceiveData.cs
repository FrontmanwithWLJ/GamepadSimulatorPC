using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.Net.Sockets;



namespace GamepadSimulator
{
    //这玩意没写好，不能用
    class ReceiveData
    {
        private BluetoothListener bluetoothListener = null;
        private BluetoothClient bluetoothClient = null;
        private bool isConnected = false;
        public ReceiveData()
        {
            //启动一个监听线程
            Thread listenThread = new Thread(ReceiveMsg);
            listenThread.IsBackground = true;
            listenThread.Start();
        }

//监听方法
        public void ReceiveMsg()
        {
            while (1 == 1)
            {
                try
                {
                    Guid mGUID = Guid.Parse("db764ac8-4b08-7f25-aafe-59d03c27bae3");
                    bluetoothListener = new BluetoothListener(mGUID);
                    bluetoothListener.Start();
                    bluetoothClient = bluetoothListener.AcceptBluetoothClient();
                    isConnected = true;

                    while (isConnected)
                    {
                        NetworkStream peerStream = null;
                        if (bluetoothClient != null && bluetoothClient.Connected)
                        {
                            peerStream = bluetoothClient.GetStream();
                        }
                        else
                        {
                            break;
                        }

                        byte[] buffer = new byte[100];
                        peerStream.Read(buffer, 0, 100);
                        String receive = Encoding.UTF8.GetString(buffer).ToString();
                        Console.WriteLine(receive + "receiveMsg");


                    }
                    bluetoothListener.Stop();
                    bluetoothClient = null;

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
        }
    }
}
