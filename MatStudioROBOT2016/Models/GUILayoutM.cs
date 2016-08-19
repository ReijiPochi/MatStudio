using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

using MatGUI;
using MatFramework;
using System.Windows;

namespace MatStudioROBOT2016.Models
{
    public class GUILayoutM : NotificationObject
    {
        public static void ShowControlPanel(object panelOwner)
        {
            PhantasmagoriaTabItem trg = panelOwner as PhantasmagoriaTabItem;
            if (trg == null) return;

            MatWindow w = new MatWindow();
            //ibw.InputBindings.AddRange(Application.Current.MainWindow.InputBindings);
            w.SetTabItem(trg.Clone());
            w.Show();

            MatApp.ApplicationLog.Log(new LogData(LogCondition.Action, trg.Content.GetType().Name + "を表示しました", "", "GUILayoutM", "MatStudioROBOT2016"));
        }
    }
}
