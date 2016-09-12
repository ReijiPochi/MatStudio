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


        public MatDataOutputPort CommandOut = new MatDataOutputPort(typeof(DUALSHOCK3), "Command") { IsHardwarePort = true };
    }
}
