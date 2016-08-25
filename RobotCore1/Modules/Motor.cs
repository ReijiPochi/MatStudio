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

        public MatDataOutputPort<MatData<double>> Duty { get; private set; }

        public override ObservableCollection<MatDataPort> GetInputPorts()
        {
            return new ObservableCollection<MatDataPort>();
        }

        public override ObservableCollection<MatDataPort> GetOutputPorts()
        {
            ObservableCollection<MatDataPort> outputs = new ObservableCollection<MatDataPort>();

            outputs.Add(Duty);

            return outputs;
        }
    }
}
