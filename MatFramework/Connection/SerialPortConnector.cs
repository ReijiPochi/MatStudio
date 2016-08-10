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

        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }

        public delegate void DataReceivedHandler(byte[] data);
        public event DataReceivedHandler DataReceived;

        public SerialPortConnector()
        {

        }

        public void Start()
        {
            myPort = new SerialPort(PortName, BaudRate, Parity, DataBits, StopBits);
            myPort.Open();
            receiveThread = new Thread(ReceiveWork);
            receiveThread.Start(this);
        }

        public static void ReceiveWork(object target)
        {
            SerialPortConnector my = target as SerialPortConnector;
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
                catch (IOException ex)
                {
                }
                catch (InvalidOperationException ex)
                {
                }
            } while (myPort.IsOpen);
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
