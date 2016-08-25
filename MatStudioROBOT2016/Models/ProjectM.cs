using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using RobotCoreBase;
using MatFramework.DataFlow;
using System.Collections.ObjectModel;

namespace MatStudioROBOT2016.Models
{
    public class ProjectM : NotificationObject
    {
        public ProjectM()
        {
            DataFlow = new ObservableCollection<MatDataObject>();
        }

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

        #region DataFlow変更通知プロパティ
        private ObservableCollection<MatDataObject> _DataFlow;

        public ObservableCollection<MatDataObject> DataFlow
        {
            get
            { return _DataFlow; }
            set
            { 
                if (_DataFlow == value)
                    return;
                _DataFlow = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
