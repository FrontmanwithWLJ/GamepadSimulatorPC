using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network.Packets;
using InTheHand.Net.Bluetooth;

namespace GamepadSimulator
{
    class BTmsg : Packet
    {
        public BTmsg()
        {
            BluetoothRadio radio = BluetoothRadio.PrimaryRadio;

        }
    }
}
