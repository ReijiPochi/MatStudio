using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

using MatFramework.Connection;
using System.Windows;
using MatFramework;

namespace MatStudioROBOT2016.Models
{
    public delegate void RecievedALineEventHandler(object sender, RecievedALineEventArgs e);

    public class RecievedALineEventArgs
    {
        public string NewLine { get; set; }
    }

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

        public Queue<string> RecievedDataLines { get; private set; } = new Queue<string>();

        public string RecievedData { get; private set; }

        public MatObservableSynchronizedCollection<string> RecievedLines { get; set; } = new MatObservableSynchronizedCollection<string>();

        public event RecievedALineEventHandler RecievedALine;
        private void RaiseRecievedALineEvent(RecievedALineEventArgs e)
        {
            RecievedALine?.Invoke(this, e);
        }

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
                    Ports.Add(new SerialPortConnector(name, 1250000, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One));
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

        private string line = null;

        private void CurrentPort_DataReceived(MatObject sender, byte[] data)
        {
            foreach(char d in data)
            {
                line += d;
                RecievedData += d;

                if (d == '\n')
                {

                    //sender.Dispatcher.BeginInvoke((Action)(() =>
                    //{
                    //    RecievedDataLines.Enqueue(line);
                    //    RaiseRecievedALineEvent(new RecievedALineEventArgs() { NewLine = line });
                    //}));

                    RecievedLines.Add(line);

                    line = null;
                }
            }

            RaisePropertyChanged("RecievedData");
        }

        /// <summary>
        /// 指定された数の新規受信データを取得します
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public string GetRecieveData(int count)
        {
            if (RecievedData == null)
                return null;

            int length = RecievedData.Length;
            string data = null;

            if (count > length)
                count = length;

            for(int c = count; c > 0; c--)
            {
                data += RecievedData[length - c];
            }

            return data;
        }

        /// <summary>
        /// データを送信します
        /// </summary>
        /// <param name="data"></param>
        public void Send(string data)
        {
            CurrentPort.WriteData(data);
        }

        /// <summary>
        /// データを送信します
        /// </summary>
        /// <param name="data"></param>
        public void Send(byte[] data)
        {
            CurrentPort.WriteData(data);
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
