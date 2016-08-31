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
using MatFramework;
using System.Collections.ObjectModel;

namespace MatStudioROBOT2016.ViewModels.ControlPanels
{
    public class ApplicationLogVM : ViewModel
    {
        public ApplicationLogVM()
        {
            Log = MatApp.ApplicationLog.LogList;

            MatApp.ApplicationLog.LogList.CollectionChanged += LogList_CollectionChanged;
        }

        private void LogList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Log = MatApp.ApplicationLog.LogList;
        }


        #region Log変更通知プロパティ
        private MatObservableSynchronizedCollection<LogData> _Log;

        public MatObservableSynchronizedCollection<LogData> Log
        {
            get
            { return _Log; }
            set
            { 
                if (_Log == value)
                    return;
                _Log = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
