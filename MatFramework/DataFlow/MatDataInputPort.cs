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

        private MatData _Value;
        public MatData Value
        {
            get { return _Value; }
            set
            {
                MatData old = null;
                if (_Value != null) old = new MatData(_Value.DataType, _Value.DataValue, _Value.Time);

                _Value = value;

                RaiseMatDataInput(new MatDataInputEventArgs(value, old));
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
            if (trg == null) return false;

            return CanConnectToAnything || trg.MatDataType == MatDataType;
        }
    }
}
