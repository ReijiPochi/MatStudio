using RobotCoreBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatFramework.DataFlow;

namespace RobotCore1.Modules
{
    public enum BluetoothCommand
    {
        Module_DL_Activation    = 0x01,
        Module_UP_Activation    = 0x02,
        CommandIn_DL_State      = 0x03,
        CommandIn_DL_Value      = 0x04,
        CommandOut_DL_State     = 0x05,
        CommandOut_UP_Value     = 0x06,
        CommandOut_DL_ConnectToHardwarePort = 0x7
    }

    public class DUALSHOCK3
    {
        public DUALSHOCK3(string bytes)
        {
            if (bytes.Length != 7) return;

            UpArrow = (bytes[0] & 0x01) != 0;
            DownArrow = (bytes[0] & 0x02) != 0;
            RightArrow = (bytes[0] & 0x04) != 0;
            LeftArrow = (bytes[0] & 0x08) != 0;
            Sankaku = (bytes[0] & 0x10) != 0;
            Batsu = (bytes[0] & 0x20) != 0;
            Maru = (bytes[0] & 0x40) != 0;
            Shikaku = (bytes[1] & 0x01) != 0;
            L1 = (bytes[1] & 0x02) != 0;
            L2 = (bytes[1] & 0x04) != 0;
            R1 = (bytes[1] & 0x08) != 0;
            R2 = (bytes[1] & 0x10) != 0;
            AnalogL_X = (sbyte)bytes[2];
            AnalogL_Y = (sbyte)bytes[3];
            AnalogR_X = (sbyte)bytes[4];
            AnalogR_Y = (sbyte)bytes[5];
        }
        public bool UpArrow;
        public bool DownArrow;
        public bool RightArrow;
        public bool LeftArrow;
        public bool Sankaku;
        public bool Batsu;
        public bool Maru;
        public bool Shikaku;
        public bool L1;
        public bool L2;
        public bool R1;
        public bool R2;
        public bool Start;
        public bool Select;
        public sbyte AnalogL_X;
        public sbyte AnalogL_Y;
        public sbyte AnalogR_X;
        public sbyte AnalogR_Y;
    }

    public class Bluetooth : Module
    {
        public Bluetooth(string name, string modSimbol) : base(name, modSimbol)
        {
            outputs.Add(CommandOut);
        }

        public override void Activate()
        {
            SendCommand((int)BluetoothCommand.Module_DL_Activation, (int)ModuleState.Active);
            IsHardwareActivated = true;
        }

        public override void Deactivate()
        {
            SendCommand((int)BluetoothCommand.Module_DL_Activation, (int)ModuleState.Deactive);
            IsHardwareActivated = false;
        }

        public override void DownloadValues()
        {
            if (Host == null) return;

            if (CommandOut.IsConnecting)
            {
                SendCommand((int)BluetoothCommand.CommandOut_DL_State, (int)ModulePortState.LookByHost);
                if (CommandOut.IsConnectingToHardwarePort)
                {
                    foreach (MatDataInputPort inp in CommandOut.SendTo)
                    {
                        if (inp.IsHardwarePort)
                            SendCommand((int)BluetoothCommand.CommandOut_DL_ConnectToHardwarePort, inp.HardwarePortAdress);
                    }
                }
                else
                {
                    SendCommand((int)BluetoothCommand.CommandOut_DL_ConnectToHardwarePort, 0);
                }
            }
            else
            {
                SendCommand((int)BluetoothCommand.CommandOut_DL_State, (int)ModulePortState.Free);
                SendCommand((int)BluetoothCommand.CommandOut_DL_ConnectToHardwarePort, 0);
            }
        }

        public override MatDataObject GetNewInstance()
        {
            return null;
        }

        public override void RequestUploadValues()
        {
            
        }

        public override void SetRecievedData(string data)
        {
            string[] s = data.Split(':');
            int command = int.Parse(s[0]);

            switch(command)
            {
                case (int)BluetoothCommand.CommandOut_UP_Value:
                    CommandOut.Value = new MatData(typeof(DUALSHOCK3), new DUALSHOCK3(s[1]));
                    break;

                default:
                    break;
            }
        }


        public MatDataOutputPort CommandOut = new MatDataOutputPort(typeof(DUALSHOCK3), "Command") { IsHardwarePort = true, AllowHardwareConnection = true };
    }
}
