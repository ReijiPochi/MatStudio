using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MatFramework.DataFlow
{
    public abstract class MatDataPort : MatNotificationObject
    {

        private string _Name;
        public string Name
        {
            get
            { return _Name; }
            set
            { 
                if (_Name == value)
                    return;
                _Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public bool IsHardwarePort { get; set; }

        public bool AllowHardwareConnection { get; set; }

        public bool IsConnecting { get; protected set; }

        public Type MatDataType { get; protected set; }

        public abstract bool CanConnectTo(MatDataPort port);

        public event MatPortConnectEvent MatPortConnect;
        protected void RaiseMatPortConnectEvent(MatPortConnectEventArgs e)
        {
            MatPortConnect?.Invoke(this, e);
            IsConnecting = true;
        }

        public event MatPortDisconnectEvent MatPortDisconnect;
        protected void RaiseMatPortDisconnectEvent(MatPortDisconnectEventArgs e)
        {
            MatPortDisconnect?.Invoke(this, e);
            IsConnecting = false;
        }
    }
}
