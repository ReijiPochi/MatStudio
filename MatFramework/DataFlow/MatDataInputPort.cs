using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework.DataFlow
{
    public class MatDataInputPort<T> : MatDataPort
    {
        public MatDataInputPort(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        private MatData<T> _Value;
        public MatData<T> Value
        {
            get { return _Value; }
            set
            {
                MatData<T> old = new MatData<T>(_Value.DataValue, _Value.Time);
                _Value = value;

                RaiseMatDataInput(new MatDataInputEventArgs(value, old));
            }
        }

        public event MatDataInputEventHandler MatDataInput;
        protected void RaiseMatDataInput(MatDataInputEventArgs e)
        {
            MatDataInput?.Invoke(this, e);
        }
    }
}
