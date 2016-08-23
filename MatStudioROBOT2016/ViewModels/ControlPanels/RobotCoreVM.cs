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

using MatGUI;
using MatStudioROBOT2016.Models;
using RobotCoreBase;
using System.Collections.ObjectModel;

namespace MatStudioROBOT2016.ViewModels.ControlPanels
{
    public class RobotCoreVM : ViewModel
    {
        public RobotCoreVM()
        {
            BoardsList = RobotCoreM.Current.BoardList;
        }

        #region BoardsList変更通知プロパティ
        private ObservableCollection<IRobotCore> _BoardsList;

        public ObservableCollection<IRobotCore> BoardsList
        {
            get
            { return _BoardsList; }
            set
            { 
                if (_BoardsList == value)
                    return;
                _BoardsList = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region SelectedBoard変更通知プロパティ
        private IRobotCore _SelectedBoard;

        public IRobotCore SelectedBoard
        {
            get
            { return _SelectedBoard; }
            set
            { 
                if (_SelectedBoard == value)
                    return;
                _SelectedBoard = value;
                RaisePropertyChanged();

                ShowMainCPCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region ShowMainCPCommand
        private ViewModelCommand _ShowMainCPCommand;

        public ViewModelCommand ShowMainCPCommand
        {
            get
            {
                if (_ShowMainCPCommand == null)
                {
                    _ShowMainCPCommand = new ViewModelCommand(ShowMainCP, CanShowMainCP);
                }
                return _ShowMainCPCommand;
            }
        }

        public bool CanShowMainCP()
        {
            if (SelectedBoard != null && !SelectedBoard.IsOpen)
                return true;
            else
                return false;
        }

        public void ShowMainCP()
        {
            PhantasmagoriaTabItem ti = new PhantasmagoriaTabItem()
            {
                Header = SelectedBoard.Name,
                Content = SelectedBoard.GetMainControlPanel()
            };

            GUILayoutM.ShowControlPanel(ti);

            SelectedBoard.IsOpen = true;
            ShowMainCPCommand.RaiseCanExecuteChanged();
        }
        #endregion
    }
}
