using RobotCoreBase;
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

namespace MatStudioROBOT2016.Models.DataFlow.Logger
{
    /// <summary>
    /// Interaction logic for RemoconLoggerControl.xaml
    /// </summary>
    public partial class RemoconLoggerControl : UserControl
    {
        public RemoconLoggerControl()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            GraphCanvas.MouseDown += GraphCanvas_MouseDown;
            GraphScrollViewer.ScrollChanged += GraphScrollViewer_ScrollChanged;
            ListScrollViewer.ScrollChanged += ListScrollViewer_ScrollChanged;

            foreach(DUALSHOCK3Buttons btn in Enum.GetValues(typeof(DUALSHOCK3Buttons)))
            {
                TextBlock tb = new TextBlock()
                {
                    Text = Enum.GetName(typeof(DUALSHOCK3Buttons), btn),
                    Height = 25,
                    HorizontalAlignment = HorizontalAlignment.Right
                };

                ListStackPanel.Children.Add(tb);
            }

            owner = DataContext as RemoconLogger;
        }

        public RemoconLogger owner;
        private int longestTime = 0;

        private void GraphCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(GraphCanvas);

            owner.SetCurrentTime((int)p.X);
        }

        private void GraphScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ListScrollViewer.ScrollToVerticalOffset(e.VerticalOffset);

            AnalogLGraph.OffsetX = e.HorizontalOffset;
            AnalogLGraph.RefreshGraph();
            AnalogRGraph.OffsetX = e.HorizontalOffset;
            AnalogRGraph.RefreshGraph();
        }

        private void ListScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            GraphScrollViewer.ScrollToVerticalOffset(e.VerticalOffset);
        }

        public void SetButton(DUALSHOCK3Button button)
        {
            RemoconButtonItem rbi = new RemoconButtonItem(button);
            GraphCanvas.Children.Add(rbi);

            SetEndTime(button.EndTime);
        }

        public void SetEndTime(int endTime)
        {
            if (longestTime < endTime)
            {
                longestTime = endTime;

                GraphCanvas.Width = longestTime + 100;

                GraphScrollViewer.ScrollToRightEnd();
            }
        }

        public void SetTimeBar(int time)
        {
            TimeBar.X1 = time;
            TimeBar.X2 = time;

            SetEndTime(time);

            GraphScrollViewer.ScrollToHorizontalOffset(time - 150);
        }

        public void SetAnalogStick(DUALSHOCK3 data)
        {
            AnalogLGraph.Data1.Add(new MatFramework.Coord2D(data.Time, data.AnalogL_X));
            AnalogLGraph.Data2.Add(new MatFramework.Coord2D(data.Time, data.AnalogL_Y));
            AnalogRGraph.Data1.Add(new MatFramework.Coord2D(data.Time, data.AnalogR_X));
            AnalogRGraph.Data2.Add(new MatFramework.Coord2D(data.Time, data.AnalogR_Y));
            AnalogLGraph.RefreshGraph();
            AnalogRGraph.RefreshGraph();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PlayButton.IsEnabled = false;
            PauseButton.IsEnabled = true;
            StopButton.IsEnabled = true;
            RecButton.IsEnabled = false;

            owner.StartPlay();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            PlayButton.IsEnabled = true;
            PauseButton.IsEnabled = false;
            StopButton.IsEnabled = true;
            RecButton.IsEnabled = true;

            owner.Pause();
        }

        private void RecButton_Click(object sender, RoutedEventArgs e)
        {
            PlayButton.IsEnabled = false;
            PauseButton.IsEnabled = true;
            StopButton.IsEnabled = true;
            RecButton.IsEnabled = false;

            owner.StartRec();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            PlayButton.IsEnabled = true;
            PauseButton.IsEnabled = false;
            StopButton.IsEnabled = false;
            RecButton.IsEnabled = true;

            owner.Stop();
        }
    }
}
