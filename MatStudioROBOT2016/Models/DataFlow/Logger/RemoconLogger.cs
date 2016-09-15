using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MatFramework.DataFlow;
using RobotCoreBase;

namespace MatStudioROBOT2016.Models.DataFlow.Logger
{
    public class RemoconLogger : MatDataObject
    {
        public RemoconLogger(string name) : base(name)
        {
            CommandIn.MatDataInput += CommandIn_MatDataInput;

            inputs.Add(CommandIn);
            outputs.Add(CommandOut);
        }

        List<DUALSHOCK3> Log = new List<DUALSHOCK3>();

        MatDataInputPort CommandIn = new MatDataInputPort(typeof(DUALSHOCK3), "Command") { };
        private void CommandIn_MatDataInput(object sender, MatDataInputEventArgs e)
        {
            Log.Add((DUALSHOCK3)e.NewValue.DataValue);
        }

        MatDataOutputPort CommandOut = new MatDataOutputPort(typeof(DUALSHOCK3), "Log") { };

        public override MatDataObject GetNewInstance()
        {
            return new RemoconLogger(Name);
        }
    }
}
