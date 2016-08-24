using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatGUI;
using RobotCoreBase;

namespace RobotCore1
{
    public class RobotCore1 : IRobotCore
    {
        public string Name
        {
            get { return "RobotCore1"; }
            set { }
        }

        RobotCoreInfo IRobotCore.GetBordInfo()
        {
            return new RobotCoreInfo("RobotCore1");
        }

        MatControlPanelBase IRobotCore.GetMainControlPanel()
        {
            return new MainCP();
        }
    }
}
