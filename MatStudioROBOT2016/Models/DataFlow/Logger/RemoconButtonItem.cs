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
    public class RemoconButtonItem : Control
    {
        static RemoconButtonItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RemoconButtonItem), new FrameworkPropertyMetadata(typeof(RemoconButtonItem)));
        }

        public RemoconButtonItem(DUALSHOCK3Button btn)
        {
            button = btn;
            button.EndTimeChanged += Button_EndTimeChanged;
            SetValue(Canvas.TopProperty, (double)btn.Button * 25.0);
            SetValue(Canvas.LeftProperty, (double)btn.StartTime);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Bd = GetTemplateChild("Bd") as Border;
            Cp = GetTemplateChild("Cp") as ContentPresenter;

            BitmapImage bi = Application.Current.FindResource("Shikaku") as BitmapImage;
            icon = new Image() { Source = bi, Width = 23, Height = 23};
            Cp.Content = icon;
        }

        private DUALSHOCK3Button button;
        private Image icon;

        private Border Bd;
        private ContentPresenter Cp;


        private void Button_EndTimeChanged(object sender)
        {
            double span = button.EndTime - button.StartTime;

            if (span > 23)
            {
                Width = span;
            }
        }

    }
}
