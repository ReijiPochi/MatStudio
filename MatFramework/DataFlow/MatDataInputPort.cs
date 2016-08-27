using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework.DataFlow
{
    public class MatDataInputPort : MatDataPort
    {
        public MatDataInputPort(Type t, string name)
        {
            Name = name;
            MatDataType = t;
        }

        public bool AllowHardwareConnection { get; protected set; }

        private MatData _Value;
        public MatData Value
        {
            get { return _Value; }
            set
            {
                if (SendFrom == null)
                    MatApp.ApplicationLog.Log(new LogData(LogCondition.Warning, "SendFromプロパティを設定してください", Name + " MatDataInputPortのSendFromが設定されていません", this));

                MatData old = null;
                if (_Value != null) old = new MatData(_Value.DataType, _Value.DataValue, _Value.Time);

                _Value = value;

                RaiseMatDataInput(new MatDataInputEventArgs(value, old));
            }
        }

        private MatDataOutputPort _SendFrom;
        public MatDataOutputPort SendFrom
        {
            get { return _SendFrom; }
            set
            {
                if (_SendFrom == value)
                    return;

                if (value == null) RaiseMatPortDisconnectEvent(new MatPortDisconnectEventArgs() { DisconnectTo = _SendFrom });

                _SendFrom = value;
                RaisePropertyChanged("SendFrom");

                if (value != null) RaiseMatPortConnectEvent(new MatPortConnectEventArgs() { ConnectTo = value });
            }
        }

        public event MatDataInputEventHandler MatDataInput;
        protected void RaiseMatDataInput(MatDataInputEventArgs e)
        {
            MatDataInput?.Invoke(this, e);
        }

        public override bool CanConnectTo(MatDataPort port)
        {
            MatDataOutputPort trg = port as MatDataOutputPort;
            if (trg == null || SendFrom != null) return false;

            if (IsHardwarePort && trg.IsHardwarePort && !AllowHardwareConnection) return false;

            return CanConnectToAnything || trg.MatDataType == MatDataType;
        }
    }
}
