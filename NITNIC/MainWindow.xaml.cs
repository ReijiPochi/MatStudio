using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MatFramework.Graphics._3D;
using MatFramework.Graphics;
using MatFramework.Graphics._3D.Objects;
using NITNIC.Worlds;
using SlimDX;
using MatFramework;
using System.Runtime.InteropServices;

namespace NITNIC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
            Application.Current.Exit += Current_Exit;
        }

        Mat3DView view;
        TestWorld world1;
        MatTimer inputClock;

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetCursorPos(int X, int Y);

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            view = new Mat3DView();
            view.Host.Activated += Host_Activated;
            view.Host.Deactivated += Host_Deactivated;
            view.Host.KeyDown += Host_KeyDown;
            view.Host.KeyUp += Host_KeyUp;
            view.Host.MouseMove += Host_MouseMove;
            view.Host.MouseDown += Host_MouseDown;
            view.Host.MouseUp += Host_MouseUp;

            world1 = new TestWorld();
            view.CurrentWorld = world1;

            inputClock = new MatTimer(1000.0 / 60.0);
            inputClock.MatTickEvent += InputClock_MatTickEvent;
            inputClock.Start();

            mouseLock = true;
            view.Host.Cursor = Cursors.None;
            SetCursorPos(500, 500);
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            if(world1 != null) world1.Dispose();
            if(view != null) view.Dispose();
            if (inputClock != null) inputClock.Dispose();
        }

        private void Host_Activated(object sender, EventArgs e)
        {
            mouseLock = true;
            view.Host.Cursor = Cursors.None;
            SetCursorPos(500, 500);
        }

        private void Host_Deactivated(object sender, EventArgs e)
        {
            mouseLock = false;
            view.Host.Cursor = Cursors.Arrow;
        }

        bool keyA, keyS, keyD, keyW, keyShift, keySpace, keyCtrl, mouseRight;
        double speedEW, speedSN, speedSG;
        double angleEW = Math.PI, angleSN = 0.0;

        private void Host_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Application.Current.Shutdown(0);
                    break;

                case Key.A: keyA = true; break;
                case Key.S: keyS = true; break;
                case Key.D: keyD = true; break;
                case Key.W: keyW = true; break;
                case Key.Space: keySpace = true; break;
                case Key.LeftCtrl: keyCtrl = true; break;
                case Key.LeftShift: keyShift = true; break;
            }
        }

        private void Host_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A: keyA = false; break;
                case Key.S: keyS = false; break;
                case Key.D: keyD = false; break;
                case Key.W: keyW = false; break;
                case Key.Space: keySpace = false; break;
                case Key.LeftCtrl: keyCtrl = false; break;
                case Key.LeftShift: keyShift = false; break;
            }
        }

        bool cancel, mouseLock;

        private void Host_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseLock)
                return;

            if (cancel)
            {
                cancel = false;
                return;
            }

            Point pos = e.GetPosition(view.Host);

            angleEW += (pos.X - 500) * 0.003;
            angleSN += (pos.Y - 500) * 0.003;

            if (angleSN > Math.PI * 0.45)
                angleSN = Math.PI * 0.45;
            else if(angleSN < -Math.PI * 0.45)
                angleSN = -Math.PI * 0.45;

            SetCursorPos(500, 500);
            cancel = true;
        }

        private void Host_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed) mouseRight = true;
        }

        private void Host_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Released) mouseRight = false;
        }

        private void InputClock_MatTickEvent(object sender)
        {
            DataInput();
        }

        private void DataInput()
        {
            GameControlInputData data = new GameControlInputData();

            if (keyD)
            {
                if (keyA)
                    speedEW = 0.00;
                else
                    speedEW = -0.03;
            }
            else if (keyA) speedEW = 0.03;
            else speedEW = 0.0;

            if (keyW)
            {
                if (keyS)
                    speedSN = 0.00;
                else
                    speedSN = 0.03;
            }
            else if (keyS) speedSN = -0.03;
            else speedSN = 0.0;

            if (keySpace)
            {
                if (keyCtrl)
                    speedSG = 0.00;
                else
                    speedSG = 1.0;
            }
            else if (keyCtrl) speedSG = -0.5;
            else speedSG = 0.0;

            if (keyShift)
            {
                speedEW *= 2.0;
                speedSN *= 2.0;
            }

            data.speedEW = speedEW;
            data.speedSN = speedSN;
            data.speedSG = speedSG;
            data.angleEW = angleEW;
            data.angleSN = angleSN;
            data.aimMode = mouseRight;

            world1.OnControlDataInput(data);
        }
    }
}
