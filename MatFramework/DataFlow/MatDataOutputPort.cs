using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Specialized;

namespace MatFramework.DataFlow
{
    public class MatDataOutputPort : MatDataPort
    {
        public MatDataOutputPort(Type t, string name)
        {
            Name = name;
            MatDataType = t;
            SendTo.CollectionChanged += SendTo_CollectionChanged;
        }

        public ObservableCollection<MatDataInputPort> SendTo = new ObservableCollection<MatDataInputPort>();

        private void SendTo_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                RaiseMatPortConnectEvent(new MatPortConnectEventArgs() { ConnectTo = e.NewItems[0] as MatDataPort });
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                RaiseMatPortDisconnectEvent(new MatPortDisconnectEventArgs() { DisconnectTo = e.OldItems[0] as MatDataPort });
            }
        }

        private MatData _Value;
        public MatData Value
        {
            get { return _Value; }
            set
            {
                _Value = value;

                Output(value);
            }
        }

        private void Output(MatData data)
        {
            foreach(MatDataInputPort port in SendTo)
            {
                port.Value = data;
            }
        }


        public override bool CanConnectTo(MatDataPort port)
        {
            MatDataInputPort trg = port as MatDataInputPort;
            if (trg == null) return false;

            if (IsHardwarePort && trg.IsHardwarePort && !trg.AllowHardwareConnection) return false;

            return trg.CanConnectToAnything || trg.MatDataType == MatDataType;
        }
    }
}
