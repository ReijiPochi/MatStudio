using RobotCoreBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatFramework.DataFlow;
using System.Collections.ObjectModel;

namespace RobotCore1.Modules
{
    public abstract class Motor : Module
    {
        public Motor(string name) : base(name)
        {
        }

        public MatDataInputPort<MatData<double>> DutyIn { get; private set; } = new MatDataInputPort<MatData<double>>("Duty");
        public MatDataOutputPort<MatData<double>> DutyOut { get; private set; } = new MatDataOutputPort<MatData<double>>("Duty");

        public override ObservableCollection<MatDataPort> GetInputPorts()
        {
            ObservableCollection<MatDataPort> inputs = new ObservableCollection<MatDataPort>();

            inputs.Add(DutyIn);

            return inputs;
        }

        public override ObservableCollection<MatDataPort> GetOutputPorts()
        {
            ObservableCollection<MatDataPort> outputs = new ObservableCollection<MatDataPort>();

            outputs.Add(DutyOut);

            return outputs;
        }

        public override MatDataObject GetNewInstance()
        {
            return null;
        }
    }
}
