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
        public MatTimer(double interval)
        {
            Interval = interval;
        }

        private Thread thread;
        
        private int startTime = 0;

        public bool IsRunning { get; private set; }

        public bool IsCounting { get; private set; }

        public double Interval { get; set; }

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
                IsCounting = true;
                thread.Start();
            }
            else
            {
                IsCounting = true;
            }
        }

        public void Pause()
        {
            IsCounting = false;
        }

        public void Stop()
        {
            IsRunning = false;
            IsCounting = false;

            if (thread != null) thread.Join();
        }

        private void Work()
        {
            double nextTick = 0;

            while (IsRunning)
            {
                if (!IsCounting)
                    continue;

                DateTime now = DateTime.Now;
                int current = now.Minute * 60000 + now.Second * 1000 + now.Millisecond;

                if (nextTick == 0)
                    nextTick = current + Interval;

                if(current >= nextTick)
                {
                    nextTick += Interval;
                    RaiseMatTick();
                }

                Thread.Sleep(1);
            }
        }

        public void Dispose()
        {
            IsRunning = false;
            if (thread != null) thread.Join();
        }
    }
}
