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
    public class MatFocusableBG : Control
    {
        static MatFocusableBG()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatFocusableBG), new FrameworkPropertyMetadata(typeof(MatFocusableBG)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            MouseDown += MatFocusbleBG_MouseDown;
        }

        private void MatFocusbleBG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Focus();
        }
    }
}
