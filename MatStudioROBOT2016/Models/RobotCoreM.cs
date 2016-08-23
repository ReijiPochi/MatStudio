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
            //UpdateBordList();
            BoardList.Add(new RobotCore1.RobotCore1());
        }

        public static RobotCoreM Current { get; } = new RobotCoreM();

        //private void UpdateBordList()
        //{
        //    string[] dlls = Directory.GetFiles("RobotCores", "*.dll");

        //    foreach(string dll in dlls)
        //    {
        //        try
        //        {
        //            Assembly plugin = Assembly.LoadFrom(dll);
        //            foreach (Type t in plugin.GetTypes())
        //            {
        //                if (t.IsClass && !t.IsAbstract && t.GetInterface(typeof(IRobotCore).FullName) != null)
        //                {
        //                    BoardList.Add((IRobotCore)plugin.CreateInstance(t.FullName));
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MatFramework.MatApp.ApplicationLog.LogException("ロボットコアの読み込みに失敗しました", ex, typeof(RobotCoreM));
        //        }
        //    }
        //}

        public ObservableCollection<IRobotCore> BoardList { get; private set; } = new ObservableCollection<IRobotCore>();
    }
}
