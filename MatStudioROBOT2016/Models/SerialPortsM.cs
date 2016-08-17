using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

using MatFramework.Connection;
using System.Windows;

namespace MatStudioROBOT2016.Models
{
    public class SerialPortsM : NotificationObject
    {
        public SerialPortsM()
        {
            Application.Current.Exit += Application_Exit;
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Disconnect();
        }

        /// <summary>
        /// 存在するCOMポートの名前のリスト（ RefreshPortsList()を呼ぶまではnullです ）
        /// </summary>
        public static string[] PortNames { get; private set; }

        /// <summary>
        /// 存在するCOMポートのリスト（ RefreshPortsList()を呼ぶまではnullです ）
        /// </summary>
        public static List<SerialPortConnector> Ports { get; private set; } = new List<SerialPortConnector>();

        public SerialPortConnector CurrentPort { get; private set; }

        public string RecievedData { get; private set; }

        /// <summary>
        /// PortListを更新し、Portsを更新します。
        /// </summary>
        public void RefreshPortsList()
        {
            PortNames = SerialPortConnector.GetPortsList();
            RaisePropertyChanged("PortsList");

            if (PortNames == null || Ports == null) return;

            foreach(string name in PortNames)
            {
                bool nameExist = false;

                if (Ports != null)
                {
                    foreach (SerialPortConnector port in Ports)
                    {
                        if (port.PortName == name)
                            nameExist = true;
                    }
                }

                if (!nameExist)
                {
                    Ports.Add(new SerialPortConnector(name, 9800, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One));
                }
            }

            for (int i = Ports.Count - 1; i >= 0; i--)
            {
                bool portExist = false;

                if (PortNames != null)
                {
                    foreach (string name in PortNames)
                    {
                        if (Ports[i].PortName == name)
                            portExist = true;
                    }
                }

                if(!portExist)
                {
                    Ports.Remove(Ports[i]);
                }
            }
        }

        /// <summary>
        /// COMポートを開きます。
        /// </summary>
        public void Connect(string portName)
        {
            foreach(SerialPortConnector port in Ports)
            {
                if (port.PortName == portName)
                    CurrentPort = port;
            }

            if(!CurrentPort.IsOpen)
            {
                CurrentPort.Start();
            }

            CurrentPort.DataReceived += CurrentPort_DataReceived;
        }

        private void CurrentPort_DataReceived(byte[] data)
        {
            RecievedData = null;

            foreach(char d in data)
            {
                RecievedData += d;
            }

            RaisePropertyChanged("RecievedData");
        }

        /// <summary>
        /// 指定されたCOMポートを閉じます。
        /// </summary>
        /// <param name="portName"></param>
        public void Disconnect()
        {
            if (CurrentPort != null && CurrentPort.IsOpen)
            {
                CurrentPort.Close();
                CurrentPort.DataReceived -= CurrentPort_DataReceived;
                CurrentPort = null;
            }
        }
    }
}
