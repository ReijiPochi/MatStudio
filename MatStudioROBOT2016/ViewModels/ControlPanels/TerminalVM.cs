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
                    RecievedText += myPort.RecievedData;
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
                    RecievedText = null;
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
                if (_RecievedText == value)
                    return;
                _RecievedText = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
