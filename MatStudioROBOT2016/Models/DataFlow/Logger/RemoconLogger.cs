using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MatFramework.DataFlow;
using RobotCoreBase;
using System.Timers;
using MatFramework;
using System.Windows.Controls;

namespace MatStudioROBOT2016.Models.DataFlow.Logger
{
    public class RemoconLogger : MatDataObject
    {
        public RemoconLogger(string name) : base(name)
        {
            timer = new MatTimer(1);
            timer.MatTickEvent += Timer_MatTickEvent;

            CommandIn.MatDataInput += CommandIn_MatDataInput;

            inputs.Add(CommandIn);
            outputs.Add(CommandOut);
        }

        private MatTimer timer;

        List<DUALSHOCK3> Log = new List<DUALSHOCK3>();

        public int CurrentTime { get; private set; }


        MatDataInputPort CommandIn = new MatDataInputPort(typeof(DUALSHOCK3), "Command") { };
        private void CommandIn_MatDataInput(object sender, MatDataInputEventArgs e)
        {
            Log.Add((DUALSHOCK3)e.NewValue.DataValue);
        }

        MatDataOutputPort CommandOut = new MatDataOutputPort(typeof(DUALSHOCK3), "Log") { };

        public void StartRec()
        {

        }

        public void StartPlay()
        {
            timer.Start();
        }

        private void Timer_MatTickEvent(object sender)
        {
            
        }

        public override Control GetInterfaceControl()
        {
            return new RemoconLoggerControl() { DataContext = this };
        }

        public override MatDataObject GetNewInstance()
        {
            return new RemoconLogger(Name);
        }
    }
}
