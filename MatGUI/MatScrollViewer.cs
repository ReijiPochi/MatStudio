using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatGUI
{
    public class MatScrollViewer : ScrollViewer
    {
        static MatScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatScrollViewer), new FrameworkPropertyMetadata(typeof(MatScrollViewer)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ScrollBar h = GetTemplateChild("PART_HorizontalScrollBar") as ScrollBar;
            h.PreviewMouseWheel += H_PreviewMouseWheel;

            ScrollBar v = GetTemplateChild("PART_VerticalScrollBar") as ScrollBar;
            v.PreviewMouseWheel += V_PreviewMouseWheel;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            //base.OnMouseWheel(e);

            double d;

            if (e.Delta > 0)
                d = -25;
            else
                d = 25;

            if(ScrollHorizontal)
                ScrollToHorizontalOffset(HorizontalOffset + d);
            else
                ScrollToVerticalOffset(VerticalOffset + d);
        }



        public bool ScrollHorizontal
        {
            get { return (bool)GetValue(ScrollHorizontalProperty); }
            set { SetValue(ScrollHorizontalProperty, value); }
        }
        public static readonly DependencyProperty ScrollHorizontalProperty =
            DependencyProperty.Register("ScrollHorizontal", typeof(bool), typeof(MatScrollViewer), new PropertyMetadata(false));



        private void V_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                ScrollToVerticalOffset(VerticalOffset - 25);
            else
                ScrollToVerticalOffset(VerticalOffset + 25);

            e.Handled = true;
        }

        private void H_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                ScrollToHorizontalOffset(HorizontalOffset - 25);
            else
                ScrollToHorizontalOffset(HorizontalOffset + 25);

            e.Handled = true;
        }
    }
}
