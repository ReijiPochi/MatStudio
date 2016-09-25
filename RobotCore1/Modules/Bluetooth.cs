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
    }

    public class Bluetooth : Module
    {
        public Bluetooth(string name, string modSimbol) : base(name, modSimbol)
        {
            CommandIn.MatDataInput += CommandIn_MatDataInput;

            inputs.Add(CommandIn);
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

            if(CommandIn.IsConnecting)
            {
                SendCommand((int)BluetoothCommand.CommandIn_DL_State, (int)ModulePortState.ForceByHost);
            }
            else
            {
                SendCommand((int)BluetoothCommand.CommandIn_DL_State, (int)ModulePortState.Free);
            }

            if (CommandOut.IsConnecting)
            {
                SendCommand((int)BluetoothCommand.CommandOut_DL_State, (int)ModulePortState.LookByHost);
            }
            else
            {
                SendCommand((int)BluetoothCommand.CommandOut_DL_State, (int)ModulePortState.Free);
            }
        }

        public override MatDataObject GetNewInstance()
        {
            return null;
        }

        public override void RequestUploadValues()
        {
            
        }

        public override void SetRecievedData(string command, string value)
        {
            switch(int.Parse(command))
            {
                case (int)BluetoothCommand.CommandOut_UP_Value:
                    CommandOut.Value = new MatData(typeof(DUALSHOCK3), new DUALSHOCK3(value));
                    break;

                default:
                    break;
            }
        }

        public MatDataInputPort CommandIn = new MatDataInputPort(typeof(DUALSHOCK3), "Command") { IsHardwarePort = true };
        private void CommandIn_MatDataInput(object sender, MatDataInputEventArgs e)
        {
            SendCommand((int)BluetoothCommand.CommandIn_DL_Value, (DUALSHOCK3)CommandIn.Value.DataValue);
        }

        public MatDataOutputPort CommandOut = new MatDataOutputPort(typeof(DUALSHOCK3), "Command") { IsHardwarePort = true };
    }
}
