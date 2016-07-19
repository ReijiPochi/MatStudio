using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class MatButton : Button
    {
        static MatButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatButton), new FrameworkPropertyMetadata(typeof(MatButton)));
        }

        [Category("MatGUI")]
        public bool MonoColorIcon
        {
            get { return (bool)GetValue(MonoColorIconProperty); }
            set { SetValue(MonoColorIconProperty, value); }
        }
        public static readonly DependencyProperty MonoColorIconProperty =
            DependencyProperty.Register("MonoColorIcon", typeof(bool), typeof(MatButton), new PropertyMetadata(true));
    }
}
