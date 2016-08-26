using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Livet;
using System.Collections.ObjectModel;
using RobotCoreBase;
using System.Reflection;

namespace MatStudioROBOT2016.Models
{
    public class RobotCoreM : NotificationObject
    {
        public RobotCoreM()
        {
            UpdateBordList();
        }

        public static RobotCoreM Current { get; } = new RobotCoreM();

        public ObservableCollection<IRobotCore> BoardList { get; private set; } = new ObservableCollection<IRobotCore>();

        #region CurrentRobotCore変更通知プロパティ
        private IRobotCore _CurrentRobotCore;

        public IRobotCore CurrentRobotCore
        {
            get
            { return _CurrentRobotCore; }
            set
            { 
                if (_CurrentRobotCore == value)
                    return;
                _CurrentRobotCore = value;
                RaisePropertyChanged();

                ProjectM.Current.ProjectRobotCore = value;
            }
        }
        #endregion

        private void UpdateBordList()
        {
            string[] dlls = Directory.GetFiles("RobotCores", "*.dll");
            BoardList.Clear();

            foreach (string dll in dlls)
            {
                try
                {
                    Assembly plugin = Assembly.LoadFrom(dll);
                    foreach (Type t in plugin.GetTypes())
                    {
                        if (t.IsClass && !t.IsAbstract && t.GetInterface(typeof(IRobotCore).FullName) != null)
                        {
                            BoardList.Add((IRobotCore)plugin.CreateInstance(t.FullName));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MatFramework.MatApp.ApplicationLog.LogException("ロボットコアの読み込みに失敗しました", ex, typeof(RobotCoreM));
                }
            }
        }
    }
}
