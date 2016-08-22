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

namespace MatStudioROBOT2016.ViewModels.ControlPanels
{
    public class TerminalVM : ViewModel
    {
        public TerminalVM()
        {
            myPort = new SerialPortsM();
            myPort.PropertyChanged += MyPort_PropertyChanged; ;
            myPort.RefreshPortsList();

            TextCount = 500;
            IsPause = true;
        }

        private SerialPortsM myPort;

        private void MyPort_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "PortsList":
                    PortsList = SerialPortsM.PortNames;
                    break;

                case "RecievedData":
                    RecievedText = myPort.GetRecieveData(TextCount);
                    break;

                default:
                    break;
            }
        }



        #region PortsList変更通知プロパティ
        private string[] _PortsList;

        public string[] PortsList
        {
            get
            { return _PortsList; }
            set
            { 
                if (_PortsList == value)
                    return;
                _PortsList = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region SelectedPort変更通知プロパティ
        private string _SelectedPort;

        public string SelectedPort
        {
            get
            { return _SelectedPort; }
            set
            { 
                if (_SelectedPort == value)
                    return;

                if (_SelectedPort != null)
                {
                    // 現在のCOMポートを切断
                    myPort.Disconnect();
                }

                _SelectedPort = value;

                if (_SelectedPort != null)
                {
                    // 新しいCOMポートに接続
                    IsPause = false;
                    RecievedText = null;
                    PlayCommand.Execute();
                    myPort.Connect(_SelectedPort);
                }

                RaisePropertyChanged();
            }
        }
        #endregion

        #region RecievedText変更通知プロパティ
        private string _RecievedText;

        public string RecievedText
        {
            get
            { return _RecievedText; }
            set
            {
                if (IsPause)
                    return;

                if (_RecievedText == value)
                    return;
                _RecievedText = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region TextCount変更通知プロパティ
        private int _TextCount;

        public int TextCount
        {
            get
            { return _TextCount; }
            set
            { 
                if (_TextCount == value)
                    return;
                _TextCount = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region IsPause変更通知プロパティ
        private bool _IsPause;

        public bool IsPause
        {
            get
            { return _IsPause; }
            set
            { 
                if (_IsPause == value)
                    return;
                _IsPause = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region PlayCommand
        private ViewModelCommand _PlayCommand;

        public ViewModelCommand PlayCommand
        {
            get
            {
                if (_PlayCommand == null)
                {
                    _PlayCommand = new ViewModelCommand(Play, CanPlay);
                }
                return _PlayCommand;
            }
        }

        public bool CanPlay()
        {
            return IsPause;
        }

        public void Play()
        {
            IsPause = false;
            PlayCommand.RaiseCanExecuteChanged();
            PauseCommand.RaiseCanExecuteChanged();

            RecievedText = myPort.GetRecieveData(TextCount);
        }
        #endregion

        #region PauseCommand
        private ViewModelCommand _PauseCommand;

        public ViewModelCommand PauseCommand
        {
            get
            {
                if (_PauseCommand == null)
                {
                    _PauseCommand = new ViewModelCommand(Pause, CanPause);
                }
                return _PauseCommand;
            }
        }

        public bool CanPause()
        {
            return !IsPause;
        }

        public void Pause()
        {
            IsPause = true;
            PlayCommand.RaiseCanExecuteChanged();
            PauseCommand.RaiseCanExecuteChanged();
        }
        #endregion
    }
}
