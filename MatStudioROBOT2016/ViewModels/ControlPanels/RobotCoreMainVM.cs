using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using MatStudioROBOT2016.Models;
using MatGUI;
using RobotCoreBase;

namespace MatStudioROBOT2016.ViewModels.ControlPanels
{
    public class RobotCoreMainVM : ViewModel
    {
        public RobotCoreMainVM()
        {
            if (RobotCoreM.Current == null) return;

            if (RobotCoreM.Current.CurrentRobotCore != null)
            {
                CurrentRobotCoreMainCP = RobotCoreM.Current.CurrentRobotCore.GetMainControlPanel();
            }

            RobotCoreM.Current.PropertyChanged += Current_PropertyChanged;
        }

        private void Current_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CurrentRobotCore":
                    CurrentRobotCoreMainCP = RobotCoreM.Current.CurrentRobotCore.GetMainControlPanel();
                    break;

                default:
                    break;
            }
        }

        #region CurrentRobotCoreMainCP変更通知プロパティ
        private MatControlPanelBase _CurrentRobotCoreMainCP;

        public MatControlPanelBase CurrentRobotCoreMainCP
        {
            get
            { return _CurrentRobotCoreMainCP; }
            set
            {
                if (_CurrentRobotCoreMainCP == value)
                    return;
                _CurrentRobotCoreMainCP = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
