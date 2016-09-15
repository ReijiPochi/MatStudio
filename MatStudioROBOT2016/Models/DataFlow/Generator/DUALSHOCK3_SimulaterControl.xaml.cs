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
using RobotCoreBase;

namespace MatStudioROBOT2016.Models.DataFlow.Generator
{
    /// <summary>
    /// Interaction logic for DUALSHOCK3_SimulaterControl.xaml
    /// </summary>
    public partial class DUALSHOCK3_SimulaterControl : UserControl
    {
        public DUALSHOCK3_SimulaterControl()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            owner = DataContext as DUALSHOCK3_Simulater;
        }

        DUALSHOCK3_Simulater owner;

        private void Maru_Click(object sender, RoutedEventArgs e)
        {
            if (owner != null)
            {
                owner.OutputACommand(new DUALSHOCK3() { Maru = true });
            }
        }
    }
}
