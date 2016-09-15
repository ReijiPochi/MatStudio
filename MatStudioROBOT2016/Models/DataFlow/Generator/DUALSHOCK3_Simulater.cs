using MatFramework.DataFlow;
using RobotCoreBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MatStudioROBOT2016.Models.DataFlow.Generator
{
    public class DUALSHOCK3_Simulater : MatDataObject
    {
        public DUALSHOCK3_Simulater(string name) : base(name)
        {
            outputs.Add(Command);
        }

        public override MatDataObject GetNewInstance()
        {
            return new DUALSHOCK3_Simulater(Name);
        }

        public override Control GetInterfaceControl()
        {
            return new DUALSHOCK3_SimulaterControl() { DataContext = this };
        }

        public void OutputACommand(DUALSHOCK3 command)
        {
            Command.Value = new MatData(typeof(DUALSHOCK3), command);
        }

        public MatDataOutputPort Command = new MatDataOutputPort(typeof(DUALSHOCK3), "Command") { };
    }
}
