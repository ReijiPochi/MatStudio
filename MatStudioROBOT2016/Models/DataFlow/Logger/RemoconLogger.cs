using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MatFramework.DataFlow;
using RobotCoreBase;
using System.Timers;
using MatFramework;
using System.Windows.Controls;

namespace MatStudioROBOT2016.Models.DataFlow.Logger
{
    public class RemoconLogger : MatDataObject
    {
        public RemoconLogger(string name) : base(name)
        {
            timer = new MatTimer(10);
            timer.MatTickEvent += Timer_MatTickEvent;

            view = new RemoconLoggerControl() { DataContext = this };

            CommandIn.MatDataInput += CommandIn_MatDataInput;

            inputs.Add(CommandIn);
            outputs.Add(CommandOut);

            App.Current.Exit += Current_Exit;
        }

        private void Current_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            timer.Dispose();
        }

        private MatTimer timer;
        private RemoconLoggerControl view;

        List<DUALSHOCK3> Log = new List<DUALSHOCK3>();
        List<DUALSHOCK3Button> pressingButtons = new List<DUALSHOCK3Button>();

        public bool IsLogging { get; private set; }

        public int CurrentTime { get; private set; }


        MatDataInputPort CommandIn = new MatDataInputPort(typeof(DUALSHOCK3), "Command") { };
        private void CommandIn_MatDataInput(object sender, MatDataInputEventArgs e)
        {
            if (!IsLogging)
                return;

            Dispatcher.BeginInvoke((Action)(() =>
            {
                DUALSHOCK3 data = (DUALSHOCK3)e.NewValue.DataValue;
                Log.Add(data);

                List<DUALSHOCK3Buttons> list = data.GetPressedButtons();

                if (Log.Count == 1)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        DUALSHOCK3Button btn = new DUALSHOCK3Button(list[i], data.Time);
                        pressingButtons.Add(btn);
                    }
                }
                else
                {
                    List<DUALSHOCK3Button> removeList = new List<DUALSHOCK3Button>();

                    for (int i = 0; i < list.Count; i++)
                    {
                        bool exist = false;

                        for (int c = 0; c < pressingButtons.Count; c++)
                        {
                            if (list[i] == pressingButtons[c].Button)
                            {
                                exist = true;
                                pressingButtons[c].EndTime = data.Time;
                                view.SetEndTime(data.Time);
                                CurrentTime = data.Time;
                            }
                        }

                        if (!exist)
                        {
                            DUALSHOCK3Button btn = new DUALSHOCK3Button(list[i], data.Time);
                            pressingButtons.Add(btn);
                            view.SetButton(btn);
                            CurrentTime = data.Time;
                        }

                        view.SetTimeBar(CurrentTime);
                    }

                    for (int i = 0; i < pressingButtons.Count; i++)
                    {
                        bool exist = false;

                        for (int c = 0; c < list.Count; c++)
                        {
                            if (pressingButtons[i].Button == list[c])
                                exist = true;
                        }

                        if (!exist)
                        {
                            removeList.Add(pressingButtons[i]);
                        }
                    }

                    for (int i = 0; i < removeList.Count; i++)
                    {
                        pressingButtons.Remove(removeList[i]);
                    }
                }
            }));
        }

        MatDataOutputPort CommandOut = new MatDataOutputPort(typeof(DUALSHOCK3), "Log") { };

        public void StartRec()
        {
            timer.Start();
            IsLogging = true;

            if (RobotCoreM.Current.CurrentRobotCore != null)
                RobotCoreM.Current.CurrentRobotCore.SetSystemTime(CurrentTime);
        }

        public void StartPlay()
        {
            timer.Start();
            IsLogging = false;
        }

        public void Pause()
        {
            IsLogging = false;
            timer.Pause();

            if (RobotCoreM.Current.CurrentRobotCore != null)
                RobotCoreM.Current.CurrentRobotCore.SetSystemTime(-1);
        }

        public void Stop()
        {
            IsLogging = false;
            timer.Stop();
            CurrentTime = 0;
            view.SetTimeBar(CurrentTime);
            pressingButtons.Clear();

            if (RobotCoreM.Current.CurrentRobotCore != null)
                RobotCoreM.Current.CurrentRobotCore.SetSystemTime(-1);
        }

        public void SetCurrentTime(int time)
        {
            CurrentTime = time;
            view.SetTimeBar(CurrentTime);
        }

        private void Timer_MatTickEvent(object sender)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                view.SetTimeBar(CurrentTime);
                CurrentTime++;
            }));
        }

        public override Control GetInterfaceControl()
        {
            return view;
        }

        public override MatDataObject GetNewInstance()
        {
            return new RemoconLogger(Name);
        }
    }
}
