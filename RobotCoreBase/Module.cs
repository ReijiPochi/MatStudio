using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MatFramework.DataFlow;

namespace RobotCoreBase
{
    public abstract class Module : MatDataObject
    {
        public Module(string name, string modSimbol) : base(name)
        {
            IsUsing = false;
            ModuleSimbol = modSimbol;
        }

        public string ModuleSimbol { get; private set; }

        private bool _IsUsing;
        public bool IsUsing
        {
            get
            { return _IsUsing; }
            set
            { 
                if (_IsUsing == value)
                    return;
                _IsUsing = value;
                RaisePropertyChanged("IsUsing");
            }
        }

        private bool _IsHardwareActivated;
        public bool IsHardwareActivated
        {
            get { return _IsHardwareActivated; }
            set
            {
                if (_IsHardwareActivated == value)
                    return;
                _IsHardwareActivated = value;
                RaisePropertyChanged("IsHardwareActivated");
            }
        }

        public IRobotCoreHost Host { get; set; }

        protected void SendCommand(int command)
        {
            if (Host == null) return;

            Host.SendToBoad(ModuleSimbol + ";" + command.ToString("X") + ":" + "\n");
        }

        protected void SendCommand(int command, int value)
        {
            if (Host == null) return;

            Host.SendToBoad(ModuleSimbol + ";" + command.ToString("X") + ":");
            Host.SendToBoad(BitConverter.GetBytes(value));
            Host.SendToBoad("\n");
        }

        protected void SendCommand(int command, double value)
        {
            if (Host == null) return;

            Host.SendToBoad(ModuleSimbol + ";" + command.ToString("X") + ":");
            Host.SendToBoad(BitConverter.GetBytes((float)value));
            Host.SendToBoad("\n");
        }

        public abstract void Activate();

        public abstract void Deactivate();

        public abstract void SetRecievedData(string data);

        public abstract void DownloadValues();

        public abstract void RequestUploadValues();
    }
}
