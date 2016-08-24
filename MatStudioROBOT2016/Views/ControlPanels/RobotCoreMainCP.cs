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

using MatGUI;
using RobotCoreBase;

namespace MatStudioROBOT2016.Views.ControlPanels
{
    public class RobotCoreMainCP : MatControlPanelBase
    {
        static RobotCoreMainCP()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RobotCoreMainCP), new FrameworkPropertyMetadata(typeof(RobotCoreMainCP)));
        }

        //public IRobotCore CurrentRobotCore
        //{
        //    get { return (IRobotCore)GetValue(MyPropertyProperty); }
        //    set { SetValue(MyPropertyProperty, value); }
        //}
        //public static readonly DependencyProperty MyPropertyProperty =
        //    DependencyProperty.Register("MyProperty", typeof(IRobotCore), typeof(RobotCoreMainCP), new PropertyMetadata(null,new PropertyChangedCallback(OnCurrentRobotCoreChanged)));

        //private static void OnCurrentRobotCoreChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    RobotCoreMainCP cp = d as RobotCoreMainCP;
        //    IRobotCore board = e.NewValue as IRobotCore;

        //    if (cp != null && board != null)
        //    {
        //        cp.Content = board.GetMainControlPanel();
        //    }
        //}
    }
}
