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
        }

        public MatDataInputPort<MatData<object>> ValueIn = new MatDataInputPort<MatData<object>>("Value");
        private void ValueIn_MatDataInput(object sender, MatDataInputEventArgs e)
        {
            Name = ((MatData<object>)e.NewValue).DataValue.ToString();
        }

        public override ObservableCollection<MatDataPort> GetInputPorts()
        {
            ObservableCollection<MatDataPort> inputs = new ObservableCollection<MatDataPort>();

            inputs.Add(ValueIn);

            return inputs;
        }

        public override MatDataObject GetNewInstance()
        {
            return new MatDataIndicator(Name);
        }

        public override ObservableCollection<MatDataPort> GetOutputPorts()
        {
            ObservableCollection<MatDataPort> outputs = new ObservableCollection<MatDataPort>();

            return outputs;
        }
    }
}
