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
    public class Motor : Module
    {
        public Motor(string name, int modNum) : base(name, modNum)
        {
            inputs.Add(DutyIn);
            outputs.Add(DutyOut);

            DutyIn.MatDataInput += DutyIn_MatDataInput;
        }


        public MatDataInputPort DutyIn { get; private set; } = new MatDataInputPort(typeof(double), "Duty") { IsHardwarePort = true };
        private void DutyIn_MatDataInput(object sender, MatDataInputEventArgs e)
        {
            if (Host != null) Host.SendToBoad("Mm" + ModuleNumber.ToString() + ";" + "Aa:" + ((double)e.NewValue.DataValue).ToString("f4"));
        }
        public MatDataOutputPort DutyOut { get; private set; } = new MatDataOutputPort(typeof(double), "Duty") { IsHardwarePort = true };

        public override MatDataObject GetNewInstance()
        {
            return null;
        }

        public override void SetRecievedData(string data)
        {
            string[] s = data.Split(':');

            switch (s[0])
            {
                case "Aa":
                    SetDuty(s[1]);
                    break;

                default:
                    break;
            }
        }

        private void SetDuty(string value)
        {
            DutyOut.Value = new MatData(typeof(double), double.Parse(value));
        }
    }
}
