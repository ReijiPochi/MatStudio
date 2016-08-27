using RobotCoreBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatFramework.DataFlow;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace RobotCore1.Modules
{
    public abstract class Motor : Module
    {
        public Motor(string name) : base(name)
        {
            inputs.Add(DutyIn);
            outputs.Add(DutyOut);
        }

        public MatDataInputPort DutyIn { get; private set; } = new MatDataInputPort(typeof(double), "Duty") { IsHardwarePort = true };
        public MatDataOutputPort DutyOut { get; private set; } = new MatDataOutputPort(typeof(double), "Duty") { IsHardwarePort = true };

        public override MatDataObject GetNewInstance()
        {
            return null;
        }
    }
}
