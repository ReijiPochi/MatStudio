using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Livet;
using System.Collections.ObjectModel;
using RobotCoreBase;
using MatFramework.DataFlow;
using MatFramework;

namespace MatStudioROBOT2016.Models
{
    public class RobotCoreM : NotificationObject, IRobotCoreHost
    {
        public RobotCoreM()
        {
            UpdateBordList();
        }

        public static RobotCoreM Current { get; } = new RobotCoreM();

        public ObservableCollection<IRobotCore> BoardList { get; private set; } = new ObservableCollection<IRobotCore>();

        #region CurrentSerialPort変更通知プロパティ
        private SerialPortsM _CurrentSerialPort;

        public SerialPortsM CurrentSerialPort
        {
            get
            { return _CurrentSerialPort; }
            set
            { 
                if (_CurrentSerialPort == value)
                    return;

                if (_CurrentSerialPort != null)
                    _CurrentSerialPort.PropertyChanged -= _CurrentSerialPort_PropertyChanged;

                _CurrentSerialPort = value;

                value.PropertyChanged += _CurrentSerialPort_PropertyChanged;

                RaisePropertyChanged();
            }
        }

        private void _CurrentSerialPort_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "RecievedData")
            {
                CurrentRobotCore.SetRecievedData(CurrentSerialPort.RecievedData);
            }
        }
        #endregion

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
                value.SetHost(Current);
            }
        }
        #endregion

        #region CurrentRobotCoreIsOpen変更通知プロパティ
        private bool _CurrentRobotCoreIsOpen;

        public bool CurrentRobotCoreIsOpen
        {
            get
            { return _CurrentRobotCoreIsOpen; }
            set
            { 
                if (_CurrentRobotCoreIsOpen == value)
                    return;
                _CurrentRobotCoreIsOpen = value;
                RaisePropertyChanged();
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
                    System.Reflection.Assembly plugin = System.Reflection.Assembly.LoadFrom(dll);
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
                    MatApp.ApplicationLog.LogException("ロボットコアの読み込みに失敗しました", ex, typeof(RobotCoreM));
                }
            }
        }

        public void Open()
        {
            CurrentRobotCore.RobotCoreStart();
            CurrentRobotCoreIsOpen = true;
            CurrentRobotCore.SetSystemTime(-1);

            MatApp.ApplicationLog.Log(new LogData(LogCondition.Action,
                "設定をダウンロードしました",
                CurrentRobotCore.Name + " は MatStudio と同期しています", this));
        }

        public void Close()
        {
            CurrentRobotCore.RobotCoreClose();
            CurrentRobotCoreIsOpen = false;
            CurrentRobotCore.SetSystemTime(0);

            MatApp.ApplicationLog.Log(new LogData(LogCondition.Action, "同期を切断しました",
                CurrentRobotCore.Name + " は MatStudio から切断されました", this));
        }

        void IRobotCoreHost.SendToBoad(string data)
        {
            if (CurrentSerialPort != null && CurrentRobotCore.IsOpen)
                CurrentSerialPort.Send(data);
        }

        void IRobotCoreHost.SendToBoad(byte[] data)
        {
            if (CurrentSerialPort != null && CurrentRobotCore.IsOpen)
                CurrentSerialPort.Send(data);
        }
    }
}
