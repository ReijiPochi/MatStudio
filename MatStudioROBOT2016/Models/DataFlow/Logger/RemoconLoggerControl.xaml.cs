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
        }

        private int longestTime = 0;

        private void GraphScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ListScrollViewer.ScrollToVerticalOffset(e.VerticalOffset);
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
                longestTime = endTime;

            GraphCanvas.Width = longestTime + 100;

            GraphScrollViewer.ScrollToRightEnd();
        }
    }
}
