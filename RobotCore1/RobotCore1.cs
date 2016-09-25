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

        IRobotCoreHost currnetHost;

        public string Name
        {
            get { return "RobotCore1"; }
            set { }
        }

        public bool IsOpen { get; set; }

        ObservableCollection<Module> list = new ObservableCollection<Module>();

        void IRobotCore.RobotCoreStart()
        {
            if (currnetHost == null) return;

            IsOpen = true;

            currnetHost.SendToBoad("S;1:1\n");

            foreach (Module m in list)
            {
                m.DownloadValues();
                m.RequestUploadValues();
            }
        }

        void IRobotCore.RobotCoreClose()
        {
            currnetHost.SendToBoad("S;1:0\n");

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
            currnetHost = host;

            foreach(Module m in list)
            {
                m.Host = host;
            }
        }


        private int lastIndex = 0;

        private string trg = null;
        private bool trgRecieved = false;

        private string command = null;
        private bool commandRecieved = false;

        private string valueLength = null;
        private bool valueLengthRecieved = false;

        private string value = null;
        private bool valueRecieved = false;
        private int valueIndex = 0;
        private int lengthOfValue = 0;

        void IRobotCore.SetRecievedData(string data)
        {
            //if (!IsOpen) return;

            while (lastIndex <= data.Length)
            {
                if (data[lastIndex] == ';')
                {
                    trgRecieved = true;
                }
                else if (data[lastIndex] == ':')
                {
                    commandRecieved = true;
                }
                else if (data[lastIndex] == ')')
                {
                    valueLengthRecieved = true;
                    int.TryParse(valueLength, out lengthOfValue);
                }

                if (!trgRecieved)
                {
                    trg += data[lastIndex];
                }
                else if (!commandRecieved && data[lastIndex] != ';')
                {
                    command += data[lastIndex];
                }
                else if (!valueLengthRecieved && data[lastIndex] != ':')
                {
                    valueLength += data[lastIndex];
                }
                else if (valueIndex != lengthOfValue - 1 && data[lastIndex] != ')')
                {
                    value += data[valueIndex];
                    valueIndex++;

                    if (lastIndex == lengthOfValue)
                        valueRecieved = true;
                }
                
                if(valueRecieved && valueLengthRecieved && commandRecieved && trgRecieved)
                {
                    trgRecieved = false;
                    commandRecieved = false;
                    valueLengthRecieved = false;
                    valueRecieved = false;
                    valueIndex = 0;

                    switch (trg)
                    {
                        case "Mm1":
                            motor1.SetRecievedData(command, value);
                            break;

                        case "Mm2":
                            motor2.SetRecievedData(command, value);
                            break;

                        case "Mm3":
                            motor3.SetRecievedData(command, value);
                            break;

                        case "Mm4":
                            motor4.SetRecievedData(command, value);
                            break;

                        case "Mm5":
                            motor5.SetRecievedData(command, value);
                            break;

                        case "Mm6":
                            motor6.SetRecievedData(command, value);
                            break;

                        case "Mb0":
                            bluetooth.SetRecievedData(command, value);
                            break;

                        default:
                            break;
                    }
                }

                lastIndex++;
            }

        }
    }
}
