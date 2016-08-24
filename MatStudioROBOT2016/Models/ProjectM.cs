using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using RobotCoreBase;

namespace MatStudioROBOT2016.Models
{
    public class ProjectM : NotificationObject
    {
        public static ProjectM Current { get; } = new ProjectM();

        #region ProjectRobotCore変更通知プロパティ
        private IRobotCore _ProjectRobotCore;

        public IRobotCore ProjectRobotCore
        {
            get
            { return _ProjectRobotCore; }
            set
            { 
                if (_ProjectRobotCore == value)
                    return;
                _ProjectRobotCore = value;
                RaisePropertyChanged();

                RobotCoreM.Current.CurrentRobotCore = value;
            }
        }
        #endregion
    }
}
