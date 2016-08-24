using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MatGUI;

namespace RobotCoreBase
{
    public interface IRobotCore
    {
        string Name { get; set; }
        RobotCoreInfo GetBordInfo();
        MatControlPanelBase GetMainControlPanel();
    }

    public class RobotCoreInfo
    {
        public RobotCoreInfo(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
