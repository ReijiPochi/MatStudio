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
            list.Add(new Motor1("Motor1"));
        }

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
    }
}
