using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MatGUI;
using MatFramework.DataFlow;
using System.Collections.ObjectModel;

namespace RobotCoreBase
{
    public interface IRobotCore
    {
        string Name { get; set; }
        bool IsOpen { get; set; }
        void RobotCoreStart();
        void RobotCoreClose();
        RobotCoreInfo GetBordInfo();
        MatControlPanelBase GetMainControlPanel();
        ObservableCollection<Module> GetModules();
        void SetRecievedData(string data);
        void SetHost(IRobotCoreHost host);
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
