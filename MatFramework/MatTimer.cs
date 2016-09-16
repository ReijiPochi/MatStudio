using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MatFramework
{
    public delegate void MatTickEventHandler(object sender);

    public class MatTimer : IDisposable
    {
        public MatTimer(int interval)
        {
            Interval = interval;
        }

        private Thread thread;
        
        private int startTime = 0;

        public bool IsRunning { get; private set; }

        public int Interval { get; set; }

        public event MatTickEventHandler MatTickEvent;
        protected void RaiseMatTick()
        {
            MatTickEvent?.Invoke(this);
        }

        public void Start()
        {
            if (!IsRunning)
            {
                thread = new Thread(Work);
                DateTime now = DateTime.Now;
                startTime = now.Minute * 60000 + now.Second * 1000 + now.Millisecond;
                IsRunning = true;
                thread.Start();
            }
        }

        public void Stop()
        {
            IsRunning = false;
            if (thread != null) thread.Join();
        }

        private void Work()
        {
            while (IsRunning)
            {
                DateTime now = DateTime.Now;
                int current = now.Minute * 60000 + now.Second * 1000 + now.Millisecond;

                if((current - startTime) >= Interval)
                {
                    startTime = current;
                    RaiseMatTick();
                }
            }
        }

        public void Dispose()
        {
            IsRunning = false;
            if (thread != null) thread.Join();
        }
    }
}
