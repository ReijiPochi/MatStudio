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
    public class MatDoubleBox : TextBox
    {
        static MatDoubleBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatDoubleBox), new FrameworkPropertyMetadata(typeof(MatDoubleBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            LostFocus += MatDoubleBox_LostFocus;
            MouseWheel += MatDoubleBox_MouseWheel;
            KeyDown += MatDoubleBox_KeyDown;
        }

        private void MatDoubleBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void MatDoubleBox_LostFocus(object sender, RoutedEventArgs e)
        {
            double value;
            if (double.TryParse(Text, out value))
            {
                if (value > Maximum) value = Maximum;
                else if (value < Minimum) value = Minimum;

                string temp = value.ToString(Format);
                DoubleValue = double.Parse(temp);
            }

            Text = DoubleValue.ToString(Format);
        }

        private void MatDoubleBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!IsFocused) return;

            double temp = DoubleValue;

            if (e.Delta > 0)
            {
                if (DoubleValue + Delta <= Maximum) temp += Delta;
            }
            else
            {
                if (DoubleValue - Delta >= Minimum) temp -= Delta;
            }

            string tempString = temp.ToString(Format);
            DoubleValue = double.Parse(tempString);
        }


        public double DoubleValue
        {
            get { return (double)GetValue(DoubleValueProperty); }
            set { SetValue(DoubleValueProperty, value); }
        }
        public static readonly DependencyProperty DoubleValueProperty =
            DependencyProperty.Register("DoubleValue", typeof(double), typeof(MatDoubleBox), new PropertyMetadata(0.0, new PropertyChangedCallback(OnDoubleValueChanged)));

        private static void OnDoubleValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MatDoubleBox trg = (MatDoubleBox)d;

            if ((double)e.NewValue > trg.Maximum) trg.DoubleValue = trg.Maximum;
            else if ((double)e.NewValue < trg.Minimum) trg.DoubleValue = trg.Minimum;
            trg.Text = trg.DoubleValue.ToString(trg.Format);
        }


        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(MatDoubleBox), new PropertyMetadata(0.0));


        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(MatDoubleBox), new PropertyMetadata(1.0));


        public double Delta
        {
            get { return (double)GetValue(DeltaProperty); }
            set { SetValue(DeltaProperty, value); }
        }
        public static readonly DependencyProperty DeltaProperty =
            DependencyProperty.Register("Delta", typeof(double), typeof(MatDoubleBox), new PropertyMetadata(0.01));


        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof(string), typeof(MatDoubleBox), new PropertyMetadata("f2"));

    }
}
