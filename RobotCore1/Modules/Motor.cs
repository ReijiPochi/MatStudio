using RobotCoreBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatFramework.DataFlow;
using System.Collections.ObjectModel;
using System.Windows.Threading;

using MatFramework.Converters;

namespace RobotCore1.Modules
{
    public enum MotorCommand
    {
        Module_DL_Activation    = 0x0,
        Module_UP_Activation    = 0x1,
        DutyIn_DL_State         = 0x2,
        DutyIn_DL_Value         = 0x3,
        DutyOut_DL_State        = 0x4,
        DutyOut_UP_Value        = 0x5
    }

    public class Motor : Module
    {
        public Motor(string name, int modNum) : base(name, modNum)
        {
            inputs.Add(DutyIn);
            outputs.Add(DutyOut);

            DutyIn.MatDataInput += DutyIn_MatDataInput;
            DutyOut.MatPortConnect += DutyOut_MatPortConnect;
            DutyOut.MatPortDisconnect += DutyOut_MatPortDisconnect;
        }



        public override void Activate()
        {
            SendCommand(MotorCommand.Module_DL_Activation, (int)ModuleState.Active);
            IsHardwareActivated = true;
        }

        public override void Deactivate()
        {
            SendCommand(MotorCommand.Module_DL_Activation, (int)ModuleState.Deactive);
            IsHardwareActivated = false;
        }

        public override void DownloadValues()
        {
            if (Host == null) return;

            if (DutyIn.IsConnecting)
            {
                SendCommand(MotorCommand.DutyIn_DL_State, (int)ModulePortState.ForceByHost);
                SendCommand(MotorCommand.DutyIn_DL_Value, (double)DutyIn.Value.DataValue);
            }
            else
            {
                SendCommand(MotorCommand.DutyIn_DL_State, (int)ModulePortState.Free);
            }

            if (DutyOut.IsConnecting)
            {
                SendCommand(MotorCommand.DutyOut_DL_State, (int)ModulePortState.LookByHost);
            }
            else
            {
                SendCommand(MotorCommand.DutyOut_DL_State, (int)ModulePortState.Free);
            }
        }

        public override void RequestUploadValues()
        {
            if (Host == null) return;

            SendCommand(MotorCommand.Module_UP_Activation);

            if (DutyOut.IsConnecting)
            {
                SendCommand(MotorCommand.DutyOut_UP_Value);
            }
        }

        public MatDataInputPort DutyIn { get; private set; } = new MatDataInputPort(typeof(double), "Duty") { IsHardwarePort = true };
        private void DutyIn_MatDataInput(object sender, MatDataInputEventArgs e)
        {
            SendCommand(MotorCommand.DutyIn_DL_Value, (double)DutyIn.Value.DataValue);
        }

        public MatDataOutputPort DutyOut { get; private set; } = new MatDataOutputPort(typeof(double), "Duty") { IsHardwarePort = true };
        private void DutyOut_MatPortConnect(object sender, MatPortConnectEventArgs e)
        {
            SendCommand(MotorCommand.DutyOut_DL_State, (int)ModulePortState.LookByHost);
        }
        private void DutyOut_MatPortDisconnect(object sender, MatPortDisconnectEventArgs e)
        {
            SendCommand(MotorCommand.DutyOut_DL_State, (int)ModulePortState.Free);
        }

        public override MatDataObject GetNewInstance()
        {
            return null;
        }

        public override void SetRecievedData(string data)
        {
            string[] s = data.Split(':');
            int command = int.Parse(s[0]);

            switch (command)
            {
                case (int)MotorCommand.DutyOut_UP_Value:
                    DutyOut.Value = new MatData(typeof(double), DataConverter.BitsStringToDouble(s[1]));
                    break;

                default:
                    break;
            }
        }

        private void SendCommand(MotorCommand command)
        {
            if (Host == null) return;

            Host.SendToBoad("Mm" + ModuleNumber.ToString("X") + ";" + ((int)command).ToString("X") + ":" + "\n");
        }

        private void SendCommand(MotorCommand command, int value)
        {
            if (Host == null) return;

            Host.SendToBoad("Mm" + ModuleNumber.ToString("X") + ";" + ((int)command).ToString("X") + ":");
            Host.SendToBoad(BitConverter.GetBytes(value));
            Host.SendToBoad("\n");
        }

        private void SendCommand(MotorCommand command, double value)
        {
            if (Host == null) return;

            Host.SendToBoad("Mm" + ModuleNumber.ToString("X") + ";" + ((int)command).ToString("X") + ":");
            Host.SendToBoad(BitConverter.GetBytes((float)value));
            Host.SendToBoad("\n");
        }

    }
}
