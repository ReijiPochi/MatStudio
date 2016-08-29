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
using System.Collections.ObjectModel;
using MatFramework.DataFlow;
using RobotCoreBase;
using MatStudioROBOT2016.Models.DataFlow.Indicator;
using MatStudioROBOT2016.Models.DataFlow.Generator;
using MatFramework;

namespace MatStudioROBOT2016.ViewModels.ControlPanels
{
    public class DataFlowVM : ViewModel
    {
        public DataFlowVM()
        {
            if (RobotCoreM.Current == null || ProjectM.Current == null ) return;

            if (RobotCoreM.Current.CurrentRobotCore != null)
            {
                Modules = RobotCoreM.Current.CurrentRobotCore.GetModules();
            }

            if(ProjectM.Current.DataFlow != null)
            {
                DataObjects = ProjectM.Current.DataFlow;
                ProjectM.Current.DataFlow.CollectionChanged += DataFlow_CollectionChanged;
            }

            RobotCoreM.Current.PropertyChanged += RobotCoreM_PropertyChanged;
            ProjectM.Current.PropertyChanged += ProjectM_PropertyChanged;

            Indicators = new ObservableCollection<MatDataObject>();
            Indicators.Add(new MatDataIndicator("Indicator"));
            Indicators.Add(new MatDataConstant("Constant"));
        }

        private void RobotCoreM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CurrentRobotCore":
                    Modules = RobotCoreM.Current.CurrentRobotCore.GetModules();
                    RobotCoreStartCommand.RaiseCanExecuteChanged();
                    RobotCoreCloseCommand.RaiseCanExecuteChanged();
                    break;

                case "CurrentRobotCoreIsOpen":
                    RobotCoreStartCommand.RaiseCanExecuteChanged();
                    RobotCoreCloseCommand.RaiseCanExecuteChanged();
                    break;

                default:
                    break;
            }
        }

        private void ProjectM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "DataFlow":
                    DataObjects = ProjectM.Current.DataFlow;
                    break;

                default:
                    break;
            }
        }

        private void DataFlow_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //RaisePropertyChanged("DataObjects");
        }

        #region Modules変更通知プロパティ
        private ObservableCollection<Module> _Modules;

        public ObservableCollection<Module> Modules
        {
            get
            { return _Modules; }
            set
            { 
                if (_Modules == value)
                    return;
                _Modules = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Indicators変更通知プロパティ
        private ObservableCollection<MatDataObject> _Indicators;

        public ObservableCollection<MatDataObject> Indicators
        {
            get
            { return _Indicators; }
            set
            { 
                if (_Indicators == value)
                    return;
                _Indicators = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region DataObjects変更通知プロパティ
        private ObservableCollection<MatDataObject> _DataObjects;

        public ObservableCollection<MatDataObject> DataObjects
        {
            get
            { return _DataObjects; }
            set
            { 
                if (_DataObjects == value)
                    return;
                _DataObjects = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region RobotCoreStartCommand
        private ViewModelCommand _RobotCoreStartCommand;

        public ViewModelCommand RobotCoreStartCommand
        {
            get
            {
                if (_RobotCoreStartCommand == null)
                {
                    _RobotCoreStartCommand = new ViewModelCommand(RobotCoreStart, CanRobotCoreStart);
                }
                return _RobotCoreStartCommand;
            }
        }

        public bool CanRobotCoreStart()
        {
            if (RobotCoreM.Current.CurrentRobotCore == null) return false;

            return !RobotCoreM.Current.CurrentRobotCore.IsOpen;
        }

        public void RobotCoreStart()
        {
            RobotCoreM.Current.CurrentRobotCore.RobotCoreStart();
            RobotCoreM.Current.CurrentRobotCoreIsOpen = true;

            MatApp.ApplicationLog.Log(new LogData(LogCondition.Action,
                "設定をダウンロードしました",
                RobotCoreM.Current.CurrentRobotCore.Name + " は MatStudio と同期しています", this));

            RobotCoreStartCommand.RaiseCanExecuteChanged();
            RobotCoreCloseCommand.RaiseCanExecuteChanged();
        }
        #endregion

        #region RobotCoreCloseCommand
        private ViewModelCommand _RobotCoreCloseCommand;

        public ViewModelCommand RobotCoreCloseCommand
        {
            get
            {
                if (_RobotCoreCloseCommand == null)
                {
                    _RobotCoreCloseCommand = new ViewModelCommand(RobotCoreClose, CanRobotCoreClose);
                }
                return _RobotCoreCloseCommand;
            }
        }

        public bool CanRobotCoreClose()
        {
            if (RobotCoreM.Current.CurrentRobotCore == null) return false;

            return RobotCoreM.Current.CurrentRobotCore.IsOpen;
        }

        public void RobotCoreClose()
        {
            RobotCoreM.Current.CurrentRobotCore.RobotCoreClose();
            RobotCoreM.Current.CurrentRobotCoreIsOpen = false;

            MatApp.ApplicationLog.Log(new LogData(LogCondition.Action, "同期を切断しました",
                RobotCoreM.Current.CurrentRobotCore.Name + " は MatStudio から切断されました", this));

            RobotCoreStartCommand.RaiseCanExecuteChanged();
            RobotCoreCloseCommand.RaiseCanExecuteChanged();
        }
        #endregion
    }
}
