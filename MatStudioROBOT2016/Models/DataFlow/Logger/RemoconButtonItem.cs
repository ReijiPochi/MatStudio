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

        public RemoconButtonItem(DUALSHOCK3Buttons btn)
        {
            button = btn;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Bd = GetTemplateChild("Bd") as Border;
        }

        private DUALSHOCK3Buttons button;
        private Border Bd;
    }
}
