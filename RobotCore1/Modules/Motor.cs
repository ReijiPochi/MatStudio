using RobotCoreBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatFramework.DataFlow;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace RobotCore1.Modules
{
    public enum MotorCommand
    {
        DutyIn_DL_State,
        DutyIn_DL_Value,
        DutyOut_DL_State,
        DutyOut_UP_Value
    }

    public class Motor : Module
    {
        public Motor(string name, int modNum) : base(name, modNum)
        {
            inputs.Add(DutyIn);
            outputs.Add(DutyOut);

            DutyIn.MatDataInput += DutyIn_MatDataInput;
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
        }

        public override void RequestUploadValues()
        {
            if (Host == null) return;

            if (DutyOut.IsConnecting)
            {
                SendCommand(MotorCommand.DutyOut_DL_State, (int)ModulePortState.LookByHost);
                SendCommand(MotorCommand.DutyOut_UP_Value);
            }
            else
            {
                SendCommand(MotorCommand.DutyOut_DL_State, (int)ModulePortState.Free);
            }
        }

        public MatDataInputPort DutyIn { get; private set; } = new MatDataInputPort(typeof(double), "Duty") { IsHardwarePort = true };
        private void DutyIn_MatDataInput(object sender, MatDataInputEventArgs e)
        {
            if (Host != null) SendCommand(MotorCommand.DutyIn_DL_Value, (double)DutyIn.Value.DataValue);
        }

        public MatDataOutputPort DutyOut { get; private set; } = new MatDataOutputPort(typeof(double), "Duty") { IsHardwarePort = true };


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
                    DutyOut.Value = new MatData(typeof(double), double.Parse(s[1]));
                    break;

                default:
                    break;
            }
        }

        private void SendCommand(MotorCommand command)
        {
            Host.SendToBoad("Mm" + ModuleNumber.ToString("X") + ";" + ((int)command).ToString("X") + ":" + "\n");
        }

        private void SendCommand(MotorCommand command, int value)
        {
            Host.SendToBoad("Mm" + ModuleNumber.ToString("X") + ";" + ((int)command).ToString("X") + ":" + value.ToString() + "\n");
        }

        private void SendCommand(MotorCommand command, double value)
        {
            Host.SendToBoad("Mm" + ModuleNumber.ToString("X") + ";" + ((int)command).ToString("X") + ":" + value.ToString("f5") + "\n");
        }

    }
}
