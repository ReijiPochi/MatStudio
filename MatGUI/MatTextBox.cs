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
    public class MatTextBox : TextBox
    {
        static MatTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatTextBox), new FrameworkPropertyMetadata(typeof(MatTextBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            TextChanged += MatTextBox_TextChanged;
        }

        public bool ShowNewAllways
        {
            get { return (bool)GetValue(ShowNewAllwaysProperty); }
            set { SetValue(ShowNewAllwaysProperty, value); }
        }
        public static readonly DependencyProperty ShowNewAllwaysProperty =
            DependencyProperty.Register("ShowNewAllways", typeof(bool), typeof(MatTextBox), new PropertyMetadata(false));


        private void MatTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ShowNewAllways && !IsFocused)
            {
                ScrollToEnd();
            }
        }

    }
}
