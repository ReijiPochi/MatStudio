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
    public class InterfaceVM : ViewModel
    {
        public InterfaceVM()
        {
            port = new SerialPortsM();
            port.PropertyChanged += Port_PropertyChanged;
            port.RefreshPortsList();
        }

        private void Port_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "PortsList":
                    PortsList = SerialPortsM.PortNames;
                    break;

                default:
                    break;
            }
        }

        private SerialPortsM port;

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
                    port.Disconnect();

                _SelectedPort = value;

                port.Connect(_SelectedPort);
                RobotCoreM.Current.CurrentSerialPort = port;

                RaisePropertyChanged();
            }
        }
        #endregion

    }
}
