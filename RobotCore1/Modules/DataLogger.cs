using RobotCoreBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatFramework.DataFlow;

namespace RobotCore1.Modules
{
    public enum DataLoggerCommand
    {
        Module_DL_Activation = 0x0,
        Module_UP_Activation = 0x1,
        DataIn_DL_IsLogging  = 0x2,
    }

    public enum LoggingState
    {
        Stop        = 0x0,
        Start       = 0x1
    }

    public class DataLogger : Module
    {
        public DataLogger(string name, string modSimbol, int dataInPortAdress) : base(name, modSimbol)
        {
            DataIn.MatDataInput += DataIn_MatDataInput;
            DataIn.HardwarePortAdress = dataInPortAdress;

            inputs.Add(DataIn);
        }


        public override void Activate()
        {
            SendCommand((int)DataLoggerCommand.Module_DL_Activation, (int)ModuleState.Active);
            IsHardwareActivated = true;
        }

        public override void Deactivate()
        {
            SendCommand((int)DataLoggerCommand.Module_DL_Activation, (int)ModuleState.Deactive);
            IsHardwareActivated = false;
        }

        public override void DownloadValues()
        {
            if (Host == null) return;

            if (DataIn.IsConnecting)
            {
                SendCommand((int)DataLoggerCommand.DataIn_DL_IsLogging, (int)LoggingState.Start);
            }
            else
            {
                SendCommand((int)DataLoggerCommand.DataIn_DL_IsLogging, (int)LoggingState.Stop);
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
            
        }


        public MatDataInputPort DataIn = new MatDataInputPort(typeof(object), "Data") { IsHardwarePort = true, AllowHardwareConnection = true };
        private void DataIn_MatDataInput(object sender, MatDataInputEventArgs e)
        {
            
        }

    }
}
