using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatFramework.DataFlow;
using MatGUI;
using RobotCoreBase;
using RobotCore1.Modules;

namespace RobotCore1
{
    public class RobotCore1 : IRobotCore
    {
        public RobotCore1()
        {
            list.Add(motor1);
            list.Add(motor2);
            list.Add(motor3);
            list.Add(motor4);
            list.Add(motor5);
            list.Add(motor6);
        }

        Motor motor1 = new Motor("Motor1", 1);
        Motor motor2 = new Motor("Motor2", 2);
        Motor motor3 = new Motor("Motor3", 3);
        Motor motor4 = new Motor("Motor4", 4);
        Motor motor5 = new Motor("Motor5", 5);
        Motor motor6 = new Motor("Motor6", 6);


        public string Name
        {
            get { return "RobotCore1"; }
            set { }
        }

        ObservableCollection<Module> list = new ObservableCollection<Module>();

        RobotCoreInfo IRobotCore.GetBordInfo()
        {
            return new RobotCoreInfo("RobotCore1");
        }

        MatControlPanelBase IRobotCore.GetMainControlPanel()
        {
            return new MainCP();
        }

        ObservableCollection<Module> IRobotCore.GetModules()
        {
            return list;
        }

        void IRobotCore.SetHost(IRobotCoreHost host)
        {
            foreach(Module m in list)
            {
                m.Host = host;
            }
        }

        void IRobotCore.SetRecievedData(string data)
        {
            string[] s = data.Split(';');

            switch (s[0])
            {
                case "Mm1":
                    motor1.SetRecievedData(s[1]);
                    break;

                case "Mm2":
                    motor2.SetRecievedData(s[1]);
                    break;

                case "Mm3":
                    motor3.SetRecievedData(s[1]);
                    break;

                case "Mm4":
                    motor4.SetRecievedData(s[1]);
                    break;

                case "Mm5":
                    motor5.SetRecievedData(s[1]);
                    break;

                case "Mm6":
                    motor6.SetRecievedData(s[1]);
                    break;

                default:
                    break;
            }
        }
    }
}
