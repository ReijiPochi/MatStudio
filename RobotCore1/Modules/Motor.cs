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
        DutyOut_UP_Value        = 0x5,
        DutyOut_DL_ConnectToHardwarePort = 0x6
    }

    public class Motor : Module
    {
        public Motor(string name, string modSimbol) : base(name, modSimbol)
        {
            inputs.Add(DutyIn);
            outputs.Add(DutyOut);

            DutyIn.MatDataInput += DutyIn_MatDataInput;
            DutyOut.MatPortConnect += DutyOut_MatPortConnect;
            DutyOut.MatPortDisconnect += DutyOut_MatPortDisconnect;
        }



        public override void Activate()
        {
            SendCommand((int)MotorCommand.Module_DL_Activation, (int)ModuleState.Active);
            IsHardwareActivated = true;
        }

        public override void Deactivate()
        {
            SendCommand((int)MotorCommand.Module_DL_Activation, (int)ModuleState.Deactive);
            IsHardwareActivated = false;
        }

        public override void DownloadValues()
        {
            if (Host == null) return;

            if (DutyIn.IsConnecting)
            {
                SendCommand((int)MotorCommand.DutyIn_DL_State, (int)ModulePortState.ForceByHost);
                SendCommand((int)MotorCommand.DutyIn_DL_Value, (double)DutyIn.Value.DataValue);
            }
            else
            {
                SendCommand((int)MotorCommand.DutyIn_DL_State, (int)ModulePortState.Free);
            }

            if (DutyOut.IsConnecting)
            {
                SendCommand((int)MotorCommand.DutyOut_DL_State, (int)ModulePortState.LookByHost);
                if (DutyOut.IsConnectingToHardwarePort)
                {
                    foreach (MatDataInputPort inp in DutyOut.SendTo)
                    {
                        if (inp.IsHardwarePort)
                            SendCommand((int)MotorCommand.DutyOut_DL_ConnectToHardwarePort, inp.HardwarePortAdress);
                    }
                }
                else
                {
                    SendCommand((int)MotorCommand.DutyOut_DL_ConnectToHardwarePort, 0);
                }
            }
            else
            {
                SendCommand((int)MotorCommand.DutyOut_DL_State, (int)ModulePortState.Free);
                SendCommand((int)MotorCommand.DutyOut_DL_ConnectToHardwarePort, 0);
            }
        }

        public override void RequestUploadValues()
        {
            if (Host == null) return;

            SendCommand((int)MotorCommand.Module_UP_Activation);

            if (DutyOut.IsConnecting)
            {
                SendCommand((int)MotorCommand.DutyOut_UP_Value);
            }
        }

        public MatDataInputPort DutyIn { get; private set; } = new MatDataInputPort(typeof(double), "Duty") { IsHardwarePort = true };
        private void DutyIn_MatDataInput(object sender, MatDataInputEventArgs e)
        {
            SendCommand((int)MotorCommand.DutyIn_DL_Value, (double)DutyIn.Value.DataValue);
        }

        public MatDataOutputPort DutyOut { get; private set; } = new MatDataOutputPort(typeof(double), "Duty") { IsHardwarePort = true, AllowHardwareConnection = true };
        private void DutyOut_MatPortConnect(object sender, MatPortConnectEventArgs e)
        {
            SendCommand((int)MotorCommand.DutyOut_DL_State, (int)ModulePortState.LookByHost);

            if (e.ConnectTo.IsHardwarePort)
                SendCommand((int)MotorCommand.DutyOut_DL_ConnectToHardwarePort, ((MatDataInputPort)e.ConnectTo).HardwarePortAdress);
        }
        private void DutyOut_MatPortDisconnect(object sender, MatPortDisconnectEventArgs e)
        {
            SendCommand((int)MotorCommand.DutyOut_DL_State, (int)ModulePortState.Free);

            if (e.DisconnectTo.IsHardwarePort)
                SendCommand((int)MotorCommand.DutyOut_DL_ConnectToHardwarePort, 0);
        }

        public override MatDataObject GetNewInstance()
        {
            return null;
        }

        public override void SetRecievedData(string command, string value)
        {
            switch (int.Parse(command))
            {
                case (int)MotorCommand.DutyOut_UP_Value:
                    DutyOut.Value = new MatData(typeof(double), DataConverter.BitsStringToDouble(value));
                    break;

                default:
                    break;
            }
        }
    }
}
