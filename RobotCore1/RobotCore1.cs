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
            list.Add(logger1);
            list.Add(bluetooth);
        }

        Motor motor1 = new Motor("Motor1", "Mm1");
        Motor motor2 = new Motor("Motor2", "Mm2");
        Motor motor3 = new Motor("Motor3", "Mm3");
        Motor motor4 = new Motor("Motor4", "Mm4");
        Motor motor5 = new Motor("Motor5", "Mm5");
        Motor motor6 = new Motor("Motor6", "Mm6");
        DataLogger logger1 = new DataLogger("DataLogger1", "Md1", 1);
        Bluetooth bluetooth = new Bluetooth("Bluetooth", "Mb0");

        public string Name
        {
            get { return "RobotCore1"; }
            set { }
        }

        public bool IsOpen { get; set; }

        ObservableCollection<Module> list = new ObservableCollection<Module>();

        void IRobotCore.RobotCoreStart()
        {
            IsOpen = true;

            foreach (Module m in list)
            {
                m.DownloadValues();
                m.RequestUploadValues();
            }
        }

        void IRobotCore.RobotCoreClose()
        {
            IsOpen = false;
        }

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
            if (!IsOpen) return;

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

                case "Mb0":
                    bluetooth.SetRecievedData(s[1]);
                    break;

                default:
                    break;
            }
        }
    }
}
