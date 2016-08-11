using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace MatFramework.Connection
{
    /// <summary>
    /// シリアルポートでの通信を別スレッドで行います。
    /// </summary>
    class SerialPortConnector : MatObject
    {
        private SerialPort myPort = null;
        private Thread receiveThread = null;

        public string PortName { get; private set; }
        public int BaudRate { get; private set; }
        public Parity Parity { get; private set; }
        public int DataBits { get; private set; }
        public StopBits StopBits { get; private set; }

        public delegate void DataReceivedHandler(byte[] data);
        public event DataReceivedHandler DataReceived;

        public SerialPortConnector(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            PortName = portName;
            BaudRate = baudRate;
            Parity = parity;
            DataBits = dataBits;
            StopBits = stopBits;
        }

        public void Start()
        {
            myPort = new SerialPort(PortName, BaudRate, Parity, DataBits, StopBits);

            try
            {
                myPort.Open();
            }
            catch(Exception ex)
            {
                MatApp.ApplicationLog.LogException("シリアルポート" + PortName + " を開けませんでした", ex);
            }
            
            receiveThread = new Thread(ReceiveWork);
            receiveThread.Start(this);
        }

        public static void ReceiveWork(object target)
        {
            SerialPortConnector my = target as SerialPortConnector;
            MatApp.ApplicationLog.Log(new LogData(LogCondition.Action, "シリアルポート" + my.PortName + " を開きました", "null"));
            my.ReceiveData();
        }

        public void WriteData(byte[] buffer)
        {
            myPort.Write(buffer, 0, buffer.Length);
        }

        public void WriteData(string data)
        {
            myPort.Write(data);
        }

        public void ReceiveData()
        {
            if (myPort == null)
            {
                return;
            }

            do
            {
                try
                {
                    int rbyte = myPort.BytesToRead;
                    byte[] buffer = new byte[rbyte];
                    int read = 0;

                    while (read < rbyte)
                    {
                        int length = myPort.Read(buffer, read, rbyte - read);
                        read += length;
                    }

                    if (rbyte > 0)
                    {
                        DataReceived(buffer);
                    }
                }
                catch (Exception ex)
                {
                    MatApp.ApplicationLog.LogException("シリアルポート" + PortName + " でのデータ受信に失敗しました", ex);
                }
            } while (myPort.IsOpen);

            MatApp.ApplicationLog.Log(new LogData(LogCondition.Action, "シリアルポート" + PortName + " がクローズしました", "null"));
        }

        public void Close()
        {
            if (receiveThread != null && myPort != null)
            {
                myPort.Close();
                receiveThread.Join();
            }
        }
    }
}
