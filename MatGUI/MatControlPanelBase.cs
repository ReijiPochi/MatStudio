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

namespace MatGUI
{
    public class MatControlPanelBase : Control
    {
        static MatControlPanelBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatControlPanelBase), new FrameworkPropertyMetadata(typeof(MatControlPanelBase)));
        }

        public bool IsActivePanel
        {
            get { return (bool)GetValue(IsActivePanelProperty); }
            set { SetValue(IsActivePanelProperty, value); }
        }
        public static readonly DependencyProperty IsActivePanelProperty =
            DependencyProperty.Register("IsActivePanel", typeof(bool), typeof(MatControlPanelBase), new PropertyMetadata(false));
    }
}
