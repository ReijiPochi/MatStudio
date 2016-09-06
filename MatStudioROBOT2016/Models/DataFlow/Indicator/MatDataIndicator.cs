using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MatFramework.DataFlow;

namespace MatStudioROBOT2016.Models.DataFlow.Indicator
{
    public class MatDataIndicator : MatDataObject
    {
        public MatDataIndicator(string name) : base(name)
        {
            ValueIn.MatDataInput += ValueIn_MatDataInput;
            ValueIn.CanConnectToAnything = true;

            inputs.Add(ValueIn);
        }

        public MatDataInputPort ValueIn = new MatDataInputPort(typeof(object), "Value");
        private void ValueIn_MatDataInput(object sender, MatDataInputEventArgs e)
        {
            if (e.NewValue.DataType == typeof(double))
                Name = ((double)e.NewValue.DataValue).ToString("f6");
            else
                Name = e.NewValue.DataValue.ToString();
        }

        public override MatDataObject GetNewInstance()
        {
            return new MatDataIndicator(Name);
        }
    }
}
