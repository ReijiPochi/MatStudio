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

            RobotCoreM.Current.PropertyChanged += Current_PropertyChanged;
            ProjectM.Current.PropertyChanged += Current_PropertyChanged1;

            Indicators = new ObservableCollection<MatDataIndicator>();
            Indicators.Add(new MatDataIndicator("Indicator"));
        }

        private void Current_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CurrentRobotCore":
                    Modules = RobotCoreM.Current.CurrentRobotCore.GetModules();
                    break;

                default:
                    break;
            }
        }

        private void Current_PropertyChanged1(object sender, PropertyChangedEventArgs e)
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
        private ObservableCollection<MatDataIndicator> _Indicators;

        public ObservableCollection<MatDataIndicator> Indicators
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


    }
}
